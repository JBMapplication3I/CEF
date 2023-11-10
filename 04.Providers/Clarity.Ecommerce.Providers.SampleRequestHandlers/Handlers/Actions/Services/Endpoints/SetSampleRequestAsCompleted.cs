// <copyright file="SetSampleRequestAsCompleted.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sample request as completed class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A complete sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Sampling/Actions/Complete/{ID}", "PATCH",
         Summary = "The order will be set to 'Completed' status, no further modifications will be allowed. An email"
             + " notification will be sent to the customer.")]
    public class SetSampleRequestAsCompleted : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
