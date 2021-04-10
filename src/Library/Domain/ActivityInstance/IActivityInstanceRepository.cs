using NetModular.Lib.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Domain.ActivityInstance
{
    /// <summary>
    /// ActivityInstance仓储
    /// </summary>
    public interface IActivityInstanceRepository : IRepository<ActivityInstanceEntity>
    {
        Task<IList<ActivityInstanceEntity>> ListByWorkflowInstanceIdsAsync(List<Guid> ids);
        Task<IList<ActivityInstanceEntity>> ListByWorkflowInstanceIdAsync(Guid id);
        Task<bool> BatchInsertAsync(List<ActivityInstanceEntity> entities, IUnitOfWork uow = default);
        Task<bool> BatchDeleteByWorkflowInstanceAsync(Guid instanceId, IUnitOfWork uow = default);
        Task<bool> BatchDeleteByWorkflowInstancesAsync(List<Guid> instanceIds, IUnitOfWork uow = default);
    }
}
