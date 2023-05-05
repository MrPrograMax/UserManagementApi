using MediatR;
using System;

namespace Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }
}
