using System.Threading;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using tweetee.Application.Commands;
using tweetee.Infrastructure.Utility.Security;

namespace TweeteeTest
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