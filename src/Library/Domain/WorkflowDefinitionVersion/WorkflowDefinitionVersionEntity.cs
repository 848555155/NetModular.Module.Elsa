using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion
{
    /// <summary>
    /// WorkflowDefinitionVersion
    /// </summary>
    [Table("WorkflowDefinitionVersion")]
    public partial class WorkflowDefinitionVersionEntity : Entity<Guid>
    {
        /// <summary>
        /// VersionId
        /// </summary>
        public string VersionId { get; set; }

        /// <summary>
        /// DefinitionId
        /// </summary>
        public string DefinitionId { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Name
        /// </summary>
		[Max]
        [Nullable]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
		[Max]
        [Nullable]
        public string Description { get; set; }

        /// <summary>
        /// Variables
        /// </summary>
		[Max]
        public string Variables { get; set; }

        /// <summary>
        /// IsSingleton
        /// </summary>
        public bool IsSingleton { get; set; }

        /// <summary>
        /// IsDisabled
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// IsPublished
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// IsLatest
        /// </summary>
        public bool IsLatest { get; set; }

    }
}
