using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class Response
    {
        public bool IsSuccess { get; } = true;
        public IList<string> Errors { get; } = new List<string>();
        public string Message { get; } = string.Empty;

        protected Response(bool succeeded, IList<string> errors, string message)
        {
            IsSuccess = succeeded;
            Errors = errors ?? new List<string>();
            Message = message;
        }
    }
}
