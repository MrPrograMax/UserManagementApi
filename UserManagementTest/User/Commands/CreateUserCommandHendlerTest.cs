using Application.Users.Commands.CreateUser;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserManagementTest.Common;
using Xunit;

namespace UserManagementTest.User.Commands
{
    public class CreateUserCommandHendlerTest : TestCommandBase
    {
        [Fact]
        public async Task CreateUserCommandHendler_Success()
        {
            //Arrange
            var hendler = new CreateUserCommandHendler(Context);

            var login = "UserCreate";
            var password = "UserCreate";
            var groupId = UserManagementContextFactory.GroupUserId;

            //Act
            var userId = await hendler.Handle(
                new CreateUserCommand
                {
                    Login = login,
                    Password = password,
                    UserGroupId = groupId,
                },
                CancellationToken.None);

            //Assert
            Assert.NotNull(
                await Context.Users.SingleOrDefaultAsync(user =>
                    user.Id == userId &&
                    user.Login == login &&
                    user.UserGroupId == groupId
            ));
        }
    }
}
