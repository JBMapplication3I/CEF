// <copyright file="CreateSalesReturnAsAdmin.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create sales return as admin class</summary>
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>An update sales return.</summary>
    /// <seealso cref="SalesReturnModel"/>
    /// <seealso cref="IReturn{SalesReturnModel}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate, RequiredPermission("Returning.SalesReturn.Create"),
     Route("/Providers/Returning/Actions/CreateSalesReturn", "POST",
         Summary = "Use to create a sales return admin side")]
    public class CreateSalesReturnAsAdmin : SalesReturnModel, IReturn<CEFActionResponse<int>>
    {
    }
}
