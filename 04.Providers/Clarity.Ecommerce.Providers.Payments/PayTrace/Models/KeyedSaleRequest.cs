// <copyright file="KeyedSaleRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace KeyedSaleRequest class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>Class for keyed sale request and Keyed Authorization. Please refer the account security page on
    /// PayTrace virtual Terminal to determine the property.</summary>
    public class KeyedSaleRequest
    {
        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        [DataMember(Name = "amount"), JsonProperty("amount"), ApiMember(Name = "amount")]
        public double Amount { get; set; }

        /// <summary>Gets or sets the credit card.</summary>
        /// <value>The credit card.</value>
        [DataMember(Name = "credit_card"), JsonProperty("credit_card"), ApiMember(Name = "credit_card")]
        public CreditCard? CreditCard { get; set; }

        /// <summary>Gets or sets the eCheck data.</summary>
        /// <value>The check.</value>
        [DataMember(Name = "check"), JsonProperty("check"), ApiMember(Name = "check")]
        public Check? Check { get; set; }

        /// <summary>Declare 'encrypted_csc' instead of 'csc' in case of using PayTrace Client-Side Encryption JavaScript
        /// Library.</summary>
        /// <value>The csc.</value>
        [DataMember(Name = "csc"), JsonProperty("csc"), ApiMember(Name = "csc")]
        public string? Csc { get; set; }

        /// <summary>Gets or sets the billing address.</summary>
        /// <value>The billing address.</value>
        [DataMember(Name = "billing_address"), JsonProperty("billing_address"), ApiMember(Name = "billing_address")]
        public Address? BillingAddress { get; set; }

        /// <summary>Gets or sets the identifier of the integrator.</summary>
        /// <value>The identifier of the integrator.</value>
        [DataMember(Name = "integrator_id"), JsonProperty("integrator_id"), ApiMember(Name = "integrator_id")]
        public string? IntegratorId { get; set; }
    }
}
