using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Elsa.Domain.BlockingActivity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.SqlServer
{
    public class BlockingActivityRepository : RepositoryAbstract<BlockingActivityEntity>, IBlockingActivityRepository
    {
        public BlockingActivityRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> BatchDeleteByWorkflowInstanceAsync(Guid instanceId, IUnitOfWork uow = null)
        {
            return Db.Find()
                .Where(a => a.WorkflowInstance == instanceId)
                .UseUow(uow)
                .DeleteAsync();
        }

        public Task<bool> BatchDeleteByWorkflowInstancesAsync(List<Guid> instanceIds, IUnitOfWork uow = null)
        {
            return Db.Find()
                .Where(a => instanceIds.Contains(a.WorkflowInstance))
                .UseUow(uow)
                .DeleteAsync();
        }

        public Task<bool> BatchInsertAsync(List<BlockingActivityEntity> entities, IUnitOfWork uow = null)
        {
            return Db.BatchInsertAsync(entities, uow: uow);
        }

        public Task<IList<BlockingActivityEntity>> ListByWorkflowInstanceIdAsync(Guid id)
        {
            return Db.Find()
                .Where(a => a.WorkflowInstance == id)
                .ToListAsync();
        }

        public Task<IList<BlockingActivityEntity>> ListByWorkflowInstanceIdsAsync(List<Guid> ids)
        {
            return Db.Find()
                .Where(a => ids.Contains(a.WorkflowInstance))
                .ToListAsync();
        }
    }
}
