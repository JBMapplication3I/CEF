// <copyright file="AdminGetRecordsForPortal.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get records class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get sales returns.</summary>
    /// <seealso cref="SalesReturnSearchModel"/>
    /// <seealso cref="IReturn{SalesReturnPagedResults}"/>
    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin,
        Authenticate,
        Route("/Providers/Returning/Queries/Admin/Portals/Records", "POST",
            Summary = "Use to get history of sales returns for the current administrative portal.")]
    public class AdminGetSalesReturnsForPortal : SalesReturnSearchModel, IReturn<SalesReturnPagedResults>
    {
    }
}
