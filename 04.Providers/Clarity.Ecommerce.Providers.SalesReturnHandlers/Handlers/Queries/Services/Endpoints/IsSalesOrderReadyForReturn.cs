// <copyright file="IsSalesOrderReadyForReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the is sales order ready for return class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A complete sales return.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Returning/Queries/IsSalesOrderReadyForReturn/{ID}", "GET",
            Summary = "Test if the Sales Order is eligible for return.")]
    public class IsSalesOrderReadyForReturn : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
