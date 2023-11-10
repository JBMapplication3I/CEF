// <copyright file="Address.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace Address class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;

    /// <summary>Class for address.</summary>
    public class Address : IAddress
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>Gets or sets the street address.</summary>
        /// <value>The street address.</value>
        [JsonProperty("street_address")]
        public string? StreetAddress { get; set; }

        /// <summary>Gets or sets the street address 2.</summary>
        /// <value>The street address 2.</value>
        [JsonProperty("street_address2")]
        public string? StreetAddress2 { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [JsonProperty("city")]
        public string? City { get; set; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        [JsonProperty("state")]
        public string? State { get; set; }

        /// <summary>Gets or sets the zip.</summary>
        /// <value>The zip.</value>
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        [JsonProperty("country")]
        public string? Country { get; set; }
    }
}
