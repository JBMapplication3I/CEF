// <copyright file="ProductIndexer.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product indexer class</summary>
// ReSharper disable StyleCop.SA1009, StyleCop.SA1111, StyleCop.SA1115, StyleCop.SA1116
#nullable enable
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Indexer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Data;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using MoreLinq;
    using Nest;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A product indexer.</summary>
    internal class ProductIndexer
    {
        #region Constant Strings
        // General
        private const string Keyword = "keyword";
        private const string Raw = "raw";
        private const string Lowercase = "lowercase";
        private const string Whitespace = "whitespace";
        private const string TokenFilterStops = "my_stop";
        private const string TokenFilterSnowmall = "my_snowball";
        private const string TokenFilterSynonyms = "my_synonyms";
        private const string AnalyzerNameForSynonyms = "synonym-analyzer";
        // Products
        private const string AnalyzerNameForProductName = "product-name-analyzer";
        private const string AnalyzerNameForProductKeyword = "product-name-keyword";
        private const string SuggestAnalyzerNameForProductKeyword = "product-name-suggest-analyzer";
        private const string TokenizerNameForProductName = "product-name-tokenizer";
        private const string WordDelimiterNameForProductName = "product-name-words";
        private const string TokenizerWithPathHierarcherNameForProductName = "product-name-path-tokenizer";
        // Categories
        private const string AnalyzerNameForCategoryName = "category-name-analyzer";
        private const string AnalyzerNameForCategoryKeyword = "category-name-keyword";
        private const string TokenizerNameForCategoryName = "category-name-tokenizer";
        private const string WordDelimiterNameForCategoryName = "category-name-words";
        // Attributes
        private const string AnalyzerNameForAttributeName = "attribute-name-analyzer";
        private const string AnalyzerNameForAttributeKeyword = "attribute-name-keyword";
        private const string TokenizerNameForAttributeName = "attribute-name-tokenizer";
        private const string WordDelimiterNameForAttributeName = "attribute-name-words";
        #endregion

        private static ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        private static ElasticClient Client { get; } = ElasticSearchClientFactory.GetClient();

        private static ProductDumpReader DumpReader { get; } = new();

        private static string CurrentIndexName { get; set; } = ElasticSearchClientFactory.CreateProductIndexName();

        private static IEnumerable<string>? Synonyms { get; set; }

        /// <summary>Index.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ct">                The ct.</param>
        /// <returns>A Task.</returns>
        public static async Task IndexAsync(string contextProfileName, CancellationToken ct)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            CurrentIndexName = ElasticSearchClientFactory.CreateProductIndexName();
            await CreateIndexAsync(contextProfileName, ct).ConfigureAwait(false);
            await IndexProductsAsync(contextProfileName, ct).ConfigureAwait(false);
            await SwapAliasAsync(contextProfileName, ct).ConfigureAwait(false);
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Deletes the index if exists.</summary>
        /// <param name="index">             Zero-based index of the.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        public static async Task DeleteIndexIfExistsAsync(string index, string contextProfileName)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            var existsResponse = await Client.Indices.ExistsAsync(index).ConfigureAwait(false);
            if (!existsResponse.Exists)
            {
                return;
            }
            var deleteResponse = await Client.Indices.DeleteAsync(index).ConfigureAwait(false);
            if (!deleteResponse.Acknowledged || deleteResponse.OriginalException != null)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(ProductIndexer)}.${nameof(DeleteIndexIfExistsAsync)}.Error",
                        message: deleteResponse.DebugInformation,
                        deleteResponse.OriginalException,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return;
            }
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        private static async Task CreateIndexAsync(string contextProfileName, CancellationToken ct)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var synonymsSetting = context.Settings
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByTypeKey<DataModel.Setting, DataModel.SettingType>("Synonyms", true)
                .Select(x => x.Value)
                .SingleOrDefault();
            var synonyms = synonymsSetting?.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                ?? new[] { "i-pod, i pod => ipod", "sea biscuit, sea biscit => seabiscuit" };
            await Client.Indices.CreateAsync(
                    index: CurrentIndexName,
                    selector: i => i
                        .Index<IndexableProductModel>()
                        .IncludeTypeName()
                        .ErrorTrace()
                        .Settings(s => s
                            .NumberOfShards(2)
                            .NumberOfReplicas(0)
                            .Analysis(x => GetAnalysis(x, synonyms)))
                        .Map<IndexableProductModel>(MapProduct),
                    ct: ct)
                .ConfigureAwait(false);
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        private static TypeMappingDescriptor<IndexableProductModel> MapProduct(TypeMappingDescriptor<IndexableProductModel> map) => map
            .AutoMap<IndexableProductModel>()
            .Properties(ps => ps
                .Text(t => t.Name(p => p.Name).Analyzer(AnalyzerNameForProductName).Fields(f => f.Text(p => p.Name(Keyword).Analyzer(AnalyzerNameForProductKeyword)).Keyword(p => p.Name(Raw))))
                .Text(t => t.Name(p => p.CustomKey).Analyzer(AnalyzerNameForProductName).Fields(f => f.Text(p => p.Name(Keyword).Analyzer(AnalyzerNameForProductKeyword)).Keyword(p => p.Name(Raw))))
                .Text(t => t.Name(p => p.BrandName).Analyzer(AnalyzerNameForProductName).Fields(f => f.Text(p => p.Name(Keyword).Analyzer(AnalyzerNameForProductKeyword)).Keyword(p => p.Name(Raw))))
                .Text(t => t.Name(p => p.ManufacturerPartNumber).Analyzer(AnalyzerNameForProductName).Fields(f => f.Text(p => p.Name(Keyword).Analyzer(AnalyzerNameForProductKeyword)).Keyword(p => p.Name(Raw))))
                .Nested<IndexableProductCategory>(n => n
                    .Name(p => p.ProductCategories.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.CategoryName).Analyzer(AnalyzerNameForCategoryName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForCategoryKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.CategoryDisplayName).Analyzer(AnalyzerNameForCategoryName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForCategoryKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.CategoryParent1Name).Analyzer(AnalyzerNameForCategoryName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForCategoryKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.CategoryParent2Name).Analyzer(AnalyzerNameForCategoryName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForCategoryKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.CategoryParent3Name).Analyzer(AnalyzerNameForCategoryName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForCategoryKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.CategoryParent4Name).Analyzer(AnalyzerNameForCategoryName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForCategoryKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.CategoryParent5Name).Analyzer(AnalyzerNameForCategoryName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForCategoryKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.CategoryParent6Name).Analyzer(AnalyzerNameForCategoryName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForCategoryKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableSerializableAttributeObject>(n => n
                    .Name(p => p.Attributes.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Fields(fs => fs.Text(ss => ss.Name(Raw)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Key).Analyzer(AnalyzerNameForAttributeName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForAttributeKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.UofM).Fields(fs => fs.Text(ss => ss.Name(Raw)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Value).Fields(fs => fs.Text(ss => ss.Name(Raw)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableProductBrand>(n => n
                    .Name(p => p.ProductBrands.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.BrandID).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.BrandKey).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.BrandName).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableProductStore>(n => n
                    .Name(p => p.ProductStores.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.StoreID).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.StoreKey).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.StoreName).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableProductRole>(n => n
                    .Name(p => p.RequiresRolesList.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.RoleName).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Completion(c => c.Name(p => p.SuggestedByName).Analyzer(SuggestAnalyzerNameForProductKeyword))
                .Completion(c => c.Name(p => p.SuggestedByCustomKey).Analyzer(SuggestAnalyzerNameForProductKeyword))
                .Completion(c => c.Name(p => p.SuggestedByBrandName).Analyzer(SuggestAnalyzerNameForProductKeyword))
                .Completion(c => c.Name(p => p.SuggestedByManufacturerPartNumber).Analyzer(SuggestAnalyzerNameForProductKeyword))
                .Completion(c => c.Name(p => p.SuggestedByShortDescription).Analyzer(SuggestAnalyzerNameForProductKeyword))
            );

        private static AnalysisDescriptor GetAnalysis(AnalysisDescriptor analysis, IEnumerable<string> synonyms)
        {
            Synonyms = synonyms;
            return Analysis(analysis);
        }

        private static AnalysisDescriptor Analysis(AnalysisDescriptor analysis) => analysis
            .Tokenizers(tokenizers => tokenizers
                .Pattern(TokenizerNameForProductName, p => p.Pattern(@"\W+"))
                .Pattern(TokenizerNameForCategoryName, p => p.Pattern(@"(?:(.+)(?:\W*))\|").Group(1))
                .Pattern(TokenizerNameForAttributeName, p => p.Pattern(@"\W+"))
                .PathHierarchy(TokenizerWithPathHierarcherNameForProductName, p => p
                    .Delimiter(' ')
                    .Reverse())
            )
            .TokenFilters(tokenFilters => tokenFilters
                .WordDelimiter(WordDelimiterNameForProductName, w => w
                    .PreserveOriginal()
                    .SplitOnCaseChange()
                    .SplitOnNumerics()
                    .GenerateNumberParts(false)
                    .GenerateWordParts()
                )
                .WordDelimiter(WordDelimiterNameForCategoryName, w => w
                    .SplitOnCaseChange()
                    .PreserveOriginal()
                    .SplitOnNumerics(false)
                    .GenerateNumberParts()
                    .GenerateWordParts()
                )
                .WordDelimiter(WordDelimiterNameForAttributeName, w => w
                    .SplitOnCaseChange()
                    .PreserveOriginal()
                    .SplitOnNumerics()
                    .GenerateNumberParts()
                    .GenerateWordParts()
                )
                .Stop(TokenFilterStops, st => st
                    .StopWords("a", "an", "and", "are", "as", "at", "be", "but", "by",
                               "for", "if", "in", "into", "is", "it",
                               "no", "not", "of", "on", "or", "such",
                               "that", "the", "their", "then", "there", "these",
                               "they", "this", "to", "was", "will", "with", "&")
                    .RemoveTrailing()
                )
                .Snowball(TokenFilterSnowmall, sb => sb
                    .Language(SnowballLanguage.English)
                )
                //*
                .Synonym(TokenFilterSynonyms, s => s
                    // .SynonymsPath(
                    //     JSConfigs.CEFConfigDictionary.SearchingElasticSearchSynonymsFilePath
                    //         .Replace(@"{CEF_RootPath}\", Globals.CEFRootPath))
                    .Synonyms(Synonyms)
                    .Format(SynonymFormat.Solr)
                    .Tokenizer(Whitespace)
                )
                //*/
            )
            .Analyzers(analyzers => analyzers
                .Custom(AnalyzerNameForProductName, c => c.Tokenizer(TokenizerNameForProductName).Filters(WordDelimiterNameForProductName, Lowercase, TokenFilterStops, TokenFilterSnowmall, TokenFilterSynonyms))
                .Custom(SuggestAnalyzerNameForProductKeyword, c => c.Tokenizer(TokenizerWithPathHierarcherNameForProductName).Filters(Lowercase))
                .Custom(AnalyzerNameForCategoryName, c => c.Tokenizer(TokenizerNameForCategoryName).Filters(WordDelimiterNameForCategoryName, Lowercase, TokenFilterStops, TokenFilterSnowmall, TokenFilterSynonyms))
                .Custom(AnalyzerNameForAttributeName, c => c.Tokenizer(TokenizerNameForAttributeName).Filters(WordDelimiterNameForAttributeName, Lowercase, TokenFilterStops, TokenFilterSnowmall, TokenFilterSynonyms))
                .Custom(AnalyzerNameForProductKeyword, c => c.Tokenizer(Keyword).Filters(Lowercase, TokenFilterStops, TokenFilterSnowmall, TokenFilterSynonyms))
                .Custom(AnalyzerNameForCategoryKeyword, c => c.Tokenizer(Keyword).Filters(Lowercase, TokenFilterStops, TokenFilterSnowmall, TokenFilterSynonyms))
                .Custom(AnalyzerNameForAttributeKeyword, c => c.Tokenizer(Keyword).Filters(Lowercase, TokenFilterStops, TokenFilterSnowmall, TokenFilterSynonyms))
                .Custom(AnalyzerNameForSynonyms, c => c.Tokenizer(Whitespace).Filters(Lowercase, TokenFilterStops, TokenFilterSnowmall, TokenFilterSynonyms))
            );

        private static async Task IndexProductsAsync(string contextProfileName, CancellationToken ct)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            await Log("Indexing documents into ElasticSearch...", contextProfileName).ConfigureAwait(false);
            /*
            var counter = 0;
            var errorCounter = 0;
            foreach (var product in DumpReader.GetProducts())
            {
                ++counter;
                var result = Client.Index(product, i => i.Type("product"));
                if (result.IsValid) { continue; }
                ++errorCounter;
                QuickLogger.Logger(contextProfileName, "ProductIndexer.IndexProductsAsync(...)", result.DebugInformation);
            }
            QuickLogger.Logger(contextProfileName, "ProductIndexer.IndexProductsAsync(...)", $"Completed {counter:N0} records, encountered {errorCounter:N0} errors");
            //*/
            /*
            var result = Client.Bulk(b =>
            {
                foreach (var product in DumpReader.GetProducts())
                {
                    b.Index<IndexableProductModel>(i => i.Document(product));
                }
                return b;
            });
            if (!result.IsValid)
            {
                foreach (var item in result.ItemsWithErrors)
                {
                    QuickLogger.Logger(contextProfileName, "ProductIndexer.IndexProductsAsync(...)", $"Failed to index document {item.Id}: {item.Error}");
                }
                QuickLogger.Logger(contextProfileName, "ProductIndexer.IndexProductsAsync(...)", result.DebugInformation);
            }
            //*/
            // *
            var products = DumpReader.GetProducts(contextProfileName);
            // Don't include the count here, let the enumerable pass in to the bulk all unaffected
            ////await Log($"Product Count {products?.Count()}", contextProfileName).ConfigureAwait(false);
            ////var waitHandle = new CountdownEvent(1);
            var took = 0L;
            async Task<bool> AttemptIndexBatchAsync(IEnumerable<IndexableProductModel> batch)
            {
                var bulkResponse1 = await Client.IndexManyAsync(
                    objects: batch,
                    index: CurrentIndexName,
                    cancellationToken: ct)
                    .ConfigureAwait(false);
                // Add the amount of time this batch took to index
                Interlocked.Add(ref took, bulkResponse1.Took);
                if (bulkResponse1.IsValid && !bulkResponse1.Errors)
                {
                    return true;
                }
                await Logger.LogErrorAsync(
                        name: $"{nameof(ProductIndexer)}.{nameof(AttemptIndexBatchAsync)}.Error",
                        message: JsonConvert.SerializeObject(new
                        {
                            bulkResponse1.ItemsWithErrors,
                            bulkResponse1.ServerError,
                        }),
                        ex: bulkResponse1.OriginalException,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return false;
            }
            var completedAttempts = 0;
            var failedAttempts = 0;
            var resultsPerBatch = await products.Batch(1000).ForEachAsync(
                    4,
                    async batch =>
                    {
                        var retry = 0;
                        while (retry < 5)
                        {
                            var result = await AttemptIndexBatchAsync(batch).ConfigureAwait(false);
                            if (result)
                            {
                                Interlocked.Increment(ref completedAttempts);
                                return true;
                            }
                            Interlocked.Increment(ref failedAttempts);
                            retry++;
                            // Wait 3 seconds before trying again (let the server have a chance to free up resources
                            Thread.Sleep(3_000);
                        }
                        return false;
                    })
                .ConfigureAwait(false);
            await Log(
                    $"Product Indexing is complete with {failedAttempts} failed attempts, {completedAttempts} completed attempts and took {took:n0}ms",
                    contextProfileName)
                .ConfigureAwait(false);
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        private static async Task SwapAliasAsync(string contextProfileName, CancellationToken ct)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            var exists = await Task.WhenAll(
                    Client.Indices.ExistsAsync(index: CurrentIndexName, ct: ct),
                    Client.Indices.AliasExistsAsync(name: ElasticSearchingProviderConfig.SearchingProductIndexOld, ct: ct),
                    Client.Indices.AliasExistsAsync(name: ElasticSearchingProviderConfig.SearchingProductIndex, ct: ct))
                .ConfigureAwait(false);
            var newIndexExists = exists[0].Exists;
            var oldAliasExists = exists[1].Exists;
            var liveAliasExists = exists[2].Exists;
            await Log(JsonConvert.SerializeObject(new { newIndexExists, oldAliasExists, liveAliasExists }), contextProfileName).ConfigureAwait(false);
            if (!newIndexExists)
            {
                // Don't do anything, leave the index aliases alone
                return;
            }
            var pointing = await Task.WhenAll(
                    Client.GetIndicesPointingToAliasAsync(alias: ElasticSearchingProviderConfig.SearchingProductIndexOld),
                    Client.GetIndicesPointingToAliasAsync(alias: ElasticSearchingProviderConfig.SearchingProductIndex))
                .ConfigureAwait(false);
            var indicesPointingToOldAlias = pointing[0];
            var indicesPointingToLiveAlias = pointing[1];
            if (Contract.CheckNotEmpty(indicesPointingToLiveAlias))
            {
                // On each live-aliased index (should only be one, but accounting for issues) push an old alias to it
                await indicesPointingToLiveAlias.ForEachAsync(
                        4,
                        async index =>
                        {
                            var putAliasResponse = await Client.Indices.PutAliasAsync(
                                    index: index,
                                    name: ElasticSearchingProviderConfig.SearchingProductIndexOld,
                                    ct: ct)
                                .ConfigureAwait(false);
                            if (!putAliasResponse.IsValid)
                            {
                                await Logger.LogErrorAsync(
                                        name: $"{nameof(ProductIndexer)}.{nameof(SwapAliasAsync)}.Error",
                                        message: putAliasResponse.DebugInformation,
                                        ex: putAliasResponse.OriginalException,
                                        contextProfileName: contextProfileName)
                                    .ConfigureAwait(false);
                            }
                        })
                    .ConfigureAwait(false);
                // Refresh the data for the old alias list
                indicesPointingToOldAlias = await Client.GetIndicesPointingToAliasAsync(
                        alias: ElasticSearchingProviderConfig.SearchingProductIndexOld)
                    .ConfigureAwait(false);
                // Clear all live index aliases
                var deleteLiveAliasesResponse = await Client.Indices.DeleteAliasAsync(
                        index: "*",
                        name: ElasticSearchingProviderConfig.SearchingProductIndex,
                        ct: ct)
                    .ConfigureAwait(false);
                if (!deleteLiveAliasesResponse.IsValid)
                {
                    await Logger.LogErrorAsync(
                            name: $"{nameof(ProductIndexer)}.{nameof(SwapAliasAsync)}.Error",
                            message: deleteLiveAliasesResponse.DebugInformation,
                            ex: deleteLiveAliasesResponse.OriginalException,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
            }
            if (Contract.CheckNotEmpty(indicesPointingToOldAlias) && indicesPointingToOldAlias.Count > 2)
            {
                // We will only keep the latest two in this alias, remove all older ones (should have been added to this list by this point)
                indicesPointingToOldAlias = indicesPointingToOldAlias.OrderByDescending(x => x).ToList();
                var toRemove = indicesPointingToOldAlias.Skip(2).ToArray();
                var deleteIndexesResponse = await Client.Indices.DeleteAsync(toRemove, ct: ct).ConfigureAwait(false);
                if (!deleteIndexesResponse.IsValid)
                {
                    await Logger.LogErrorAsync(
                            name: $"{nameof(ProductIndexer)}.{nameof(SwapAliasAsync)}.Error",
                            message: deleteIndexesResponse.DebugInformation,
                            ex: deleteIndexesResponse.OriginalException,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
            }
            // Add a live alias to the latest index
            var putLiveAliasResponse = await Client.Indices.PutAliasAsync(
                    index: CurrentIndexName,
                    name: ElasticSearchingProviderConfig.SearchingProductIndex,
                    ct: ct)
                .ConfigureAwait(false);
            if (!putLiveAliasResponse.IsValid)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(ProductIndexer)}.{nameof(SwapAliasAsync)}.Error",
                        message: putLiveAliasResponse.DebugInformation,
                        ex: putLiveAliasResponse.OriginalException,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        private static Task Log(string body, string? contextProfileName, [CallerMemberName] string? callerMemberName = null)
        {
            return Logger.LogInformationAsync($"{nameof(ProductIndexer)}.{callerMemberName}", body, contextProfileName);
        }
    }
}
