using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PayVortex.Service.AuthAPI.Core.Interfaces.Services;
using PayVortex.Service.AuthAPI.Core.Models;
using PayVortex.Service.AuthAPI.DTOs;

namespace PayVortex.Service.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthAPIController(IAuthService userService, IMapper mapper)
        {
            _authService = userService;
            _mapper = mapper;
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<UserDto>> CreateUser([FromBody] RegistrationRequestDto registrationDto)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }

        //    var user = _mapper.Map<RegistrationRequest>(registrationDto);
        //    var createdUser = await _authService.Register(user);
        //    var createdUserDto = _mapper.Map<UserDto>(createdUser);
        //    return CreatedAtAction(nameof(this.GetUserByIdAsync), new { userId = createdUserDto.Id }, createdUserDto);
        //}

        //[HttpPost("login")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto userLoginDTO)
        //{
        //    var userLogin = _mapper.Map<UserDto>(userLoginDTO);
        //    var token = await _authService.AuthenticateAndGenerateToken(userLogin);
        //    if (token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(new { Token = token });
        //}
    }
}
