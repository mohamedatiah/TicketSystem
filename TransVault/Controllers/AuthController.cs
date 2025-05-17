using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TransVault.Application.DTOs;
using TransVault.Application.Interfaces;
using TransVault.Common;

namespace TransVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseApiController
    {

        private IUserService _userService;
        private IAuthService _authService;

        public AuthController(IMapper mapper, IUserService UserService, IAuthService authService)
        {
            _userService = UserService;
            _authService = authService;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest loginDTO)
        {
            var user = await _userService.ValidateUserAsync(loginDTO.Email, loginDTO.Password);
            if (user == null)
            {
                return ErrorResponse(HttpStatusCode.BadRequest, "Invalid Email or Password");
            }

            var claims = _authService.generateClaim(user.Email, user.Password,user.Id.ToString());
            string jwtSecurityToken = _authService.GeneratejwtSecurityToken(claims);
            return OKResponse(jwtSecurityToken);
        }
        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO UserDto)
        {
            var res = await _userService.GetUserByEmail(UserDto.Email);
            if (res != null)
            {
                return ErrorResponse(HttpStatusCode.BadRequest, "This Email Registerd Before");
            }
            await _userService.AddUser(UserDto);
            return OKResponse();
        }
    }
}
