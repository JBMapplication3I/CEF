// <copyright file="ManuallyRefundSalesReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manually refund sales return class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A manual refund sales return.</summary>
    /// <seealso cref="SalesReturnModel"/>
    /// <seealso cref="IReturn{SalesReturnModel}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Returning.SalesReturn.ManualRefund"),
     Route("/Providers/Returning/Actions/ManuallyRefund", "POST", Priority = 1,
        Summary = "Use to manually refund a sales return")]
    public class ManuallyRefundSalesReturn : SalesReturnModel, IReturn<CEFActionResponse>
    {
    }
}
