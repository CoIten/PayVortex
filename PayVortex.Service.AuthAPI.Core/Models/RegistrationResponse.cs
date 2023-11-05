using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class RegistrationResponse : Response
    {
        public User? CreatedUser { get; set; }
        public RegistrationResponse(bool isSuccess, IList<string> errors, string message, User createdUser)
            :base(isSuccess, errors, message)
        {
            CreatedUser = createdUser;
        }

        public RegistrationResponse(bool isSuccess, IList<string> errors, string message)
            : base(isSuccess, errors, message)
        {

        }

        public static RegistrationResponse Success(string message, User createdUser)
        {
            return new RegistrationResponse(true, new List<string>(), message, createdUser);
        }

        public static RegistrationResponse Failure(string message, IList<string> errors)
        {
            return new RegistrationResponse(false, errors, message);
        }
    }
}
