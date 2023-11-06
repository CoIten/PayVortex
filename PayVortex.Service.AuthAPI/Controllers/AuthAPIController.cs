using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PayVortex.Service.AuthAPI.Core.Interfaces.Services;
using PayVortex.Service.AuthAPI.Core.Models;
using PayVortex.Service.AuthAPI.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PayVortex.Service.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthAPIController> _logger;

        public AuthAPIController(IAuthService userService, IMapper mapper, ILogger<AuthAPIController> logger)
        {
            _authService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegistrationRequestDto registrationDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var registrationRequest = _mapper.Map<RegistrationRequest>(registrationDto);
                    var registrationResponse = await _authService.Register(registrationRequest);
                    if (registrationResponse.IsSuccess)
                    {
                        var createdUserDto = _mapper.Map<UserDto>(registrationResponse.CreatedUser);
                        return Ok(new { User = createdUserDto, registrationResponse.Message });
                    }
                    else
                    {
                        return BadRequest(new { registrationResponse.Message, registrationResponse.Errors });
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { Message = "Validation Failed", Errors = errors});
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during registration");
                return StatusCode(500, new { Message = "An unexpected error occurred", Error = ex.Message });
            }
        }

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
