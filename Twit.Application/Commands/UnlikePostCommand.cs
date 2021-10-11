using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Twit.Core.Utils;
using Twit.Infrastructure.Data;


namespace Twit.Application.Commands
{
    public class UnlikePostCommand : IRequest<GenericResponse>
    {
         public int UserId { get; set; }
         public int PostId { get; set; }

    }
      public class UnlikePostCommandValidator : AbstractValidator<UnlikePostCommand>
    {
        public UnlikePostCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.PostId).NotNull().NotEmpty();
        
        }
    }
    public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand, GenericResponse>
    {
        private readonly TwitContext _context;
        private readonly ILogger<UnlikePostCommandHandler> _logger;
        public UnlikePostCommandHandler(
            TwitContext context,
            ILogger<UnlikePostCommandHandler> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(UnlikePostCommand request, CancellationToken cancellationToken)
        {
            bool liked = false;
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
            return new GenericResponse(true, "Post has been unliked.");
        }


    }

}