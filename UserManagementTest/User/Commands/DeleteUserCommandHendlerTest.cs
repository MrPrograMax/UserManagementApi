using Application.Users.Commands.DeleteUser;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserManagementTest.Common;
using Xunit;

namespace UserManagementTest.User.Commands
{
    public class DeleteUserCommandHendlerTest : TestCommandBase
    {
        [Fact]
        public async Task DeleteUserCommandHendler_Success()
        {
            //Arrange
            var hendler = new DeleteUserCommandHendler(Context);

            //Act
            var userId = await hendler.Handle(
                new DeleteUserCommand
                {
                    UserId = UserManagementContextFactory.UserForDeleteId
                }, CancellationToken.None);

            //Assert
            Assert.NotNull(Context.Users.SingleOrDefaultAsync(user =>
                user.Id == UserManagementContextFactory.UserForDeleteId &&
                user.UserGroupId == UserManagementContextFactory.StateBlockedId));
        }
    }
} 
