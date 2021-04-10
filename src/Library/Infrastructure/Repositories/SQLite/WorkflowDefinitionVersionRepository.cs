using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.SQLite
{
    public class WorkflowDefinitionVersionRepository : SqlServer.WorkflowDefinitionVersionRepository
    {
        public WorkflowDefinitionVersionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
