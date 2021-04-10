using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Elsa.Domain.ActivityInstance
{
    /// <summary>
    /// ActivityInstance
    /// </summary>
    [Table("ActivityInstance")]
    public partial class ActivityInstanceEntity : Entity<Guid>
    {
        /// <summary>
        /// ActivityId
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// WorkflowInstance
        /// </summary>
        public Guid WorkflowInstance { get; set; }

        /// <summary>
        /// Type
        /// </summary>
		[Max]
        public string Type { get; set; }

        /// <summary>
        /// State
        /// </summary>
		[Max]
        public string State { get; set; }

        /// <summary>
        /// Output
        /// </summary>
		[Max]
        public string Output { get; set; }

    }
}
