// <copyright file="HangfireState.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire state class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IHangfireState : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        long Id { get; set; }

        /// <summary>Gets or sets the identifier of the job.</summary>
        /// <value>The identifier of the job.</value>
        long JobId { get; set; }

        /// <summary>Gets or sets the Date/Time of the created at.</summary>
        /// <value>The created at.</value>
        DateTime CreatedAt { get; set; }

        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.</value>
        string? Data { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the reason.</summary>
        /// <value>The reason.</value>
        string? Reason { get; set; }

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

    [SqlSchema("Hangfire", "State")]
    public class HangfireState : IHangfireState
    {
        /// <inheritdoc/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <inheritdoc/>
        [Required, StringLength(20)]
        public string? Name { get; set; }

        /// <inheritdoc/>
        [StringLength(100)]
        public string? Reason { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc/>
        public string? Data { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(IBase.ID)), ForeignKey(nameof(HangfireJob))]
        public long JobId { get; set; }

        /// <inheritdoc/>
        public virtual HangfireJob? HangfireJob { get; set; }
    }
}
