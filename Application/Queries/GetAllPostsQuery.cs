using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using tweetee.Application.Models;
using tweetee.Infrastructure.Persistence;
using tweetee.Infrastructure.Utility.Security;

namespace tweetee.Application.Queries
{
    public class GetAllPostsQuery: IRequest<GenericResponse<List<PostResponse>>> 
    {
        
    }
     public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, GenericResponse<List<PostResponse>>>
    {
        private readonly TweeteeContext _context;
        private readonly ILogger<GetAllPostsQueryHandler> _logger;

        public GetAllPostsQueryHandler(TweeteeContext context, ILogger<GetAllPostsQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<GenericResponse<List<PostResponse>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {

            var posts = await _context.Posts.Select(p => new PostResponse
            {
                PostId = p.Id,
                Content = p.Content,
                IsDeleted = p.IsDeleted,
                IsLiked = p.Liked,
                NumberOfLikes = _context.PostLikes.First(l => l.PostId == p.Id).NumberOfLikes,
                PostedBy = _context.Users.First(u => u.Id == p.UserId).UserName
        }).Where(p=> p.IsDeleted == false).ToListAsync();
            
            return new GenericResponse<List<PostResponse>>(true, "post information fetched",posts);
        }

    }
}