// <copyright file="SetSampleRequestAsBackordered.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sample request as backordered class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A backorder sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Sampling/Actions/Backorder/{ID}", "PATCH",
         Summary = "The order items do not have sufficient stock. The order status will be set to 'Backordered'. An"
             + " email notification will be sent to the customer. A Purchase Order should be created and reference"
             + " this order by an Inventory Manager to refill stock.")]
    public class SetSampleRequestAsBackordered : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
