using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TikToken.DTOs;
using TikToken.Interfaces;
using TikToken.Models;

namespace TikToken.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUser([FromBody, Required] RegisterDTO newUser)
        {
            int userId = await _userService.RegisterAsync(newUser);
            return userId != -1 ? Ok(new { userid = userId }) : StatusCode(500, new { error = "Error registering user" });
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody, Required] LoginDTO userLogin)
        {
            User? user = _userService.Login(userLogin);
            if (user == null) return Unauthorized(new { error = "Invalid user or password" });
            string token = _tokenService.CreateToken(user);
            return Ok(new UserDTO{UserName = user.UserName, Token = token});
        }

        [Authorize]    
        [HttpGet()]
        public ActionResult GetSomething()
        {
            return Ok("Hello");
        }
    }
}