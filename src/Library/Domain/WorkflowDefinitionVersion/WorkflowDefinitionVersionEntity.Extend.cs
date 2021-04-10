using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Module.Elsa.Domain.ActivityDefinition;
using NetModular.Module.Elsa.Domain.ConnectionDefinition;
using System.Collections.Generic;

namespace NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion
{
    public partial class WorkflowDefinitionVersionEntity
    {
        [Ignore]
        public IList<ActivityDefinitionEntity> Activities { get; set; }

        [Ignore]
        public IList<ConnectionDefinitionEntity> Connections { get; set; }
    }
}
