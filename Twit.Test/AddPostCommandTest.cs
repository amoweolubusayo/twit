using System.Threading;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Twit.Application.Commands;
using Twit.Core.Utils;

namespace TwitTest
{
    [TestClass]
    public class AddPostCommandTest
    {
        private Mock<IMediator> mockMediator;

        public AddPostCommandTest() => mockMediator = new Mock<IMediator>();

        [TestMethod]
        public void HandlerAddPostCommand()
        {
            mockMediator.Setup(m => m.Send(It.IsAny<AddPostCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new GenericResponse()) 
        .Verifiable("Response was not sent.");

            mockMediator.Verify(x => x.Send(It.IsAny<AddPostCommand>(), It.IsAny<CancellationToken>()), Times.Never());
        }
    }
}