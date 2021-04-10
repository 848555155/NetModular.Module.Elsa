using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Elsa.Domain.ActivityDefinition;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.SqlServer
{
    public class ActivityDefinitionRepository : RepositoryAbstract<ActivityDefinitionEntity>, IActivityDefinitionRepository
    {
        public ActivityDefinitionRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> BatchDeleteByWorkflowDefinitionVersionAsync(Guid workflowDefinitionVersion, IUnitOfWork uow = default)
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

        public Task<bool> BatchInsertAsync(List<ActivityDefinitionEntity> entities, IUnitOfWork uow = default)
        {
            return Db.BatchInsertAsync(entities, uow: uow);
        }

        public Task<IList<ActivityDefinitionEntity>> GetListByWorkflowDefinitionsVersionAsync(List<Guid> workflowDefinitionVersions)
        {
            return Db.Find()
                .Where(a => workflowDefinitionVersions.Contains(a.WorkflowDefinitionVersion))
                .ToListAsync();
        }

        public Task<IList<ActivityDefinitionEntity>> GetListByWorkflowDefinitionVersionAsync(Guid workflowDefinitionVersion)
        {
            return Db.Find()
                .Where(a => a.WorkflowDefinitionVersion == workflowDefinitionVersion)
                .ToListAsync();
        }
    }
}
