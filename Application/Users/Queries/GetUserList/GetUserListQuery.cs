using MediatR;

namespace Application.Users.Queries.GetUserList
{
    public class GetUserListQuery : IRequest<UserListVm>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
