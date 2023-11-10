// <copyright file="ElasticSearchService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic search service class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Providers.Searching;
    using JetBrains.Annotations;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>An index products.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Products/Product/Index", "GET",
         Summary = "Calls the searching provider and re-indexes the products in the database")]
    public class IndexProducts : IReturn<bool>
    {
    }

    /// <summary>A suggest product catalog with provider.</summary>
    /// <seealso cref="ProductCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_ProductSuggestOption}"/>
    [PublicAPI, UsedInStorefront,
     Route("/ProductCatalog/SuggestWithProvider", "GET",
        Summary = "Search the product catalog. Returns a range of data")]
    public class SuggestProductCatalogWithProvider : ProductCatalogSearchForm, IReturn<List<ProductSuggestOption>>
    {
    }

    /// <summary>A search product catalog with provider.</summary>
    /// <seealso cref="ProductCatalogSearchForm"/>
    /// <seealso cref="IReturn{ProductSearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
     Route("/ProductCatalog/SearchWithProvider", "GET",
        Summary = "Search the product catalog. Returns a range of data")]
    public class SearchProductCatalogWithProvider : ProductCatalogSearchForm, IReturn<ProductSearchViewModel>
    {
    }

#if STORES
    /// <summary>An index stores.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Stores/Store/Index", "GET",
        Summary = "Calls the searching provider and re-indexes the stores in the database")]
    public class IndexStores : IReturn<bool>
    {
    }

    /// <summary>A search store catalog with provider.</summary>
    /// <seealso cref="StoreCatalogSearchForm"/>
    /// <seealso cref="IReturn{StoreSearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
     Route("/Stores/SearchWithProvider", "POST", Priority = 1,
         Summary = "Search the store catalog. Returns a range of data")]
    public class SearchStoreCatalogWithProvider : StoreCatalogSearchForm, IReturn<StoreSearchViewModel>
    {
    }

    /// <summary>A suggest store catalog with provider.</summary>
    /// <seealso cref="StoreCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_StoreSuggestOption}"/>
    [PublicAPI, UsedInStorefront,
     Route("/Stores/SuggestWithProvider", "POST", Priority = 1,
         Summary = "Search the store catalog. Returns a range of data")]
    public class SuggestStoreCatalogWithProvider : StoreCatalogSearchForm, IReturn<List<StoreSuggestOption>>
    {
    }
#endif

    /// <summary>An elastic search service.</summary>
    /// <seealso cref="ClarityEcommerceServiceBase"/>
    [PublicAPI]
    public class ElasticSearchService : ClarityEcommerceServiceBase
    {
        /// <summary>A GET handler for the <seealso cref="IndexProducts" /> endpoint.</summary>
        /// <param name="_">The  to get.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        public async Task<object> Get(IndexProducts _)
        {
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(contextProfileName: null)
                    .IndexAsync(
                        contextProfileName: null,
                        index: ElasticSearchingProviderConfig.SearchingProductIndex,
                        ct: default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("IndexProducts Error", ex.Message, ex, null).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>A GET handler for the <seealso cref="SuggestProductCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}</returns>
        public async Task<object> Get(SuggestProductCatalogWithProvider request)
        {
            if (GetSession()?.IsAuthenticated == true)
            {
                request.RolesAny = GetSession().Roles.ToArray();
            }
            return await RegistryLoaderWrapper.GetSearchingProvider(contextProfileName: null)
                .GetProductSuggestionsFromProviderAsync(
                    form: request,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        /// <summary>A GET handler for the <seealso cref="SearchProductCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request to get.</param>
        /// <returns>A Task{object}</returns>
        public async Task<object> Get(SearchProductCatalogWithProvider request)
        {
            var pricingFactoryContext = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            if (Contract.CheckValidID(request.StoreID))
            {
                pricingFactoryContext.StoreID = request.StoreID;
            }
            if (Contract.CheckValidID(request.BrandID))
            {
                pricingFactoryContext.BrandID = request.BrandID;
            }
            if (GetSession()?.IsAuthenticated == true)
            {
                request.RolesAny = GetSession().Roles.ToArray();
            }
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => RegistryLoaderWrapper.GetSearchingProvider(contextProfileName: null)
                        .GetProductSearchResultsFromProviderAndMapEachWithAdditionalDataLastModifiedAsync(
                            form: request,
                            contextProfileName: null,
                            roles: pricingFactoryContext.UserRoles),
                    async () =>
                    {
                        var productSearchViewModel = await RegistryLoaderWrapper.GetSearchingProvider(contextProfileName: null)
                            .GetProductSearchResultsFromProviderAndMapEachWithAdditionalDataAsync(
                                form: request,
                                contextProfileName: null,
                                roles: pricingFactoryContext.UserRoles)
                            .ConfigureAwait(false);
                        productSearchViewModel.Form.RolesAny = null;
                        return productSearchViewModel;
                    },
                    varyByUser: true)
                .ConfigureAwait(false);
        }

#if STORES
        public async Task<object> Post(SuggestStoreCatalogWithProvider request)
        {
            return await Workflows.Stores.GetStoreSuggestionsFromProviderAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        public async Task<object> Post(SearchStoreCatalogWithProvider request)
        {
            return await Workflows.Stores.GetStoreSearchResultsFromProviderAndMapEachWithAdditionalDataAsync(
                    request,
                    true,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object> Get(IndexStores _)
        {
            // NOTE: Never cached, admins only
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(contextProfileName: null)
                    .IndexAsync(
                        contextProfileName: null,
                        index: ElasticSearchingProviderConfig.SearchingStoreIndex,
                        default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("IndexStores Error", ex.Message, ex, null).ConfigureAwait(false);
                return false;
            }
        }
#endif
    }

    /// <summary>An elasticsearch feature.</summary>
    /// <seealso cref="IPlugin"/>
    [PublicAPI]
    public class ElasticSearchFeature : IPlugin
    {
        /// <summary>Registers this ElasticSearchFeature.</summary>
        /// <param name="appHost">The application host.</param>
        public void Register(IAppHost appHost)
        {
            // This function intentionally left blank
        }
    }
}
