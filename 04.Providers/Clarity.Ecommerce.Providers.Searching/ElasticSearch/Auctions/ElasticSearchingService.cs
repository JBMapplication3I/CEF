// <copyright file="ElasticSearchingService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic search service. auctions class</summary>
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

    /// <summary>An index auctions.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Providers/Searching/Auctions/Index", "GET",
            Summary = "Calls the searching provider and re-indexes the auctions in the database")]
    public class IndexAuctions : IReturn<CEFActionResponse>
    {
    }

    /// <summary>A suggest auction catalog with provider.</summary>
    /// <seealso cref="AuctionCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_AuctionSuggestOption}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Auctions/Suggest", "GET",
            Summary = "Search the auction catalog. Returns a range of data")]
    public class SuggestAuctionCatalogWithProvider
        : AuctionCatalogSearchForm,
            IReturn<List<AuctionSuggestResult>>
    {
    }

    /// <summary>A search auction catalog with provider.</summary>
    /// <seealso cref="AuctionCatalogSearchForm"/>
    /// <seealso cref="IReturn{AuctionSearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Auctions/Query", "GET",
            Summary = "Search the auction catalog. Returns a range of data")]
    public class SearchAuctionCatalogWithProvider
        : AuctionCatalogSearchForm,
            IReturn<AuctionSearchViewModel>
    {
    }

    public partial class ElasticSearchingService
    {
        /// <summary>A GET handler for the <seealso cref="IndexAuctions" /> endpoint.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(IndexAuctions _)
        {
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                    .IndexAsync(
                        contextProfileName: ServiceContextProfileName,
                        index: ElasticSearchingProviderConfig.SearchingAuctionIndex,
                        ct: default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync($"{nameof(IndexAuctions)}.Error", ex.Message, ex, null).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>A GET handler for the <seealso cref="SuggestAuctionCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(SuggestAuctionCatalogWithProvider request)
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

        /// <summary>A GET handler for the <seealso cref="SearchAuctionCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object?> Get(SearchAuctionCatalogWithProvider request)
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
                            .GetSearchResultsFromProviderAsync<AuctionSearchViewModel, AuctionCatalogSearchForm, AuctionIndexableModel>(
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
