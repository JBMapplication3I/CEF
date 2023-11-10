// <copyright file="HangfireAggregatedCounter.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire aggregated counter class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;

    public interface IHangfireAggregatedCounter : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        string? Key { get; set; }

        /// <summary>Gets or sets the Date/Time of the expire at.</summary>
        /// <value>The expire at.</value>
        DateTime? ExpireAt { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        long Value { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "AggregatedCounter")]
    public class HangfireAggregatedCounter : IHangfireAggregatedCounter
    {
        [Key, Required, StringLength(100)]
        public string? Key { get; set; }

        public long Value { get; set; }

        [/*Column(TypeName = "datetime"),*/ Index("[IX_HangFire_AggregatedCounter_ExpireAt]", IsClustered = false)]
        public DateTime? ExpireAt { get; set; }
    }
}
