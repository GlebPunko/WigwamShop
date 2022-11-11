using Hangfire;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.HubHangfireHelper;
using SignalRChat.Models;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;
        public readonly IDictionary<string, UserConnection> _connections;

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            _botUser = "MyChat Bot";
            _connections = connections;
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var room = _connections.FirstOrDefault(x => x.Key == Context.ConnectionId)
               .Value.Room;

            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                Clients.Group(userConnection.Room)
                    .SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has left");

                SendConnectedUsers(userConnection.Room);
            }

            var countUsersInRoom = _connections.Where(x => x.Value.Room == room)
                .Select(x => x.Value.Room).ToList();

            if (countUsersInRoom.Count == 0)
            {
                RecurringJob.RemoveIfExists($"Advertising{room}");
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
            {
                await Clients.Group(userConnection.Room)
                    .SendAsync("ReceiveMessage", userConnection.User, message);
            }
        }

        public async Task SendDelayMessage(string message, int time)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection? userConnection))
            {
                BackgroundJob.Schedule<MyHubHelper>(x =>
                    x.SendOutDelayMessage(message, userConnection.Room, userConnection.User),
                    TimeSpan.FromSeconds(time));
            }
        }

        public async Task JoinRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);

            _connections[Context.ConnectionId] = userConnection;

            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser,
                $"{userConnection.User} has joined {userConnection.Room}");

            await SendConnectedUsers(userConnection.Room);

            BackgroundJob.Schedule<MyHubHelper>(x => x.SendOutAlertOneMunute(_botUser, userConnection.User),
                TimeSpan.FromSeconds(5));
            RecurringJob.AddOrUpdate<MyHubHelper>($"Advertising{userConnection.Room}", x => 
                x.SendOutAdvertising(_botUser, userConnection.Room), Cron.Minutely);
        }

        public Task SendConnectedUsers(string room)
        {
            var users = _connections.Values.Where(x => x.Room == room).Select(c => c.User);

            return Clients.Group(room).SendAsync("UsersInRoom", users);
        }
    }
}
