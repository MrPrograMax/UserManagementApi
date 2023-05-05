namespace Persistence
{
    public class DbInitializer
    {
        public static void Initialize(UserManagementDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
