// <copyright file="AdminGetRecordsForPortal.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get records class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>An admin get portal sales quote.</summary>
    /// <seealso cref="SalesQuoteSearchModel"/>
    /// <seealso cref="IReturn{SalesQuotePagedResults}"/>
    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Providers/Quoting/Queries/Admin/Portal/Records", "POST",
            Summary = "Use to get history of sales quotes for the current brand")]
    public class AdminGetSalesQuotesForPortal : SalesQuoteSearchModel, IReturn<SalesQuotePagedResults>
    {
    }
}
