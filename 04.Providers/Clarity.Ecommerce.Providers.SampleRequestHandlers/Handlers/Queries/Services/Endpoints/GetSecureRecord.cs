// <copyright file="GetSecureRecord.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get secure record class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get secure sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SampleRequestModel}"/>
    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Providers/Sampling/Queries/Secured/{ID}", "GET",
            Summary = "Use to get a specific sample request and check for ownership by the current Account.")]
    public class GetSecureSampleRequest : ImplementsIDBase, IReturn<SampleRequestModel>
    {
    }
}
