using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.SQLite
{
    public class ActivityInstanceRepository : SqlServer.ActivityInstanceRepository
    {
        public ActivityInstanceRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
