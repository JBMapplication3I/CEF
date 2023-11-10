// <copyright file="GetCurrentUserRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current user records class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current user sales returns.</summary>
    /// <seealso cref="SalesReturnSearchModel"/>
    /// <seealso cref="IReturn{SalesReturnPagedResults}"/>
    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Providers/Returning/Queries/RecordsForCurrentUser", "POST",
            Summary = "Use to get history of returns for the current user.")]
    public class GetCurrentUserSalesReturns : SalesReturnSearchModel, IReturn<SalesReturnPagedResults>
    {
    }
}
