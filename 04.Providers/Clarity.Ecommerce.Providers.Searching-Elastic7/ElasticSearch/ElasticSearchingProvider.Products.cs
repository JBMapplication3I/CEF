// <copyright file="ElasticSearchingProvider.Products.cs" company="clarity-ventures.com">
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
    using Indexer;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using MoreLinq;
    using Nest;
    using Newtonsoft.Json;
    using Utilities;
    using Web.Search;

    /// <summary>An elastic searching provider.</summary>
    /// <seealso cref="SearchingProviderBase"/>
    public partial class ElasticSearchingProvider : SearchingProviderBase
    {
        /// <summary>The map product suggest model.</summary>
        private static readonly Func<ISuggestOption<IndexableProductModel>, ISearchSuggestResult> MapProductSuggestModel =
            suggest => new ProductSuggestOption
            {
                ID = suggest.Source.ID,
                Name = suggest.Source.Name,
                CustomKey = suggest.Source.CustomKey,
                BrandName = suggest.Source.BrandName,
                ManufacturerPartNumber = suggest.Source.ManufacturerPartNumber,
                SeoUrl = suggest.Source.SeoUrl,
                Score = suggest.Score,
                TotalPurchasedQuantity = suggest.Source.TotalPurchasedQuantity,
            };

        /// <summary>The product suggest module.</summary>
        private static ProductSuggestModule productSuggestModule;

        /// <inheritdoc/>
        public override bool HasValidConfiguration =>
            ElasticSearchingProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <summary>Gets or sets the event type identifier for product catalog search.</summary>
        /// <value>The event type identifier for product catalog search.</value>
        private static int EventTypeIDForProductCatalogSearch { get; set; }

        /// <summary>Gets or sets the event status identifier for normal.</summary>
        /// <value>The event status identifier for normal.</value>
        private static int EventStatusIDForNormal { get; set; }

        /// <summary>Gets the product suggest module.</summary>
        /// <value>The product suggest module.</value>
        private static ProductSuggestModule ProductSuggestModule => productSuggestModule
            ??= new(ProductSuggestFields, MapProductSuggestModel);

        /// <inheritdoc/>
        public override async Task PurgeAsync(string contextProfileName, string index)
        {
            Contract.RequiresValidKey(index, "The index name passed into the Elastic Searching Provider was null");
            if (index == "all" || index.StartsWith("product-search"))
            {
                await ProductIndexer.DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
#if STORES
            if (index == "all" || index.StartsWith("store-search"))
            {
                await StoreIndexer.DeleteIndexIfExistsAsync(index, contextProfileName).ConfigureAwait(false);
            }
#endif
        }

        /// <inheritdoc/>
        public override async Task IndexAsync(string contextProfileName, string index, CancellationToken ct)
        {
            Contract.RequiresValidKey(index, "The index name passed into the Elastic Searching Provider was null");
            if (index == "all" || index.StartsWith("product-search"))
            {
                await ProductIndexer.IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
#if STORES
            if (index == "all" || index.StartsWith("store-search"))
            {
                await StoreIndexer.IndexAsync(contextProfileName, ct).ConfigureAwait(false);
            }
#endif
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<ISearchSuggestResult>> SuggestionsAsync<TSearchForm, TResult>(
            TSearchForm form,
            string index,
            string contextProfileName)
        {
            Contract.RequiresValidKey(index, "The index name passed into the Elastic Searching Provider was null");
            var results = new List<ISearchSuggestResult>();
            if (index == "all" || index.StartsWith("product-search"))
            {
                results.AddRange(await ProductSuggestModule.SuggestResultsAsync(form as ProductCatalogSearchForm, index).ConfigureAwait(false));
            }
#if STORES
            if (index == "all" || index.StartsWith("store-search"))
            {
                results.AddRange(await StoreSuggestModule.SuggestResultsAsync(form as StoreCatalogSearchForm, index).ConfigureAwait(false));
            }
#endif
            return results;
        }

        /// <inheritdoc/>
        public override async Task<TSearchViewModel> QueryAsync<TSearchViewModel, TSearchForm, TIndexModel>(
            TSearchForm form,
            string index,
            string contextProfileName)
        {
            Contract.RequiresValidKey(index, "The index name passed into the Elastic Searching Provider was null");
            // ReSharper disable once InvertIf
            if (index.StartsWith("product-search"))
            {
                if (form is not ProductCatalogSearchForm asForm)
                {
                    throw new ArgumentException("Invalid form data for product search.");
                }
                // ReSharper disable once InvertIf
                if (Contract.CheckValidKey(asForm.Query))
                {
                    await Logger.LogInformationAsync("ElasticSearch.Products.Keyword", asForm.Query, contextProfileName).ConfigureAwait(false);
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    if (Contract.CheckInvalidID(EventTypeIDForProductCatalogSearch))
                    {
                        EventTypeIDForProductCatalogSearch = await Workflows.EventTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Product Catalog Search",
                                byName: "Product Catalog Search",
                                byDisplayName: "Product Catalog Search",
                                model: null,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                    }
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
                    context.Events.Add(new()
                    {
                        Active = true,
                        CreatedDate = DateExtensions.GenDateTime,
                        CustomKey = Guid.NewGuid().ToString(),
                        TypeID = EventTypeIDForProductCatalogSearch,
                        StatusID = EventStatusIDForNormal,
                        Name = "Product Catalog Search Keyword",
                        Description = JsonConvert.SerializeObject(asForm),
                        Keywords = asForm.Query,
                    });
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
                return await new ProductSearchModule().SearchResultsAsync(asForm).ConfigureAwait(false) as TSearchViewModel;
            }
#if STORES
            if (index.StartsWith("store-search"))
            {
                return await new StoreSearchModule().SearchResultsAsync(form as StoreCatalogSearchForm).ConfigureAwait(false) as TSearchViewModel;
            }
#endif
            return new();
        }

        /// <inheritdoc/>
        public override async Task<List<ISearchSuggestResult>> GetProductSuggestionsFromProviderAsync(
            IProductCatalogSearchForm form,
            string contextProfileName)
        {
            return (await RegistryLoaderWrapper.GetSearchingProvider(contextProfileName)
                    .SuggestionsAsync<ProductCatalogSearchForm, ProductSuggestOption>(
                        (ProductCatalogSearchForm)form,
                        ElasticSearchingProviderConfig.SearchingProductIndex,
                        contextProfileName)
                    .ConfigureAwait(false))
                .DistinctBy(x => x.ID)
                .ToList();
        }

        /// <inheritdoc/>
        public override Task<ProductSearchViewModel> GetProductSearchResultsFromProviderAsync(
            IProductCatalogSearchForm form,
            string contextProfileName)
        {
            return RegistryLoaderWrapper.GetSearchingProvider(contextProfileName)
                .QueryAsync<ProductSearchViewModel, ProductCatalogSearchForm, IndexableProductModel>(
                    (ProductCatalogSearchForm)form,
                    ElasticSearchingProviderConfig.SearchingProductIndex,
                    contextProfileName);
        }

        /// <inheritdoc/>
        public override async Task<DateTime?> GetProductSearchResultsFromProviderAndMapEachWithAdditionalDataLastModifiedAsync(
            IProductCatalogSearchForm form,
            string contextProfileName,
            List<string> roles)
        {
            roles ??= new();
            ReplacePlaceholderWithCommas(form);
            var formClone = JsonConvert.DeserializeObject<ProductCatalogSearchForm>(
                JsonConvert.SerializeObject((ProductCatalogSearchForm)form));
            Contract.RequiresNotNull(formClone);
            formClone.PageSize = 10_000; // Huge page size so we can see all ids returned
            var results = await GetProductSearchResultsFromProviderAsync(
                    formClone,
                    contextProfileName)
                .ConfigureAwait(false);
            if (results.Documents == null)
            {
                return null;
            }
            var productIDsFound = results.Documents.Select(x => x.ID).ToArray();
            if (productIDsFound.Length == 0)
            {
                return null;
            }
            var lastModified = DateTime.MinValue;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            for (var i = 0; i < productIDsFound.Length; i += 100)
            {
                var productIDsChunk = productIDsFound.Skip(i).Take(100);
                var productsWithMaps = context.Products
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterProductsByIsVisible(true)
                    .FilterProductsByIsDiscontinued(false)
                    .FilterByIDs(productIDsChunk);
                if (ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles)
                {
                    productsWithMaps = productsWithMaps.FilterByProductRequiresRoles(roles);
                }
                var lastModifiedInChunk = productsWithMaps
                    .Select(x => x.UpdatedDate ?? x.CreatedDate)
                    .DefaultIfEmpty(DateTime.MinValue)
                    .Max();
                lastModified = lastModifiedInChunk > lastModified ? lastModifiedInChunk : lastModified;
            }
            return lastModified == DateTime.MinValue ? null : lastModified;
        }

        /// <inheritdoc/>
        public override async Task<ProductSearchViewModel> GetProductSearchResultsFromProviderAndMapEachWithAdditionalDataAsync(
            IProductCatalogSearchForm form,
            string contextProfileName,
            List<string> roles)
        {
            roles ??= new();
            ReplacePlaceholderWithCommas(form);
            var results = await GetProductSearchResultsFromProviderAsync(form, contextProfileName).ConfigureAwait(false);
            if (results.Documents == null)
            {
                return results;
            }
            var productIDsFound = results.Documents.Select(x => x.ID).ToArray();
            if (productIDsFound.Length == 0)
            {
                return results;
            }
            // Clean bad Data
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await Workflows.Products.CleanProductsAsync(productIDsFound, context).ConfigureAwait(false);
            // Mappings we are familiar with
            var matched = context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterProductsByIsVisible(true)
                .FilterProductsByIsDiscontinued(false)
                .FilterByIDs(productIDsFound);
            if (ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles)
            {
                matched = matched.FilterByProductRequiresRoles(roles);
            }
            results.ProductIDs = await matched.Select(p => p.ID).ToListAsync().ConfigureAwait(false);
            results.Documents = null;
            return results;
        }

        /// <summary>Product suggest fields.</summary>
        /// <typeparam name="TIndexModel">Type of the index model.</typeparam>
        /// <param name="f">The FieldsDescriptor{TIndexModel} to process.</param>
        /// <returns>An IPromise{Fields}.</returns>
        private static IPromise<Fields> ProductSuggestFields<TIndexModel>(FieldsDescriptor<TIndexModel> f)
            where TIndexModel : IndexableProductModel
        {
            return f
                .Field(ff => ff.ID)
                .Field(ff => ff.Name)
                .Field(ff => ff.CustomKey)
                .Field(ff => ff.BrandName)
                .Field(ff => ff.ManufacturerPartNumber)
                .Field(ff => ff.SuggestedByName)
                .Field(ff => ff.SuggestedByCustomKey)
                .Field(ff => ff.SuggestedByBrandName)
                .Field(ff => ff.SuggestedByManufacturerPartNumber)
                .Field(ff => ff.SuggestedByShortDescription)
                .Field(ff => ff.SeoUrl)
                .Field(ff => ff.ShortDescription);
        }

        /// <summary>Replace placeholder with commas in attributes all/any.</summary>
        /// <param name="form">The form.</param>
        private static void ReplacePlaceholderWithCommas(IProductCatalogSearchForm form)
        {
            if (Contract.CheckNotEmpty(form.AttributesAll))
            {
                foreach (var value in form.AttributesAll.Values)
                {
                    for (var i = 0; i < value.Length; i++)
                    {
                        value[i] = value[i].Replace("`", ",");
                    }
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckNotEmpty(form.AttributesAny))
            {
                foreach (var value in form.AttributesAny.Values)
                {
                    for (var i = 0; i < value.Length; i++)
                    {
                        value[i] = value[i].Replace("`", ",");
                    }
                }
            }
        }
    }
}
