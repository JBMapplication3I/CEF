// <copyright file="CreateAuthenticatedURLReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create authenticated URL return class</summary>
#pragma warning disable 1591
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    /// <summary>A create authenticated URL return.</summary>
    [PublicAPI]
    [DataContract]
    public class CreateAuthenticatedURLReturn
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [JsonProperty("user_id"), DataMember(Name = "user_id")]
        public long UserID { get; set; } // 17792

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("account_id"), DataMember(Name = "account_id")]
        public long AccountID { get; set; } // 290465

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri"), DataMember(Name = "uri")]
        public string? Uri { get; set; } // "/accounts/290465/bankaccounts"

        /// <summary>Gets or sets the action.</summary>
        /// <value>The action.</value>
        [JsonProperty("action"), DataMember(Name = "action")]
        public string? Action { get; set; } // "create"

        /// <summary>Gets or sets URL of the document.</summary>
        /// <value>The URL.</value>
        [JsonProperty("url"), DataMember(Name = "url")]
        public string? Url { get; set; } // "https://pay.payoneer.com/accounts/290465/bankaccounts?action=create&user_auth_token=fa7c6352d60b2128b91767ee5b6a32e7"

        /// <summary>Gets or sets the Date/Time of the created.</summary>
        /// <value>The created.</value>
        [JsonProperty("created"), DataMember(Name = "created")]
        public DateTime Created { get; set; } // "2014-07-21T22:26:52-05:00"

        /// <summary>Gets or sets the Date/Time of the expires.</summary>
        /// <value>The expires.</value>
        [JsonProperty("expires"), DataMember(Name = "expires")]
        public DateTime Expires { get; set; } // "2014-07-21T23:26:52-05:00"
    }
}
