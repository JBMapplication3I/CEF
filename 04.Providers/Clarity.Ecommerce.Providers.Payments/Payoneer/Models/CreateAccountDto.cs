// <copyright file="CreateAccountDto.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create account dto class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>A create account data transfer object.</summary>
    [DataContract]
    internal class CreateAccountDto
    {
        /// <summary>Gets or sets the type of the account.</summary>
        /// <value>The type of the account.</value>
        [JsonProperty("account_type")]
        [DataMember(Name = "account_type")]
        public int AccountType { get; set; } // 1

        /// <summary>Gets or sets the company.</summary>
        /// <value>The company.</value>
        [JsonProperty("company")]
        [DataMember(Name = "company")]
        public string? Company { get; set; } // "ACME Inc."

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        [JsonProperty("user_name")]
        [DataMember(Name = "user_name")]
        public string? Username { get; set; } // "John Doe"

        /// <summary>Gets or sets the user email.</summary>
        /// <value>The user email.</value>
        [JsonProperty("user_email")]
        [DataMember(Name = "user_email")]
        public string? UserEmail { get; set; } // "j.doe@example.com"

        /// <summary>Gets or sets the user phone.</summary>
        /// <value>The user phone.</value>
        [JsonProperty("user_phone")]
        [DataMember(Name = "user_phone")]
        public string? UserPhone { get; set; } // "+1 800-555-1234"

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        [JsonProperty("address")]
        [DataMember(Name = "address")]
        public string? Address { get; set; } // "123 Main St."

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [JsonProperty("city")]
        [DataMember(Name = "city")]
        public string? City { get; set; } // "Anytown"

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        [JsonProperty("state")]
        [DataMember(Name = "state")]
        public string? State { get; set; } // "CA"

        /// <summary>Gets or sets the zip.</summary>
        /// <value>The zip.</value>
        [JsonProperty("zip")]
        [DataMember(Name = "zip")]
        public string? Zip { get; set; } // "12345"

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        [JsonProperty("country")]
        [DataMember(Name = "country")]
        public string? Country { get; set; } // "us"

        /// <summary>Gets or sets a value indicating whether the email confirmed.</summary>
        /// <value>True if email confirmed, false if not.</value>
        [JsonProperty("email_confirmed")]
        [DataMember(Name = "email_confirmed")]
        public bool EmailConfirmed { get; set; } // true

        /// <summary>Gets or sets a value indicating whether the agreed terms.</summary>
        /// <value>True if agreed terms, false if not.</value>
        [JsonProperty("agreed_terms")]
        [DataMember(Name = "agreed_terms")]
        public bool AgreedTerms { get; set; } // true
    }
}
