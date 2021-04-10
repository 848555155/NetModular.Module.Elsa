using Elsa.Models;
using NetModular.Lib.Data.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion
{
    /// <summary>
    /// WorkflowDefinitionVersion仓储
    /// </summary>
    public interface IWorkflowDefinitionVersionRepository : IRepository<WorkflowDefinitionVersionEntity>
    {
        Task<WorkflowDefinitionVersionEntity> GetByVersionIdAsync(string versionId);
        Task<WorkflowDefinitionVersionEntity> GetByVersionIdAndVersionOptionsAsync(string definitionId, VersionOptions versions);
        Task<IList<WorkflowDefinitionVersionEntity>> ListByVersionOptionsAsync(VersionOptions versions);
        Task<IList<WorkflowDefinitionVersionEntity>> ListByDefinitionIdAsync(string definitionId);
        Task<bool> BatchDeleteByDefinitionIdAsync(string definitionId, IUnitOfWork uow = default);
    }
}
