using System;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class DeleteUserCommand : IRequest<Guid>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid UserStateId { get; set; }
    }
}
