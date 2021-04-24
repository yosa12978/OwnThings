using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwnThings.API.Payload.Request;
using OwnThings.API.Payload.Response;
using OwnThings.Core.Models;
using OwnThings.Core.Repositories.Interfaces;

namespace OwnThings.API.Controllers
{
    [Route("api/v1/auth/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get user bearer token
        /// </summary>
        /// <param name="signinRequest">username and password</param>
        /// <returns>Bearer token</returns>
        [HttpPost("signin")]
        public UserTokenResponse signin([FromBody]SigninRequest signinRequest)
        {
            User user = _userRepository.Authenticate(signinRequest.username, signinRequest.password);
            return new UserTokenResponse
            {
                token = user.token
            };
        }

        /// <summary>
        /// Create User Account
        /// </summary>
        /// <param name="userRequest">username and password</param>
        /// <returns>Status code 201 or 400</returns>
        [HttpPost("signup")]
        public IActionResult signup([FromBody]CreateUserRequest userRequest)
        {
            User user = _userRepository.CreateUser(userRequest.username, userRequest.password);
            if (user == null)
                return BadRequest(new { statusCode = 400, message = "bad_request" });
            return StatusCode(201, new { statusCode = 201, message = "created" });
        }  
    }
}