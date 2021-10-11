using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Twit.Core.Entities;
using Twit.Core.DTOs.APIResponse;
using Twit.Core.DTOs.APIRequests;
namespace Twit.Core.Services
{
    public interface IUserService
    {
        AuthResponse Authenticate (AuthRequest model);
        IEnumerable<User> GetAll ();
        User GetById (int id); 
    }
}