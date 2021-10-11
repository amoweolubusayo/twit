using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Twit.Infrastructure.Data;
using Twit.Application.Models;
using Twit.Core.DTOs.APIResponse;
using Twit.Core.Utils;

namespace Twit.Application.Queries
{
    public class GetPostByUserIdQuery: IRequest<GenericResponse<List<PostResponse>>> 
    {
        public int UserId { get; set; }
    }
     public class GetPostByUserIdQueryValidator : AbstractValidator<GetPostByUserIdQuery>
    {
        public GetPostByUserIdQueryValidator()
        {
            RuleFor(x => x.UserId).NotNull().NotEmpty();

        }
    }
     public class GetPostByUserIdQueryHandler : IRequestHandler<GetPostByUserIdQuery, GenericResponse<List<PostResponse>>>
    {
        private readonly TwitContext _context;
        private readonly ILogger<GetPostByUserIdQueryHandler> _logger;

        public GetPostByUserIdQueryHandler(TwitContext context, ILogger<GetPostByUserIdQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<GenericResponse<List<PostResponse>>> Handle(GetPostByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Id == request.UserId);
            if (!userExists)
            {
                return new GenericResponse<List<PostResponse>>(false, "User not found");
            }
               var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
                var posts = await (from post in _context.Posts join u in _context.Users 
                                           on post.UserId equals u.Id 
                                           where post.UserId == request.UserId
                                           orderby post.Id descending
                                           select new PostResponse
                                           {
                                             PostId = post.Id,
                                             Content = post.Content,
                                             IsDeleted = post.IsDeleted,
                                             IsLiked = post.Liked,
                                             PostedBy = user.UserName
                                           }).Where(p=> p.IsDeleted == false).ToListAsync();
            
            return new GenericResponse<List<PostResponse>>(true, "posts information fetched",posts);
        }

    }
}