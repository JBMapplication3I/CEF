// <copyright file="GetDiscountsForQuote.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get discounts for quote class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Service;
    using ServiceStack;

    /// <summary>A get discounts for quote.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{DiscountsForQuote}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin,
     Route("/Providers/Quoting/Queries/Secured/DiscountsFor/{ID}", "GET",
         Summary = "Use to get the discounts for the top level and item levels at the same time.")]
    public class GetDiscountsForQuote : ImplementsIDBase, IReturn<DTOs.DiscountsForQuote>
    {
    }
}
