using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserManagementDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<UserGroup> UserGroups { get; set; }
        DbSet<UserState> UserStates { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
