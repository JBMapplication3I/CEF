// <copyright file="SetSalesReturnAsCancelled.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales return as cancelled class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A validate sales return.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate, RequiredPermission("Returning.SalesReturn.Cancel"),
     Route("/Providers/Returning/Actions/Cancel/{ID}", "PATCH",
        Summary = "The return will be set to 'Canceled' status, no further modifications will be allowed. An email"
            + " notification will be sent to the customer.")]
    public class SetSalesReturnAsCancelled : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
