// <copyright file="Customer.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace Customer class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Newtonsoft.Json;

    /// <summary>Class for Customer.</summary>
    public class Customer
    {
        /// <summary>Gets or sets the identifier of the customer.</summary>
        /// <value>The identifier of the customer.</value>
        [JsonProperty("customer_id")]
        public string? CustomerID { get; set; }

        /// <summary>Gets or sets the Credit Card.</summary>
        /// <value>The Credit Card.</value>
        [JsonProperty("credit_card")]
        public CreditCard? CreditCard { get; set; }

        /// <summary>Gets or sets the Billing Address.</summary>
        /// <value>The Billing Address.</value>
        [JsonProperty("billing_address")]
        public Address? BillingAddress { get; set; }

        /// <summary>Gets or sets the Shipping Address.</summary>
        /// <value>The Shipping Address.</value>
        [JsonProperty("shipping_address")]
        public Address? ShippingAddress { get; set; }

        /// <summary>Gets or sets the Check.</summary>
        /// <value>The Check.</value>
        [JsonProperty("check")]
        public Check? Check { get; set; }

        /// <summary>Gets or sets the Email.</summary>
        /// <value>The Email.</value>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>Gets or sets the Phone.</summary>
        /// <value>The Phone.</value>
        [JsonProperty("phone")]
        public string? Phone { get; set; }

        /// <summary>Gets or sets the Fax.</summary>
        /// <value>The Fax.</value>
        [JsonProperty("fax")]
        public string? Fax { get; set; }

        /// <summary>Gets or sets the Created object.</summary>
        /// <value>The Created object.</value>
        [JsonProperty("created")]
        public Created? Created { get; set; }
    }
}
