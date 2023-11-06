using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Models
{
    public class RoleAssignmentResponse : Response
    {
        protected RoleAssignmentResponse(bool succeeded, IList<string> errors, string message)
            : base(succeeded, errors, message)
        {
        }

        public static RoleAssignmentResponse Success(string message)
        {
            return new RoleAssignmentResponse(true, new List<string>(), message);
        }

        public static RoleAssignmentResponse Failure(string message, IList<string> errors)
        {
            return new RoleAssignmentResponse(false, errors, message);
        }
    }
}
