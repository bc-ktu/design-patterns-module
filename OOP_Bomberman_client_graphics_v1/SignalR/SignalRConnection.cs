using Microsoft.AspNetCore.SignalR.Client;

namespace client_graphics.SignalR
{
    public class SignalRConnection
    {
        public HubConnection Connection { get; set; }
        public string ID { get { return Connection.ConnectionId; } }

        public SignalRConnection(string url)
        {
            Connection = new HubConnectionBuilder()                
               .WithUrl(new Uri(url))
               .WithAutomaticReconnect()
               .Build();
        }

        public bool ConnectToServer()
        {
            Connection.StartAsync();

            Connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await Connection.StartAsync();
            };

            while (1 == 1)
            {
                if (Connection.State == HubConnectionState.Connected)
                {
                    return true;
                }
            }
            
        }
        
    }
}
