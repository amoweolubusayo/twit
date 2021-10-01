using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FluentValidation;

using MediatR;

using tweetee.Application.Models;
using tweetee.Infrastructure.Persistence;
using tweetee.Infrastructure.Utility.Security;

namespace tweetee.Application.Queries
{
    public class GetLikesByPostIdQuery : IRequest<GenericResponse<LikeResponse>>
    {
        public int PostId { get; set; }
    }
    public class GetLikesByPostIdQueryValidator : AbstractValidator<GetLikesByPostIdQuery>
    {
        public GetLikesByPostIdQueryValidator()
        {
            RuleFor(x => x.PostId).NotNull().NotEmpty();

        }
    }
    public class GetLikesByPostIdQueryHandler : IRequestHandler<GetLikesByPostIdQuery, GenericResponse<LikeResponse>>
    {
        private readonly TweeteeContext _context;
        private readonly ILogger<GetLikesByPostIdQueryHandler> _logger;

        public GetLikesByPostIdQueryHandler(TweeteeContext context, ILogger<GetLikesByPostIdQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<GenericResponse<LikeResponse>> Handle(GetLikesByPostIdQuery request, CancellationToken cancellationToken)
        {
            var postExists = await _context.Posts.AnyAsync(x => x.Id == request.PostId);
            if (!postExists)
            {
                return new GenericResponse<LikeResponse>(false, "Post not found");
            }
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId);
            if (post.IsDeleted)
            {
                return new GenericResponse<LikeResponse>(false, "Post has been deleted");
            }
            var liked = await _context.PostLikes.AnyAsync(x => x.PostId == request.PostId);
            if (!liked)
            {
                var unlikes = new LikeResponse
                {
                    PostId = request.PostId,
                    NumberOfLikes = 0,

                };
              return new GenericResponse<LikeResponse>(true, "like information fetched", unlikes);
            }
            
            var posts = await _context.PostLikes.FirstOrDefaultAsync(l => l.PostId == request.PostId);
            var likes = new LikeResponse
            {
                PostId = posts.PostId,
                NumberOfLikes = posts.NumberOfLikes,
            };

            return new GenericResponse<LikeResponse>(true, "like information fetched", likes);
        }

    }

}