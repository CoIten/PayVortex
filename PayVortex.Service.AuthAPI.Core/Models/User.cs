using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class User : IdentityUser
    {
        [MaxLength(256)]
        public string Name { get; set; }
        [MaxLength(256)]
        public string LastName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
