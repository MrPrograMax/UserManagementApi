using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class AdminAlreadyExists : Exception
    {
        public AdminAlreadyExists()
            : base($"User with role \"Admin\" already exists") { }
    }
}
