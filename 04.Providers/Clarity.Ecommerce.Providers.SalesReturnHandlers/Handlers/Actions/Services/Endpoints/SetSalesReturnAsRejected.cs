// <copyright file="SetSalesReturnAsRejected.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales return as rejected class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A reject sales return.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Returning.SalesReturn.Reject"),
     Route("/Providers/Returning/Actions/Reject/{ID}", "PATCH",
        Summary = "The return will be set to 'Rejected' status, no further modifications will be allowed. An email"
            + " notification will be sent to the customer.")]
    public class SetSalesReturnAsRejected : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
