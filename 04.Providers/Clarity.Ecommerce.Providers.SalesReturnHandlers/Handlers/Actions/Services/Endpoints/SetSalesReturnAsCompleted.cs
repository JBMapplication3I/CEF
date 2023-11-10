// <copyright file="SetSalesReturnAsCompleted.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales return as completed class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A complete sales return.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
     Authenticate, RequiredPermission("Returning.SalesReturn.Complete"),
     Route("/Providers/Returning/Actions/Complete/{ID}", "PATCH",
        Summary = "The return will be set to 'Completed' status, no further modifications will be allowed. An email"
            + " notification will be sent to the customer.")]
    public class SetSalesReturnAsCompleted : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
