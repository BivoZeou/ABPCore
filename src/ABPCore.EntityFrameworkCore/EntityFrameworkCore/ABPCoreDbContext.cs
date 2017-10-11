using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ABPCore.EntityFrameworkCore
{
    public class ABPCoreDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public ABPCoreDbContext(DbContextOptions<ABPCoreDbContext> options) 
            : base(options)
        {

        }
    }
}
