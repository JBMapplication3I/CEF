// <copyright file="ElasticSearchingService.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic search service. products class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Providers.Searching;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Service;
    using ServiceStack;
    using Utilities;

    /// <summary>An index products.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Providers/Searching/Products/Index", "GET",
            Summary = "Calls the searching provider and re-indexes the products in the database")]
    public class IndexProducts : IReturn<CEFActionResponse>
    {
    }

    /// <summary>A suggest product catalog with provider.</summary>
    /// <seealso cref="ProductCatalogSearchForm"/>
    /// <seealso cref="IReturn{List_ProductSuggestOption}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Products/Suggest", "GET",
            Summary = "Search the product catalog. Returns a range of data")]
    public class SuggestProductCatalogWithProvider
        : ProductCatalogSearchForm,
            IReturn<List<ProductSuggestResult>>
    {
    }

    /// <summary>A search product catalog with provider.</summary>
    /// <seealso cref="ProductCatalogSearchForm"/>
    /// <seealso cref="IReturn{ProductSearchViewModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Providers/Searching/Products/Query", "GET",
            Summary = "Search the product catalog. Returns a range of data")]
    public class SearchProductCatalogWithProvider
        : ProductCatalogSearchForm,
            IReturn<ProductSearchViewModel>
    {
    }

    public partial class ElasticSearchingService
    {
        /// <summary>A GET handler for the <seealso cref="IndexProducts" /> endpoint.</summary>
        /// <param name="_">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(IndexProducts _)
        {
            try
            {
                await RegistryLoaderWrapper.GetSearchingProvider(ServiceContextProfileName)!
                    .IndexAsync(
                        contextProfileName: ServiceContextProfileName,
                        index: ElasticSearchingProviderConfig.SearchingProductIndex,
                        ct: default)
                    .ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync($"{nameof(IndexProducts)}.Error", ex.Message, ex, null).ConfigureAwait(false);
                throw;
            }
        }

        /// <summary>A GET handler for the <seealso cref="SuggestProductCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object> Get(SuggestProductCatalogWithProvider request)
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

        /// <summary>A GET handler for the <seealso cref="SearchProductCatalogWithProvider" /> endpoint.</summary>
        /// <param name="request">The request.</param>
        /// <returns>A Task{object}.</returns>
        public async Task<object?> Get(SearchProductCatalogWithProvider request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            request.Query = request.Query?.ToLower();
            if (request.AttributesAll?.ContainsKey("Size") == true)
            {
                for (var i = 0; i < request.AttributesAll["Size"].Length; i++)
                {
                    request.AttributesAll["Size"][i] = request.AttributesAll["Size"][i].Replace("\\", "\"");
                }
            }
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
            if (GetSession()?.IsAuthenticated == true
                && CEFConfigDictionary.FilterByAccountRoles
                && Contract.CheckValidKey(request.FilterByCurrentAccountRoles))
            {
                if (Contract.CheckValidID(CurrentAccountID))
                {
                    request.RolesAny = new string[] { request.FilterByCurrentAccountRoles!, };
                }
            }
            else
            {
                //var allRoles = await context.Roles.Select(x => x.Name).ToListAsync().ConfigureAwait(false);
                //allRoles.Add("Anonymous");
                request.RolesAny = new string[] { "Anonymous", };
            }
            //request.PriceListsAny = await context.AccountPricePoints
            //    .Where(x => x.MasterID == CurrentAccountIDOrThrow401)
            //    .Select(x => x.Slave!.Name!)
            //    .Distinct()
            //    .ToListAsync()
            //    .ConfigureAwait(false);
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
                        .GetSearchResultsFromProviderAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>(
                            form: request,
                            contextProfileName: ServiceContextProfileName,
                            roles: pricingFactoryContext.UserRoles)
                        .ConfigureAwait(false);
                    // using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
                    // await Workflows.Products.CleanProductsAsync(viewModel.ResultIDs, context).ConfigureAwait(false);
                    if (!ElasticSearchingProviderConfig.SearchingProductIndexResultsIncludeSearchDocuments)
                    {
                        viewModel.Documents = null;
                    }
                    viewModel.Form!.RolesAny = null;
                    return viewModel;
                },
                varyByUser: true,
                noCache: CEFConfigDictionary.FilterByAccountRoles ? 1 : 0)
            .ConfigureAwait(false);
        }
    }
}
