using Application.Common.Mappings;
using Application.Interfaces;
using AutoMapper;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UserManagementTest.Common
{
    public class QueryTestFixture : IDisposable
    {
        public UserManagementDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = UserManagementContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IUserManagementDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            UserManagementContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
