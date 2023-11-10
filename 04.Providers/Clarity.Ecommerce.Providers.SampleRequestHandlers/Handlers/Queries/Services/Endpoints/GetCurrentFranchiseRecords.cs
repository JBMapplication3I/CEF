// <copyright file="GetCurrentFranchiseRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current franchise records class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current franchise sample requests.</summary>
    /// <seealso cref="SampleRequestSearchModel"/>
    /// <seealso cref="IReturn{SampleRequestPagedResults}"/>
    [PublicAPI, UsedInFranchiseAdmin,
        Authenticate,
        Route("/Providers/Sampling/Queries/RecordsForCurrentFranchise", "POST",
            Summary = "Use to get history of sample requests for the current franchise")]
    public class AdminGetSampleRequestsForFranchise : SampleRequestSearchModel, IReturn<SampleRequestPagedResults>
    {
    }
}
