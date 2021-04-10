    using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.PostgreSQL
{
    public class BlockingActivityRepository : SqlServer.BlockingActivityRepository
    {
        public BlockingActivityRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}