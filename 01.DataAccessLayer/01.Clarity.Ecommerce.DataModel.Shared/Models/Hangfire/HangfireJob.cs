// <copyright file="HangfireJob.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire job class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IHangfireJob : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        long Id { get; set; }

        /// <summary>Gets or sets the identifier of the state.</summary>
        /// <value>The identifier of the state.</value>
        long? StateId { get; set; }

        /// <summary>Gets or sets the name of the state.</summary>
        /// <value>The name of the state.</value>
        string? StateName { get; set; }

        /// <summary>Gets or sets information describing the invocation.</summary>
        /// <value>Information describing the invocation.</value>
        string? InvocationData { get; set; }

        /// <summary>Gets or sets the arguments.</summary>
        /// <value>The arguments.</value>
        string? Arguments { get; set; }

        /// <summary>Gets or sets the Date/Time of the created at.</summary>
        /// <value>The created at.</value>
        DateTime CreatedAt { get; set; }

        /// <summary>Gets or sets the Date/Time of the expire at.</summary>
        /// <value>The expire at.</value>
        DateTime? ExpireAt { get; set; }

        /// <summary>Gets or sets the states.</summary>
        /// <value>The states.</value>
        ICollection<HangfireState>? States { get; set; }

        /// <summary>Gets or sets options for controlling the job.</summary>
        /// <value>Options that control the job.</value>
        ICollection<HangfireJobParameter>? JobParameters { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "Job")]
    public class HangfireJob : IHangfireJob
    {
        private ICollection<HangfireJobParameter>? jobParameters;
        private ICollection<HangfireState>? states;

        public HangfireJob()
        {
            jobParameters = new HashSet<HangfireJobParameter>();
            states = new HashSet<HangfireState>();
        }

        /// <inheritdoc/>
        [Key, Index, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <inheritdoc/>
        public long? StateId { get; set; }

        /// <inheritdoc/>
        [StringLength(20), Index]
        public string? StateName { get; set; }

        /// <inheritdoc/>
        [Required]
        public string? InvocationData { get; set; }

        /// <inheritdoc/>
        [Required]
        public string? Arguments { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime"),*/ Index]
        public DateTime? ExpireAt { get; set; }

        #region Associated Objects
        /// <inheritdoc/>
        public virtual ICollection<HangfireJobParameter>? JobParameters { get => jobParameters; set => jobParameters = value; }

        /// <inheritdoc/>
        public virtual ICollection<HangfireState>? States { get => states; set => states = value; }
        #endregion
    }
}
