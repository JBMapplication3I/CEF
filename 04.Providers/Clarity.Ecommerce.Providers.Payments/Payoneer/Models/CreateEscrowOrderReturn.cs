// <copyright file="CreateEscrowOrderReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create escrow order return class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;

    /// <summary>A create escrow order return.</summary>
    [PublicAPI]
    [DataContract]
    public class CreateEscrowOrderReturn
    {
        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        [JsonProperty("order_id"), DataMember(Name = "order_id")]
        public long OrderID { get; set; } // 1401028714

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("account_id"), DataMember(Name = "account_id")]
        public long AccountID { get; set; } // 290465

        /// <summary>Gets or sets the identifier of the seller.</summary>
        /// <value>The identifier of the seller.</value>
        [JsonProperty("seller_id"), DataMember(Name = "seller_id")]
        public long SellerID { get; set; } // 17792

        /// <summary>Gets or sets the identifier of the buyer account.</summary>
        /// <value>The identifier of the buyer account.</value>
        [JsonProperty("buyer_account_id"), DataMember(Name = "buyer_account_id")]
        public long BuyerAccountID { get; set; } // 452981

        /// <summary>Gets or sets the identifier of the buyer user.</summary>
        /// <value>The identifier of the buyer user.</value>
        [JsonProperty("buyer_id"), DataMember(Name = "buyer_id")]
        public long BuyerUserID { get; set; } // 29338

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [JsonProperty("type"), DataMember(Name = "type")]
        public int Type { get; set; } // 1

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        [JsonProperty("status"), DataMember(Name = "status")]
        public int Status { get; set; } // 1

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        [JsonProperty("amount"), DataMember(Name = "amount")]
        public decimal Amount { get; set; } // 10000

        /// <summary>Gets or sets the balance.</summary>
        /// <value>The balance.</value>
        [JsonProperty("balance"), DataMember(Name = "balance")]
        public decimal Balance { get; set; } // 0

        /// <summary>Gets or sets the available balance.</summary>
        /// <value>The available balance.</value>
        [JsonProperty("available_balance"), DataMember(Name = "available_balance")]
        public decimal AvailableBalance { get; set; } // 0

        /// <summary>Gets or sets the summary.</summary>
        /// <value>The summary.</value>
        [JsonProperty("summary"), DataMember(Name = "summary")]
        public string? Summary { get; set; } // "Example Goods Order"

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [JsonProperty("description"), DataMember(Name = "description")]
        public string? Description { get; set; } // "An escrow for goods order for use as an example."

        /// <summary>Gets or sets the invoice number.</summary>
        /// <value>The invoice number.</value>
        [JsonProperty("invoice_num"), DataMember(Name = "invoice_num")]
        public string? InvoiceNumber { get; set; } // "12345"

        /// <summary>Gets or sets the purchase order number.</summary>
        /// <value>The purchase order number.</value>
        [JsonProperty("purchase_order_num"), DataMember(Name = "purchase_order_num")]
        public string? PurchaseOrderNumber { get; set; } // "67890"

        /// <summary>Gets or sets the Date/Time of the status last changed.</summary>
        /// <value>The status last changed.</value>
        [JsonProperty("status_change"), DataMember(Name = "status_change")]
        public DateTime StatusLastChanged { get; set; } // "2014-01-02T20:48:27+00:00"

        /// <summary>Gets or sets the Date/Time of the created.</summary>
        /// <value>The created.</value>
        [JsonProperty("created"), DataMember(Name = "created")]
        public DateTime Created { get; set; } // "2014-01-02T20:48:27+00:00"

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri"), DataMember(Name = "uri")]
        public string? Uri { get; set; } // "/accounts/290465/orders/1401028714"
    }
}
