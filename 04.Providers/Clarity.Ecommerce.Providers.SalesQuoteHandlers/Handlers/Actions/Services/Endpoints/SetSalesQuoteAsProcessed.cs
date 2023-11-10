// <copyright file="SetSalesQuoteAsProcessed.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales quote as processed class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A processed sales quote.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Quoting.SalesQuote.Processed"),
     Route("/Providers/Quoting/Actions/Processed/{ID}", "PATCH",
         Summary = "Marks the specified Quote as Processed")]
    public class SetSalesQuoteAsProcessed : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
