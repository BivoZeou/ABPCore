using System;
using System.Threading.Tasks;
using Abp.TestBase;
using ABPCore.EntityFrameworkCore;
using ABPCore.Tests.TestDatas;

namespace ABPCore.Tests
{
    public class ABPCoreTestBase : AbpIntegratedTestBase<ABPCoreTestModule>
    {
        public ABPCoreTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<ABPCoreDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<ABPCoreDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<ABPCoreDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<ABPCoreDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<ABPCoreDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<ABPCoreDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<ABPCoreDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<ABPCoreDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
