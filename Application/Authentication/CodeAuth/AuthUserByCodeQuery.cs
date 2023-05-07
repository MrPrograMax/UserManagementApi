using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.CodeAuth
{
    public class AuthUserByCodeQuery : IRequest<bool>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string GroupCode { get; set; }
    }
}
