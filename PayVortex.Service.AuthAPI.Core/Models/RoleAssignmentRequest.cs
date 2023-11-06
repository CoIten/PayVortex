using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class RoleAssignmentRequest
    {
        public string Email { get; set; }
        public string RoleName {  get; set; }
    }
}
