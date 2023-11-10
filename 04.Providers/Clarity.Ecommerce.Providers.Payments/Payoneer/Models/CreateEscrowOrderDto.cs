// <copyright file="CreateEscrowOrderDto.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create escrow order dto class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Payoneer
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;

    /// <summary>A create escrow order data transfer object.</summary>
    [DataContract]
    internal class CreateEscrowOrderDto
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [DataMember(Name = "type")]
        public int Type { get; set; } // 1

        /// <summary>Gets or sets the identifier of the seller user.</summary>
        /// <value>The identifier of the seller user.</value>
        [DataMember(Name = "seller_id")]
        public long SellerUserID { get; set; } // 17792

        /// <summary>Gets or sets the identifier of the buyer user.</summary>
        /// <value>The identifier of the buyer user.</value>
        [DataMember(Name = "buyer_id")]
        public long BuyerUserID { get; set; } // 29338

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; } // 10000

        /// <summary>Gets or sets the summary.</summary>
        /// <value>The summary.</value>
        [DataMember(Name = "summary"), JsonRequired]
        public string? Summary { get; set; } // "Example Goods Order"

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [DataMember(Name = "description")]
        public string? Description { get; set; } // "An escrow for goods order for use as an example."

        /// <summary>Gets or sets a value indicating whether the credit card terms accepted.</summary>
        /// <value>True if credit card terms accepted, false if not.</value>
        [DataMember(Name = "cc_accept")]
        public bool CreditCardTermsAccepted { get; set; } // true

        /// <summary>Gets or sets the invoice number.</summary>
        /// <value>The invoice number.</value>
        [DataMember(Name = "invoice_num")]
        public string? InvoiceNumber { get; set; } // "12345"

        /// <summary>Gets or sets the purchase order number.</summary>
        /// <value>The purchase order number.</value>
        [DataMember(Name = "purchase_order_num")]
        public string? PurchaseOrderNumber { get; set; } // "67890"

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        [DataMember(Name = "message")]
        public string? Message { get; set; } // "Hello, Example Buyer! Thank you for your example goods order."
    }
}
