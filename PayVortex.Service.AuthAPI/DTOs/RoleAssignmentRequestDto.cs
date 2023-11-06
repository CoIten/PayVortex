using System.ComponentModel.DataAnnotations;

namespace PayVortex.Service.AuthAPI.DTOs
{
    public class RoleAssignmentRequestDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MaxLength(256)]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string RoleName {  get; set; }
    }
}
