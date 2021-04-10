using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.MySql
{
    public class BlockingActivityRepository : SqlServer.BlockingActivityRepository
    {
        public BlockingActivityRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}