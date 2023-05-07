using System.Collections.Generic;

namespace Application.Users.Queries.GetUserList
{
    public class UserListVm
    {
        public IList<UserLookupDto> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCountRecord { get; set; }
        public int TotalPages { get; set; }
    }
}
