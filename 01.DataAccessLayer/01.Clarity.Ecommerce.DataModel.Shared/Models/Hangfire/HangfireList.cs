// <copyright file="HangfireList.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire list class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;

    public interface IHangfireList : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        long Id { get; set; }

        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        string? Key { get; set; }

        /// <summary>Gets or sets the Date/Time of the expire at.</summary>
        /// <value>The expire at.</value>
        DateTime? ExpireAt { get; set; }

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

    [SqlSchema("Hangfire", "List")]
    public class HangfireList : IHangfireList
    {
        /// <inheritdoc/>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <inheritdoc/>
        [Required, StringLength(100)]
        public string? Key { get; set; }

        /// <inheritdoc/>
        public string? Value { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime"),*/ Index]
        public DateTime? ExpireAt { get; set; }
    }
}
