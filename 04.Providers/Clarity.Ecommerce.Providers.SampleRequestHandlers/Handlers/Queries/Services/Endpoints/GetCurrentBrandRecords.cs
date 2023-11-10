// <copyright file="GetCurrentBrandRecords.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get current brand records class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get current brand sample requests.</summary>
    /// <seealso cref="SampleRequestSearchModel"/>
    /// <seealso cref="IReturn{SampleRequestPagedResults}"/>
    [PublicAPI, UsedInBrandAdmin,
        Authenticate,
        Route("/Providers/Sampling/Queries/RecordsForCurrentBrand", "POST",
            Summary = "Use to get history of sample requests for the current brand")]
    public class AdminGetSampleRequestsForBrand : SampleRequestSearchModel, IReturn<SampleRequestPagedResults>
    {
    }
}
