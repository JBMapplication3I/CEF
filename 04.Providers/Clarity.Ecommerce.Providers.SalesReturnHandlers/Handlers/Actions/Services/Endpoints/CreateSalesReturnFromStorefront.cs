// <copyright file="CreateSalesReturnFromStorefront.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create store front sales return class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>Creates a return from the storefront.</summary>
    /// <seealso cref="SalesReturnModel"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
     Authenticate, RequiredPermission("Returning.SalesReturn.CreateFromStorefront"),
     Route("/Providers/Returning/Actions/CreateSalesReturnFromStorefront", "POST",
        Summary = "Generates a RMA for each item. An email notification will be sent to the customer.")]
    public class CreateSalesReturnFromStorefront : SalesReturnModel, IReturn<CEFActionResponse<int>>
    {
    }
}
