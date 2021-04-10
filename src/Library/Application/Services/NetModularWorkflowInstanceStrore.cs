using Elsa.Extensions;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Services;
using NetModular.Module.Elsa.Application.WorkflowInstanceService;
using NetModular.Module.Elsa.Domain.WorkflowInstance;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Application.Services
{
    public class NetModularWorkflowInstanceStrore : IWorkflowInstanceStore
    {
        private readonly IMapper _mapper;
        private readonly IWorkflowInstanceService _workflowInstanceService;
        public NetModularWorkflowInstanceStrore(IMapper mapper, IWorkflowInstanceService workflowInstanceService)
        {
            _mapper = mapper;
            _workflowInstanceService = workflowInstanceService;
        }

        public async Task<WorkflowInstance> SaveAsync(
            WorkflowInstance instance,
            CancellationToken cancellationToken = default)
        {
            var exisitingEntity = await _workflowInstanceService.GetById(instance.Id);
            if (exisitingEntity == null)
                return await AddAsync(instance, cancellationToken);
            return await UpdateAsync(instance, cancellationToken);
        }

        public async Task<WorkflowInstance> AddAsync(
            WorkflowInstance definition,
            CancellationToken cancellationToken = default)
        {
            var dest = Map(definition);
            var entity = await _workflowInstanceService.Add(dest);
            var result = Map(entity);
            return result;
        }

        public async Task<WorkflowInstance> UpdateAsync(
            WorkflowInstance definition,
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowInstanceService.Update(Map(definition)));
        }

        public async Task<WorkflowInstance> GetByIdAsync(
            string id,
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowInstanceService.GetById(id));
        }

        public async Task<WorkflowInstance> GetByCorrelationIdAsync(
            string correlationId,
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowInstanceService.GetByCorrelationId(correlationId));
        }

        public async Task<IEnumerable<WorkflowInstance>> ListByDefinitionAsync(
            string definitionId,
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowInstanceService.ListByDefinitionId(definitionId));
        }

        public async Task<IEnumerable<WorkflowInstance>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return Map(await _workflowInstanceService.ListAll());
        }

        public async Task<IEnumerable<(WorkflowInstance, ActivityInstance)>> ListByBlockingActivityAsync(
            string activityType,
            string correlationId = default,
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowInstanceService.ListByBlockingActivity(activityType, correlationId)).GetBlockingActivities(activityType);
        }


        public async Task<IEnumerable<WorkflowInstance>> ListByStatusAsync(
            string definitionId, 
            WorkflowStatus status, 
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowInstanceService.ListByStatus(definitionId, status));
        }

        public async Task<IEnumerable<WorkflowInstance>> ListByStatusAsync(
            WorkflowStatus status, 
            CancellationToken cancellationToken = default)
        {
            return Map(await _workflowInstanceService.ListByStatus(status));
        }

        public async Task DeleteAsync(
            string id, 
            CancellationToken cancellationToken = default)
        {
            await _workflowInstanceService.Delete(id);
        }

        private WorkflowInstanceEntity Map(WorkflowInstance source) => _mapper.Map<WorkflowInstanceEntity>(source);
        private WorkflowInstance Map(WorkflowInstanceEntity source) => _mapper.Map<WorkflowInstance>(source);
        private IEnumerable<WorkflowInstance> Map(IEnumerable<WorkflowInstanceEntity> source) => _mapper.Map<IEnumerable<WorkflowInstance>>(source);
    }
}
