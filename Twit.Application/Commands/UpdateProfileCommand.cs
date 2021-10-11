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
    public class UpdateProfileCommand : IRequest<GenericResponse>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
     public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
        }
    }
     public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, GenericResponse>
    {
        private readonly TwitContext _context;
        private readonly ILogger<UpdateProfileCommandHandler> _logger;
        public UpdateProfileCommandHandler(
            TwitContext context,
            ILogger<UpdateProfileCommandHandler> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GenericResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Email == request.Email);
            if (!userExists)
            {
                _logger.LogError("User does not exist.");
                return new GenericResponse(false,"User doesn't exist.");
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UserName = request.UserName;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new GenericResponse(true, "Update successful.");
        }

    }
}