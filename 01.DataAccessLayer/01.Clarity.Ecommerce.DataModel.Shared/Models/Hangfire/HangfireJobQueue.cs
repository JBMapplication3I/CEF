// <copyright file="HangfireJobQueue.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire job queue class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IHangfireJobQueue : IHasId, IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the queue.</summary>
        /// <value>The queue.</value>
        string? Queue { get; set; }

        /// <summary>Gets or sets the Date/Time of the fetched at.</summary>
        /// <value>The fetched at.</value>
        DateTime? FetchedAt { get; set; }

        /// <summary>Gets or sets the identifier of the job.</summary>
        /// <value>The identifier of the job.</value>
        long JobId { get; set; }

        /// <summary>Gets or sets the hangfire job.</summary>
        /// <value>The hangfire job.</value>
        HangfireJob? HangfireJob { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "JobQueue")]
    public class HangfireJobQueue : IHangfireJobQueue
    {
        /// <inheritdoc/>
        [Required, StringLength(50),
         Index("IX_HangFire_JobQueue_QueueAndId", order: 0)]
        public string? Queue { get; set; }

        /// <inheritdoc/>
        [Key, Index, DatabaseGenerated(DatabaseGeneratedOption.Identity),
         Index("IX_HangFire_JobQueue_QueueAndId", order: 1)]
        public int Id { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime")]
        public DateTime? FetchedAt { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(IBase.ID)), ForeignKey(nameof(HangfireJob))]
        public long JobId { get; set; }

        /// <inheritdoc/>
        public virtual HangfireJob? HangfireJob { get; set; }
    }
}
