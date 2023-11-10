// <copyright file="CreateAccountResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create account result class</summary>
#pragma warning disable 1591
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    /// <summary>Encapsulates the result of a create account.</summary>
    [PublicAPI]
    [DataContract]
    public class CreateAccountResult
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("account_id"), DataMember(Name = "account_id")]
        public long AccountID { get; set; } // 290465

        /// <summary>Gets or sets the company.</summary>
        /// <value>The company.</value>
        [JsonProperty("company"), DataMember(Name = "company")]
        public string? Company { get; set; } // "ACME Inc."

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        [JsonProperty("address"), DataMember(Name = "address")]
        public string? Address { get; set; } // "123 Main St."

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [JsonProperty("city"), DataMember(Name = "city")]
        public string? City { get; set; } // "Any town"

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        [JsonProperty("state"), DataMember(Name = "state")]
        public string? State { get; set; } // "CA"

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        [JsonProperty("postal_code"), DataMember(Name = "postal_code")]
        public string? PostalCode { get; set; } // "12345"

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        [JsonProperty("country"), DataMember(Name = "country")]
        public string? Country { get; set; } // "us"

        /// <summary>Gets or sets the Date/Time of the created.</summary>
        /// <value>The created.</value>
        [JsonProperty("created"), DataMember(Name = "created")]
        public DateTime Created { get; set; } // "2014-01-28T17:31:25+00:00"

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri"), DataMember(Name = "uri")]
        public string? Uri { get; set; } // "/accounts/290465"
    }
}
