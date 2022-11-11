using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;

namespace SignalRChat.HubHangfireHelper
{
    public class MyHubHelper : IMyHubHelper
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public MyHubHelper(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void SendOutAlertOneMunute(string bot, string name)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", bot, $"Hello from my little bot for {name}! -__-");
        }

        public void SendOutAdvertising(string bot, string room)
        {
            _hubContext.Clients.Group(room).SendAsync("ReceiveMessage", bot, "Only here and now! " +
                "Free registration on the site habr.com and kissing a bunch of cool articles! Only for you!");
        }

        public void SendOutDelayMessage(string message, string room, string client)
        {
            _hubContext.Clients.Group(room).SendAsync("ReceiveMessage", client, message);
        }
    }
}
