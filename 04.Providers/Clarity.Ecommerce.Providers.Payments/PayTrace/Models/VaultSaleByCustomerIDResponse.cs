// <copyright file="VaultSaleByCustomerIDResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace VaultSaleByCustomerIDResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>A vault sale by customer identifier response.</summary>
    /// <seealso cref="PayTraceBasicSaleResponse"/>
    public class VaultSaleByCustomerIDResponse : PayTraceBasicSaleResponse
    {
        /// <summary>Gets or sets the identifier of the external transaction.</summary>
        /// <value>The identifier of the external transaction.</value>
        [JsonProperty("external_transaction_id")]
        public string? ExternalTransactionID { get; set; }
    }
}
