// <copyright file="SageAddress.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sage address class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Sage
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>(Serializable) a sage address.</summary>
    [PublicAPI, Serializable]
    public class SageAddress
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [DataMember(Name = "name"), JsonProperty("name"), ApiMember(Name = "name")]
        public SageName? Name { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        [DataMember(Name = "address"), JsonProperty("address"), ApiMember(Name = "address")]
        public string? Address { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [DataMember(Name = "city"), JsonProperty("city"), ApiMember(Name = "city")]
        public string? City { get; set; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        [DataMember(Name = "state"), JsonProperty("state"), ApiMember(Name = "state")]
        public string? State { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        [DataMember(Name = "postalCode"), JsonProperty("postalCode"), ApiMember(Name = "postalCode")]
        public string? PostalCode { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        [DataMember(Name = "country"), JsonProperty("country"), ApiMember(Name = "country")]
        public string? Country { get; set; }
    }
}
