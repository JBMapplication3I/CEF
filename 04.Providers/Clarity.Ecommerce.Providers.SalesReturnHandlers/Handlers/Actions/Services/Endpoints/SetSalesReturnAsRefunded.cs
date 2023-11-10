// <copyright file="SetSalesReturnAsRefunded.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales return as refunded class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A refund sales return.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Returning.SalesReturn.Refund"),
     Route("/Providers/Returning/Actions/Refund/{ID}", "PATCH",
        Summary = "The return amount will be refunded. If successful, an email notification will be sent to the"
            + " customer.")]
    public class SetSalesReturnAsRefunded : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
