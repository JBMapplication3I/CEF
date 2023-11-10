// <copyright file="SetSalesInvoiceAsVoided.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales invoice as voided class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A void sales invoice.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStoreAdmin, UsedInAdmin,
     Authenticate, RequiredPermission("Invoicing.SalesInvoice.Void"),
     Route("/Providers/Invoicing/Actions/Void/{ID}", "PATCH",
         Summary = "Void the invoice. It will no longer be processed and will be visible on the Completed Invoices view. An email notification will be sent to the customer.")]
    public class SetSalesInvoiceAsVoided : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
