// <copyright file="GetCurrentUserRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current user records class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current user sales invoices.</summary>
    /// <seealso cref="SalesInvoiceSearchModel"/>
    /// <seealso cref="IReturn{SalesInvoicePagedResults}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate, RequiresAnyPermission("Invoicing.SalesInvoice.View", "Storefront.UserDashboard.SalesInvoices.View"),
     Route("/Providers/Invoicing/Queries/RecordsForCurrentUser", "POST",
        Summary = "Use to get history of invoices for this user")]
    public class GetCurrentUserSalesInvoices : SalesInvoiceSearchModel, IReturn<SalesInvoicePagedResults>
    {
    }
}
