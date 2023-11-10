// <copyright file="QueriesService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queries service class</summary>
#pragma warning disable CA1822
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.SalesReturnHandlers.Queries.Services
{
    using System.Threading.Tasks;
    using Endpoints;
    using Interfaces.Models;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A sales return queries service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SalesReturnQueriesService : ClarityEcommerceServiceBase
    {
        /// <summary>Get handler for <see cref="GetSecureSalesReturn"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{ISalesReturnModel}"/>.</returns>
        public async Task<object?> Get(GetSecureSalesReturn request)
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
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="GetCurrentUserSalesReturns"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesReturnPagedResults}"/>.</returns>
        public async Task<object?> Post(GetCurrentUserSalesReturns request)
        {
            request.Active = true;
            request.UserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<ISalesReturnModel, SalesReturnModel, ISalesReturnSearchModel, SalesReturnPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesReturns)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="GetCurrentAccountSalesReturns"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesReturnPagedResults}"/>.</returns>
        public async Task<object?> Post(GetCurrentAccountSalesReturns request)
        {
            request.Active = true;
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false);
            return await GetPagedResultsAsync<ISalesReturnModel, SalesReturnModel, ISalesReturnSearchModel, SalesReturnPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesReturns)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="AdminGetSalesReturnsForPortal"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesReturnPagedResults}"/>.</returns>
        public async Task<object?> Post(AdminGetSalesReturnsForPortal request)
        {
            switch (CurrentAPIKind)
            {
                case Enums.APIKind.BrandAdmin:
                {
                    request.BrandID = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.FranchiseAdmin:
                {
                    request.FranchiseID = await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.StoreAdmin:
                {
                    request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.ManufacturerAdmin:
                case Enums.APIKind.VendorAdmin:
                default:
                {
                    throw HttpError.Forbidden("Invalid operation");
                }
            }
            request.Active = true;
            request.AsListing = true;
            return await GetPagedResultsAsync<ISalesReturnModel, SalesReturnModel, ISalesReturnSearchModel, SalesReturnPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesReturns)
                .ConfigureAwait(false);
        }

        /// <summary>Get handler for <see cref="IsSalesOrderReadyForReturn"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{CEFActionResponse}"/>.</returns>
        public async Task<object?> Get(IsSalesOrderReadyForReturn request)
        {
            return await RegistryLoaderWrapper.GetSalesReturnQueriesProvider(ServiceContextProfileName)!
                .ValidateSalesOrderReadyForReturnAsync(request.ID, ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
