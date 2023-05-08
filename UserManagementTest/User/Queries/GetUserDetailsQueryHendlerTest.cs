using Application.Users.Queries.GetUserDetails;
using AutoMapper;
using Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagementTest.Common;
using Shouldly;
using Xunit;

namespace UserManagementTest.User.Queries
{
    [Collection("QueryCollection")]
    public class GetUserDetailsQueryHendlerTest
    {
        private readonly UserManagementDbContext Context;
        private readonly IMapper Mapper;

        public GetUserDetailsQueryHendlerTest(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNoteDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetUserDetailsQueryHendler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetUserDetailsQuery
                {
                    UserId = UserManagementContextFactory.UserAId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UserDetailsVm>();
            result.Login.ShouldBe("Admin");
            result.UserGroupCode.ShouldBe("Admin");
            result.UserStateCode.ShouldBe("Active");
        }
    }
}
