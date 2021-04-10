using AutoMapper;
using Elsa.Models;
using Elsa.Services;
using NetModular.Module.Elsa.Domain.ActivityDefinition;
using NetModular.Module.Elsa.Domain.ActivityInstance;
using NetModular.Module.Elsa.Domain.BlockingActivity;
using NetModular.Module.Elsa.Domain.ConnectionDefinition;
using Elsa.Serialization.Converters;
using NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion;
using NetModular.Module.Elsa.Domain.WorkflowInstance;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodaTime;
using NodaTime.Serialization.JsonNet;
using System.Collections.Generic;
using System.Linq;

namespace NetModular.Module.Elsa.Application.Services
{
    public class EntitiesProfile : MappingProfile
    {
        private readonly JsonSerializerSettings JsonCovertSetting;
        public EntitiesProfile()
        {
            JsonCovertSetting = new JsonSerializerSettings().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
            JsonCovertSetting.Converters.Add(new ExceptionConverter());

            CreateMap<WorkflowDefinitionVersion, WorkflowDefinitionVersionEntity>()
                            .ForMember(d => d.VersionId, d => d.MapFrom(s => s.Id))
                            .ForMember(d => d.Id, d => d.Ignore())
                            .ForMember(d => d.Variables, d => d.MapFrom(s => Serialize(s.Variables)));
            CreateMap<WorkflowDefinitionVersionEntity, WorkflowDefinitionVersion>()
                .ConvertUsing<WorkflowDefinitionVersionConvert>();

            CreateMap<WorkflowInstance, WorkflowInstanceEntity>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.Activities, d => d.ConvertUsing(new ActivityInstanceDictionaryConverter()))
                .ForMember(d => d.InstanceId, d => d.MapFrom(s => s.Id))
                .ForMember(d => d.Scope, d => d.MapFrom(s => Serialize(s.Scope)))
                .ForMember(d => d.ExecutionLog, d => d.MapFrom(s => Serialize(s.ExecutionLog)))
                .ForMember(d => d.Fault, d => d.MapFrom(s => Serialize(s.Fault)))
                .ForMember(d => d.Input, d => d.MapFrom(s => Serialize(s.Input)));

            CreateMap<WorkflowInstanceEntity, WorkflowInstance>()
                .ForMember(d => d.Activities, d => d.ConvertUsing(new ActivityInstanceEntityCollectionConverter()))
                .ForMember(d => d.Id, d => d.MapFrom(s => s.InstanceId))
                .ForMember(d => d.Scope, d => d.MapFrom(s => Deserialize<WorkflowExecutionScope>(s.Scope)))
                .ForMember(d => d.ExecutionLog, d => d.MapFrom(s => Deserialize<ICollection<LogEntry>>(s.ExecutionLog)))
                .ForMember(d => d.Fault, d => d.MapFrom(s => Deserialize<WorkflowFault>(s.Fault)))
                .ForMember(d => d.Input, d => d.MapFrom(s => Deserialize<Variable>(s.Input)));

            CreateMap<ActivityDefinition, ActivityDefinitionEntity>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.ActivityId, d => d.MapFrom(s => s.Id))
                .ForMember(d => d.WorkflowDefinitionVersion, d => d.Ignore())
                .ForMember(d => d.State, d => d.MapFrom(s => Serialize(s.State)));

            CreateMap<ActivityDefinitionEntity, ActivityDefinition>()
                .ConvertUsing(s => new ActivityDefinition(s.ActivityId, s.Type, Deserialize<JObject>(s.State), s.Left, s.Top)
                {
                    Name = s.Name,
                    Description = s.Description,
                    DisplayName = s.DisplayName
                });


            CreateMap<ActivityInstance, ActivityInstanceEntity>()
                  .ForMember(d => d.Id, d => d.Ignore())
                  .ForMember(d => d.ActivityId, d => d.MapFrom(s => s.Id))
                  .ForMember(d => d.WorkflowInstance, d => d.Ignore())
                  .ForMember(d => d.State, d => d.MapFrom(s => Serialize(s.State)))
                  .ForMember(d => d.Output, d => d.MapFrom(s => Serialize(s.Output)));

            CreateMap<ActivityInstanceEntity, ActivityInstance>()
                .ForMember(d => d.Id, d => d.MapFrom(s => s.ActivityId))
                .ForMember(d => d.State, d => d.MapFrom(src => Deserialize<JObject>(src.State)))
                .ForMember(d => d.Output, d => d.MapFrom(src => Deserialize<JObject>(src.Output)));

            CreateMap<BlockingActivity, BlockingActivityEntity>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.WorkflowInstance, d => d.Ignore())
                .ReverseMap();

            CreateMap<ConnectionDefinition, ConnectionDefinitionEntity>()
                .ForMember(d => d.Id, d => d.Ignore())
                .ForMember(d => d.WorkflowDefinitionVersion, d => d.Ignore())
                .ReverseMap();
        }

        public class WorkflowDefinitionVersionConvert : ITypeConverter<WorkflowDefinitionVersionEntity, WorkflowDefinitionVersion>
        {
            // todo 改用System.Text.Json
            public WorkflowDefinitionVersion Convert(WorkflowDefinitionVersionEntity source, WorkflowDefinitionVersion destination, ResolutionContext context)
            {
                return new WorkflowDefinitionVersion(
                    source.VersionId,
                    source.DefinitionId,
                    source.Version,
                    source.Name,
                    source.Description,
                    context.Mapper.Map<IEnumerable<ActivityDefinition>>(source.Activities),
                    context.Mapper.Map<IEnumerable<ConnectionDefinition>>(source.Connections),
                    source.IsSingleton,
                    source.IsDisabled,
                    JsonConvert.DeserializeObject<Variables>(source.Variables, new JsonSerializerSettings().ConfigureForNodaTime(DateTimeZoneProviders.Tzdb))
                    )
                {
                    IsPublished = source.IsPublished,
                    IsLatest = source.IsLatest,
                };
            }
        }


        public class ActivityInstanceEntityCollectionConverter : IValueConverter<IList<ActivityInstanceEntity>, IDictionary<string, ActivityInstance>>
        {
            public IDictionary<string, ActivityInstance> Convert(IList<ActivityInstanceEntity> sourceMember, ResolutionContext context)
            {
                return sourceMember.ToDictionary(x => x.ActivityId, x => context.Mapper.Map<ActivityInstance>(x));
            }
        }

        public class ActivityInstanceDictionaryConverter : IValueConverter<IDictionary<string, ActivityInstance>, IList<ActivityInstanceEntity>>
        {
            public IList<ActivityInstanceEntity> Convert(IDictionary<string, ActivityInstance> sourceMember, ResolutionContext context) =>
                sourceMember.Select(x => context.Mapper.Map<ActivityInstanceEntity>(x.Value)).ToList();
        }

        public class JobjectConverter : IValueConverter<string, JObject>
        {
            public JObject Convert(string sourceMember, ResolutionContext context) => JObject.Parse(sourceMember);
        }

        private string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, JsonCovertSetting);
        }

        private T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JsonCovertSetting);
        }
    }
}
