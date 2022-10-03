using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Bomberman_client_graphics_v1.SignalR
{//singleton
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
