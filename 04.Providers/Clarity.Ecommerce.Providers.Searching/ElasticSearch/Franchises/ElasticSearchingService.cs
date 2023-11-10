// <copyright file="ElasticSearchingService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic search service. franchises class</summary>
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

    /// <summary>An index franchises.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Providers/Searching/Franchises/Index", "GET",
            Summary = "Calls the searching provider and re-indexes the franchises in the database")]
    public class IndexFranchises : IReturn<CEFActionResponse>
    {
    }

    /// <summary>A suggest franchise catalog with provider.</summary>
    /// <seealso cref="FranchiseCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_FranchiseSuggestOption}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Franchises/Suggest", "GET",
            Summary = "Search the franchise catalog. Returns a range of data")]
    public class SuggestFranchiseCatalogWithProvider
        : FranchiseCatalogSearchForm,
            IReturn<List<FranchiseSuggestResult>>
    {
    }

    /// <summary>A search franchise catalog with provider.</summary>
    /// <seealso cref="FranchiseCatalogSearchForm"/>
    /// <seealso cref="IReturn{FranchiseSearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Franchises/Query", "GET",
            Summary = "Search the franchise catalog. Returns a range of data")]
    public class SearchFranchiseCatalogWithProvider
        : FranchiseCatalogSearchForm,
            IReturn<FranchiseSearchViewModel>
    {
    }

    public partial class ElasticSearchingService
    {
        /// <summary>A GET handler for the <seealso cref="IndexFranchises" /> endpoint.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(IndexFranchises _)
        {
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                    .IndexAsync(
                        contextProfileName: ServiceContextProfileName,
                        index: ElasticSearchingProviderConfig.SearchingFranchiseIndex,
                        ct: default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync($"{nameof(IndexFranchises)}.Error", ex.Message, ex, null).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>A GET handler for the <seealso cref="SuggestFranchiseCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(SuggestFranchiseCatalogWithProvider request)
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

        /// <summary>A GET handler for the <seealso cref="SearchFranchiseCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object?> Get(SearchFranchiseCatalogWithProvider request)
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
                            .GetSearchResultsFromProviderAsync<FranchiseSearchViewModel, FranchiseCatalogSearchForm, FranchiseIndexableModel>(
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
