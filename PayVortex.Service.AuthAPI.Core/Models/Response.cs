using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; } = true;
        public IList<string> Errors { get; } = new List<string>();
        public string Message { get; } = string.Empty;
    }
}
