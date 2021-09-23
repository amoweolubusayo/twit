using System;
using System.Threading;
using System.Threading.Tasks;
using tweetee.Application.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using FluentValidation;
using tweetee.Infrastructure.Persistence;
using tweetee.Infrastructure.Utility.Security;
namespace tweetee.Application.Commands
{
    public class AddPostCommand : IRequest<GenericResponse>
    {
         public string Email { get; set; }
         public string Content { get; set; }
    }
     public class AddPostCommandValidator : AbstractValidator<AddPostCommand>
    {
        public AddPostCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Content).NotNull().NotEmpty();
        }

    }
     public class AddPostCommandHandler : IRequestHandler<AddPostCommand, GenericResponse>
    {
        private readonly TweeteeContext _context;
        private readonly GeneralSettings _generalSettings;
        private readonly ILogger<AddPostCommandHandler> _logger;
        public AddPostCommandHandler(
            TweeteeContext context,
            IOptions<GeneralSettings> generalSettings,
            ILogger<AddPostCommandHandler> logger
        )
        {
            _context = context;
            _generalSettings = generalSettings.Value;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Email == request.Email);
            if (!userExists)
            {
                _logger.LogError("User doesn't exist.");
                return new GenericResponse(false,"User doesn't exist.");
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            Post post = new Post();
            post.UserId = user.Id;
            post.Content = request.Content;
             _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return new GenericResponse(true, "Posted successfully.");
        }

    }
}