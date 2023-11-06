using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class TokenResponse : Response
    {
        public string? Token { get; set; }
        public TokenResponse(bool succeeded, IList<string> errors, string message, string token) 
            : base(succeeded, errors, message)
        {
            Token = token;
        }

        public TokenResponse(bool isSuccess, IList<string> errors, string message)
            : base(isSuccess, errors, message)
        {

        }

        public static TokenResponse Success(string message, string token)
        {
            return new TokenResponse(true, new List<string>(), message, token);
        }

        public static TokenResponse Failure(string message, IList<string> errors)
        {
            return new TokenResponse(false, errors, message);
        }
    }
}
