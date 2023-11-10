// <copyright file="ElasticSearchingService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic search service. lots class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Providers.Searching;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>An index lots.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Providers/Searching/Lots/Index", "GET",
            Summary = "Calls the searching provider and re-indexes the lots in the database")]
    public class IndexLots : IReturn<CEFActionResponse>
    {
    }

    /// <summary>A suggest lot catalog with provider.</summary>
    /// <seealso cref="LotCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_LotSuggestOption}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Lots/Suggest", "GET",
            Summary = "Search the lot catalog. Returns a range of data")]
    public class SuggestLotCatalogWithProvider
        : LotCatalogSearchForm,
            IReturn<List<LotSuggestResult>>
    {
    }

    /// <summary>A search lot catalog with provider.</summary>
    /// <seealso cref="LotCatalogSearchForm"/>
    /// <seealso cref="IReturn{LotSearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Lots/Query", "GET",
            Summary = "Search the lot catalog. Returns a range of data")]
    public class SearchLotCatalogWithProvider
        : LotCatalogSearchForm,
            IReturn<LotSearchViewModel>
    {
    }

    public partial class ElasticSearchingService
    {
        /// <summary>A GET handler for the <seealso cref="IndexLots" /> endpoint.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(IndexLots _)
        {
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                    .IndexAsync(
                        contextProfileName: ServiceContextProfileName,
                        index: ElasticSearchingProviderConfig.SearchingLotIndex,
                        ct: default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync($"{nameof(IndexLots)}.Error", ex.Message, ex, null).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>A GET handler for the <seealso cref="SuggestLotCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(SuggestLotCatalogWithProvider request)
        {
            if (GetSession()?.IsAuthenticated == true)
            {
                request.RolesAny = GetSession().Roles.ToArray();
            }
            return await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                .GetAllSuggestionsFromProviderAsync(
                    form: request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>A GET handler for the <seealso cref="SearchLotCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object?> Get(SearchLotCatalogWithProvider request)
        {
            var pricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            if (Contract.CheckValidID(request.BrandID))
            {
                pricingFactoryContext.BrandID = request.BrandID;
            }
            if (Contract.CheckValidID(request.FranchiseID))
            {
                pricingFactoryContext.FranchiseID = request.FranchiseID;
            }
            if (Contract.CheckValidID(request.StoreID))
            {
                pricingFactoryContext.StoreID = request.StoreID;
            }
            if (GetSession()?.IsAuthenticated == true)
            {
                request.RolesAny = GetSession().Roles.ToArray();
            }
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                        .GetAllSearchResultsAsViewModelLastModifiedAsync(
                            form: request,
                            contextProfileName: ServiceContextProfileName,
                            roles: pricingFactoryContext.UserRoles),
                    async () =>
                    {
                        var viewModel = await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                            .GetSearchResultsFromProviderAsync<LotSearchViewModel, LotCatalogSearchForm, LotIndexableModel>(
                                form: request,
                                contextProfileName: ServiceContextProfileName,
                                roles: pricingFactoryContext.UserRoles)
                            .ConfigureAwait(false);
                        viewModel.Form!.RolesAny = null;
                        return viewModel;
                    },
                    varyByUser: true)
                .ConfigureAwait(false);
        }
    }
}
