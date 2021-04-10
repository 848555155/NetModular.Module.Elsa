using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Elsa.Domain.ConnectionDefinition
{
    /// <summary>
    /// ConnectionDefinition
    /// </summary>
    [Table("ConnectionDefinition")]
    public partial class ConnectionDefinitionEntity : Entity<Guid>
    {
        /// <summary>
        /// WorkflowDefinitionVersion
        /// </summary>
        public Guid WorkflowDefinitionVersion { get; set; }

        /// <summary>
        /// SourceActivityId
        /// </summary>
        public string SourceActivityId { get; set; }

        /// <summary>
        /// DestinationActivityId
        /// </summary>
        public string DestinationActivityId { get; set; }

        /// <summary>
        /// Outcome
        /// </summary>
		[Max]
        public string Outcome { get; set; }

    }
}
