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
        protected ResponseDto _response;

        public AuthAPIController(IAuthService userService, IMapper mapper, ILogger<AuthAPIController> logger)
        {
            _authService = userService;
            _mapper = mapper;
            _logger = logger;
            _response = new();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationDto)
        {
            try
            {
                var registrationRequest = _mapper.Map<RegistrationRequest>(registrationDto);
                var registrationResponse = await _authService.Register(registrationRequest);

                _response.IsSuccess = registrationResponse.IsSuccess;
                _response.Message = registrationResponse.Message;
                if (registrationResponse.IsSuccess)
                {
                    var createdUserDto = _mapper.Map<UserDto>(registrationResponse.CreatedUser);
                    _response.Result = createdUserDto;
                    return Ok(_response);
                }
                else
                {
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during registration");

                _response.IsSuccess = false;
                _response.Message = "An unexpected error occurred";
                return StatusCode(500, _response);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto userLoginDTO)
        {
            try
            {
                var userLogin = _mapper.Map<LoginRequest>(userLoginDTO);
                var loginResponse = await _authService.Login(userLogin);

                _response.IsSuccess = loginResponse.IsSuccess;
                _response.Message = loginResponse.Message;
                if (!loginResponse.IsSuccess)
                {
                    return BadRequest(_response);
                }

                var userDto = _mapper.Map<UserDto>(loginResponse.User);
                _response.Result = new { loginResponse.Token, User = userDto };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during login process");

                _response.IsSuccess = false;
                _response.Message = "An unexpected error occurred";
                return StatusCode(500, _response);
            }
        }
    }
}
