using AutoMapper;
using Elsa.Models;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Elsa.Domain.ActivityInstance;
using NetModular.Module.Elsa.Domain.BlockingActivity;
using NetModular.Module.Elsa.Domain.WorkflowInstance;
using NetModular.Module.Elsa.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Application.WorkflowInstanceService
{
    public class WorkflowInstanceService : IWorkflowInstanceService
    {
        private readonly IMapper _mapper;
        private readonly IWorkflowInstanceRepository _repository;
        private readonly IActivityInstanceRepository _activityInstanceRepository;
        private readonly IBlockingActivityRepository _blockingActivityRepository;
        private readonly ElsaDbContext _elsaDbContext;
        public WorkflowInstanceService(IMapper mapper, IWorkflowInstanceRepository repository, IActivityInstanceRepository activityInstanceRepository, IBlockingActivityRepository blockingActivityRepository, ElsaDbContext elsaDbContext)
        {
            _mapper = mapper;
            _repository = repository;
            _activityInstanceRepository = activityInstanceRepository;
            _blockingActivityRepository = blockingActivityRepository;
            _elsaDbContext = elsaDbContext;
        }

        public async Task<WorkflowInstanceEntity> Add(WorkflowInstanceEntity entity)
        {
            entity.Id = Guid.NewGuid();
            foreach (var item in entity.Activities)
            {
                item.WorkflowInstance = entity.Id;
            }
            foreach (var item in entity.BlockingActivities)
            {
                item.WorkflowInstance = entity.Id;
            }
            using var uow = _elsaDbContext.NewUnitOfWork();
            if (!await _repository.AddAsync(entity, uow))
            {
                throw new Exception("Ìí¼ÓÊ§°Ü");
            }
            if (entity.Activities.Any())
            {
                if (!await _activityInstanceRepository.BatchInsertAsync(entity.Activities.ToList(), uow))
                {
                    throw new Exception("Ìí¼ÓÊ§°Ü");
                }
            }
            if (entity.BlockingActivities.Any())
            {
                if (!await _blockingActivityRepository.BatchInsertAsync(entity.BlockingActivities.ToList(), uow))
                {
                    throw new Exception("Ìí¼ÓÊ§°Ü");
                }
            }
            uow.Commit();
            return entity;
        }

        public async Task<bool> BatchDelete(string definitionId, IUnitOfWork uow = default)
        {
            var nouow = uow == default;
            if (nouow)
                uow = _elsaDbContext.NewUnitOfWork();

            var instances = await ListByDefinitionId(definitionId);
            if (instances.Any())
            {
                var instanceIds = instances.Select(a => a.Id).ToList();
                if (!await _repository.BatchDeleteByDefinitionIdAsync(definitionId, uow))
                {
                    throw new Exception("É¾³ýÊ§°Ü");
                }
                if (!await _activityInstanceRepository.BatchDeleteByWorkflowInstancesAsync(instanceIds, uow))
                {
                    throw new Exception("É¾³ýÊ§°Ü");
                }
                if (!await _blockingActivityRepository.BatchDeleteByWorkflowInstancesAsync(instanceIds, uow))
                {
                    throw new Exception("É¾³ýÊ§°Ü");
                }
            }
            if (nouow)
                uow.Commit();
            return true;
        }

        public async Task Delete(string id)
        {
            var record = await GetById(id);
            if (record == null)
                return;
            using var uow = _elsaDbContext.NewUnitOfWork();
            if (!await _repository.DeleteAsync(record.Id, uow))
            {
                throw new Exception("É¾³ýÊ§°Ü");
            }
            if (!await _activityInstanceRepository.BatchDeleteByWorkflowInstanceAsync(record.Id, uow))
            {
                throw new Exception("É¾³ýÊ§°Ü");
            }
            if (!await _blockingActivityRepository.BatchDeleteByWorkflowInstanceAsync(record.Id, uow))
            {
                throw new Exception("É¾³ýÊ§°Ü");
            }
            uow.Commit();
        }

        public async Task<WorkflowInstanceEntity> GetByCorrelationId(string correlationId)
        {
            var result = await _repository.GetByCorrelationIdAsync(correlationId);
            if (result == null)
            {
                return result;
            }
            result.Activities = await _activityInstanceRepository.ListByWorkflowInstanceIdAsync(result.Id);
            result.BlockingActivities = await _blockingActivityRepository.ListByWorkflowInstanceIdAsync(result.Id);
            return result;
        }

        public async Task<WorkflowInstanceEntity> GetById(string id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
            {
                return result;
            }
            result.Activities = await _activityInstanceRepository.ListByWorkflowInstanceIdAsync(result.Id);
            result.BlockingActivities = await _blockingActivityRepository.ListByWorkflowInstanceIdAsync(result.Id);
            return result;
        }

        public async Task<IList<WorkflowInstanceEntity>> ListAll()
        {
            var result = await _repository.ListAllAsync();
            if (!result.Any())
            {
                return new List<WorkflowInstanceEntity>();
            }
            var ids = result.Select(a => a.Id).ToList();
            var activities = await _activityInstanceRepository.ListByWorkflowInstanceIdsAsync(ids);
            var blockingActivities = await _blockingActivityRepository.ListByWorkflowInstanceIdsAsync(ids);
            foreach (var item in result)
            {
                item.Activities = activities.Where(a => a.WorkflowInstance == item.Id).ToList();
                item.BlockingActivities = blockingActivities.Where(a => a.WorkflowInstance == item.Id).ToList();
            }
            return result;
        }

        public async Task<IList<WorkflowInstanceEntity>> ListByBlockingActivity(string activityType, string correlationId = null)
        {
            var result = await _repository.ListByBlockingActivityAsync(activityType, correlationId);
            if (!result.Any())
            {
                return new List<WorkflowInstanceEntity>();
            }
            var ids = result.Select(a => a.Id).ToList();
            var activities = await _activityInstanceRepository.ListByWorkflowInstanceIdsAsync(ids);
            var blockingActivities = await _blockingActivityRepository.ListByWorkflowInstanceIdsAsync(ids);
            foreach (var item in result)
            {
                item.Activities = activities.Where(a => a.WorkflowInstance == item.Id).ToList();
                item.BlockingActivities = blockingActivities.Where(a => a.WorkflowInstance == item.Id).ToList();
            }
            return result;
        }

        public async Task<IList<WorkflowInstanceEntity>> ListByDefinitionId(string definitionId)
        {
            var result = await _repository.ListByDefinitionIdAsync(definitionId);
            if (!result.Any())
            {
                return new List<WorkflowInstanceEntity>();
            }
            var ids = result.Select(a => a.Id).ToList();
            var activities = await _activityInstanceRepository.ListByWorkflowInstanceIdsAsync(ids);
            var blockingActivities = await _blockingActivityRepository.ListByWorkflowInstanceIdsAsync(ids);
            foreach (var item in result)
            {
                item.Activities = activities.Where(a => a.WorkflowInstance == item.Id).ToList();
                item.BlockingActivities = blockingActivities.Where(a => a.WorkflowInstance == item.Id).ToList();
            }
            return result;
        }

        public async Task<IList<WorkflowInstanceEntity>> ListByStatus(WorkflowStatus status)
        {
            var result = await _repository.ListByStatusAsync(status);
            if (!result.Any())
            {
                return new List<WorkflowInstanceEntity>();
            }
            var ids = result.Select(a => a.Id).ToList();
            var activities = await _activityInstanceRepository.ListByWorkflowInstanceIdsAsync(ids);
            var blockingActivities = await _blockingActivityRepository.ListByWorkflowInstanceIdsAsync(ids);
            foreach (var item in result)
            {
                item.Activities = activities.Where(a => a.WorkflowInstance == item.Id).ToList();
                item.BlockingActivities = blockingActivities.Where(a => a.WorkflowInstance == item.Id).ToList();
            }
            return result;
        }

        public async Task<IList<WorkflowInstanceEntity>> ListByStatus(string definitionId, WorkflowStatus status)
        {
            var result = await _repository.ListByStatusAsync(definitionId, status);
            if (!result.Any())
            {
                return new List<WorkflowInstanceEntity>();
            }
            var ids = result.Select(a => a.Id).ToList();
            var activities = await _activityInstanceRepository.ListByWorkflowInstanceIdsAsync(ids);
            var blockingActivities = await _blockingActivityRepository.ListByWorkflowInstanceIdsAsync(ids);
            foreach (var item in result)
            {
                item.Activities = activities.Where(a => a.WorkflowInstance == item.Id).ToList();
                item.BlockingActivities = blockingActivities.Where(a => a.WorkflowInstance == item.Id).ToList();
            }
            return result;
        }

        public async Task<WorkflowInstanceEntity> Update(WorkflowInstanceEntity instance)
        {
            var entity = await GetById(instance.InstanceId);
            var id = entity.Id;
            using var uow = _elsaDbContext.NewUnitOfWork();
            if (!await _activityInstanceRepository.BatchDeleteByWorkflowInstanceAsync(id, uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            if (!await _blockingActivityRepository.BatchDeleteByWorkflowInstanceAsync(id, uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            entity = _mapper.Map(instance, entity);
            entity.Id = id;
            foreach (var item in entity.Activities)
            {
                item.WorkflowInstance = id;
            }
            foreach (var item in entity.BlockingActivities)
            {
                item.WorkflowInstance = id;
            }
            if (!await _repository.UpdateAsync(entity, uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            if (!await _activityInstanceRepository.BatchInsertAsync(entity.Activities.ToList(), uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            if (!await _blockingActivityRepository.BatchInsertAsync(entity.BlockingActivities.ToList(), uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            uow.Commit();
            return entity;
        }
    }
}
