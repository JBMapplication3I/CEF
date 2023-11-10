// <copyright file="CreditCard.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace CreditCard class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;

    /// <summary>Class for credit card.</summary>
    public class CreditCard : ICreditCard
    {
        /// <summary>Gets or sets the masked number.</summary>
        /// <value>The masked number.</value>
        [JsonProperty("masked_number")]
        public string? MaskedNumber { get; set; }

        /// <summary>Gets or sets the declare 'encrypted_number' instead of 'number' in case of using PayTrace Client-
        /// Side Encryption JavaScript Library.</summary>
        /// <value>The Cc number.</value>
        [JsonProperty("number")]
        public string? Number { get; set; }

        /// <summary>Gets or sets the expiration month.</summary>
        /// <value>The expiration month.</value>
        [JsonProperty("expiration_month")]
        public string? ExpirationMonth { get; set; }

        /// <summary>Gets or sets the expiration year.</summary>
        /// <value>The expiration year.</value>
        [JsonProperty("expiration_year")]
        public string? ExpirationYear { get; set; }
    }
}
