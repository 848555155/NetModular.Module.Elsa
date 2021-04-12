using Elsa.Models;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Core;
using NetModular.Module.Elsa.Domain.BlockingActivity;
using NetModular.Module.Elsa.Domain.WorkflowInstance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.SqlServer
{
    public class WorkflowInstanceRepository : RepositoryAbstract<WorkflowInstanceEntity>, IWorkflowInstanceRepository
    {
        public WorkflowInstanceRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> BatchDeleteByDefinitionIdAsync(string definitionId, IUnitOfWork uow)
        {
            return Db.Find()
                .Where(a => a.DefinitionId == definitionId)
                .UseUow(uow)
                .DeleteAsync();
        }

        public Task<WorkflowInstanceEntity> GetByCorrelationIdAsync(string correlationId)
        {
            return Db.Find()
                .Where(a => a.CorrelationId == correlationId)
                .FirstAsync();
        }

        public Task<WorkflowInstanceEntity> GetByIdAsync(string id)
        {
            return Db.Find()
                .Where(a => a.InstanceId == id)
                .FirstAsync();
        }

        public Task<IList<WorkflowInstanceEntity>> ListAllAsync()
        {
            return Db.Find()
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public Task<IList<WorkflowInstanceEntity>> ListByBlockingActivityAsync(string activityType, string correlationId = null)
        {
            var subQuery = DbContext.Set<BlockingActivityEntity>()
                .Find()
                .Where(a => a.ActivityType == activityType)
                .Select(a => a.WorkflowInstance);
            return Db.Find()
                .Where(a => a.Status == WorkflowStatus.Executing)
                .WhereIf(!string.IsNullOrWhiteSpace(correlationId), a => a.CorrelationId == correlationId)
                .Where(a => a.Id, QueryOperator.In, subQuery)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public Task<IList<WorkflowInstanceEntity>> ListByDefinitionIdAsync(string definitionId)
        {
            return Db.Find()
                .Where(a => a.DefinitionId == definitionId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public Task<IList<WorkflowInstanceEntity>> ListByStatusAsync(string definitionId, WorkflowStatus status)
        {
            return Db.Find()
                .Where(a => a.DefinitionId == definitionId && a.Status == status)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public Task<IList<WorkflowInstanceEntity>> ListByStatusAsync(WorkflowStatus status)
        {
            return Db.Find()
                .Where(a => a.Status == status)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}
