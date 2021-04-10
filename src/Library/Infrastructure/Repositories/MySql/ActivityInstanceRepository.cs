using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.MySql
{
    public class ActivityInstanceRepository : SqlServer.ActivityInstanceRepository
    {
        public ActivityInstanceRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}