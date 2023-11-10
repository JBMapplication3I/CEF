// <copyright file="PayeezyTokenValue.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy token value class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>(Serializable)a payeezy token value.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyTokenValue
    {
        /// <summary>Gets or sets the type.</summary>
        /// <remarks>values - “American Express", "Visa", "Mastercard" or "Discover".</remarks>
        /// <value>The type.</value>
        [DataMember(Name = "type"), JsonProperty("type"), ApiMember(Name = "type")]
        public string? Type { get; set; }

        /// <summary>Gets or sets the name of the cardholder.</summary>
        /// <remarks>Name of the card holder.</remarks>
        /// <value>The name of the cardholder.</value>
        [DataMember(Name = "cardholder_name"), JsonProperty("cardholder_name"), ApiMember(Name = "cardholder_name")]
        public string? CardholderName { get; set; }

        /// <summary>Gets or sets the exponent date.</summary>
        /// <remarks>Expiration Date on the card - MMYY format. eg:1014.</remarks>
        /// <value>The exponent date.</value>
        [DataMember(Name = "exp_date"), JsonProperty("exp_date"), ApiMember(Name = "exp_date")]
        public string? ExpDate { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <remarks>Its the value of the tokenized card info via the transarmor tokenization process.</remarks>
        /// <value>The value.</value>
        [DataMember(Name = "value"), JsonProperty("value"), ApiMember(Name = "value")]
        public string? Value { get; set; }
    }
}
