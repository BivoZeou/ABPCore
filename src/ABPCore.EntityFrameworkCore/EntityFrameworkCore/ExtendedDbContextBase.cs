using System.Data.Common;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Abp.EntityFrameworkCore
{
    public class ExtendedDbContextBase : AbpDbContext
    {
        public ExtendedDbContextBase(DbContextOptions options) 
    	    : base(options)
        {
        }
    }
}
