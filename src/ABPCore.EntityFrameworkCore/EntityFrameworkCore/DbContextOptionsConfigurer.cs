using Microsoft.EntityFrameworkCore;

namespace ABPCore.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for ABPCoreDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}
