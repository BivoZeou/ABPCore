using ABPCore.EntityFrameworkCore;

namespace ABPCore.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly ABPCoreDbContext _context;

        public TestDataBuilder(ABPCoreDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}