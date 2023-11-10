// <copyright file="PayMultipleInvoicesByAmounts.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay multiple invoices by amounts class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Services.Endpoints
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A pay multiple invoices endpoint.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate,
     Route("/Providers/Invoicing/Actions/PayMultiple", "POST",
         Summary = "Use to pay multiple invoices, specifying the amount with each one")]
    public class PayMultipleInvoicesByAmounts : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the amounts.</summary>
        /// <value>The amounts.</value>
        [ApiMember(Name = nameof(Amounts), DataType = "Dictionary<int, decimal>", ParameterType = "body", IsRequired = true)]
        public Dictionary<int, decimal> Amounts { get; set; } = null!;

        /// <summary>Gets or sets the payment.</summary>
        /// <value>The payment.</value>
        [ApiMember(Name = nameof(Payment), DataType = "PaymentModel", ParameterType = "body", IsRequired = true)]
        public PaymentModel Payment { get; set; } = null!;

        /// <summary>Gets or sets the billing.</summary>
        /// <value>The billing.</value>
        [ApiMember(Name = nameof(Billing), DataType = "ContactModel", ParameterType = "body", IsRequired = false)]
        public ContactModel? Billing { get; set; }
    }
}
