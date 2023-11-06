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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationDto)
        {
            try
            {
                var registrationRequest = _mapper.Map<RegistrationRequest>(registrationDto);
                var registrationResponse = await _authService.Register(registrationRequest);
                if (registrationResponse.IsSuccess)
                {
                    var createdUserDto = _mapper.Map<UserDto>(registrationResponse.CreatedUser);
                    _response.Result = createdUserDto;
                    _response.Message = registrationResponse.Message;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.Message = registrationResponse.Message;
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
            var userLogin = _mapper.Map<LoginRequest>(userLoginDTO);
            var loginResponse = await _authService.Login(userLogin);
            if (!loginResponse.IsSuccess)
            {
                _response.IsSuccess = false;
                _response.Message = loginResponse.Message;
                return BadRequest(_response);
            }

            _response.IsSuccess = true;
            _response.Message = loginResponse.Message;
            _response.Result = loginResponse.Token;
            return Ok(_response);
        }
    }
}
