using Application.Users.Queries.GetUserList;
using System.Collections.Generic;

namespace Application.Users.Queries.GetAllUsers
{
    public class AllUserListVm
    {
        public IList<UserLookupDto> Users { get; set; }
    }
}
