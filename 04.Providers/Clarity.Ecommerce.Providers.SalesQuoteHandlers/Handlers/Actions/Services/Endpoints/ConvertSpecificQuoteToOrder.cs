// <copyright file="ConvertSpecificQuoteToOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the convert specific quote to order class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A convert specific quote to order.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Quoting.SalesQuote.Approve"),
     Route("/Providers/Quoting/Actions/Specific/ConvertToOrder/{ID}", "POST",
         Summary = "As an administrator, convert a quote to an order on behalf of the customer. This action also marks the quote Approved.")]
    public class ConvertSpecificQuoteToOrder : ImplementsIDBase, IReturn<CEFActionResponse<(int orderID, int? invoiceID)>>
    {
    }
}
