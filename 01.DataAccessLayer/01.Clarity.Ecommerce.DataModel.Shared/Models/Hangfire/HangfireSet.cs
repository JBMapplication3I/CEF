// <copyright file="HangfireSet.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire set class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;

    public interface IHangfireSet : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        string? Key { get; set; }

        /// <summary>Gets or sets the score.</summary>
        /// <value>The score.</value>
#if ORACLE
        decimal Score { get; set; }
#else
        float Score { get; set; }
#endif

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string? Value { get; set; }

        /// <summary>Gets or sets the Date/Time of the expire at.</summary>
        /// <value>The expire at.</value>
        DateTime? ExpireAt { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "Set")]
    public class HangfireSet : IHangfireSet
    {
        /// <inheritdoc/>
        [Key, Column(Order = 0), Required, StringLength(100), Index,
         Index("UX_HangFire_Set_KeyAndValue", order: 0),
         Index("IX_HangFire_Set_Score", order: 0)]
        public string? Key { get; set; }

        /// <inheritdoc/>
        [Column(Order = 1/*, TypeName = "float"*/),
         Index("IX_HangFire_Set_Score", order: 1)]
#if ORACLE
        public decimal Score { get; set; }
#else
        public float Score { get; set; }
#endif

        /// <inheritdoc/>
        [Key, Column(Order = 2), Required, StringLength(256),
         Index("UX_HangFire_Set_KeyAndValue", order: 1)]
        public string? Value { get; set; }

        /// <inheritdoc/>
        [Column(Order = 3/*, TypeName = "datetime"*/), Index]
        public DateTime? ExpireAt { get; set; }
    }
}
