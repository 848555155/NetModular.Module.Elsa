using Elsa.Models;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.SqlQueryable;
using NetModular.Lib.Data.Core;
using NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Module.Elsa.Infrastructure.Extensions;
namespace NetModular.Module.Elsa.Infrastructure.Repositories.SqlServer
{
    public class WorkflowDefinitionVersionRepository : RepositoryAbstract<WorkflowDefinitionVersionEntity>, IWorkflowDefinitionVersionRepository
    {
        public WorkflowDefinitionVersionRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> BatchDeleteByDefinitionIdAsync(string definitionId, IUnitOfWork uow = default)
        {
            return Db.Find()
                .Where(a => a.DefinitionId == definitionId)
                .UseUow(uow)
                .DeleteAsync();
        }

        public Task<WorkflowDefinitionVersionEntity> GetByVersionIdAndVersionOptionsAsync(string definitionId, VersionOptions versions)
        {
            return Db.Find()
                .Where(a => a.DefinitionId == definitionId)
                .WithVersion(versions)
                .FirstAsync();
        }

        public Task<WorkflowDefinitionVersionEntity> GetByVersionIdAsync(string versionId)
        {
            return Db.Find()
                .Where(a => a.VersionId == versionId)
                .FirstAsync();
        }

        public Task<IList<WorkflowDefinitionVersionEntity>> ListByDefinitionIdAsync(string definitionId)
        {
            return Db.Find()
                .Where(a => a.DefinitionId == definitionId)
                .ToListAsync();
        }

        public Task<IList<WorkflowDefinitionVersionEntity>> ListByVersionOptionsAsync(VersionOptions versions)
        {
            return Db.Find()
                .WithVersion(versions)
                .ToListAsync();
        }
    }
}
