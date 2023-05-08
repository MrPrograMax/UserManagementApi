using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using Domain;
using System.Security.Cryptography;

namespace UserManagementTest.Common
{
    public class UserManagementContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();
        public static Guid UserForDeleteId = Guid.NewGuid();

        public static Guid UserIdForDelete = Guid.NewGuid();

        public static Guid GroupAdminId = Guid.NewGuid();
        public static Guid GroupUserId = Guid.NewGuid();

        public static Guid StateActiveId = Guid.NewGuid();
        public static Guid StateBlockedId = Guid.NewGuid();



        public static UserManagementDbContext Create()
        {
            var options = new DbContextOptionsBuilder<UserManagementDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new UserManagementDbContext(options);
            context.Database.EnsureCreated();

            context.UserGroups.AddRange(
                new UserGroup
                {
                    Id = GroupAdminId,
                    Code = "Admin",
                    Description = "Role Admin" 
                },
                new UserGroup
                { 
                    Id = GroupUserId,
                    Code = "User",
                    Description = "Role User"
                }
            );

            context.UserStates.AddRange(
                new UserState
                {
                    Id = StateActiveId,
                    Code = "Active",
                    Description = "State: Active"
                },
                new UserState
                {
                    Id = StateBlockedId,
                    Code = "Blocked",
                    Description = "State: Blocked"
                }
            );

            context.Users.AddRange(
                new Domain.User
                {
                    Id = UserAId,
                    Login = "Admin",
                    Password = "Admin",
                    CreatedDate = DateTime.Now,
                    UserGroupId = GroupAdminId,
                    UserStateId = StateActiveId,
                    Salt = GenerateSalt()
                },
                new Domain.User
                {
                    Id = UserBId,
                    Login = "User",
                    Password = "User",
                    CreatedDate = DateTime.Now,
                    UserGroupId = GroupUserId,
                    UserStateId = StateActiveId,
                    Salt = GenerateSalt()
                },
                new Domain.User
                {
                    Id = UserForDeleteId,
                    Login = "UserFprDelete",
                    Password = "UserForDelete",
                    CreatedDate = DateTime.Now,
                    UserGroupId = GroupUserId,
                    UserStateId = StateActiveId,
                    Salt = GenerateSalt()
                }
            );

            context.SaveChanges();
            return context;
        }

        public static void Destroy(UserManagementDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);
                return salt;
            }
        }
    }
}
