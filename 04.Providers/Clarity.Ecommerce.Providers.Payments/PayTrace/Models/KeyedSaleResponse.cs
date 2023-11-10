// <copyright file="KeyedSaleResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace KeyedSaleResponse class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>KeyedSaleResponse Class following properties are Specific to Keyed Sale Response and Keyed
    /// Authorization Response.</summary>
    /// <seealso cref="T:Clarity.Ecommerce.Providers.Payments.PayTrace.Models.PayTraceBasicSaleResponse"/>
    public class KeyedSaleResponse : PayTraceBasicSaleResponse
    {
        /// <summary>Gets or sets the masked card number.</summary>
        /// <value>The masked card number.</value>
        [JsonProperty("masked_card_number")]
        public string? MaskedCardNumber { get; set; }
    }
}
