// <copyright file="SetSalesQuoteAsRejected.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales quote as rejected class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A rejected sales quote.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin, UsedInStorefront,
        Authenticate, RequiredPermission("Quoting.SalesQuote.Reject"),
        Route("/Providers/Quoting/Actions/Reject/{ID}", "PATCH",
            Summary = "Rejects the specified Quote")]
    public class SetSalesQuoteAsRejected : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
