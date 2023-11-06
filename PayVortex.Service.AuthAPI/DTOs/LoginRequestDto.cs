using System.ComponentModel.DataAnnotations;

namespace PayVortex.Service.AuthAPI.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
