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
using Twit.Core.Utils;

namespace Twit.Application.Commands
{
    public class UpdatePostCommand : IRequest<GenericResponse>
    {
         public string Email { get; set; }
         public int PostId { get; set; }
         public string Content { get; set; }
    }
     public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.PostId).NotNull().NotEmpty();
            RuleFor(x => x.Content).NotNull().NotEmpty();
        }

    }
     public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, GenericResponse>
    {
        private readonly TwitContext _context;
        private readonly ILogger<UpdatePostCommandHandler> _logger;
        public UpdatePostCommandHandler(
            TwitContext context,
            ILogger<UpdatePostCommandHandler> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Email == request.Email);
            if (!userExists)
            {
                _logger.LogError("User does not exist.");
                return new GenericResponse(false,"User doesn't exist.");
            }
            var postExists = await _context.Posts.AnyAsync(x => x.Id == request.PostId && x.IsDeleted == true);
            if (!postExists)
            {
                _logger.LogError("Post does not exist or has been deleted.");
                return new GenericResponse(false,"Post doesn't exist or has been deleted.");
            }
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId);
            post.Content = request.Content;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return new GenericResponse(true, "Post Update successful.");
        }

    }
}