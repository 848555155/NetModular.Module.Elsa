using Elsa.Models;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Elsa.Domain.WorkflowInstance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Application.WorkflowInstanceService
{
    /// <summary>
    /// WorkflowInstance服务
    /// </summary>
    public interface IWorkflowInstanceService
    {
        Task<IList<WorkflowInstanceEntity>> ListByDefinitionId(string definitionId);
        Task<IList<WorkflowInstanceEntity>> ListByStatus(WorkflowStatus status);
        Task<IList<WorkflowInstanceEntity>> ListByStatus(string definitionId, WorkflowStatus status);
        Task<IList<WorkflowInstanceEntity>> ListByBlockingActivity(string activityType, string correlationId = default);
        Task<IList<WorkflowInstanceEntity>> ListAll();
        Task<WorkflowInstanceEntity> GetByCorrelationId(string correlationId);
        Task<WorkflowInstanceEntity> GetById(string id);
        Task<WorkflowInstanceEntity> Add(WorkflowInstanceEntity entity);
        Task<WorkflowInstanceEntity> Update(WorkflowInstanceEntity entity);
        Task Delete(string id);
        Task<bool> BatchDelete(string definitionId, IUnitOfWork uow = default);
    }
}
