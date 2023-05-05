using System;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid UserGroupId { get; set; }
    }
}
