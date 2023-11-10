// <copyright file="IndexerBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the indexer base class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>An indexer base.</summary>
    /// <typeparam name="TDumpReader">    Type of the dump reader.</typeparam>
    /// <typeparam name="TIndexableModel">Type of the indexable model.</typeparam>
    /// <seealso cref="LoggingBase"/>
    internal abstract class IndexerBase<TDumpReader, TIndexableModel> : LoggingBase
        where TDumpReader : DumpReaderBase<TIndexableModel>, new()
        where TIndexableModel : IndexableModelBase
    {
        #region Constant Strings
        // General
        protected internal const string AnalyzerNameForProductName = "product-name-analyzer";
        protected const string Keyword = "keyword";
        protected const string Raw = "raw";
        protected const string Lowercase = "lowercase";
        protected const string Whitespace = "whitespace";
        protected const string TokenFilterStops = "my_stop";
        protected const string TokenFilterSnowmall = "my_snowball";
        protected const string TokenFilterSynonyms = "my_synonyms";
        protected const string AnalyzerNameForSynonyms = "synonym-analyzer";
        protected const string SynonymsName = "Synonyms";
        // Products
        protected const string AnalyzerNameForProductKeyword = "product-name-keyword";
        protected const string SuggestAnalyzerNameForProductKeyword = "product-name-suggest-analyzer";
        protected const string TokenizerNameForProductName = "product-name-tokenizer";
        protected const string WordDelimiterNameForProductName = "product-name-words";
        protected const string TokenizerWithPathHierarcherNameForProductName = "product-name-path-tokenizer";
        // Categories
        protected const string AnalyzerNameForCategoryName = "category-name-analyzer";
        protected const string AnalyzerNameForCategoryKeyword = "category-name-keyword";
        protected const string TokenizerNameForCategoryName = "category-name-tokenizer";
        protected const string WordDelimiterNameForCategoryName = "category-name-words";
        // Attributes
        protected const string AnalyzerNameForAttributeName = "attribute-name-analyzer";
        protected const string AnalyzerNameForAttributeKeyword = "attribute-name-keyword";
        protected const string TokenizerNameForAttributeName = "attribute-name-tokenizer";
        protected const string WordDelimiterNameForAttributeName = "attribute-name-words";
        #endregion

        #region Properties
        /// <summary>Gets the client.</summary>
        /// <value>The client.</value>
        protected virtual ElasticClient Client { get; } = ElasticSearchClientFactory.GetClient();

        /// <summary>Gets the dump reader.</summary>
        /// <value>The dump reader.</value>
        protected virtual TDumpReader DumpReader { get; } = new();

        /// <summary>Gets or sets the synonyms.</summary>
        /// <value>The synonyms.</value>
        protected virtual IEnumerable<string>? Synonyms { get; set; }
        #endregion

        /// <summary>Creates an index with the existing record data.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ct">                The cancellation token.</param>
        /// <returns>A Task.</returns>
        public virtual async Task IndexAsync(string? contextProfileName, CancellationToken ct)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            var currentIndexName = GenIndexName();
            if (currentIndexName.StartsWith("."))
            {
                await Log("Exited Prematurely: invalid index name", contextProfileName).ConfigureAwait(false);
                return;
            }
            await CreateIndexAsync(currentIndexName, contextProfileName, ct).ConfigureAwait(false);
            await PopulateIndexAsync(currentIndexName, contextProfileName, ct).ConfigureAwait(false);
            await SwapAliasAsync(currentIndexName, contextProfileName, ct).ConfigureAwait(false);
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Deletes the index if exists.</summary>
        /// <param name="index">             The name of the index to affect.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        public virtual async Task DeleteIndexIfExistsAsync(string index, string? contextProfileName)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            if (!(await Client.IndexExistsAsync(index).ConfigureAwait(false)).Exists)
            {
                return;
            }
            var deleteResponse = await Client.DeleteIndexAsync(index).ConfigureAwait(false);
            if (!deleteResponse.Acknowledged || deleteResponse.OriginalException != null)
            {
                await Error(deleteResponse.DebugInformation, deleteResponse.OriginalException, contextProfileName).ConfigureAwait(false);
                return;
            }
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        #region Abstract Functions
        /// <summary>Generates an index name.</summary>
        /// <returns>The index name.</returns>
        protected abstract string GenIndexName();

        /// <summary>Gets live index name from setting.</summary>
        /// <returns>The live index name from setting.</returns>
        protected abstract string GetLiveIndexNameFromSetting();

        /// <summary>Gets old index name from setting.</summary>
        /// <returns>The old index name from setting.</returns>
        protected abstract string GetOldIndexNameFromSetting();

        /// <summary>Maps the given record to the descriptor information.</summary>
        /// <param name="map">The map.</param>
        /// <returns>A <see cref="TypeMappingDescriptor{TIndexableModel}"/>.</returns>
        protected abstract TypeMappingDescriptor<TIndexableModel> Map(TypeMappingDescriptor<TIndexableModel> map);
        #endregion

        #region Base Functions
        /// <summary>Creates index.</summary>
        /// <param name="currentIndexName">  The current index name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ct">                The cancellation token.</param>
        /// <returns>The new index.</returns>
        protected virtual async Task CreateIndexAsync(string currentIndexName, string? contextProfileName, CancellationToken ct)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var synonymsSetting = await context.Settings
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByTypeKey<DataModel.Setting, DataModel.SettingType>(SynonymsName, true)
                .Select(x => x.Value)
                .SingleOrDefaultAsync(ct)
                .ConfigureAwait(false);
            var synonyms = synonymsSetting?.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                ?? new[] { "i-pod, i pod => ipod", "sea biscuit, sea biscit => seabiscuit" };
            await Client.CreateIndexAsync(
                    index: currentIndexName,
                    selector: i => i
                        .Settings(s => s
                            .NumberOfShards(2)
                            .NumberOfReplicas(0)
                            .Analysis(x => GetAnalysis(x, synonyms)))
                        .Mappings(m => m.Map<TIndexableModel>(Map)),
                    cancellationToken: ct)
                .ConfigureAwait(false);
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Populate index.</summary>
        /// <param name="currentIndexName">  The current index name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ct">                The cancellation token.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task PopulateIndexAsync(string currentIndexName, string? contextProfileName, CancellationToken ct)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            await Log("Indexing documents into ElasticSearch...", contextProfileName).ConfigureAwait(false);
            var records = DumpReader.GetRecords(contextProfileName, ct).ToList();
            // Don't include the count here, let the enumerable pass in to the bulk all unaffected
            await Log($"Record Count {records?.Count()}", contextProfileName).ConfigureAwait(false);
            var waitHandle = new CountdownEvent(1);
#pragma warning disable IDE0063 // Use simple 'using' statement
            // ReSharper disable once ConvertToUsingDeclaration
            using (var bulkAll = Client.BulkAll(
#pragma warning restore IDE0063 // Use simple 'using' statement
                documents: records,
                selector: b => b
                    .Index(currentIndexName)
                    .RefreshOnCompleted()
                    // If a thread fails, retry up to x times
                    .BackOffRetries(5)
                    // If a thread fails, wait x seconds before trying again
                    .BackOffTime("3s")
                    // Up to 4 threads in parallel
                    .MaxDegreeOfParallelism(4)
                    // The size of each batch (not the total size to send over)
                    .Size(1000),
                cancellationToken: ct))
            {
                bulkAll.Subscribe(new(
                    onNext: b =>
                    {
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
                        Log($"BulkAllObserver.onNext [Page:{b.Page:000}][Retries:{b.Retries}]", contextProfileName).Wait(10_000, ct);
#pragma warning restore AsyncFixer02 // Long-running or blocking operations inside an async method
                    },
                    onError: e =>
                    {
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
                        Error("BulkAllObserver.onError", e, contextProfileName).Wait(10_000, ct);
#pragma warning restore AsyncFixer02 // Long-running or blocking operations inside an async method
                        waitHandle.Signal();
                        throw e;
                    },
                    onCompleted: () =>
                    {
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
                        Log("BulkAllObserver.onCompleted", contextProfileName).Wait(10_000, ct);
#pragma warning restore AsyncFixer02 // Long-running or blocking operations inside an async method
                        waitHandle.Signal();
                    }));
                waitHandle.Wait(ct);
                if (!bulkAll.IsDisposed)
                {
                    bulkAll.Dispose();
                }
                await Log("Exited", contextProfileName).ConfigureAwait(false);
            }
        }

        /// <summary>Swap alias.</summary>
        /// <param name="currentIndexName">  The current index name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ct">                The cancellation token.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task SwapAliasAsync(string currentIndexName, string? contextProfileName, CancellationToken ct)
        {
            await Log("Entered", contextProfileName).ConfigureAwait(false);
            var indexExists = (await Client.IndexExistsAsync(GetLiveIndexNameFromSetting(), cancellationToken: ct).ConfigureAwait(false)).Exists;
            await Log($"indexExists: {indexExists}", contextProfileName).ConfigureAwait(false);
            await Client.AliasAsync(
                    selector: aliases =>
                    {
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
                        Log("Checking Alias...", contextProfileName).Wait(10_000, ct);
                        if (indexExists)
                        {
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
                            Log("Index exists, so adding an old index alias", contextProfileName).Wait(10_000, ct);
#pragma warning restore AsyncFixer02 // Long-running or blocking operations inside an async method
                            aliases.Add(a => a.Alias(GetOldIndexNameFromSetting()).Index(GetLiveIndexNameFromSetting()));
                        }
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
                        Log("Swapping live index to the latest content and marking the old one with the old index name pattern", contextProfileName).Wait(10_000, ct);
#pragma warning restore AsyncFixer02 // Long-running or blocking operations inside an async method
                        return aliases
                            .Remove(a => a.Alias(GetLiveIndexNameFromSetting()).Index("*"))
                            .Add(a => a.Alias(GetLiveIndexNameFromSetting()).Index(currentIndexName));
                    },
                    cancellationToken: ct)
                .ConfigureAwait(false);
            var oldIndices = (await Client.GetIndicesPointingToAliasAsync(GetOldIndexNameFromSetting()).ConfigureAwait(false))
                .OrderByDescending(name => name)
                .Skip(2);
            foreach (var oldIndex in oldIndices)
            {
                await Log($"Deleting old index '{oldIndex}'", contextProfileName).ConfigureAwait(false);
                await Client.DeleteIndexAsync(oldIndex, cancellationToken: ct).ConfigureAwait(false);
                await Log($"Old index '{oldIndex}' Deleted", contextProfileName).ConfigureAwait(false);
            }
            await Log("Exited", contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Gets the analysis and sets the synonym list.</summary>
        /// <param name="analysis">The analysis.</param>
        /// <param name="synonyms">The synonyms.</param>
        /// <returns>The AnalysisDescriptor after modification.</returns>
        protected virtual AnalysisDescriptor GetAnalysis(AnalysisDescriptor analysis, IEnumerable<string> synonyms)
        {
            Synonyms = synonyms;
            return Analysis(analysis);
        }

        /// <summary>Analysis the given analysis.</summary>
        /// <param name="analysis">The analysis.</param>
        /// <returns>The AnalysisDescriptor after modification.</returns>
        protected virtual AnalysisDescriptor Analysis(AnalysisDescriptor analysis) => analysis
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
                    .GenerateNumberParts()
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
        #endregion
    }
}
