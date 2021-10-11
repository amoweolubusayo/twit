
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

namespace Twit.Application.Queries
{
    public class GetProfileInformationQuery: IRequest<GenericResponse<ProfileInformationResult>>
    {
        public string Email { get; set; }
        
        
    }

     public class ProfileInformationResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
     public class GetProfileInformationQueryValidator : AbstractValidator<GetProfileInformationQuery>
    {
        public GetProfileInformationQueryValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
        }
    }
     public class GetProfileInformationQueryHandler : IRequestHandler<GetProfileInformationQuery, GenericResponse<ProfileInformationResult>>
    {
        private readonly TwitContext _context;
        private readonly ILogger<GetProfileInformationQueryHandler> _logger;

        public GetProfileInformationQueryHandler(TwitContext context, ILogger<GetProfileInformationQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<GenericResponse<ProfileInformationResult>> Handle(GetProfileInformationQuery request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Email == request.Email);
            if (!userExists)
            {
                return new GenericResponse<ProfileInformationResult>(false, "User not found");
            }
             var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                var profile = new ProfileInformationResult
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName
                };
            

            return new GenericResponse<ProfileInformationResult>(true, "profile information fetched",profile);


        }

    }

}