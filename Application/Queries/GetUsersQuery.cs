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
    public class GetUsersQuery:  IRequest<GenericResponse<List<UserResponse>>>
    {
        
    }
      public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GenericResponse<List<UserResponse>>>
    {
        private readonly TweeteeContext _context;
        private readonly ILogger<GetUsersQueryHandler> _logger;

        public GetUsersQueryHandler(TweeteeContext context, ILogger<GetUsersQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<GenericResponse<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {

            var user = await _context.Users.Select(u => new UserResponse
            {
                UserId = u.Id,
                UserName = u.UserName,
                NumberOfPosts = _context.Posts.Where(p => p.UserId == u.Id).Count(),
        }).ToListAsync();
            
            return new GenericResponse<List<UserResponse>>(true, "user information fetched",user);
        }

    }
}