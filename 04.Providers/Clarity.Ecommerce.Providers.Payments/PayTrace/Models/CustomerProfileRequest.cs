// <copyright file="CustomerProfileRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace CustomerProfileRequest class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>A customer profile request.</summary>
    public class CustomerProfileRequest
    {
        /// <summary>Gets or sets the identifier of the customer.</summary>
        /// <value>The identifier of the customer.</value>
        [JsonProperty("customer_id")]
        public string? CustomerID { get; set; }

        /// <summary>Gets or sets the credit card.</summary>
        /// <value>The credit card.</value>
        [JsonProperty("credit_card")]
        public CreditCard? CreditCard { get; set; }

        /// <summary>Gets or sets the identifier of the integrator.</summary>
        /// <value>The identifier of the integrator.</value>
        [JsonProperty("integrator_id")]
        public string? IntegratorID { get; set; }

        /// <summary>Gets or sets the billing address.</summary>
        /// <value>The billing address.</value>
        [JsonProperty("billing_address")]
        public Address? BillingAddress { get; set; }

        /// <summary>Gets or sets the shipping address.</summary>
        /// <value>The shipping address.</value>
        [JsonProperty("shipping_address")]
        public Address? ShippingAddress { get; set; }

        /// <summary>Gets or sets the check.</summary>
        /// <value>The check.</value>
        [JsonProperty("check")]
        public Check? Check { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        [JsonProperty("phone")]
        public string? Phone { get; set; }

        /// <summary>Gets or sets the fax.</summary>
        /// <value>The fax.</value>
        [JsonProperty("fax")]
        public string? Fax { get; set; }

        /// <summary>Gets or sets the New ID.</summary>
        /// <value>The New ID.</value>
        [JsonProperty("new_id")]
        public string? NewID { get; set; }
    }
}
