using Application.Users.Queries.GetUserList;
using AutoMapper;
using Persistence;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagementTest.Common;
using Xunit;

namespace UserManagementTest.User.Queries
{
    [Collection("QueryCollection")]
    public class GetUserListQueryHendlerTest
    {
        private readonly UserManagementDbContext Context;
        private readonly IMapper Mapper;

        public GetUserListQueryHendlerTest(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNoteListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetUserListQueryHendler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetUserListQuery
                {
                    CurrentPage = 1,
                    PageSize = 10,
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<UserListVm>();
            result.Users.Count.ShouldBe(3);
        }
    }
}
