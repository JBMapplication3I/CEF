// <copyright file="YRCResponseCustomer.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseCustomer class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response customer.</summary>
    [JsonObject("customer")]
    public class YRCResponseCustomer
    {
        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        [JsonProperty("role")]
        public string? Role { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        [JsonProperty("address")]
        public string? Address { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [JsonProperty("city")]
        public string? City { get; set; }

        /// <summary>Gets or sets the state postal code.</summary>
        /// <value>The state postal code.</value>
        [JsonProperty("statePostalCode")]
        public string? StatePostalCode { get; set; }

        /// <summary>Gets or sets the zip code.</summary>
        /// <value>The zip code.</value>
        [JsonProperty("zipCode")]
        public string? ZipCode { get; set; }

        /// <summary>Gets or sets the nation code.</summary>
        /// <value>The nation code.</value>
        [JsonProperty("nationCode")]
        public string? NationCode { get; set; }

        /// <summary>Gets or sets the terminal.</summary>
        /// <value>The terminal.</value>
        [JsonProperty("terminal")]
        public string? Terminal { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        [JsonProperty("account")]
        public string? Account { get; set; }

        /// <summary>Gets or sets the identifier of the bus.</summary>
        /// <value>The identifier of the bus.</value>
        [JsonProperty("busId")]
        public string? BusId { get; set; }

        /// <summary>Gets or sets the location credit indicator.</summary>
        /// <value>The location credit indicator.</value>
        [JsonProperty("locationCreditIndicator")]
        public string? LocationCreditIndicator { get; set; }
    }
}
