using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Twit.Application.Models;
using Twit.Core.DTOs.APIResponse;
using Twit.Core.Utils;
using Twit.Infrastructure.Data;


namespace Twit.Application.Queries
{
    public class GetLikesQuery:  IRequest<GenericResponse<List<LikeResponse>>> 
    {
        
    }
      public class GetLikesQueryHandler : IRequestHandler<GetLikesQuery, GenericResponse<List<LikeResponse>>>
    {
        private readonly TwitContext _context;
        private readonly ILogger<GetLikesQueryHandler> _logger;

        public GetLikesQueryHandler(TwitContext context, ILogger<GetLikesQueryHandler> logger)
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