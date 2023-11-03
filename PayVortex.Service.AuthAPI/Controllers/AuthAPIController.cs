using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PayVortex.Service.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserPostDTO registrationDTO)
        //{
        //    var userRegistration = _mapper.Map<UserPost>(registrationDTO);
        //    var createdUser = await _userService.CreateUser(userRegistration);
        //    var createdUserDTO = _mapper.Map<UserDTO>(createdUser);
        //    return CreatedAtAction(nameof(this.GetUserByIdAsync), new { userId = createdUserDTO.Id }, createdUserDTO);
        //}

        //[HttpPost("login")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        //{
        //    var userLogin = _mapper.Map<UserLogin>(userLoginDTO);
        //    var token = await _userService.AuthenticateAndGenerateToken(userLogin);
        //    if (token == null)
        //    {
        //        return Unauthorized();
        //    }
        //    return Ok(new { Token = token });
        //}
    }
}
