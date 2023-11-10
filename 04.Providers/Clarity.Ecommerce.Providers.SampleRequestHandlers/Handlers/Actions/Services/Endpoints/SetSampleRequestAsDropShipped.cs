// <copyright file="SetSampleRequestAsDropShipped.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sample request as drop shipped class</summary>
#pragma warning disable CS1584,CS0081,CS1658 // XML comment has syntactically incorrect cref attribute
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A drop ship sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{PurchaseOrderModel}}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Sampling/Actions/DropShipped/{ID}", "PATCH",
         Summary = "A Purchase Order will be created with this order's line items where a Vendor can be selected that"
             + " allows Drop Shipping. The order will be set to 'Shipped from Vendor' status. An email notification"
             + " will be sent to the customer.")]
    public class SetSampleRequestAsDropShipped : ImplementsIDBase, IReturn<CEFActionResponse<PurchaseOrderModel>>
    {
    }
}
