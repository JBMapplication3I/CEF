// <copyright file="SetSalesQuoteAsInProcess.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales quote as in process class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>An in process sales quote.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Quoting.SalesQuote.InProcess"),
     Route("/Providers/Quoting/Actions/InProcess/ID/{ID}", "PATCH",
         Summary = "Marks the specified Quote as In Process")]
    public class SetSalesQuoteAsInProcess : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
