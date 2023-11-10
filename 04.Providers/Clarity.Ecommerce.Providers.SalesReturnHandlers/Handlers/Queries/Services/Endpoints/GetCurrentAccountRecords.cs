// <copyright file="GetCurrentAccountRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current account records class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current account sales returns.</summary>
    /// <seealso cref="SalesReturnSearchModel"/>
    /// <seealso cref="IReturn{SalesReturnPagedResults}"/>
    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Providers/Returning/Queries/CurrentAccountSalesReturns", "POST",
            Summary = "Use to get history of returns for the current account")]
    public class GetCurrentAccountSalesReturns : SalesReturnSearchModel, IReturn<SalesReturnPagedResults>
    {
    }
}
