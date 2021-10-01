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
    public class GetLikesQuery:  IRequest<GenericResponse<List<LikeResponse>>> 
    {
        
    }
      public class GetLikesQueryHandler : IRequestHandler<GetLikesQuery, GenericResponse<List<LikeResponse>>>
    {
        private readonly TweeteeContext _context;
        private readonly ILogger<GetLikesQueryHandler> _logger;

        public GetLikesQueryHandler(TweeteeContext context, ILogger<GetLikesQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<GenericResponse<List<LikeResponse>>> Handle(GetLikesQuery request, CancellationToken cancellationToken)
        {

            var likes = await _context.PostLikes.Select(p => new LikeResponse
            {
                PostId = p.PostId,
                NumberOfLikes = p.NumberOfLikes
        }).ToListAsync();
            
            return new GenericResponse<List<LikeResponse>>(true, "like information fetched",likes);
        }

    }
}