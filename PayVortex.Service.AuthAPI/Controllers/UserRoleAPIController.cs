using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayVortex.Service.AuthAPI.Core.Interfaces.Services;
using PayVortex.Service.AuthAPI.Core.Models;
using PayVortex.Service.AuthAPI.DTOs;

namespace PayVortex.Service.AuthAPI.Controllers
{
    [Route("api/userRole")]
    [ApiController]
    public class UserRoleAPIController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRoleAPIController> _logger;
        protected ResponseDto _response;

        public UserRoleAPIController(IUserRoleService userRoleService, IMapper mapper, ILogger<UserRoleAPIController> logger)
        {
            _userRoleService = userRoleService;
            _mapper = mapper;
            _logger = logger;
            _response = new();
        }

        [HttpPost("assignRole")]
        //[Authorize]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignmentRequestDto requestDto)
        {
            try
            {
                var request = _mapper.Map<RoleAssignmentRequest>(requestDto);
                var roleAssignmentResponse = await _userRoleService.AssignRole(request);

                _response.IsSuccess = roleAssignmentResponse.IsSuccess;
                _response.Message = roleAssignmentResponse.Message;
                if (roleAssignmentResponse.IsSuccess)
                {
                    return Ok(_response);
                }
                else
                {
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during role assignment");

                _response.IsSuccess = false;
                _response.Message = "An unexpected error occurred";
                return StatusCode(500, _response);
            }
        }
    }
}
