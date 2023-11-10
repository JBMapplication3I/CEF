// <copyright file="QueriesService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queries service class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Queries.Services
{
    using System.Threading.Tasks;
    using Endpoints;
    using Interfaces.Models;
    using Models;
    using Service;

    /// <summary>A sample request queries service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SampleRequestQueriesService : ClarityEcommerceServiceBase
    {
        /// <summary>Get handler for <see cref="GetSecureSampleRequest"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{ISampleRequestModel}"/>.</returns>
        public async Task<object?> Get(GetSecureSampleRequest request)
        {
            await ThrowIfNoRightsToRecordSalesReturnAsync(request.ID).ConfigureAwait(false);
            var provider = RegistryLoaderWrapper.GetSalesReturnQueriesProvider(ServiceContextProfileName);
            if (provider is null)
            {
                return null;
            }
            return provider.GetRecordSecurelyAsync(
                    id: request.ID,
                    accountIDs: CurrentAccountAndOneLevelDownAssociatedOrThrow401,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="GetCurrentUserSampleRequests"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SampleRequestPagedResults}"/>.</returns>
        public async Task<object?> Post(GetCurrentUserSampleRequests request)
        {
            request.Active = true;
            request.UserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<ISampleRequestModel, SampleRequestModel, ISampleRequestSearchModel, SampleRequestPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SampleRequests)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="GetCurrentAccountSampleRequests"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SampleRequestPagedResults}"/>.</returns>
        public async Task<object?> Post(GetCurrentAccountSampleRequests request)
        {
            request.Active = true;
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false);
            return await GetPagedResultsAsync<ISampleRequestModel, SampleRequestModel, ISampleRequestSearchModel, SampleRequestPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SampleRequests)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="AdminGetSampleRequestsForBrand"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SampleRequestPagedResults}"/>.</returns>
        public async Task<object?> Post(AdminGetSampleRequestsForBrand request)
        {
            request.Active = true;
            request.BrandID = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
            return await GetPagedResultsAsync<ISampleRequestModel, SampleRequestModel, ISampleRequestSearchModel, SampleRequestPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SampleRequests)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="AdminGetSampleRequestsForFranchise"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SampleRequestPagedResults}"/>.</returns>
        public async Task<object?> Post(AdminGetSampleRequestsForFranchise request)
        {
            request.Active = true;
            request.FranchiseID = await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false);
            return await GetPagedResultsAsync<ISampleRequestModel, SampleRequestModel, ISampleRequestSearchModel, SampleRequestPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SampleRequests)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="AdminGetSampleRequestsForStore"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SampleRequestPagedResults}"/>.</returns>
        public async Task<object?> Post(AdminGetSampleRequestsForStore request)
        {
            request.Active = true;
            request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
            return await GetPagedResultsAsync<ISampleRequestModel, SampleRequestModel, ISampleRequestSearchModel, SampleRequestPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SampleRequests)
                .ConfigureAwait(false);
        }
    }
}
