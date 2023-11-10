// <copyright file="GetCurrentUserRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current user records class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current user sales quote.</summary>
    /// <seealso cref="SalesQuoteSearchModel"/>
    /// <seealso cref="IReturn{SalesQuotePagedResults}"/>
    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Providers/Quoting/Queries/RecordsForCurrentUser", "POST",
            Summary = "Use to get history of quotes for the current user.")]
    public class GetCurrentUserSalesQuotes : SalesQuoteSearchModel, IReturn<SalesQuotePagedResults>
    {
    }
}
