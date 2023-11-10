// <copyright file="SetSampleRequestAsShipped.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sample request as shipped class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A ship sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Sampling/Actions/Shipped/{ID}", "PATCH",
         Summary = "The order will be set to 'Shipped' status. An email notification will be sent to the customer.")]
    public class SetSampleRequestAsShipped : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
