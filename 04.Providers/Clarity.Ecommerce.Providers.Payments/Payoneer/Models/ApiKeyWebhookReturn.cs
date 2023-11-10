// <copyright file="ApiKeyWebhookReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the API key webhook return class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>An API key webhook return.</summary>
    /*
    "api_key": {
        "api_key": "7000e66bb0119800a4a96fef005e7a60",
        "account_id": "170908002131",
        "user_id": "170908003807",
        "webhook": "http:\/\/dev.ms2.goafrica.tech\/DesktopModules\/ClarityEcommerce\/API\/Payments\/Payoneer\/OrderEventWebhooks",
        "created": "2017-09-08T00:27:10+00:00",
        "uri": null
    },
    */
    [DataContract]
    public class ApiKeyWebhookReturn
    {
        /// <summary>Gets or sets the API key.</summary>
        /// <value>The API key.</value>
        [JsonProperty("api_key")]
        [DataMember(Name = "api_key")]
        [ApiMember(Name = "api_key", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? APIKey { get; set; } // "7000e66bb0119800a4a96fef005e7a60",

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("account_id")]
        [DataMember(Name = "account_id")]
        [ApiMember(Name = "account_id", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? AccountID { get; set; } // 170908002131

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [JsonProperty("user_id")]
        [DataMember(Name = "user_id")]
        [ApiMember(Name = "user_id", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? UserID { get; set; } // 170908003807

        /// <summary>Gets or sets the webhook.</summary>
        /// <value>The webhook.</value>
        [JsonProperty("webhook")]
        [DataMember(Name = "webhook")]
        [ApiMember(Name = "webhook", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Webhook { get; set; } // "http:\/\/dev.ms2.goafrica.tech\/DesktopModules\/ClarityEcommerce\/API\/Payments\/Payoneer\/OrderEventWebhooks",

        /// <summary>Gets or sets the Date/Time of the created.</summary>
        /// <value>The created.</value>
        [JsonProperty("created")]
        [DataMember(Name = "created")]
        [ApiMember(Name = "created", DataType = "DateTime?", ParameterType = "body", IsRequired = false)]
        public DateTime? Created { get; set; } // "2017-09-08T00:27:10+00:00"

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri")]
        [DataMember(Name = "uri")]
        [ApiMember(Name = "uri", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? URI { get; set; } // "/accounts/412503/users/357206/apikeys/9710775e0a35344eee84e2587762b551"
    }
}
