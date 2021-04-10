using Elsa.Models;
using NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Application.WorkflowDefinitionVersionService
{
    /// <summary>
    /// WorkflowDefinitionVersion服务
    /// </summary>
    public interface IWorkflowDefinitionVersionService
    {
        Task<WorkflowDefinitionVersionEntity> GetByVersionId(string versionId);
        Task<WorkflowDefinitionVersionEntity> GetByVersionIdAndVersionOptions(string versionId, VersionOptions versions);
        Task<IList<WorkflowDefinitionVersionEntity>> ListByVersionOptions(VersionOptions versions);
        Task<WorkflowDefinitionVersionEntity> Add(WorkflowDefinitionVersionEntity entity);
        Task<WorkflowDefinitionVersionEntity> Update(WorkflowDefinitionVersionEntity entity);
        Task<int> Delete(string definitionId);
    }
}
