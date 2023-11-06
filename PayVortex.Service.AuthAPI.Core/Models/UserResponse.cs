using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class UserResponse : Response
    {
        public User? User { get; set; }
        public UserResponse(bool isSuccess, IList<string> errors, string message, User user)
            : base(isSuccess, errors, message)
        {
            User = user;
        }

        public UserResponse(bool isSuccess, IList<string> errors, string message)
            : base(isSuccess, errors, message)
        {

        }

        public static UserResponse Success(string message, User user)
        {
            return new UserResponse(true, new List<string>(), message, user);
        }

        public static UserResponse Failure(string message, IList<string> errors)
        {
            return new UserResponse(false, errors, message);
        }
    }
}
