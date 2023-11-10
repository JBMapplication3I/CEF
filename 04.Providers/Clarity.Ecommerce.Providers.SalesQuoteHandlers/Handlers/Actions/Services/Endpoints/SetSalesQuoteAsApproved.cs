// <copyright file="SetSalesQuoteAsApproved.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales quote as approved class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>An approved sales quote.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInStorefront,
        Authenticate, RequiredPermission("Quoting.SalesQuote.Approve"),
        Route("/Providers/Quoting/Actions/Approve/{ID}", "PATCH",
            Summary = "Approves the specified Quote")]
    public class SetSalesQuoteAsApproved : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
