// <copyright file="SetSampleRequestAsConfirmed.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sample request as confirmed class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A confirm sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Sampling/Actions/Confirm/{ID}", "PATCH",
         Summary = "The order items each have sufficient stock and will be allocated against their stock (reducing"
             + " each). The order status will be set to 'Confirmed'. An email notification will be sent to the"
             + " customer.")]
    public class SetSampleRequestAsConfirmed : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
