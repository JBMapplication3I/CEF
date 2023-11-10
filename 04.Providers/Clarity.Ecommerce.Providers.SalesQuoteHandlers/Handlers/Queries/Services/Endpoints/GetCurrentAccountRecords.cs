// <copyright file="GetCurrentAccountRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current account records class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current account sales quotes.</summary>
    /// <seealso cref="SalesQuoteSearchModel"/>
    /// <seealso cref="IReturn{SalesQuotePagedResults}"/>
    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Providers/Quoting/Queries/RecordsForCurrentAccount", "POST",
            Summary = "Use to get history of quotes for the current account")]
    public class GetCurrentAccountSalesQuotes : SalesQuoteSearchModel, IReturn<SalesQuotePagedResults>
    {
    }
}
