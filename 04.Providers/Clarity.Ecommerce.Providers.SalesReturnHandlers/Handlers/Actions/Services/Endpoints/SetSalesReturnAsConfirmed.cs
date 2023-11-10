// <copyright file="SetSalesReturnAsConfirmed.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales return as confirmed class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A confirm sales return.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Returning.SalesReturn.Confirm"),
     Route("/Providers/Returning/Actions/Confirm/{ID}", "PATCH",
        Summary = "The return items each have sufficient stock and will be allocated against their stock (reducing"
            + " each). The return status will be set to 'Confirmed'. An email notification will be sent to the"
            + " customer.")]
    public class SetSalesReturnAsConfirmed : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
