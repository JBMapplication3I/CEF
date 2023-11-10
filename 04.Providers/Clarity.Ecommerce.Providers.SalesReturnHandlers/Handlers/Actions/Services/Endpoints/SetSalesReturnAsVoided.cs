// <copyright file="SetSalesReturnAsVoided.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales return as voided class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A void sales return.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Returning.SalesReturn.Void"),
     Route("/Providers/Returning/Actions/Void/{ID}", "PATCH",
        Summary = "Void the return. It will no longer be processed and will be visible on the Completed Returns view."
            + " An email notification will be sent to the customer.")]
    public class SetSalesReturnAsVoided : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
