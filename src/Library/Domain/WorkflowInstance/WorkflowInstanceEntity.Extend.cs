using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Module.Elsa.Domain.ActivityInstance;
using NetModular.Module.Elsa.Domain.BlockingActivity;
using System.Collections.Generic;

namespace NetModular.Module.Elsa.Domain.WorkflowInstance
{
    public partial class WorkflowInstanceEntity
    {
        [Ignore]
        public IList<ActivityInstanceEntity> Activities { get; set; }

        [Ignore]
        public IList<BlockingActivityEntity> BlockingActivities { get; set; }
    }
}
