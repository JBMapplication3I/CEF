// <copyright file="OrderWebhookReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the order webhook return class</summary>
// ReSharper disable InconsistentNaming
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System;
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>An order webhook return.</summary>
    /*
    {
        "order_id": 180110219852,
        "account_id": 170908017857,
        "seller_id": 170908015893,
        "buyer_account_id": 171213191288,
        "buyer_id": 171213198999,
        "currency": "USD",
        "type": 1,
        "status": 1,
        "status_name": "Request Sent",
        "amount": 599.99,
        "balance": 457.5,
        "available_balance": 457.5,
        "summary": "Example Goods Order",
        "description": "An escrow for goods order for use as an example.",
        "invoice_num": "12345",
        "purchase_order_num": "67890",
        "status_change": "2018-01-10T21:25:41+00:00",
        "created": "2018-01-10T21:25:40+00:00",
        "uri": "\/accounts\/170908017857\/orders\/180110219852",
        "parent_order_id": ""
    }
    */
    [DataContract]
    public class OrderWebhookReturn
    {
        /// <summary>Gets or sets the identifier of the order.</summary>
        /// <value>The identifier of the order.</value>
        [JsonProperty("order_id")]
        [DataMember(Name = "order_id")]
        [ApiMember(Name = "order_id", DataType = "long?", ParameterType = "body", IsRequired = false)]
        public long? OrderID { get; set; } // 180110219852

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("account_id")]
        [DataMember(Name = "account_id")]
        [ApiMember(Name = "account_id", DataType = "long?", ParameterType = "body", IsRequired = false)]
        public long? SellerAccountID { get; set; } // 170908017857

        /// <summary>Gets or sets the identifier of the buyer account.</summary>
        /// <value>The identifier of the buyer account.</value>
        [JsonProperty("buyer_account_id")]
        [DataMember(Name = "buyer_account_id")]
        [ApiMember(Name = "buyer_account_id", DataType = "long?", ParameterType = "body", IsRequired = false)]
        public long? BuyerAccountID { get; set; } // 171213191288

        /// <summary>Gets or sets the identifier of the buyer.</summary>
        /// <value>The identifier of the buyer.</value>
        [JsonProperty("buyer_id")]
        [DataMember(Name = "buyer_id")]
        [ApiMember(Name = "buyer_id", DataType = "long?", ParameterType = "body", IsRequired = false)]
        public long? BuyerCustomerID { get; set; } // 171213198999

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        [JsonProperty("currency")]
        [DataMember(Name = "currency")]
        [ApiMember(Name = "currency", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Currency { get; set; } // "USD"

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        [DataMember(Name = "type")]
        [ApiMember(Name = "type", DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? Type { get; set; } // 1

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        [JsonProperty("status")]
        [DataMember(Name = "status")]
        [ApiMember(Name = "status", DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? Status { get; set; } // 1

        /// <summary>Gets or sets the name of the status.</summary>
        /// <value>The name of the status.</value>
        [JsonProperty("status_name")]
        [DataMember(Name = "status_name")]
        [ApiMember(Name = "status_name", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? StatusName { get; set; } // "Request Sent"

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        [JsonProperty("amount")]
        [DataMember(Name = "amount")]
        [ApiMember(Name = "amount", DataType = "decimal?", ParameterType = "body", IsRequired = false)]
        public decimal? Amount { get; set; } // 599.99

        /// <summary>Gets or sets the balance.</summary>
        /// <value>The balance.</value>
        [JsonProperty("balance")]
        [DataMember(Name = "amount")]
        [ApiMember(Name = "balance", DataType = "decimal?", ParameterType = "body", IsRequired = false)]
        public decimal? Balance { get; set; } // 457.5

        /// <summary>Gets or sets the available balance.</summary>
        /// <value>The available balance.</value>
        [JsonProperty("available_balance")]
        [DataMember(Name = "available_balance")]
        [ApiMember(Name = "available_balance", DataType = "decimal?", ParameterType = "body", IsRequired = false)]
        public decimal? AvailableBalance { get; set; } // 457.5

        /// <summary>Gets or sets the summary.</summary>
        /// <value>The summary.</value>
        [JsonProperty("summary")]
        [DataMember(Name = "summary")]
        [ApiMember(Name = "summary", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Summary { get; set; } // "Example Goods Order"

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [JsonProperty("description")]
        [DataMember(Name = "description")]
        [ApiMember(Name = "description", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? Description { get; set; } // "An escrow for goods order for use as an example."

        /// <summary>Gets or sets the invoice number.</summary>
        /// <value>The invoice number.</value>
        [JsonProperty("invoice_num")]
        [DataMember(Name = "description")]
        [ApiMember(Name = "invoice_num", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? InvoiceNumber { get; set; } // "12345"

        /// <summary>Gets or sets the purchase order number.</summary>
        /// <value>The purchase order number.</value>
        [JsonProperty("purchase_order_num")]
        [DataMember(Name = "purchase_order_num")]
        [ApiMember(Name = "purchase_order_num", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? PurchaseOrderNumber { get; set; } // "67890"

        /// <summary>Gets or sets the Date/Time of the status last changed.</summary>
        /// <value>The status last changed.</value>
        [JsonProperty("status_change")]
        [DataMember(Name = "status_change")]
        [ApiMember(Name = "status_change", DataType = "DateTime?", ParameterType = "body", IsRequired = false)]
        public DateTime? StatusLastChange { get; set; } // "2018-01-10T21:25:41+00:00""

        /// <summary>Gets or sets the Date/Time of the created.</summary>
        /// <value>The created.</value>
        [JsonProperty("created")]
        [DataMember(Name = "created")]
        [ApiMember(Name = "created", DataType = "DateTime?", ParameterType = "body", IsRequired = false)]
        public DateTime? Created { get; set; } // "2018-01-10T21:25:40+00:00"

        /// <summary>Gets or sets URI of the document.</summary>
        /// <value>The URI.</value>
        [JsonProperty("uri")]
        [DataMember(Name = "uri")]
        [ApiMember(Name = "uri", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? URI { get; set; } // "\/accounts\/170908017857\/orders\/180110219852"

        /// <summary>Gets or sets the purchase order number.</summary>
        /// <value>The purchase order number.</value>
        [JsonProperty("parent_order_id")]
        [DataMember(Name = "parent_order_id")]
        [ApiMember(Name = "parent_order_id", DataType = "string", ParameterType = "body", IsRequired = false)]
        public string? ParentOrderID { get; set; } // ""
    }
}
