using Elsa.Models;
using NetModular.Lib.Data.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Domain.WorkflowInstance
{
    /// <summary>
    /// WorkflowInstance仓储
    /// </summary>
    public interface IWorkflowInstanceRepository : IRepository<WorkflowInstanceEntity>
    {
        Task<IList<WorkflowInstanceEntity>> ListByDefinitionIdAsync(string definitionId);
        Task<IList<WorkflowInstanceEntity>> ListByStatusAsync(string definitionId, WorkflowStatus status);
        Task<IList<WorkflowInstanceEntity>> ListByStatusAsync(WorkflowStatus status);
        Task<WorkflowInstanceEntity> GetByCorrelationIdAsync(string correlationId);
        Task<WorkflowInstanceEntity> GetByIdAsync(string id);
        Task<IList<WorkflowInstanceEntity>> ListAllAsync();
        Task<IList<WorkflowInstanceEntity>> ListByBlockingActivityAsync(string activityType, string correlationId = null);
        Task<bool> BatchDeleteByDefinitionIdAsync(string definitionId, IUnitOfWork uow);
    }
}
