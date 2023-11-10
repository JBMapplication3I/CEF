// <copyright file="UserResult.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user result class</summary>
#pragma warning disable 1591
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    /// <summary>Encapsulates the result of an user.</summary>
    [PublicAPI]
    [DataContract]
    public class UserResult
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [JsonProperty("user_id"), DataMember(Name = "user_id")]
        public string? UserID { get; set; } // 17792

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("account_id"), DataMember(Name = "account_id")]
        public string? AccountID { get; set; } // 290465

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [JsonProperty("name"), DataMember(Name = "name")]
        public string? Name { get; set; } // "John Doe"

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [JsonProperty("email"), DataMember(Name = "email")]
        public string? Email { get; set; } // "j.doe@example.com"

        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        [JsonProperty("phone"), DataMember(Name = "phone")]
        public string? Phone { get; set; } // "+1 800-555-1234"

        /// <summary>Gets or sets a value indicating whether the confirmed.</summary>
        /// <value>True if confirmed, false if not.</value>
        [JsonProperty("confirmed"), DataMember(Name = "confirmed")]
        public bool Confirmed { get; set; } // true

        /// <summary>Gets or sets the Date/Time of the agreed terms.</summary>
        /// <value>The agreed terms.</value>
        [JsonProperty("agreed_terms"), DataMember(Name = "agreed_terms")]
        public DateTime AgreedTerms { get; set; } // "2013-11-28T23:19:42+00:00"

        /// <summary>Gets or sets the Date/Time of the created.</summary>
        /// <value>The created.</value>
        [JsonProperty("created"), DataMember(Name = "created")]
        public DateTime Created { get; set; } // "2013-11-28T23:19:42+00:00"

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri"), DataMember(Name = "uri")]
        public string? Uri { get; set; } // "/accounts/290465/users/17792"
    }
}
