// <copyright file="SetSalesQuoteAsVoided.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales quote as voided class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A void sales quote.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStoreAdmin, UsedInAdmin, UsedInStorefront,
     Authenticate, RequiredPermission("Quoting.SalesQuote.Void"),
     Route("/Providers/Quoting/Actions/Void/{ID}", "PATCH",
        Summary = "Voids the specified Quote")]
    public class SetSalesQuoteAsVoided : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
