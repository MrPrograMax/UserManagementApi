using Persistence;
using System;

namespace UserManagementTest.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly UserManagementDbContext Context;

        public TestCommandBase() 
        {  
            Context = UserManagementContextFactory.Create(); 
        }

        public void Dispose()
        {
            UserManagementContextFactory.Destroy(Context);
        }
    }
}
