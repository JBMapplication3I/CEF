// <copyright file="CreateInvoiceForSalesOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create invoice for sales order class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A create invoice for sales order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse_SalesInvoiceModel}"/>
    [PublicAPI, UsedInStoreAdmin, UsedInAdmin,
     Authenticate, RequiredPermission("Ordering.SalesOrder.CreateInvoice"),
     Route("/Providers/Invoicing/Actions/CreateFromOrderID/{ID}", "PATCH",
         Summary = "Generates an Invoice with the same information as this Order with the Balance Due amount. An "
             + "email notification will be sent to the customer. No status change will occur.")]
    public class CreateInvoiceForSalesOrder : ImplementsIDBase, IReturn<CEFActionResponse<SalesInvoiceModel>>
    {
    }
}
