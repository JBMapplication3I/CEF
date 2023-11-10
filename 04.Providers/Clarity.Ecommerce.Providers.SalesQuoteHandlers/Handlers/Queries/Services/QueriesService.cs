// <copyright file="QueriesService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queries service class</summary>
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Queries.Services
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Endpoints;
    using Endpoints.DTOs;
    using Interfaces.Models;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>A sales quote queries service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SalesQuoteQueriesService : ClarityEcommerceServiceBase
    {
        /// <summary>Get handler for <see cref="GetSecureSalesQuote"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{ISalesQuoteModel}"/>.</returns>
        public async Task<object?> Get(GetSecureSalesQuote request)
        {
            await ThrowIfNoRightsToRecordSalesQuoteAsync(request.ID).ConfigureAwait(false);
            var provider = RegistryLoaderWrapper.GetSalesQuoteQueriesProvider(contextProfileName: ServiceContextProfileName);
            if (provider is null)
            {
                return null;
            }
            return await provider.GetRecordSecurelyAsync(
                    id: request.ID,
                    accountIDs: CurrentAccountAndOneLevelDownAssociatedOrThrow401,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Get handler for <see cref="GetDiscountsForQuote"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{DiscountsForQuote}"/>.</returns>
        public async Task<object?> Get(GetDiscountsForQuote request)
        {
            Contract.RequiresValidID(
                await Workflows.SalesQuotes.CheckExistsAsync(
                        Contract.RequiresValidID(request.ID),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false));
            var result = new DiscountsForQuote();
            var topLevelSearch = new AppliedSalesQuoteDiscountSearchModel
            {
                Active = true,
                AsListing = true,
                MasterID = request.ID,
            };
            var (results, _, _) = await Workflows.AppliedSalesQuoteDiscounts.SearchAsync(
                    search: topLevelSearch,
                    asListing: true,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            result.Discounts = results.Cast<AppliedSalesQuoteDiscountModel>().ToList();
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var innerSearch = new AppliedSalesQuoteItemDiscountSearchModel
            {
                Active = true,
                AsListing = true,
                MasterIDs = await context.SalesQuoteItems
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterSalesItemsByMasterID<SalesQuoteItem, AppliedSalesQuoteItemDiscount, SalesQuoteItemTarget>(request.ID)
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false),
            };
            var innerDiscounts = await Workflows.AppliedSalesQuoteItemDiscounts.SearchAsync(
                    search: innerSearch,
                    asListing: true,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            result.ItemDiscounts = innerDiscounts.results.Cast<AppliedSalesQuoteItemDiscountModel>().ToList();
            return result;
        }

        /// <summary>Post handler for <see cref="GetCurrentUserSalesQuotes"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesQuotePagedResults}"/>.</returns>
        public async Task<object?> Post(GetCurrentUserSalesQuotes request)
        {
            request.Active = true;
            request.UserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<ISalesQuoteModel, SalesQuoteModel, ISalesQuoteSearchModel, SalesQuotePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesQuotes)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="GetCurrentAccountSalesQuotes"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesQuotePagedResults}"/>.</returns>
        public async Task<object?> Post(GetCurrentAccountSalesQuotes request)
        {
            request.Active = true;
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false);
            return await GetPagedResultsAsync<ISalesQuoteModel, SalesQuoteModel, ISalesQuoteSearchModel, SalesQuotePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesQuotes)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="AdminGetSalesQuotesForPortal"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesQuotePagedResults}"/>.</returns>
        public async Task<object?> Post(AdminGetSalesQuotesForPortal request)
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
            return await GetPagedResultsAsync<ISalesQuoteModel, SalesQuoteModel, ISalesQuoteSearchModel, SalesQuotePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesQuotes)
                .ConfigureAwait(false);
        }
    }
}
