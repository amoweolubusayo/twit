using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using tweetee.Infrastructure.Persistence;
using tweetee.Infrastructure.Utility.Security;

namespace tweetee.Application.Commands
{
    public class LikePostCommand : IRequest<GenericResponse>
    {
         public int UserId { get; set; }
         public int PostId { get; set; }

    }
      public class LikePostCommandValidator : AbstractValidator<LikePostCommand>
    {
        public LikePostCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.PostId).NotNull().NotEmpty();
        
        }
    }
    public class LikePostCommandHandler : IRequestHandler<LikePostCommand, GenericResponse>
    {
        private readonly TweeteeContext _context;
        private readonly ILogger<LikePostCommandHandler> _logger;
        public LikePostCommandHandler(
            TweeteeContext context,
            ILogger<LikePostCommandHandler> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(LikePostCommand request, CancellationToken cancellationToken)
        {
            bool liked = true;
            var userExists = await _context.Users.AnyAsync(x => x.Id == request.UserId);
            if (!userExists)
            {
                _logger.LogError("User doesn't exist.");
                return new GenericResponse(false, "User doesn't exist.");
            }
            var postExists = await _context.Posts.AnyAsync(x => x.Id == request.PostId && x.IsDeleted == true);

            if (!postExists)
            {
                _logger.LogError("Post doesn't exist or has been deleted.");
                return new GenericResponse(false, "Post doesn't exist or has been deleted.");
            }

            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId);
            post.Liked = liked;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return new GenericResponse(true, "Post has been liked.");
        }


    }

}