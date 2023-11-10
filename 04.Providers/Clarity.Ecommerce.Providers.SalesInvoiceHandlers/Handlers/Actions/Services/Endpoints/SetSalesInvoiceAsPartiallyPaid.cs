// <copyright file="SetSalesInvoiceAsPartiallyPaid.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales invoice as partially paid class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A set sales invoice as partially paid.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStoreAdmin, UsedInBrandAdmin, UsedInAdmin,
     Authenticate, RequiredPermission("Invoicing.SalesInvoice.PartiallyPaid"),
     Route("/Providers/Invoicing/Actions/PartiallyPaid/{ID}", "PATCH",
         Summary = "Mark the invoice as Partially Paid. An email notification will be sent to the customer.")]
    public class SetSalesInvoiceAsPartiallyPaid : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
