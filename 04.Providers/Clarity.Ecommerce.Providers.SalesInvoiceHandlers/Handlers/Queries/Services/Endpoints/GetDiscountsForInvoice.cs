// <copyright file="GetDiscountsForInvoice.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get discounts for invoice class</summary>
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Service;
    using ServiceStack;

    /// <summary>A get discounts for invoice.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{DiscountsForRecord}"/>
    [PublicAPI, UsedInStorefront, UsedInAdmin,
     Route("/Providers/Invoicing/Queries/Secured/DiscountsFor/{ID}", "GET",
         Summary = "Use to get the discounts for the top level and item levels at the same time.")]
    public class GetDiscountsForInvoice : ImplementsIDBase, IReturn<DTOs.DiscountsForInvoice>
    {
    }
}
