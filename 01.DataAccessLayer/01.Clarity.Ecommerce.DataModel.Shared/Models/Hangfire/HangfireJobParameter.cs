// <copyright file="HangfireJobParameter.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Hangfire job parameter class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IHangfireJobParameter : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier of the job.</summary>
        /// <value>The identifier of the job.</value>
        long JobId { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string? Value { get; set; }

        /// <summary>Gets or sets the hangfire job.</summary>
        /// <value>The hangfire job.</value>
        HangfireJob? HangfireJob { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "JobParameter")]
    public class HangfireJobParameter : IHangfireJobParameter
    {
        /// <inheritdoc/>
        [Key, Column(Order = 0), InverseProperty(nameof(IBase.ID)), ForeignKey(nameof(HangfireJob)),
         Index("IX_HangFire_JobParameter_JobIdAndName", order: 0)]
        public long JobId { get; set; }

        /// <inheritdoc/>
        [Key, Column(Order = 1), Required, StringLength(40),
         Index("IX_HangFire_JobParameter_JobIdAndName", order: 1)]
        public string? Name { get; set; }

        /// <inheritdoc/>
        [Column(Order = 2)]
        public string? Value { get; set; }

        /// <inheritdoc/>
        public virtual HangfireJob? HangfireJob { get; set; }
    }
}
