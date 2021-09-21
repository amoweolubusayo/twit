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
    public interface IUserService
    {
        AuthResponse Authenticate (AuthRequest model);
        IEnumerable<User> GetAll ();
        User GetById (int id); 
    }
}