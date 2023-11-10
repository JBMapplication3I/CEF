// <copyright file="PayeezyTokenRequestResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Payeezy token request response class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A Payeezy token request response.</summary>
    [Serializable, PublicAPI]
    public class PayeezyTokenRequestResponse
    {
        /// <summary>Gets or sets the identifier of the correlation.</summary>
        /// <remarks>Payeezy Internal log id.</remarks>
        /// <value>The identifier of the correlation.</value>
        [DataMember(Name = "correlation_id"), JsonProperty("correlation_id"), ApiMember(Name = "correlation_id")]
        public string? CorrelationID { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <remarks>Status message for Token creation. Value = "success" / "declined".</remarks>
        /// <value>The status.</value>
        [DataMember(Name = "status"), JsonProperty("status"), ApiMember(Name = "status")]
        public string? Status { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <remarks>Type of the token being created. Value = FDToken.</remarks>
        /// <value>The type.</value>
        [DataMember(Name = "type"), JsonProperty("type"), ApiMember(Name = "type")]
        public string? Type { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <remarks>JSON object that holds the tokenized card information.</remarks>
        /// <value>The token.</value>
        [DataMember(Name = "token"), JsonProperty("token"), ApiMember(Name = "token")]
        public PayeezyTokenValue? Token { get; set; }

        /// <summary>Gets or sets the error.</summary>
        /// <value>The error.</value>
        public PayeezyApiError? Error { get; set; }
    }
}
