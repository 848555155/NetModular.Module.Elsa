using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;
using NetModular.Module.Elsa.Application.WorkflowDefinitionVersionService;
using NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Application.Services
{
    public class NetModularWorkflowDefinitionStore : IWorkflowDefinitionStore
    {
        private readonly IMapper _mapper;
        private readonly IWorkflowDefinitionVersionService _workflowDefinitionVersionService;
        public NetModularWorkflowDefinitionStore(IMapper mapper, IWorkflowDefinitionVersionService workflowDefinitionVersionService)
        {
            _mapper = mapper;
            _workflowDefinitionVersionService = workflowDefinitionVersionService;
        }

        public async Task<WorkflowDefinitionVersion> SaveAsync(
            WorkflowDefinitionVersion definition, 
            CancellationToken cancellationToken = default)
        {
            var exisitingEntity = await _workflowDefinitionVersionService.GetByVersionId(definition.Id);
            if (exisitingEntity == null)
                return await AddAsync(definition, cancellationToken);
            return await UpdateAsync(definition, cancellationToken);
        }

        public async Task<WorkflowDefinitionVersion> AddAsync(
            WorkflowDefinitionVersion definition, 
            CancellationToken cancellationToken = default)
        {
            var dest = Map(definition);
            var entity = await _workflowDefinitionVersionService.Add(dest);
            var result = Map(entity);
            return result;
        }

        public async Task<WorkflowDefinitionVersion> UpdateAsync(
            WorkflowDefinitionVersion definition, 
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowDefinitionVersionService.Update(Map(definition)));
        }

        public async Task<WorkflowDefinitionVersion> GetByIdAsync(
            string id, 
            VersionOptions version, 
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowDefinitionVersionService.GetByVersionIdAndVersionOptions(id, version));
        }

        public async Task<IEnumerable<WorkflowDefinitionVersion>> ListAsync(
            VersionOptions version, 
            CancellationToken cancellationToken = default)
        {
            var entities = await _workflowDefinitionVersionService.ListByVersionOptions(version);
            var result = _mapper.Map<IEnumerable<WorkflowDefinitionVersion>>(entities);
            return result;
        }

        public Task<int> DeleteAsync(
            string id, 
            CancellationToken cancellationToken = default)
        {
            return _workflowDefinitionVersionService.Delete(id);
        }

        private WorkflowDefinitionVersionEntity Map(WorkflowDefinitionVersion source) => _mapper.Map<WorkflowDefinitionVersionEntity>(source);
        private WorkflowDefinitionVersion Map(WorkflowDefinitionVersionEntity source) => _mapper.Map<WorkflowDefinitionVersion>(source);
    }
}
