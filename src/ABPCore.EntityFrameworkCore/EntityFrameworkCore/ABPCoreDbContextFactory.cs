using ABPCore.Configuration;
using ABPCore.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ABPCore.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class ABPCoreDbContextFactory : IDesignTimeDbContextFactory<ABPCoreDbContext>
    {
        public ABPCoreDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ABPCoreDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(ABPCoreConsts.ConnectionStringName)
            );

            return new ABPCoreDbContext(builder.Options);
        }
    }
}