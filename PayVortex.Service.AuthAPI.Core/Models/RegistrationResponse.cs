using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class RegistrationResponse : Response
    {
        public User CreatedUser { get; set; }
    }
}
