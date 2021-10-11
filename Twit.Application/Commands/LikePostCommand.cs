using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Twit.Core.Entities;
using Twit.Core.Utils;
using Twit.Infrastructure.Data;


namespace Twit.Application.Commands
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
        private readonly TwitContext _context;
        private readonly ILogger<LikePostCommandHandler> _logger;
        public LikePostCommandHandler(
            TwitContext context,
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

            if (postExists)
            {
                _logger.LogError("Post doesn't exist or has been deleted.");
                return new GenericResponse(false, "Post doesn't exist or has been deleted.");
            }

            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId);
            post.Liked = liked;
            var likes = await _context.PostLikes.AnyAsync(x => x.PostId == request.PostId);
            if(!likes){
                PostLike p = new PostLike();
                p.NumberOfLikes += 1;
                p.PostId = request.PostId;
               _context.PostLikes.Add(p);
            }
            else{
                var l = await _context.PostLikes.FirstOrDefaultAsync(x => x.PostId == request.PostId);
                l.NumberOfLikes += 1;
                 _context.PostLikes.Update(l);
            }
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return new GenericResponse(true, "Post has been liked.");
        }


    }

}