// <copyright file="ConvertQuoteToOrderForCurrentUser.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the convert quote to order for current user class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A convert quote to order for current user.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CheckoutResult}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate, RequiredPermission("Quoting.SalesQuote.Approve"),
     Route("/Providers/Quoting/Actions/CurrentUser/ConvertToOrder/{ID}", "POST",
         Summary = "As the customer, convert a quote to an order. This action also marks the quote Approved.")]
    public class ConvertQuoteToOrderForCurrentUser : ImplementsIDBase, IReturn<CEFActionResponse<(int orderID, int? invoiceID)>>
    {
    }
}
