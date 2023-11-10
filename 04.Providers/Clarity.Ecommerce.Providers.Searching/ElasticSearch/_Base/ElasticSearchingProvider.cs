// <copyright file="ElasticSearchingProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the elastic searching provider class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using JSConfigs;
    using MoreLinq;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>An elastic searching provider.</summary>
    /// <seealso cref="SearchingProviderBase"/>
    public partial class ElasticSearchingProvider : SearchingProviderBase
    {
        /// <summary>(Immutable) all indexes.</summary>
        private const string AllIndexes = "all";

        private const string IndexPrefixForAuctions = "auction-search";
        private const string IndexPrefixForCategories = "category-search";
        private const string IndexPrefixForFranchises = "franchise-search";
        private const string IndexPrefixForLots = "lot-search";
        private const string IndexPrefixForManufacturers = "manufacturer-search";
        private const string IndexPrefixForProducts = "product-search";
        private const string IndexPrefixForStores = "store-search";
        private const string IndexPrefixForVendors = "vendor-search";

        /// <inheritdoc/>
        public override bool HasValidConfiguration =>
            ElasticSearchingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task IndexAsync(string? contextProfileName, string index, CancellationToken ct)
        {
            _ = Contract.RequiresValidKey(index, "The index name passed into the Elastic Searching Provider was null");
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForAuctions)) && ElasticSearchingProviderConfig.SearchingAuctionsEnabled)
            {
                await new AuctionIndexer().IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForCategories)) && ElasticSearchingProviderConfig.SearchingCategoriesEnabled)
            {
                await new CategoryIndexer().IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForFranchises)) && ElasticSearchingProviderConfig.SearchingFranchisesEnabled)
            {
                await new FranchiseIndexer().IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForLots)) && ElasticSearchingProviderConfig.SearchingLotsEnabled)
            {
                await new LotIndexer().IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForManufacturers)) && ElasticSearchingProviderConfig.SearchingManufacturersEnabled)
            {
                await new ManufacturerIndexer().IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
            if (/*(*/index == AllIndexes || index.StartsWith(IndexPrefixForProducts)/*) && CEFConfigDictionary.ProductsEnabled*/)
            {
                await new ProductIndexer().IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForStores)) && ElasticSearchingProviderConfig.SearchingStoresEnabled)
            {
                await new StoreIndexer().IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForVendors)) && ElasticSearchingProviderConfig.SearchingVendorsEnabled)
            {
                await new VendorIndexer().IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<TSuggestResult?>> SuggestionsAsync<TSearchForm, TSuggestResult>(
                TSearchForm form,
                string index,
                string? contextProfileName)
            where TSuggestResult : class
        {
            _ = Contract.RequiresValidKey(index, "The index name passed into the Elastic Searching Provider was null");
            var results = new List<TSuggestResult?>();
            //if ((index == AllIndexes || index.StartsWith(IndexPrefixForAuctions)) && ElasticSearchingProviderConfig.SearchingAuctionsEnabled)
            //{
            //    results.AddRange(
            //            (await AuctionSuggestModule.SuggestResultsAsync(
            //                (AuctionCatalogSearchForm)(SearchFormBase)form,
            //                index,
            //                contextProfileName)
            //            .ConfigureAwait(false))
            //        .Cast<TSuggestResult>());
            //}
            //if ((index == AllIndexes || index.StartsWith(IndexPrefixForCategories)) && ElasticSearchingProviderConfig.SearchingCategoriesEnabled)
            //{
            //    results.AddRange(
            //            (await CategorySuggestModule.SuggestResultsAsync(
            //                (CategoryCatalogSearchForm)(SearchFormBase)form,
            //                index,
            //                contextProfileName)
            //            .ConfigureAwait(false))
            //        .Cast<TSuggestResult>());
            //}
            //if ((index == AllIndexes || index.StartsWith(IndexPrefixForFranchises)) && ElasticSearchingProviderConfig.SearchingFranchisesEnabled)
            //{
            //    results.AddRange(
            //            (await FranchiseSuggestModule.SuggestResultsAsync(
            //                (FranchiseCatalogSearchForm)(SearchFormBase)form,
            //                index,
            //                contextProfileName)
            //            .ConfigureAwait(false))
            //        .Cast<TSuggestResult>());
            //}
            //if ((index == AllIndexes || index.StartsWith(IndexPrefixForLots)) && ElasticSearchingProviderConfig.SearchingLotsEnabled)
            //{
            //    results.AddRange(
            //            (await LotSuggestModule.SuggestResultsAsync(
            //                (LotCatalogSearchForm)(SearchFormBase)form,
            //                index,
            //                contextProfileName)
            //            .ConfigureAwait(false))
            //        .Cast<TSuggestResult>());
            //}
            //if ((index == AllIndexes || index.StartsWith(IndexPrefixForManufacturers)) && ElasticSearchingProviderConfig.SearchingManufacturersEnabled)
            //{
            //    results.AddRange(
            //            (await ManufacturerSuggestModule.SuggestResultsAsync(
            //                (ManufacturerCatalogSearchForm)(SearchFormBase)form,
            //                index,
            //                contextProfileName)
            //            .ConfigureAwait(false))
            //        .Cast<TSuggestResult>());
            //}
            if (/*(*/index == AllIndexes || index.StartsWith(IndexPrefixForProducts)/*) && CEFConfigDictionary.ProductsEnabled*/)
            {
                results.AddRange(
                        (await ProductSuggestModule.SuggestResultsAsync(
                            (ProductCatalogSearchForm)(SearchFormBase)form,
                            index,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Cast<TSuggestResult>());
            }
            //if ((index == AllIndexes || index.StartsWith(IndexPrefixForStores)) && ElasticSearchingProviderConfig.SearchingStoresEnabled)
            //{
            //    results.AddRange(
            //            (await StoreSuggestModule.SuggestResultsAsync(
            //                (StoreCatalogSearchForm)(SearchFormBase)form,
            //                index,
            //                contextProfileName)
            //            .ConfigureAwait(false))
            //        .Cast<TSuggestResult>());
            //}
            //if ((index == AllIndexes || index.StartsWith(IndexPrefixForVendors)) && ElasticSearchingProviderConfig.SearchingVendorsEnabled)
            //{
            //    results.AddRange(
            //            (await VendorSuggestModule.SuggestResultsAsync(
            //                (VendorCatalogSearchForm)(SearchFormBase)form,
            //                index,
            //                contextProfileName)
            //            .ConfigureAwait(false))
            //        .Cast<TSuggestResult>());
            //}
            return results;
        }

        /// <inheritdoc/>
        public override async Task<TSearchViewModel> QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
            TSearchForm form,
            string index,
            string? contextProfileName)
        {
            Contract.RequiresValidKey(index, "The index name passed into the Elastic Searching Provider was null");
            if (Contract.CheckInvalidID(EventStatusIDForNormal))
            {
                EventStatusIDForNormal = await Workflows.EventStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Normal",
                        byName: "Normal",
                        byDisplayName: "Normal",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (index.StartsWith(IndexPrefixForAuctions))
            {
                if (form is not AuctionCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for auction search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync(
                            "ElasticSearch.Auctions.Keyword",
                            asForm.Query!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForAuctionCatalogSearch))
                    {
                        EventTypeIDForAuctionCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Auction Catalog Search",
                                byName: "Auction Catalog Search",
                                byDisplayName: "Auction Catalog Search",
                                model: null,
                                context: context)
                            .ConfigureAwait(false);
                    }
                    await AddNewEventAndSaveAsync(
                            context: context,
                            name: "Auction Catalog Search Keyword",
                            typeID: EventTypeIDForAuctionCatalogSearch,
                            statusID: EventStatusIDForNormal,
                            asForm: asForm,
                            query: asForm.Query)
                        .ConfigureAwait(false);
                }
                return (await new AuctionSearchModule().SearchResultsAsync(asForm, contextProfileName).ConfigureAwait(false) as TSearchViewModel)!;
            }
            if (index.StartsWith(IndexPrefixForCategories))
            {
                if (form is not CategoryCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for category search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync(
                            "ElasticSearch.Categories.Keyword",
                            asForm.Query!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForCategoryCatalogSearch))
                    {
                        EventTypeIDForCategoryCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Category Catalog Search",
                                byName: "Category Catalog Search",
                                byDisplayName: "Category Catalog Search",
                                model: null,
                                context: context)
                            .ConfigureAwait(false);
                    }
                    await AddNewEventAndSaveAsync(
                            context: context,
                            name: "Category Catalog Search Keyword",
                            typeID: EventTypeIDForCategoryCatalogSearch,
                            statusID: EventStatusIDForNormal,
                            asForm: asForm,
                            query: asForm.Query)
                        .ConfigureAwait(false);
                }
                return (await new CategorySearchModule().SearchResultsAsync(asForm, contextProfileName).ConfigureAwait(false) as TSearchViewModel)!;
            }
            if (index.StartsWith(IndexPrefixForFranchises))
            {
                if (form is not FranchiseCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for franchise search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync(
                            "ElasticSearch.Franchises.Keyword",
                            asForm.Query!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForFranchiseCatalogSearch))
                    {
                        EventTypeIDForFranchiseCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Franchise Catalog Search",
                                byName: "Franchise Catalog Search",
                                byDisplayName: "Franchise Catalog Search",
                                model: null,
                                context: context)
                            .ConfigureAwait(false);
                    }
                    await AddNewEventAndSaveAsync(
                            context: context,
                            name: "Franchise Catalog Search Keyword",
                            typeID: EventTypeIDForFranchiseCatalogSearch,
                            statusID: EventStatusIDForNormal,
                            asForm: asForm,
                            query: asForm.Query)
                        .ConfigureAwait(false);
                }
                return (await new FranchiseSearchModule().SearchResultsAsync(asForm, contextProfileName).ConfigureAwait(false) as TSearchViewModel)!;
            }
            if (index.StartsWith(IndexPrefixForLots))
            {
                if (form is not LotCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for lot search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync(
                            "ElasticSearch.Lots.Keyword",
                            asForm.Query!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForLotCatalogSearch))
                    {
                        EventTypeIDForLotCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Lot Catalog Search",
                                byName: "Lot Catalog Search",
                                byDisplayName: "Lot Catalog Search",
                                model: null,
                                context: context)
                            .ConfigureAwait(false);
                    }
                    await AddNewEventAndSaveAsync(
                            context: context,
                            name: "Lot Catalog Search Keyword",
                            typeID: EventTypeIDForLotCatalogSearch,
                            statusID: EventStatusIDForNormal,
                            asForm: asForm,
                            query: asForm.Query)
                        .ConfigureAwait(false);
                }
                return (await new LotSearchModule().SearchResultsAsync(asForm, contextProfileName).ConfigureAwait(false) as TSearchViewModel)!;
            }
            if (index.StartsWith(IndexPrefixForManufacturers))
            {
                if (form is not ManufacturerCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for manufacturer search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync(
                            "ElasticSearch.Manufacturers.Keyword",
                            asForm.Query!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForManufacturerCatalogSearch))
                    {
                        EventTypeIDForManufacturerCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Manufacturer Catalog Search",
                                byName: "Manufacturer Catalog Search",
                                byDisplayName: "Manufacturer Catalog Search",
                                model: null,
                                context: context)
                            .ConfigureAwait(false);
                    }
                    await AddNewEventAndSaveAsync(
                            context: context,
                            name: "Manufacturer Catalog Search Keyword",
                            typeID: EventTypeIDForManufacturerCatalogSearch,
                            statusID: EventStatusIDForNormal,
                            asForm: asForm,
                            query: asForm.Query)
                        .ConfigureAwait(false);
                }
                return (await new ManufacturerSearchModule().SearchResultsAsync(asForm, contextProfileName).ConfigureAwait(false) as TSearchViewModel)!;
            }
            if (index.StartsWith(IndexPrefixForProducts))
            {
                if (form is not ProductCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for product search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync(
                            "ElasticSearch.Products.Keyword",
                            asForm.Query!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForProductCatalogSearch))
                    {
                        EventTypeIDForProductCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Product Catalog Search",
                                byName: "Product Catalog Search",
                                byDisplayName: "Product Catalog Search",
                                model: null,
                                context: context)
                            .ConfigureAwait(false);
                    }
                    await AddNewEventAndSaveAsync(
                            context: context,
                            name: "Product Catalog Search Keyword",
                            typeID: EventTypeIDForProductCatalogSearch,
                            statusID: EventStatusIDForNormal,
                            asForm: asForm,
                            query: asForm.Query)
                        .ConfigureAwait(false);
                }
                return (await new ProductSearchModule().SearchResultsAsync(asForm, contextProfileName).ConfigureAwait(false) as TSearchViewModel)!;
            }
            if (index.StartsWith(IndexPrefixForStores))
            {
                if (form is not StoreCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for store search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync(
                            "ElasticSearch.Stores.Keyword",
                            asForm.Query!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForStoreCatalogSearch))
                    {
                        EventTypeIDForStoreCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Store Catalog Search",
                                byName: "Store Catalog Search",
                                byDisplayName: "Store Catalog Search",
                                model: null,
                                context: context)
                            .ConfigureAwait(false);
                    }
                    await AddNewEventAndSaveAsync(
                            context: context,
                            name: "Store Catalog Search Keyword",
                            typeID: EventTypeIDForStoreCatalogSearch,
                            statusID: EventStatusIDForNormal,
                            asForm: asForm,
                            query: asForm.Query)
                        .ConfigureAwait(false);
                }
                return (await new StoreSearchModule().SearchResultsAsync(asForm, contextProfileName).ConfigureAwait(false) as TSearchViewModel)!;
            }
            // ReSharper disable once InvertIf
            if (index.StartsWith(IndexPrefixForVendors))
            {
                if (form is not VendorCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for vendor search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync(
                            "ElasticSearch.Vendors.Keyword",
                            asForm.Query!,
                            contextProfileName)
                        .ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForVendorCatalogSearch))
                    {
                        EventTypeIDForVendorCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Vendor Catalog Search",
                                byName: "Vendor Catalog Search",
                                byDisplayName: "Vendor Catalog Search",
                                model: null,
                                context: context)
                            .ConfigureAwait(false);
                    }
                    await AddNewEventAndSaveAsync(
                            context: context,
                            name: "Vendor Catalog Search Keyword",
                            typeID: EventTypeIDForVendorCatalogSearch,
                            statusID: EventStatusIDForNormal,
                            asForm: asForm,
                            query: asForm.Query)
                        .ConfigureAwait(false);
                }
                return (await new VendorSearchModule().SearchResultsAsync(asForm, contextProfileName).ConfigureAwait(false) as TSearchViewModel)!;
            }
            throw new ArgumentException(
                $"ERROR! Unable to match index name '{index}' to a catalog type",
                nameof(index));
        }

        /// <inheritdoc/>
        public override async Task PurgeAsync(string? contextProfileName, string index)
        {
            Contract.RequiresValidKey(index, "The index name passed into the Elastic Searching Provider was null");
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForAuctions)) && ElasticSearchingProviderConfig.SearchingAuctionsEnabled)
            {
                await new AuctionIndexer().DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForCategories)) && ElasticSearchingProviderConfig.SearchingCategoriesEnabled)
            {
                await new CategoryIndexer().DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForFranchises)) && ElasticSearchingProviderConfig.SearchingFranchisesEnabled)
            {
                await new FranchiseIndexer().DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForLots)) && ElasticSearchingProviderConfig.SearchingLotsEnabled)
            {
                await new LotIndexer().DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForManufacturers)) && ElasticSearchingProviderConfig.SearchingManufacturersEnabled)
            {
                await new ManufacturerIndexer().DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
            if (/*(*/index == AllIndexes || index.StartsWith(IndexPrefixForProducts)/*) && CEFConfigDictionary.ProductsEnabled*/)
            {
                await new ProductIndexer().DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForStores)) && ElasticSearchingProviderConfig.SearchingStoresEnabled)
            {
                await new StoreIndexer().DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
            if ((index == AllIndexes || index.StartsWith(IndexPrefixForVendors)) && ElasticSearchingProviderConfig.SearchingVendorsEnabled)
            {
                await new VendorIndexer().DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public override async Task<List<SuggestResultBase?>> GetAllSuggestionsFromProviderAsync(
            SearchFormBase form,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            List<SuggestResultBase?> retVal = new();
            {
                var retValInner = (await SuggestionsAsync<ProductCatalogSearchForm, ProductSuggestResult>(
                            new(form),
                            ElasticSearchingProviderConfig.SearchingProductIndex,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Where(x => x != null)
                    .DistinctBy(x => x!.ID)
                    .ToList();
                await AppendQueryableAttributesAsync<ProductSuggestResult, Product>(
                        ElasticSearchingProviderConfig.SearchingProductIndexQueryByAttributeKeys,
                        retValInner,
                        context)
                    .ConfigureAwait(false);
                retVal.AddRange(retValInner);
            }
            if (ElasticSearchingProviderConfig.SearchingAuctionsEnabled)
            {
                var retValInner = (await SuggestionsAsync<AuctionCatalogSearchForm, AuctionSuggestResult>(
                            new(form),
                            ElasticSearchingProviderConfig.SearchingAuctionIndex,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Where(x => x != null)
                    .DistinctBy(x => x!.ID)
                    .ToList();
                await AppendQueryableAttributesAsync<AuctionSuggestResult, Auction>(
                        ElasticSearchingProviderConfig.SearchingAuctionIndexQueryByAttributeKeys,
                        retValInner,
                        context)
                    .ConfigureAwait(false);
                retVal.AddRange(retValInner);
            }
            if (ElasticSearchingProviderConfig.SearchingLotsEnabled)
            {
                var retValInner = (await SuggestionsAsync<LotCatalogSearchForm, LotSuggestResult>(
                            new(form),
                            ElasticSearchingProviderConfig.SearchingLotIndex,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Where(x => x != null)
                    .DistinctBy(x => x!.ID)
                    .ToList();
                await AppendQueryableAttributesAsync<LotSuggestResult, Lot>(
                        ElasticSearchingProviderConfig.SearchingLotIndexQueryByAttributeKeys,
                        retValInner,
                        context)
                    .ConfigureAwait(false);
                retVal.AddRange(retValInner);
            }
            if (ElasticSearchingProviderConfig.SearchingCategoriesEnabled)
            {
                var retValInner = (await SuggestionsAsync<CategoryCatalogSearchForm, CategorySuggestResult>(
                            new(form),
                            ElasticSearchingProviderConfig.SearchingCategoryIndex,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Where(x => x != null)
                    .DistinctBy(x => x!.ID)
                    .ToList();
                await AppendQueryableAttributesAsync<CategorySuggestResult, Category>(
                        ElasticSearchingProviderConfig.SearchingCategoryIndexQueryByAttributeKeys,
                        retValInner,
                        context)
                    .ConfigureAwait(false);
                retVal.AddRange(retValInner);
            }
            if (ElasticSearchingProviderConfig.SearchingManufacturersEnabled)
            {
                var retValInner = (await SuggestionsAsync<ManufacturerCatalogSearchForm, ManufacturerSuggestResult>(
                            new(form),
                            ElasticSearchingProviderConfig.SearchingManufacturerIndex,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Where(x => x != null)
                    .DistinctBy(x => x!.ID)
                    .ToList();
                await AppendQueryableAttributesAsync<ManufacturerSuggestResult, Manufacturer>(
                        ElasticSearchingProviderConfig.SearchingManufacturerIndexQueryByAttributeKeys,
                        retValInner,
                        context)
                    .ConfigureAwait(false);
                retVal.AddRange(retValInner);
            }
            if (ElasticSearchingProviderConfig.SearchingStoresEnabled)
            {
                var retValInner = (await SuggestionsAsync<StoreCatalogSearchForm, StoreSuggestResult>(
                            new(form),
                            ElasticSearchingProviderConfig.SearchingStoreIndex,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Where(x => x != null)
                    .DistinctBy(x => x!.ID)
                    .ToList();
                await AppendQueryableAttributesAsync<StoreSuggestResult, Store>(
                        ElasticSearchingProviderConfig.SearchingStoreIndexQueryByAttributeKeys,
                        retValInner,
                        context)
                    .ConfigureAwait(false);
                retVal.AddRange(retValInner);
            }
            if (ElasticSearchingProviderConfig.SearchingVendorsEnabled)
            {
                var retValInner = (await SuggestionsAsync<VendorCatalogSearchForm, VendorSuggestResult>(
                            new(form),
                            ElasticSearchingProviderConfig.SearchingVendorIndex,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Where(x => x != null)
                    .DistinctBy(x => x!.ID)
                    .ToList();
                await AppendQueryableAttributesAsync<VendorSuggestResult, Vendor>(
                        ElasticSearchingProviderConfig.SearchingVendorIndexQueryByAttributeKeys,
                        retValInner,
                        context)
                    .ConfigureAwait(false);
                retVal.AddRange(retValInner);
            }
            if (ElasticSearchingProviderConfig.SearchingFranchisesEnabled)
            {
                var retValInner = (await SuggestionsAsync<FranchiseCatalogSearchForm, FranchiseSuggestResult>(
                            new(form),
                            ElasticSearchingProviderConfig.SearchingFranchiseIndex,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .Where(x => x != null)
                    .DistinctBy(x => x!.ID)
                    .ToList();
                await AppendQueryableAttributesAsync<FranchiseSuggestResult, Franchise>(
                        ElasticSearchingProviderConfig.SearchingFranchiseIndexQueryByAttributeKeys,
                        retValInner,
                        context)
                    .ConfigureAwait(false);
                retVal.AddRange(retValInner);
            }
            return retVal;
        }

        /// <inheritdoc/>
        public override async Task<TSearchViewModel> GetSearchResultsFromProviderAsync<TSearchViewModel, TSearchForm, TIndexModel>(
            TSearchForm form,
            string? contextProfileName,
            List<string>? roles = null)
        {
            var resultTask = form switch
            {
                AuctionCatalogSearchForm => QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                    form, ElasticSearchingProviderConfig.SearchingAuctionIndex, contextProfileName),
                CategoryCatalogSearchForm => QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                    form, ElasticSearchingProviderConfig.SearchingCategoryIndex, contextProfileName),
                FranchiseCatalogSearchForm => QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                    form, ElasticSearchingProviderConfig.SearchingFranchiseIndex, contextProfileName),
                LotCatalogSearchForm => QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                    form, ElasticSearchingProviderConfig.SearchingLotIndex, contextProfileName),
                ManufacturerCatalogSearchForm => QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                    form, ElasticSearchingProviderConfig.SearchingManufacturerIndex, contextProfileName),
                ProductCatalogSearchForm => QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                    form, ElasticSearchingProviderConfig.SearchingProductIndex, contextProfileName),
                StoreCatalogSearchForm => QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                    form, ElasticSearchingProviderConfig.SearchingStoreIndex, contextProfileName),
                VendorCatalogSearchForm => QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                    form, ElasticSearchingProviderConfig.SearchingVendorIndex, contextProfileName),
                _ => throw new ArgumentException("ERROR! Unable to match search form to a catalog type", nameof(form)),
            };
            var result = await resultTask.ConfigureAwait(false);
            var ids = result.Documents!.Select(doc => doc.ID).ToArray();
            result.ResultIDs = ids.ToList();
            // Assign nulls so they don't serialize over the wire and bloat the response (especially when it could be disabled)
            if (result.BrandIDs?.Any() != true)
            {
                result.BrandIDs = null;
            }
            if (result.FranchiseIDs?.Any() != true)
            {
                result.FranchiseIDs = null;
            }
            if (result.ProductIDs?.Any() != true)
            {
                result.ProductIDs = null;
            }
            if (result.ManufacturerIDs?.Any() != true)
            {
                result.ManufacturerIDs = null;
            }
            if (result.StoreIDs?.Any() != true)
            {
                result.StoreIDs = null;
            }
            if (result.Types?.Any() != true)
            {
                result.Types = null;
            }
            if (result.VendorIDs?.Any() != true)
            {
                result.VendorIDs = null;
            }
            if (result.PricingRanges?.Any() != true)
            {
                result.PricingRanges = null;
            }
            if (result.CategoriesTree?.Children?.Any() != true)
            {
                result.CategoriesTree = null;
            }
            /* TODO@JTG: Per type
            if (!ElasticSearchingProviderConfig.SearchingProductIndexResultsIncludeSearchDocuments)
            {
                result.Documents = null;
            }
            */
            return result;
        }

        /// <inheritdoc/>
        public override async Task<DateTime?> GetAllSearchResultsAsViewModelLastModifiedAsync(
            SearchFormBase form,
            string? contextProfileName,
            List<string>? roles = null)
        {
            roles ??= new();
            ReplacePlaceholderWithCommas(form);
            var lastModified = DateTime.MinValue;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            async Task AppendResultsLastModAsync<TSearchViewModel2, TSearchForm2, TIndexModel2, TEntity2>(
                    bool setting = false)
                where TSearchViewModel2 : SearchViewModelBase<TSearchForm2, TIndexModel2>
                where TSearchForm2 : SearchFormBase, new()
                where TIndexModel2 : IndexableModelBase
                where TEntity2 : class, IBase
            {
                var otherForm = new TSearchForm2();
                otherForm.CopyFrom(form);
                otherForm.PageSize = 10_000;
                var results = await GetSearchResultsFromProviderAsync<TSearchViewModel2, TSearchForm2, TIndexModel2>(
                        otherForm,
                        contextProfileName,
                        roles)
                    .ConfigureAwait(false);
                if (results is null)
                {
                    return;
                }
                var ids = results.Documents!.Select(doc => doc.ID).ToArray();
                for (var i = 0; i < ids.Length; i += 100)
                {
                    var idsChunk = ids.Skip(i).Take(100);
                    // ReSharper disable once AccessToDisposedClosure
                    var recordsWithMaps = context.Set<TEntity2>()
                        .AsNoTracking()
                        .Where(x => x.Active)
                        .FilterByIDs(idsChunk);
                    if (setting)
                    {
                        if (typeof(TEntity2) == typeof(Category))
                        {
                            var rolesIsNotNull = roles != null;
                            recordsWithMaps = (IQueryable<TEntity2>)((IQueryable<Category>)recordsWithMaps)
                                .Where(x => x.RequiresRoles == null
                                    || x.RequiresRoles == string.Empty
                                    || rolesIsNotNull && roles!.Any(y => x.RequiresRoles.Contains(y)));
                        }
                        else if (typeof(TEntity2) == typeof(Product))
                        {
                            recordsWithMaps = (IQueryable<TEntity2>)((IQueryable<Product>)recordsWithMaps)
                                .FilterByProductRequiresRoles(roles);
                        }
                    }
                    var lastModifiedInChunk = await recordsWithMaps
                        .Select(x => x.UpdatedDate ?? x.CreatedDate)
                        .DefaultIfEmpty(DateTime.MinValue)
                        .MaxAsync()
                        .ConfigureAwait(false);
                    lastModified = lastModifiedInChunk > lastModified ? lastModifiedInChunk : lastModified;
                }
            }
            await AppendResultsLastModAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel, Product>(ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles).ConfigureAwait(false);
            if (CEFConfigDictionary.AuctionsEnabled)
            {
                await AppendResultsLastModAsync<AuctionSearchViewModel, AuctionCatalogSearchForm, AuctionIndexableModel, Auction>().ConfigureAwait(false);
                await AppendResultsLastModAsync<LotSearchViewModel, LotCatalogSearchForm, LotIndexableModel, Lot>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.CategoriesEnabled)
            {
                await AppendResultsLastModAsync<CategorySearchViewModel, CategoryCatalogSearchForm, CategoryIndexableModel, Category>(ElasticSearchingProviderConfig.SearchingCategoryIndexFiltersIncludeRoles).ConfigureAwait(false);
            }
            if (CEFConfigDictionary.ManufacturersEnabled)
            {
                await AppendResultsLastModAsync<ManufacturerSearchViewModel, ManufacturerCatalogSearchForm, ManufacturerIndexableModel, Manufacturer>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.StoresEnabled)
            {
                await AppendResultsLastModAsync<StoreSearchViewModel, StoreCatalogSearchForm, StoreIndexableModel, Store>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.VendorsEnabled)
            {
                await AppendResultsLastModAsync<VendorSearchViewModel, VendorCatalogSearchForm, VendorIndexableModel, Vendor>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.FranchisesEnabled)
            {
                await AppendResultsLastModAsync<FranchiseSearchViewModel, FranchiseCatalogSearchForm, FranchiseIndexableModel, Franchise>().ConfigureAwait(false);
            }
            return lastModified == DateTime.MinValue ? null : lastModified;
        }

        /// <inheritdoc/>
        public override async Task<List<SearchViewModelBase>> GetAllSearchResultsAsViewModelsAsync(
            SearchFormBase form,
            string? contextProfileName,
            List<string>? roles = null)
        {
            roles ??= new();
            ReplacePlaceholderWithCommas(form);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var listOfResultModels = new List<SearchViewModelBase>();
            async Task AppendResultsAsync<TSearchViewModel, TSearchForm, TIndexModel>()
                where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>
                where TSearchForm : SearchFormBase, new()
                where TIndexModel : IndexableModelBase
            {
                var otherForm = new TSearchForm();
                otherForm.CopyFrom(form);
                var results = await GetSearchResultsFromProviderAsync<TSearchViewModel, TSearchForm, TIndexModel>(
                        otherForm,
                        contextProfileName,
                        roles)
                    .ConfigureAwait(false);
                if (results is null)
                {
                    return;
                }
                var ids = results.Documents!.Select(doc => doc.ID).ToArray();
                switch (form)
                {
                    case ProductCatalogSearchForm:
                    {
                        // ReSharper disable once AccessToDisposedClosure
                        await Workflows.Products.CleanProductsAsync(ids, context).ConfigureAwait(false);
                        goto default;
                    }
                    default:
                    {
                        results.ResultIDs = ids.ToList();
                        break;
                    }
                }
                listOfResultModels.Add(results);
            }
            await AppendResultsAsync<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>().ConfigureAwait(false);
            if (CEFConfigDictionary.AuctionsEnabled)
            {
                await AppendResultsAsync<AuctionSearchViewModel, AuctionCatalogSearchForm, AuctionIndexableModel>().ConfigureAwait(false);
                await AppendResultsAsync<LotSearchViewModel, LotCatalogSearchForm, LotIndexableModel>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.CategoriesEnabled)
            {
                await AppendResultsAsync<CategorySearchViewModel, CategoryCatalogSearchForm, CategoryIndexableModel>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.ManufacturersEnabled)
            {
                await AppendResultsAsync<ManufacturerSearchViewModel, ManufacturerCatalogSearchForm, ManufacturerIndexableModel>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.StoresEnabled)
            {
                await AppendResultsAsync<StoreSearchViewModel, StoreCatalogSearchForm, StoreIndexableModel>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.VendorsEnabled)
            {
                await AppendResultsAsync<VendorSearchViewModel, VendorCatalogSearchForm, VendorIndexableModel>().ConfigureAwait(false);
            }
            if (CEFConfigDictionary.FranchisesEnabled)
            {
                await AppendResultsAsync<FranchiseSearchViewModel, FranchiseCatalogSearchForm, FranchiseIndexableModel>().ConfigureAwait(false);
            }
            return listOfResultModels;
        }

        /// <summary>Appends a queryable attributes.</summary>
        /// <typeparam name="TSuggestResult">Type of the suggest result.</typeparam>
        /// <typeparam name="TEntity">       Type of the entity.</typeparam>
        /// <param name="setting">    The setting.</param>
        /// <param name="retValInner">The ret value inner.</param>
        /// <param name="context">    The context.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task AppendQueryableAttributesAsync<TSuggestResult, TEntity>(
                string? setting,
                IEnumerable<TSuggestResult?> retValInner,
                IClarityEcommerceEntities context)
            where TSuggestResult : SuggestResultBase
            where TEntity : class, IBase
        {
            if (!Contract.CheckValidKey(setting))
            {
                return;
            }
            var toParse = setting!
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
            foreach (var option in retValInner.Where(x => Contract.CheckEmpty(x!.QueryableAttributes)))
            {
                option!.QueryableAttributes = (await context.Set<TEntity>()
                        .AsNoTracking()
                        .FilterByID(option.ID)
                        .Select(x => x.JsonAttributes)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false))
                    .DeserializeAttributesDictionary()
                    .Where(x => toParse.Contains(x.Key))
                    .ToDictionary(x => x.Key, x => x.Value.Value);
            }
        }

        /// <summary>Adds a new event and save.</summary>
        /// <typeparam name="TSearchForm">Type of the search form.</typeparam>
        /// <param name="context"> The context.</param>
        /// <param name="name">    The name.</param>
        /// <param name="typeID">  Identifier for the type.</param>
        /// <param name="statusID">Identifier for the status.</param>
        /// <param name="asForm">  as form.</param>
        /// <param name="query">   The query.</param>
        /// <returns>A Task.</returns>
        private static Task AddNewEventAndSaveAsync<TSearchForm>(
            IClarityEcommerceEntities context,
            string name,
            int typeID,
            int statusID,
            TSearchForm asForm,
            string? query)
        {
            var @event = context.Events.Create();
            @event.Active = true;
            @event.CreatedDate = DateExtensions.GenDateTime;
            @event.CustomKey = Guid.NewGuid().ToString();
            @event.TypeID = typeID;
            @event.StatusID = statusID;
            @event.Name = name; // "Auction Catalog Search Keyword";
            @event.Description = JsonConvert.SerializeObject(asForm);
            @event.Keywords = query is not null && query.Length > 100 ? query?.Substring(0, 100) : query;
            context.Events.Add(@event);
            return context.SaveUnitOfWorkAsync(true);
        }
    }
}
