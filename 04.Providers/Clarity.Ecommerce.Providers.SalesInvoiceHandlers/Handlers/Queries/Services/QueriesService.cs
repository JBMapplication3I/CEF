// <copyright file="QueriesService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the queries service class</summary>
#nullable enable
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Queries.Services
{
    using System.Collections.Generic;
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

    /// <summary>A sales invoice queries service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    public class SalesInvoiceQueriesService : ClarityEcommerceServiceBase
    {
        /// <summary>Get handler for <see cref="GetSecureSalesInvoice"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{ISalesInvoiceModel}"/>.</returns>
        public async Task<object?> Get(GetSecureSalesInvoice request)
        {
            await ThrowIfNoRightsToRecordSalesInvoiceAsync(request.ID).ConfigureAwait(false);
            var provider = RegistryLoaderWrapper.GetSalesInvoiceQueriesProvider(contextProfileName: ServiceContextProfileName);
            if (provider is null)
            {
                return null;
            }
            return await provider.GetRecordSecurelyAsync(
                    id: request.ID,
                    accountIDs: new List<int> { await LocalAdminAccountIDOrThrow401Async(CurrentAccountID).ConfigureAwait(false), },
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Get handler for <see cref="GetDiscountsForInvoice"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{DiscountsForRecord}"/>.</returns>
        public async Task<object> Get(GetDiscountsForInvoice request)
        {
            _ = Contract.RequiresValidID(
                await Workflows.SalesInvoices.CheckExistsAsync(
                        Contract.RequiresValidID(request.ID),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false));
            var result = new DiscountsForInvoice();
            var topLevelSearch = new AppliedSalesInvoiceDiscountSearchModel
            {
                Active = true,
                AsListing = true,
                MasterID = request.ID,
            };
            var (results, _, _) = await Workflows.AppliedSalesInvoiceDiscounts.SearchAsync(
                    search: topLevelSearch,
                    asListing: true,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            result.Discounts = results.Cast<AppliedSalesInvoiceDiscountModel>().ToList();
            using var context = RegistryLoaderWrapper.GetContext(null);
            var innerSearch = new AppliedSalesInvoiceItemDiscountSearchModel
            {
                Active = true,
                AsListing = true,
                MasterIDs = await context.SalesInvoiceItems
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterSalesItemsByMasterID<SalesInvoiceItem, AppliedSalesInvoiceItemDiscount, SalesInvoiceItemTarget>(request.ID)
                    .Select(x => x.ID)
                    .ToListAsync()
                    .ConfigureAwait(false),
            };
            var innerDiscounts = await Workflows.AppliedSalesInvoiceItemDiscounts.SearchAsync(
                    search: innerSearch,
                    asListing: true,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            result.ItemDiscounts = innerDiscounts.results.Cast<AppliedSalesInvoiceItemDiscountModel>().ToList();
            return result;
        }

        /// <summary>Post handler for <see cref="GetCurrentUserSalesInvoices"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesInvoicePagedResults}"/>.</returns>
        public async Task<object> Post(GetCurrentUserSalesInvoices request)
        {
            request.Active = true;
            request.UserID = CurrentUserIDOrThrow401;
            return await GetPagedResultsAsync<ISalesInvoiceModel, SalesInvoiceModel, ISalesInvoiceSearchModel, SalesInvoicePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesInvoices)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="GetCurrentAccountSalesInvoices"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesInvoicePagedResults}"/>.</returns>
        public async Task<object> Post(GetCurrentAccountSalesInvoices request)
        {
            request.Active = true;
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(request.AccountID ?? CurrentAccountIDOrThrow401).ConfigureAwait(false);
            return await GetPagedResultsAsync<ISalesInvoiceModel, SalesInvoiceModel, ISalesInvoiceSearchModel, SalesInvoicePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesInvoices)
                .ConfigureAwait(false);
        }

        /// <summary>Post handler for <see cref="AdminGetSalesInvoicesForPortal"/> request.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A <see cref="Task{SalesInvoicePagedResults}"/>.</returns>
        public async Task<object> Post(AdminGetSalesInvoicesForPortal request)
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
            return await GetPagedResultsAsync<ISalesInvoiceModel, SalesInvoiceModel, ISalesInvoiceSearchModel, SalesInvoicePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.SalesInvoices)
                .ConfigureAwait(false);
        }
    }
}
