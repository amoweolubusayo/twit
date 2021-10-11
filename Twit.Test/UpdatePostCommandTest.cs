using System.Threading;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using twit.Application.Commands;
using twit.Infrastructure.Utility.Security;

namespace twitTest
{
   [TestClass]
    public class UpdatePostCommandTest
    {
        private Mock<IMediator> mockMediator;
         public UpdatePostCommandTest() => mockMediator = new Mock<IMediator>();

        [TestMethod]
         public void HandlerUpdatePostCommand()
        {
            
            mockMediator.Setup(m => m.Send(It.IsAny<UpdatePostCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new GenericResponse()) 
        .Verifiable("Response was not sent.");

            mockMediator.Verify(x => x.Send(It.IsAny<UpdatePostCommand>(), It.IsAny<CancellationToken>()), Times.Never());
        }
    }
}