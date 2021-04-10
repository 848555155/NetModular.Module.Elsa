using System;
using Elsa.Models;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Elsa.Domain.WorkflowInstance
{
    /// <summary>
    /// WorkflowInstance
    /// </summary>
    [Table("WorkflowInstance")]
    public partial class WorkflowInstanceEntity : Entity<Guid>
    {
        /// <summary>
        /// InstanceId
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// DefinitionId
        /// </summary>
        public string DefinitionId { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public WorkflowStatus Status { get; set; }

        /// <summary>
        /// CorrelationId
        /// </summary>
        [Nullable]
        public string CorrelationId { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// StartedAt
        /// </summary>
        public DateTime? StartedAt { get; set; }

        /// <summary>
        /// FinishedAt
        /// </summary>
        public DateTime? FinishedAt { get; set; }

        /// <summary>
        /// FaultedAt
        /// </summary>
        public DateTime? FaultedAt { get; set; }

        /// <summary>
        /// AbortedAt
        /// </summary>
        public DateTime? AbortedAt { get; set; }

        /// <summary>
        /// Scope
        /// </summary>
		[Max]
        public string Scope { get; set; }

        /// <summary>
        /// Input
        /// </summary>
		[Max]
        public string Input { get; set; }

        /// <summary>
        /// ExecutionLog
        /// </summary>
		[Max]
        public string ExecutionLog { get; set; }

        /// <summary>
        /// Fault
        /// </summary>
		[Max]
        public string Fault { get; set; }

    }
}
