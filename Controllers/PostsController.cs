using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tweetee.Application.Commands;
using tweetee.Application.Models;
using tweetee.Application.Queries;
using tweetee.Services;
using tweetee.Application.Entities;
namespace tweetee.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add Post
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("addpost")]
        [Authorize]
        public async Task<IActionResult> AddPost([FromBody] AddPostCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

         /// <summary>
        /// Update Post
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("updatepost")]
        [Authorize]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Update Post
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("deletepost")]
        [Authorize]
        public async Task<IActionResult> DeletePost([FromBody] DeletePostCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

         /// <summary>
        /// Like Post
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("likepost")]
        [Authorize]
        public async Task<IActionResult> LikePost([FromBody] LikePostCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Unlike Post
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost("unlikepost")]
        [Authorize]
        public async Task<IActionResult> LikePost([FromBody] UnlikePostCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

          /// <summary>
        /// Get post by id
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getpostById")]
        [Authorize]
        public async Task<IActionResult> GetPostById(int Id)
        {
            var query = new GetPostByIdQuery
            {
                Id = Id
            };
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

         /// <summary>
        /// Get post by user id
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getpostByUserId")]
        [Authorize]
        public async Task<IActionResult> GetPostByUserId(int userId)
        {
            var query = new GetPostByUserIdQuery
            {
                UserId = userId
            };
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }
         /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var query = new GetAllPostsQuery
            {
                
            };
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

         /// Get likes
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getLikes")]
        public async Task<IActionResult> GetLikes()
        {
            var query = new GetLikesQuery
            {
                
            };
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }

        /// Get likes per post
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getLikesPerPost")]
        [Authorize]
        public async Task<IActionResult> GetLikesByPost(int postId)
        {
            var query = new GetLikesByPostIdQuery
            {
                PostId = postId
            };
            var result = await _mediator.Send(query);
            return result.Status ? (IActionResult)Ok(result) : BadRequest(result);
        }
    }
}