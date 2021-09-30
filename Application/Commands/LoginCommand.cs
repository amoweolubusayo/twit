using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using tweetee.Application.Entities;
using tweetee.Application.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using FluentValidation;
using tweetee.Infrastructure.Persistence;
using tweetee.Infrastructure.Utility.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace tweetee.Application.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
         public string Email { get; set; }
         public string Password { get; set; }
    }
     public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }

    }

     public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly TweeteeContext _context;
        private readonly GeneralSettings _generalSettings;
        private readonly ILogger<LoginCommandHandler> _logger;
        public LoginCommandHandler(
            TweeteeContext context,
            IOptions<GeneralSettings> generalSettings,
            ILogger<LoginCommandHandler> logger
        )
        {
            _context = context;
            _generalSettings = generalSettings.Value;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return null;

            User user = await _context.Users.SingleOrDefaultAsync(x => x.Email == request.Email);

            // check if user exists
            if (user == null)
                 return new AuthResponse(user, "This user does not exist.");

            // check if password is correct
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            var token = generateJwtToken (user);
            return new AuthResponse (user, token);
        }
         private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
         private string generateJwtToken (User user) {
            //token is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = Encoding.ASCII.GetBytes (_generalSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (new [] { new Claim ("id", user.Id.ToString ()) }),
                Expires = DateTime.UtcNow.AddDays (7),
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken (tokenDescriptor);
            return tokenHandler.WriteToken (token);
        }

    }
}