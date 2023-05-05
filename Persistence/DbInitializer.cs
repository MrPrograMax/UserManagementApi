using Domain;
using System.Linq;

namespace Persistence
{
    public class DbInitializer
    {
        public static void Initialize(UserManagementDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.UserGroups.Any())
            {
                context.UserGroups.AddRange(
                    new UserGroup { Code = "Admin", Description = "You have more options" },
                    new UserGroup { Code = "User", Description = "You are just a user" }
                );

                context.SaveChanges();
            }

            if (!context.UserStates.Any())
            {
                context.UserStates.AddRange(
                    new UserState { Code = "Active", Description = "You are active in the system" },
                    new UserState { Code = "Blocked", Description = "You are removed from the system" }
                );

                context.SaveChanges();
            }
        }
    }
}
