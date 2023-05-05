using MediatR;
using System;

namespace Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetailsVm>
    {
        public Guid UserId { get; set; }

    }
}
