// <copyright file="SetSalesReturnAsShipped.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales return as shipped class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A ship sales return.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate, RequiredPermission("Returning.SalesReturn.Ship"),
     Route("/Providers/Returning/Actions/Shipped/{ID}", "PATCH",
        Summary = "The return will be set to 'Shipped' status. An email notification will be sent to the customer.")]
    public class SetSalesReturnAsShipped : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
