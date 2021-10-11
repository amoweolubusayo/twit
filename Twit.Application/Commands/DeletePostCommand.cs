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
    public class DeletePostCommand  : IRequest<GenericResponse>
    {
         public int UserId { get; set; }
         public int PostId { get; set; }
    }
       public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();
            RuleFor(x => x.PostId).NotNull().NotEmpty();
        }

    }
      public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, GenericResponse>
    {
        private readonly TwitContext _context;
        private readonly ILogger<DeletePostCommandHandler> _logger;
        public DeletePostCommandHandler(
            TwitContext context,
            ILogger<DeletePostCommandHandler> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var deleted = true;
            var userExists = await _context.Users.AnyAsync(x => x.Id == request.UserId);
            if (!userExists)
            {
                _logger.LogError("User does not exist.");
                return new GenericResponse(false,"User doesn't exist.");
            }
            var postDeleted = await _context.Posts.AnyAsync(x => x.Id == request.PostId && x.IsDeleted == true);
            if (postDeleted)
            {
                _logger.LogError("Post does not exist or has been deleted.");
                return new GenericResponse(false,"Post doesn't exist or has been deleted.");
            }
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId);
            post.IsDeleted = deleted;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return new GenericResponse(true, "Post Deleted successfully.");
        }

    }
}