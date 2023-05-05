using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class UserManagementDbContext : DbContext, IUserManagementDbContext
    {
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserState> UserStates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserGroup>()
                .HasMany(userGroup => userGroup.Users)
                .WithOne(user => user.UserGroup)
                .HasForeignKey(user => user.UserGroupId);

            builder.Entity<UserState>()
                .HasMany(userState => userState.Users)
                .WithOne(user => user.UserState)
                .HasForeignKey(user => user.UserStateId);
        }
    }
}
