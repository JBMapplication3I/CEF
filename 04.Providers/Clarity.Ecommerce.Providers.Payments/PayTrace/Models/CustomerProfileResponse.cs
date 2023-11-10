// <copyright file="CustomerProfileResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace CustomerProfileResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>A customer profile response.</summary>
    /// <seealso cref="PayTraceBasicResponse"/>
    public class CustomerProfileResponse : PayTraceBasicResponse
    {
        /// <summary>Gets or sets the identifier of the customer.</summary>
        /// <value>The identifier of the customer.</value>
        [JsonProperty("customer_id")]
        public string? CustomerID { get; set; }

        /// <summary>Gets or sets the masked card number.</summary>
        /// <value>The masked card number.</value>
        [JsonProperty("masked_card_number")]
        public string? MaskedCardNumber { get; set; }
    }
}
