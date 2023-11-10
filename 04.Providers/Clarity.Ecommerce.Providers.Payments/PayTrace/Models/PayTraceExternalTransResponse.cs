// <copyright file="PayTraceExternalTransResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTraceExternalTransResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>Following properties are Available on some of the Responses with the API This class is used by some
    /// of the child classes on this page As well as for the Capture Transaction.</summary>
    /// <seealso cref="T:Clarity.Ecommerce.Providers.Payments.PayTrace.Models.PayTraceBasicResponse"/>
    public class PayTraceExternalTransResponse : PayTraceBasicResponse
    {
        /// <summary>Gets or sets the identifier of the external transaction.</summary>
        /// <value>The identifier of the external transaction.</value>
        [JsonProperty("external_transaction_id")]
        public string? ExternalTransactionId { get; set; }
    }
}
