// <copyright file="SetSalesInvoiceAsUnpaid.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales invoice as unpaid class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A set sales invoice as unpaid.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStoreAdmin, UsedInBrandAdmin, UsedInAdmin,
     Authenticate, RequiredPermission("Invoicing.SalesInvoice.Unpaid"),
     Route("/Providers/Invoicing/Actions/Unpaid/{ID}", "PATCH",
         Summary = "Mark the invoice as Unpaid. An email notification will be sent to the customer.")]
    public class SetSalesInvoiceAsUnpaid : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
