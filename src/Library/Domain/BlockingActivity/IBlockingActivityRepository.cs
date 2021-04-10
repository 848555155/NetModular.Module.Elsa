using NetModular.Lib.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Domain.BlockingActivity
{
    /// <summary>
    /// BlockingActivity仓储
    /// </summary>
    public interface IBlockingActivityRepository : IRepository<BlockingActivityEntity>
    {
        Task<IList<BlockingActivityEntity>> ListByWorkflowInstanceIdsAsync(List<Guid> ids);
        Task<IList<BlockingActivityEntity>> ListByWorkflowInstanceIdAsync(Guid id);
        Task<bool> BatchInsertAsync(List<BlockingActivityEntity> entities, IUnitOfWork uow = default);
        Task<bool> BatchDeleteByWorkflowInstanceAsync(Guid instanceId, IUnitOfWork uow = default);
        Task<bool> BatchDeleteByWorkflowInstancesAsync(List<Guid> instanceIds, IUnitOfWork uow = default);
    }
}
