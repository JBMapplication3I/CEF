// <copyright file="AdminGetRecordsForPortal.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current records class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current brand sales invoices.</summary>
    /// <seealso cref="SalesInvoiceSearchModel"/>
    /// <seealso cref="IReturn{SalesInvoicePagedResults}"/>
    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Providers/Invoicing/Queries/Admin/Portal/Records", "POST",
            Summary = "Use to get history of sales invoices for the current brand.")]
    public class AdminGetSalesInvoicesForPortal : SalesInvoiceSearchModel, IReturn<SalesInvoicePagedResults>
    {
    }
}
