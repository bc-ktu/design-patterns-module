using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Server.Hubs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class IntegrationTest
    {
        [Test]
        public async Task IntegrationTest_CheckIfConnected()
        {
            bool sendCalled = false;
            var hub = new GameHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = (Microsoft.AspNetCore.SignalR.IHubCallerClients)mockClients.Object;
            dynamic all = new ExpandoObject();
            all.broadcastMessage = new Action<string, string>((name, message) => {
                sendCalled = true;
            });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            hub.SendMessage("TestUser", "TestMessage");
            Assert.True(sendCalled);
        }
    }
}
