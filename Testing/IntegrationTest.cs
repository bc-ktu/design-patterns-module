using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Server.Hubs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IClientProxy = Microsoft.AspNetCore.SignalR.IClientProxy;

namespace Testing
{
    public class IntegrationTest
    {
        [Test]
        public async Task IntegrationTest_CheckIfConnected()
        {
            //Arrange
            Mock<IHubCallerClients> mockClients = new Mock<IHubCallerClients>();
            Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
            mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);
            GameHub simpleHub = new GameHub()
            {
                Clients = mockClients.Object
            };

            //Act
            await simpleHub.Welcome();

            //Assert
            mockClients.Verify(clients => clients.All, Times.Once);
            mockClientProxy.Verify(
                clientProxy => clientProxy.SendCoreAsync(
                    "welcome",
                    It.Is<object[]>(o => o != null && o.Length == 1 && ((object[])o[0]).Length == 3),
                    default(CancellationToken)),
                Times.Once);
        }
    }
}
