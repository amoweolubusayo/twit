using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Twit.Application.Commands;
using Twit.Application.Queries;
namespace Twit.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sign up user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("updateprofile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }


        /// <summary>
        /// Find profile info by email
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("profileinfo")]
        [Authorize]
        public async Task<IActionResult> FindProfileByEmail(string email)
        {
            var query = new GetProfileInformationQuery
            {
                Email = email
            };
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

       /// Get users
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery
            {
                
            };
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

    }
}