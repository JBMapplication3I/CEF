// <copyright file="SetSampleRequestAsVoided.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sample request as voided class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A void sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Sampling/Actions/Void/{ID}", "PATCH",
         Summary = "Void the request. It will no longer be processed and will be visible on the Completed Orders view."
             + " An email notification will be sent to the customer.")]
    public class SetSampleRequestAsVoided : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }
}
