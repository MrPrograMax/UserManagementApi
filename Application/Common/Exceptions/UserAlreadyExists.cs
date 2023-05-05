using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class UserAlreadyExists : Exception
    {
        public UserAlreadyExists(string name)
            : base($"User with login \"{name}\" already exists") { }
    }
}
