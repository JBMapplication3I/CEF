// <copyright file="HangfireHash.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire hash class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;

    public interface IHangfireHash : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        string? Key { get; set; }

        /// <summary>Gets or sets the Date/Time of the expire at.</summary>
        /// <value>The expire at.</value>
        DateTime? ExpireAt { get; set; }

        /// <summary>Gets or sets the field.</summary>
        /// <value>The field.</value>
        string? Field { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string? Value { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "Hash")]
    public class HangfireHash : IHangfireHash
    {
        /// <inheritdoc/>
        [Key, Column(Order = 0), Required, StringLength(100),
         Index(name: "UX_HangFire_Hash_Key_Field", order: 0, IsClustered = false, IsUnique = true),
         Index(name: "IX_HangFire_Hash_Key")]
        public string? Key { get; set; }

        /// <inheritdoc/>
        [Key, Column(Order = 1), Required, StringLength(100),
         Index(name: "UX_HangFire_Hash_Key_Field", order: 1, IsClustered = false, IsUnique = true)]
        public string? Field { get; set; }

        /// <inheritdoc/>
        [Column(Order = 2)]
        public string? Value { get; set; }

        /// <inheritdoc/>
        [Column(Order = 3/*, TypeName = "datetime2"*/), ////DateTimePrecision(7),
         Index(name: "IX_HangFire_Hash_ExpireAt")]
        public DateTime? ExpireAt { get; set; }
    }
}
