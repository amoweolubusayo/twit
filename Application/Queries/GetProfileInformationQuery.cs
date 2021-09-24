
using System;
using System.Threading;
using System.Threading.Tasks;
using tweetee.Application.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using tweetee.Infrastructure.Persistence;
using tweetee.Infrastructure.Utility.Security;
namespace tweetee.Application.Queries
{
    public class GetProfileInformationQuery: IRequest<GenericResponse<ProfileInformationResponse>>
    {
        public string Email { get; set; } 
    }

     public class ProfileInformationResponse
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
     public class GetProfileInformationQueryHandler : IRequestHandler<GetProfileInformationQuery, GenericResponse<ProfileInformationResponse>>
    {
        private readonly TweeteeContext _context;
        private readonly ILogger<GetProfileInformationQueryHandler> _logger;

        public GetProfileInformationQueryHandler(TweeteeContext context, ILogger<GetProfileInformationQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<GenericResponse<ProfileInformationResponse>> Handle(GetProfileInformationQuery request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Email == request.Email);
            if (!userExists)
            {
                return new GenericResponse<ProfileInformationResponse>(false, "User not found");
            }
             var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                var profile = new ProfileInformationResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName
                };
            

            return new GenericResponse<ProfileInformationResponse>(true, "profile information fetched",profile);


        }

    }

}