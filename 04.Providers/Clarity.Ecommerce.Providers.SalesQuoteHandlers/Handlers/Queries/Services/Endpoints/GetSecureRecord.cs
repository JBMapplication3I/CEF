// <copyright file="GetSecureRecord.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get secure record class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get secure sales quote.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesQuoteModel}"/>
    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Providers/Quoting/Queries/Secured/{ID}", "GET",
            Summary = "Use to get a specific sales quote and check for ownership by the current Account.")]
    public class GetSecureSalesQuote : ImplementsIDBase, IReturn<SalesQuoteModel>
    {
    }
}
