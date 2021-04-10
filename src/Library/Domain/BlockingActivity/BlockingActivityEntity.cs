using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Elsa.Domain.BlockingActivity
{
    /// <summary>
    /// BlockingActivity
    /// </summary>
    [Table("BlockingActivity")]
    public partial class BlockingActivityEntity : Entity<Guid>
    {
        /// <summary>
        /// WorkflowInstance
        /// </summary>
        public Guid WorkflowInstance { get; set; }

        /// <summary>
        /// ActivityId
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// ActivityType
        /// </summary>
        public string ActivityType { get; set; }

    }
}
