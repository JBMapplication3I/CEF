// <copyright file="ElasticSearchingService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic search service. stores class</summary>
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

    /// <summary>An index stores.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Providers/Searching/Stores/Index", "GET",
            Summary = "Calls the searching provider and re-indexes the stores in the database")]
    public class IndexStores : IReturn<CEFActionResponse>
    {
    }

    /// <summary>A suggest store catalog with provider.</summary>
    /// <seealso cref="StoreCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_StoreSuggestOption}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Stores/Suggest", "GET",
            Summary = "Search the store catalog. Returns a range of data")]
    public class SuggestStoreCatalogWithProvider
        : StoreCatalogSearchForm,
            IReturn<List<StoreSuggestResult>>
    {
    }

    /// <summary>A search store catalog with provider.</summary>
    /// <seealso cref="StoreCatalogSearchForm"/>
    /// <seealso cref="IReturn{StoreSearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Stores/Query", "GET",
            Summary = "Search the store catalog. Returns a range of data")]
    public class SearchStoreCatalogWithProvider
        : StoreCatalogSearchForm,
            IReturn<StoreSearchViewModel>
    {
    }

    public partial class ElasticSearchingService
    {
        /// <summary>A GET handler for the <seealso cref="IndexStores" /> endpoint.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(IndexStores _)
        {
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                    .IndexAsync(
                        contextProfileName: ServiceContextProfileName,
                        index: ElasticSearchingProviderConfig.SearchingStoreIndex,
                        ct: default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync($"{nameof(IndexStores)}.Error", ex.Message, ex, null).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>A GET handler for the <seealso cref="SuggestStoreCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(SuggestStoreCatalogWithProvider request)
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

        /// <summary>A GET handler for the <seealso cref="SearchStoreCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object?> Get(SearchStoreCatalogWithProvider request)
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
                            .GetSearchResultsFromProviderAsync<StoreSearchViewModel, StoreCatalogSearchForm, StoreIndexableModel>(
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
