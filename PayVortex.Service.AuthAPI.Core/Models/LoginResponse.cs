using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class LoginResponse : Response
    {
        public User? User { get; set; }
        public string? Token { get; set; }
        public LoginResponse(bool isSuccess, IList<string> errors, string message, User createdUser, string token)
            : base(isSuccess, errors, message)
        {
            User = createdUser;
            Token = token;
        }

        public LoginResponse(bool isSuccess, IList<string> errors, string message)
            : base(isSuccess, errors, message)
        {

        }

        public static LoginResponse Success(string message, User createdUser, string token)
        {
            return new LoginResponse(true, new List<string>(), message, createdUser, token);
        }

        public static LoginResponse Failure(string message, IList<string> errors)
        {
            return new LoginResponse(false, errors, message);
        }
    }
}
