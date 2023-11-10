// <copyright file="PayoneerError.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payoneer error class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>A payoneer error.</summary>
    [DataContract]
    internal class PayoneerError
    {
        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        [JsonProperty("Status")]
        [DataMember(Name = "Status")]
        public string? Status { get; set; } // "error"

        /// <summary>Gets or sets the errors.</summary>
        /// <value>The errors.</value>
        [JsonProperty("Errors")]
        [DataMember(Name = "Errors")]
        public Dictionary<string, object>? Errors { get; set; } // { "authentication": "Unrecognized API key", "uri: ["Unknown URI"] }
    }
}
