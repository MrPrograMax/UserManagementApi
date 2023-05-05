using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<UserManagementDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IUserManagementDbContext>(provider => provider.GetService<UserManagementDbContext>());

            return services;
        }
    }
}
