using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.MySql
{
    public class ActivityDefinitionRepository : SqlServer.ActivityDefinitionRepository
    {
        public ActivityDefinitionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}