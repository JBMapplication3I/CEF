// <copyright file="PaySingleInvoiceByID.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay single invoice by identifier class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A pay invoice endpoint.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin,
     Authenticate,
     Route("/Providers/Invoicing/Actions/Pay", "POST",
        Summary = "Provide payment information to a specific invoice by ID.")]
    public class PaySingleInvoiceByID : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the invoice.</summary>
        /// <value>The identifier of the invoice.</value>
        [ApiMember(Name = nameof(InvoiceID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int InvoiceID { get; set; }

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
