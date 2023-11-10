// <copyright file="UpdateSalesReturnFromStorefront.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the update sales return from storefront class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>An update sales return.</summary>
    /// <seealso cref="SalesReturnModel"/>
    /// <seealso cref="IReturn{CEFActionREsponse_int}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate, RequiredPermission("Returning.SalesReturn.Update"),
     Route("/Providers/Returning/Actions/UpdateSalesReturnFromStorefront", "PUT",
         Summary = "Use to update a sales return")]
    public class UpdateSalesReturnFromStorefront : SalesReturnModel, IReturn<CEFActionResponse<int>>
    {
    }
}
