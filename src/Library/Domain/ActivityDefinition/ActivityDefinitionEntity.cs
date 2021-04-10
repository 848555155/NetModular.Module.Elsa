using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Elsa.Domain.ActivityDefinition
{
    /// <summary>
    /// ActivityDefinition
    /// </summary>
    [Table("ActivityDefinition")]
    public partial class ActivityDefinitionEntity : Entity<Guid>
    {
        /// <summary>
        /// ActivityId
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// WorkflowDefinitionVersion
        /// </summary>
        public Guid WorkflowDefinitionVersion { get; set; }

        /// <summary>
        /// Type
        /// </summary>
		[Max]
        public string Type { get; set; }

        /// <summary>
        /// Name
        /// </summary>
		[Max]
        [Nullable]
        public string Name { get; set; }

        /// <summary>
        /// DisplayName
        /// </summary>
		[Max]
        [Nullable]
        public string DisplayName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
		[Max]
        [Nullable]
        public string Description { get; set; }

        /// <summary>
        /// Left
        /// </summary>
        public int Left { get; set; }

        /// <summary>
        /// Top
        /// </summary>
        public int Top { get; set; }

        /// <summary>
        /// State
        /// </summary>
		[Max]
        public string State { get; set; }

    }
}
