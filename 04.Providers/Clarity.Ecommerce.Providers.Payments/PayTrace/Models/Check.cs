// <copyright file="Check.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace Check class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>Class for Check.</summary>
    public class Check
    {
        /// <summary>Gets or sets the Account Number.</summary>
        /// <value>The Account Number.</value>
        [JsonProperty("account_number")]
        public int AccountNumber { get; set; }

        /// <summary>Gets or sets the Routing Number.</summary>
        /// <value>The Routing Number.</value>
        [JsonProperty("routing_number")]
        public int RoutingNumber { get; set; }
    }
}
