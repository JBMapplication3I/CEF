// <copyright file="Created.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace Created class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using System;
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;

    /// <summary>Class for Created.</summary>
    public class Created : ICreated
    {
        /// <summary>Gets or sets the create through.</summary>
        /// <value>The create through.</value>
        [JsonProperty("through")]
        public string? Through { get; set; }

        /// <summary>Gets or sets the created at date.</summary>
        /// <value>The created at date.</value>
        [JsonProperty("at")]
        public DateTime? At { get; set; }

        /// <summary>Gets or sets the created by.</summary>
        /// <value>The created by.</value>
        [JsonProperty("by")]
        public string? By { get; set; }

        /// <summary>Gets or sets the created from IP.</summary>
        /// <value>The created from IP.</value>
        [JsonProperty("from_ip")]
        public string? FromIP { get; set; }
    }
}
