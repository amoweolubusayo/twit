using System.Threading;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using tweetee.Application.Commands;
using tweetee.Infrastructure.Utility.Security;

namespace TweeteeTest
{
   [TestClass]
    public class UpdatePostCommandTest
    {
        public void HandlerUpdatePostCommand(Mock<IMediator> mockMediator)
        {
            // Note that default(CancellationToken) is the default value of the optional argument.
            mockMediator.Setup(m => m.Send(It.IsAny<UpdatePostCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(new GenericResponse()) //<-- return Task to allow await to continue
        .Verifiable("Response was not sent.");

            mockMediator.Verify(x => x.Send(It.IsAny<UpdatePostCommand>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}