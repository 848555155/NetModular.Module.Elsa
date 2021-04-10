    using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.PostgreSQL
{
    public class WorkflowInstanceRepository : SqlServer.WorkflowInstanceRepository
    {
        public WorkflowInstanceRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}