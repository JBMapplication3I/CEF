// <copyright file="AccountWebhookReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account webhook return class</summary>
// ReSharper disable InconsistentNaming
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>An order webhook return.</summary>
    /*
    {
        "account_id": 180111179776,
        "company": "Steven Arnold",
        "address": "300 N Lamar",
        "city": "Austin",
        "state": "Texas",
        "postal_code": "78703",
        "country": "united states of america",
        "phone": null,
        "account_type": "1",
        "url": null,
        "company_type": null,
        "inc_country": null,
        "inc_state": null,
        "created": "2018-01-11T17:59:01+00:00",
        "uri": "\/accounts\/180111179776"
    }
    */
    [DataContract]
    public class AccountWebhookReturn
    {
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("account_id"), DataMember(Name = "account_id"), ApiMember(Name = "account_id", DataType = "long?", ParameterType = "body", IsRequired = false)]
        public long? AccountID { get; set; } // 180111179776

        /// <summary>Gets or sets the company.</summary>
        /// <value>The company.</value>
        [JsonProperty("company"), DataMember(Name = "company"), ApiMember(Name = "company", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Company { get; set; } // "Steven Arnold"

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        [JsonProperty("address"), DataMember(Name = "address"), ApiMember(Name = "address", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Address { get; set; } // "300 N Lamar"

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [JsonProperty("city"), DataMember(Name = "city"), ApiMember(Name = "city", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? City { get; set; } // "Austin"

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        [JsonProperty("state"), DataMember(Name = "state"), ApiMember(Name = "state", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? State { get; set; } // "Texas"

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        [JsonProperty("postal_code"), DataMember(Name = "postal_code"), ApiMember(Name = "postal_code", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? PostalCode { get; set; } // "78703"

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        [JsonProperty("country"), DataMember(Name = "country"), ApiMember(Name = "country", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Country { get; set; } // "united states of america"

        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        [JsonProperty("phone"), DataMember(Name = "phone"), ApiMember(Name = "phone", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Phone { get; set; } // null

        /// <summary>Gets or sets the type of the account.</summary>
        /// <value>The type of the account.</value>
        [JsonProperty("account_type"), DataMember(Name = "account_type"), ApiMember(Name = "account_type", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? AccountType { get; set; } // "1"

        /// <summary>Gets or sets URL of the document.</summary>
        /// <value>The URL.</value>
        [JsonProperty("url"), DataMember(Name = "url"), ApiMember(Name = "url", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? URL { get; set; } // null

        /// <summary>Gets or sets the type of the company.</summary>
        /// <value>The type of the company.</value>
        [JsonProperty("company_type"), DataMember(Name = "company_type"), ApiMember(Name = "company_type", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? CompanyType { get; set; } // null

        /// <summary>Gets or sets the increment country.</summary>
        /// <value>The increment country.</value>
        [JsonProperty("inc_country"), DataMember(Name = "inc_country"), ApiMember(Name = "inc_country", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? IncCountry { get; set; } // null

        /// <summary>Gets or sets the state of the increment.</summary>
        /// <value>The increment state.</value>
        [JsonProperty("inc_state"), DataMember(Name = "inc_state"), ApiMember(Name = "inc_state", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? IncState { get; set; } // null

        /// <summary>Gets or sets the Date/Time of the created.</summary>
        /// <value>The created.</value>
        [JsonProperty("created"), DataMember(Name = "created"), ApiMember(Name = "created", DataType = "DateTime?", ParameterType = "body", IsRequired = false)]
        public DateTime? Created { get; set; } // "2018-01-11T17:59:01+00:00"

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri"), DataMember(Name = "uri"), ApiMember(Name = "uri", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? URI { get; set; } // "\/accounts\/180111179776"
    }
}
