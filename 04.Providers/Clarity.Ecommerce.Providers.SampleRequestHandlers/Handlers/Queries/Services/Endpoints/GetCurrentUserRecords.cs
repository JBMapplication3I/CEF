// <copyright file="GetCurrentUserRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current user records class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current user sample requests.</summary>
    /// <seealso cref="SampleRequestSearchModel"/>
    /// <seealso cref="IReturn{SampleRequestPagedResults}"/>
    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Providers/Sampling/Queries/RecordsForCurrentUser", "POST",
            Summary = "Use to get history of sample requests for the current user")]
    public class GetCurrentUserSampleRequests : SampleRequestSearchModel, IReturn<SampleRequestPagedResults>
    {
    }
}
