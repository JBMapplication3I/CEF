// <copyright file="HangfireCounter.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the hangfire counter class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;

    public interface IHangfireCounter : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        string? Key { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        int Value { get; set; }

        /// <summary>Gets or sets the Date/Time of the expire at.</summary>
        /// <value>The expire at.</value>
        DateTime? ExpireAt { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;

    [SqlSchema("Hangfire", "HangfireCounter")]
    public class HangfireCounter : IHangfireCounter
    {
        /// <inheritdoc/>
        [Key, Required, StringLength(100)]
        public string? Key { get; set; }

        /// <inheritdoc/>
        public int Value { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime")]
        public DateTime? ExpireAt { get; set; }
    }
}
