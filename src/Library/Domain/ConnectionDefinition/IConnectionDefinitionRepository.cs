using NetModular.Lib.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Domain.ConnectionDefinition
{
    /// <summary>
    /// ConnectionDefinition仓储
    /// </summary>
    public interface IConnectionDefinitionRepository : IRepository<ConnectionDefinitionEntity>
    {
        Task<IList<ConnectionDefinitionEntity>> GetListByWorkflowDefinitionVersionAsync(Guid workflowDefinitionVersion);
        Task<IList<ConnectionDefinitionEntity>> GetListByWorkflowDefinitionsVersionAsync(List<Guid> workflowDefinitionVersion);
        Task<bool> BatchInsertAsync(List<ConnectionDefinitionEntity> entities, IUnitOfWork uow = default);
        Task<bool> BatchDeleteByWorkflowDefinitionVersionAsync(Guid workflowDefinitionVersion, IUnitOfWork uow = default);
        Task<bool> BatchDeleteByWorkflowDefinitionVersionsAsync(List<Guid> workflowDefinitionVersionIds, IUnitOfWork uow = default);
    }
}
