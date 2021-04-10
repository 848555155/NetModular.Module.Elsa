using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.MySql
{
    public class WorkflowInstanceRepository : SqlServer.WorkflowInstanceRepository
    {
        public WorkflowInstanceRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}