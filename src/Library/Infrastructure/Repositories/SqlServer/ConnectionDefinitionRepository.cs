using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Elsa.Domain.ConnectionDefinition;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.SqlServer
{
    public class ConnectionDefinitionRepository : RepositoryAbstract<ConnectionDefinitionEntity>, IConnectionDefinitionRepository
    {
        public ConnectionDefinitionRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> BatchDeleteByWorkflowDefinitionVersionAsync(Guid workflowDefinitionVersion, IUnitOfWork uow)
        {
            return Db.Find()
                .Where(a => a.WorkflowDefinitionVersion == workflowDefinitionVersion)
                .UseUow(uow)
                .DeleteAsync();
        }

        public Task<bool> BatchDeleteByWorkflowDefinitionVersionsAsync(List<Guid> workflowDefinitionVersionIds, IUnitOfWork uow = null)
        {
            return Db.Find()
                .Where(a => workflowDefinitionVersionIds.Contains(a.WorkflowDefinitionVersion))
                .UseUow(uow)
                .DeleteAsync();
        }

        public Task<bool> BatchInsertAsync(List<ConnectionDefinitionEntity> entities, IUnitOfWork uow)
        {
            return Db.BatchInsertAsync(entities, uow: uow);
        }

        public Task<IList<ConnectionDefinitionEntity>> GetListByWorkflowDefinitionVersionAsync(Guid workflowDefinitionVersion)
        {
            return Db.Find()
                .Where(a => a.WorkflowDefinitionVersion == workflowDefinitionVersion)
                .ToListAsync();
        }

        public Task<IList<ConnectionDefinitionEntity>> GetListByWorkflowDefinitionsVersionAsync(List<Guid> workflowDefinitionVersions)
        {
            return Db.Find()
                .Where(a => workflowDefinitionVersions.Contains(a.WorkflowDefinitionVersion))
                .ToListAsync();
        }
    }
}
