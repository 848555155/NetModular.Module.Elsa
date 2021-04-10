using NetModular.Lib.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Domain.ActivityDefinition
{
    /// <summary>
    /// ActivityDefinition仓储
    /// </summary>
    public interface IActivityDefinitionRepository : IRepository<ActivityDefinitionEntity>
    {
        Task<IList<ActivityDefinitionEntity>> GetListByWorkflowDefinitionVersionAsync(Guid workflowDefinitionVersion);
        Task<IList<ActivityDefinitionEntity>> GetListByWorkflowDefinitionsVersionAsync(List<Guid> workflowDefinitionVersion);
        Task<bool> BatchInsertAsync(List<ActivityDefinitionEntity> entities, IUnitOfWork uow = default);
        Task<bool> BatchDeleteByWorkflowDefinitionVersionAsync(Guid workflowDefinitionVersion, IUnitOfWork uow = default);
        Task<bool> BatchDeleteByWorkflowDefinitionVersionsAsync(List<Guid> workflowDefinitionVersionIds, IUnitOfWork uow = default);
    }
}
