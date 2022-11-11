namespace SignalRChat.HubHangfireHelper
{
    public interface IMyHubHelper
    {
        void SendOutAlertOneMunute(string bot, string name);
        void SendOutAdvertising(string bot, string room);
        void SendOutDelayMessage(string message, string room, string client);
    }
}
