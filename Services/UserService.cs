using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using tweetee.Application.Entities;
using tweetee.Application.Models;
using tweetee.Infrastructure.Utility.Security;
namespace tweetee.Services
{
    public class UserService : IUserService
    {
         // users hardcoded for simplicity
        public List<User> _users = new List<User> {
            new User { Id = 1, FirstName = "Test", LastName = "User", UserName = "test" },
            new User { Id = 2, FirstName = "Demo", LastName = "Demo", UserName = "demo"}
        };

        private readonly GeneralSettings _generalSettings;

        public UserService (IOptions<GeneralSettings> generalSettings) {
            _generalSettings = generalSettings.Value;
        }

        public UserService(){}
        public AuthResponse Authenticate (AuthRequest model) {
            var user = _users.SingleOrDefault (x => x.UserName == model.Username);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken (user);

            return new AuthResponse (user, token);
        }

        public IEnumerable<User> GetAll () {
            return _users;
        }

        public User GetById (int id) {
            return _users.FirstOrDefault (x => x.Id == id);
        }

        // helper methods

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
