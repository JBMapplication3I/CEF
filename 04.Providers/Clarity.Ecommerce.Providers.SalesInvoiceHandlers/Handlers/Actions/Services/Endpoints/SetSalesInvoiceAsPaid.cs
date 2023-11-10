// <copyright file="SetSalesInvoiceAsPaid.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales invoice as paid class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A set sales invoice as paid.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStoreAdmin, UsedInBrandAdmin, UsedInAdmin,
     Authenticate, RequiredPermission("Invoicing.SalesInvoice.Paid"),
     Route("/Providers/Invoicing/Actions/Paid/{ID}", "PATCH",
         Summary = "Mark the invoice as Paid. It will no longer be processed and will be visible on the Completed Invoices view. An email notification will be sent to the customer.")]
    public class SetSalesInvoiceAsPaid : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
