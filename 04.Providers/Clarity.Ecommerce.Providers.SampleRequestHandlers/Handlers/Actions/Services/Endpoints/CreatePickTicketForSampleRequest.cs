// <copyright file="CreatePickTicketForSampleRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create pick ticket for sample request class</summary>
#pragma warning disable CS1584,CS0081,CS1658 // XML comment has syntactically incorrect cref attribute
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using System.Collections.Generic;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A create pick ticket for sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{List{SalesItemBaseModel{IAppliedSampleRequestItemDiscountModel,AppliedSampleRequestItemDiscountModel}}}}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Sampling/Actions/CreatePickTicketFor/{ID}", "PATCH",
         Summary = "Creates a printable Pick Ticket for the Warehouse to locate products for the order. The order will"
             + " be set to the 'Processing' status.")]
    public class CreatePickTicketForSampleRequest
        : ImplementsIDBase,
            IReturn<CEFActionResponse<List<SalesItemBaseModel<IAppliedSampleRequestItemDiscountModel, AppliedSampleRequestItemDiscountModel>>>>
    {
    }
}
