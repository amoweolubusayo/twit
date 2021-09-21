using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using tweetee.Application.Commands;
using tweetee.Application.Models;
//using tweetee.Application.Queries;
using tweetee.Services;
using tweetee.Application.Entities;
namespace tweetee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }


        /// <summary>
        /// Gets lists of all users
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}