// <copyright file="GetCurrentStoreRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current store records class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current store sample requests.</summary>
    /// <seealso cref="SampleRequestSearchModel"/>
    /// <seealso cref="IReturn{SampleRequestPagedResults}"/>
    [PublicAPI, UsedInStoreAdmin,
        Authenticate,
        Route("/Providers/Sampling/Queries/RecordsForCurrentStore", "POST",
            Summary = "Use to get history of sample requests for the current store")]
    public class AdminGetSampleRequestsForStore : SampleRequestSearchModel, IReturn<SampleRequestPagedResults>
    {
    }
}
