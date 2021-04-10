using AutoMapper;
using Elsa.Models;
using NetModular.Module.Elsa.Application.WorkflowInstanceService;
using NetModular.Module.Elsa.Domain.ActivityDefinition;
using NetModular.Module.Elsa.Domain.ConnectionDefinition;
using NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion;
using NetModular.Module.Elsa.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetModular.Module.Elsa.Application.WorkflowDefinitionVersionService
{
    public class WorkflowDefinitionVersionService : IWorkflowDefinitionVersionService
    {
        private readonly IMapper _mapper;
        private readonly IWorkflowDefinitionVersionRepository _repository;
        private readonly IWorkflowInstanceService _workflowInstanceService;
        private readonly IActivityDefinitionRepository _activityDefinitionRepository;
        private readonly IConnectionDefinitionRepository _connectionDefinitionRepository;
        private readonly ElsaDbContext _elsaDbContext;

        public WorkflowDefinitionVersionService(IMapper mapper, IWorkflowDefinitionVersionRepository repository, IWorkflowInstanceService workflowInstanceService, IActivityDefinitionRepository activityDefinitionRepository, IConnectionDefinitionRepository connectionDefinitionRepository, ElsaDbContext elsaDbContext)
        {
            _mapper = mapper;
            _repository = repository;
            _workflowInstanceService = workflowInstanceService;
            _activityDefinitionRepository = activityDefinitionRepository;
            _connectionDefinitionRepository = connectionDefinitionRepository;
            _elsaDbContext = elsaDbContext;
        }

        public async Task<WorkflowDefinitionVersionEntity> GetByVersionId(string versionId)
        {
            var entity = await _repository.GetByVersionIdAsync(versionId);
            if (entity != null)
            {
                entity.Activities = await _activityDefinitionRepository.GetListByWorkflowDefinitionVersionAsync(entity.Id);
                entity.Connections = await _connectionDefinitionRepository.GetListByWorkflowDefinitionVersionAsync(entity.Id);
            }
            return entity;
        }

        public async Task<WorkflowDefinitionVersionEntity> Add(WorkflowDefinitionVersionEntity entity)
        {
            entity.Id = Guid.NewGuid();
            foreach (var item in entity.Activities)
            {
                item.WorkflowDefinitionVersion = entity.Id;
            }
            foreach (var item in entity.Connections)
            {
                item.WorkflowDefinitionVersion = entity.Id;
            }
            using var uow = _elsaDbContext.NewUnitOfWork();
            if (!await _repository.AddAsync(entity, uow))
            {
                throw new Exception("Ìí¼ÓÊ§°Ü");
            }
            if (!await _activityDefinitionRepository.BatchInsertAsync(entity.Activities.ToList(), uow))
            {
                throw new Exception("Ìí¼ÓÊ§°Ü");
            }
            if (!await _connectionDefinitionRepository.BatchInsertAsync(entity.Connections.ToList(), uow))
            {
                throw new Exception("Ìí¼ÓÊ§°Ü");
            }
            uow.Commit();
            return entity;
        }

        public async Task<WorkflowDefinitionVersionEntity> Update(WorkflowDefinitionVersionEntity definition)
        {
            var entity = await GetByVersionId(definition.VersionId);
            var id = entity.Id;
            using var uow = _elsaDbContext.NewUnitOfWork();
            if (!await _activityDefinitionRepository.BatchDeleteByWorkflowDefinitionVersionAsync(id, uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            if (!await _connectionDefinitionRepository.BatchDeleteByWorkflowDefinitionVersionAsync(id, uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            entity = _mapper.Map(definition, entity);
            entity.Id = id;
            foreach (var item in entity.Activities)
            {
                item.WorkflowDefinitionVersion = id;
            }
            foreach (var item in entity.Connections)
            {
                item.WorkflowDefinitionVersion = id;
            }
            if (!await _repository.UpdateAsync(entity, uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            if (!await _activityDefinitionRepository.BatchInsertAsync(entity.Activities.ToList(), uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            if (!await _connectionDefinitionRepository.BatchInsertAsync(entity.Connections.ToList(), uow))
            {
                throw new Exception("¸üÐÂÊ§°Ü");
            }
            uow.Commit();
            return entity;
        }

        public async Task<WorkflowDefinitionVersionEntity> GetByVersionIdAndVersionOptions(string versionId, VersionOptions versions)
        {
            var entity = await _repository.GetByVersionIdAndVersionOptionsAsync(versionId, versions);
            if (entity != null)
            {
                entity.Activities = await _activityDefinitionRepository.GetListByWorkflowDefinitionVersionAsync(entity.Id);
                entity.Connections = await _connectionDefinitionRepository.GetListByWorkflowDefinitionVersionAsync(entity.Id);
            }
            return entity;
        }

        public async Task<IList<WorkflowDefinitionVersionEntity>> ListByVersionOptions(VersionOptions versions)
        {
            var entity = await _repository.ListByVersionOptionsAsync(versions);
            if (!entity.Any())
            {
                return new List<WorkflowDefinitionVersionEntity>();
            }
            var entityIds = entity.Select(a => a.Id).ToList();
            var activities = await _activityDefinitionRepository.GetListByWorkflowDefinitionsVersionAsync(entityIds);
            var connections = await _connectionDefinitionRepository.GetListByWorkflowDefinitionsVersionAsync(entityIds);
            foreach (var item in entity)
            {
                item.Activities = activities.Where(a => a.WorkflowDefinitionVersion == item.Id).ToList();
                item.Connections = connections.Where(a => a.WorkflowDefinitionVersion == item.Id).ToList();
            }
            return entity;
        }

        public async Task<int> Delete(string definitionId)
        {
            var records = await _repository.ListByDefinitionIdAsync(definitionId);
            if (!records.Any())
                return 0;
            using var uow = _elsaDbContext.NewUnitOfWork();
            var recordIds = records.Select(a => a.Id).ToList();
            if (!await _repository.BatchDeleteByDefinitionIdAsync(definitionId))
            {
                throw new Exception("É¾³ýÊ§°Ü");
            }
            if (!await _activityDefinitionRepository.BatchDeleteByWorkflowDefinitionVersionsAsync(recordIds, uow))
            {
                throw new Exception("É¾³ýÊ§°Ü");
            }
            if (!await _connectionDefinitionRepository.BatchDeleteByWorkflowDefinitionVersionsAsync(recordIds, uow))
            {
                throw new Exception("É¾³ýÊ§°Ü");
            }
            if (!await _workflowInstanceService.BatchDelete(definitionId, uow))
            {
                throw new Exception("É¾³ýÊ§°Ü");
            }
            uow.Commit();
            return records.Count;
        }
    }
}
