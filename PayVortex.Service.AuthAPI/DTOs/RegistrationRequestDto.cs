using System.ComponentModel.DataAnnotations;

namespace PayVortex.Service.AuthAPI.DTOs
{
    public class RegistrationRequestDto
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MaxLength(256)]
        public string Email { get; set; }
        [Required]
        [MaxLength(256)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(256)]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Last name can only contain letters and spaces.")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(256)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "User name can only contain letters and numbers.")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^[0-9]+([ -][0-9]+)*$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(256)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.")]
        public string Password { get; set; }
    }
}
