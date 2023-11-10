// <copyright file="StoreIndexer.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store indexer class</summary>
// ReSharper disable StyleCop.SA1009, StyleCop.SA1111, StyleCop.SA1115, StyleCop.SA1116
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Indexer
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Data;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A store indexer.</summary>
    internal static class StoreIndexer
    {
        static StoreIndexer()
        {
            Client = SearchConfiguration.GetClient();
            DumpReader = new StoreDumpReader();
            CurrentIndexName = SearchConfiguration.CreateStoreIndexName();
        }

        private static ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        private static ElasticClient Client { get; }

        private static StoreDumpReader DumpReader { get; }

        private static string CurrentIndexName { get; }

        /// <summary>Indexes.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        public static void Index(string contextProfileName)
        {
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(Index)}", "Entered", contextProfileName);
            CreateIndex(contextProfileName);
            IndexStores(contextProfileName);
            SwapAlias(contextProfileName);
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(Index)}", "Exited", contextProfileName);
        }

        /// <summary>Deletes the index if exists.</summary>
        /// <param name="index">             Zero-based index of the.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        public static async Task DeleteIndexIfExistsAsync(string index, string contextProfileName)
        {
            await Logger.LogInformationAsync($"{nameof(StoreIndexer)}.${nameof(DeleteIndexIfExistsAsync)}", "Entered", contextProfileName).ConfigureAwait(false);
            if (!(await Client.IndexExistsAsync(index).ConfigureAwait(false)).Exists)
            {
                return;
            }
            await Client.DeleteIndexAsync(index).ConfigureAwait(false);
            await Logger.LogInformationAsync($"{nameof(StoreIndexer)}.${nameof(DeleteIndexIfExistsAsync)}", "Exited", contextProfileName).ConfigureAwait(false);
        }

        private static void CreateIndex(string contextProfileName)
        {
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(CreateIndex)}", "Entered", contextProfileName);
            Client.CreateIndex(CurrentIndexName, i => i
                .Settings(s => s
                    .NumberOfShards(2)
                    .NumberOfReplicas(0)
                    .Analysis(Analysis))
                .Mappings(m => m.Map<IndexableStoreModel>(MapStore))
            );
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(CreateIndex)}", "Exited", contextProfileName);
        }

        private static TypeMappingDescriptor<IndexableStoreModel> MapStore(TypeMappingDescriptor<IndexableStoreModel> map) => map
            .AutoMap()
            .Properties(ps => ps
                .Text(t => t.Name(p => p.Name).Analyzer("store-name-analyzer").Fields(f => f.Text(p => p.Name("keyword").Analyzer("store-name-keyword")).Keyword(p => p.Name("raw"))))
                .Text(t => t.Name(p => p.CustomKey).Analyzer("store-name-analyzer").Fields(f => f.Text(p => p.Name("keyword").Analyzer("store-name-keyword")).Keyword(p => p.Name("raw"))))
                .Nested<IndexableSerializableAttributeObject>(n => n
                    .Name(p => p.Attributes.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Fields(fs => fs.Text(ss => ss.Name("raw")).Keyword(ss => ss.Name("keyword"))))
                        .Text(t => t.Name(a => a.Key).Analyzer("attribute-name-analyzer").Fields(fs => fs.Text(ss => ss.Name("raw").Analyzer("attribute-name-keyword")).Keyword(ss => ss.Name("keyword"))))
                        .Text(t => t.Name(a => a.UofM).Fields(fs => fs.Text(ss => ss.Name("raw")).Keyword(ss => ss.Name("keyword"))))
                        .Text(t => t.Name(a => a.Value).Fields(fs => fs.Text(ss => ss.Name("raw")).Keyword(ss => ss.Name("keyword"))))
                    )
                )
                .Completion(c => c.Name(p => p.SuggestedByName).Analyzer("store-name-analyzer"))
                .Completion(c => c.Name(p => p.SuggestedByCustomKey).Analyzer("store-name-analyzer"))
            );

        private static AnalysisDescriptor Analysis(AnalysisDescriptor analysis) => analysis
            .Tokenizers(tokenizers => tokenizers
                .Pattern("store-name-tokenizer", p => p.Pattern(@"\W+"))
                .Pattern("category-name-tokenizer", p => p.Pattern(@"(?:(.+)(?:\W*))\|").Group(1))
                .Pattern("attribute-name-tokenizer", p => p.Pattern(@"\W+"))
            )
            .TokenFilters(tokenFilters => tokenFilters
                .WordDelimiter("store-name-words", w => w
                    .SplitOnCaseChange()
                    .PreserveOriginal()
                    .SplitOnNumerics()
                    .GenerateNumberParts(false)
                    .GenerateWordParts()
                )
                .WordDelimiter("category-name-words", w => w
                    .SplitOnCaseChange()
                    .PreserveOriginal()
                    .SplitOnNumerics(false)
                    .GenerateNumberParts()
                    .GenerateWordParts()
                )
                .WordDelimiter("attribute-name-words", w => w
                    .SplitOnCaseChange()
                    .PreserveOriginal()
                    .SplitOnNumerics()
                    .GenerateNumberParts()
                    .GenerateWordParts()
                )
            )
            .Analyzers(analyzers => analyzers
                .Custom("store-name-analyzer", c => c.Tokenizer("store-name-tokenizer").Filters("store-name-words", "lowercase"))
                .Custom("category-name-analyzer", c => c.Tokenizer("category-name-tokenizer").Filters("category-name-words", "lowercase"))
                .Custom("attribute-name-analyzer", c => c.Tokenizer("attribute-name-tokenizer").Filters("attribute-name-words", "lowercase"))
                .Custom("store-name-keyword", c => c.Tokenizer("keyword").Filters("lowercase"))
                .Custom("category-name-keyword", c => c.Tokenizer("keyword").Filters("lowercase"))
                .Custom("attribute-name-keyword", c => c.Tokenizer("keyword").Filters("lowercase"))
            );

        private static void IndexStores(string contextProfileName)
        {
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(IndexStores)}", "Entered", contextProfileName);
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(IndexStores)}", "Indexing documents into ElasticSearch...", contextProfileName);
            /*
            var counter = 0;
            var errorCounter = 0;
            foreach (var store in DumpReader.GeStores())
            {
                ++counter;
                var result = Client.Index(store, i => i.Type("store"));
                if (result.IsValid) { continue; }
                ++errorCounter;
                QuickLogger.Logger(contextProfileName, "StoreIndexer.IndexStores(...)", result.DebugInformation);
            }
            QuickLogger.Logger(contextProfileName, "StoreIndexer.IndexStores(...)", $"Completed {counter:N0} records, encountered {errorCounter:N0} errors");
            //*/
            /*
            var result = Client.Bulk(b =>
            {
                foreach (var store in DumpReader.GetStores())
                {
                    b.Index<IndexableStoreModel>(i => i.Document(store));
                }
                return b;
            });
            if (!result.IsValid)
            {
                foreach (var item in result.ItemsWithErrors)
                {
                    QuickLogger.Logger(contextProfileName, "StoreIndexer.IndexStores(...)", $"Failed to index document {item.Id}: {item.Error}");
                }
                QuickLogger.Logger(contextProfileName, "StoreIndexer.IndexStores(...)", result.DebugInformation);
            }
            //*/
            // *
            var stores = DumpReader.GetStores(contextProfileName);
            var waitHandle = new CountdownEvent(1);
            var bulkAll = Client.BulkAll(stores, b => b
                .Index(CurrentIndexName)
                .BackOffRetries(2)
                .BackOffTime("5s")
                .RefreshOnCompleted()
                .MaxDegreeOfParallelism(8)
                .Size(100)
            );
            bulkAll.Subscribe(new BulkAllObserver(
                onNext: b =>
                {
                    Logger.LogInformation(
                        $"{nameof(StoreIndexer)}.${nameof(IndexStores)}",
                        $"BulkAllObserver.onNext [Page:{b.Page}][Retries:{b.Retries}]",
                        contextProfileName);
                },
                onError: e =>
                {
                    Logger.LogInformation(
                        $"{nameof(StoreIndexer)}.${nameof(IndexStores)}",
                        "BulkAllObserver.onError",
                        contextProfileName);
                    waitHandle.Signal();
                    throw e;
                },
                onCompleted: () =>
                {
                    Logger.LogInformation(
                        $"{nameof(StoreIndexer)}.${nameof(IndexStores)}",
                        "BulkAllObserver.onCompleted",
                        contextProfileName);
                    waitHandle.Signal();
                }));
            waitHandle.Wait();
            if (!bulkAll.IsDisposed)
            {
                bulkAll.Dispose();
            }
            // */
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(IndexStores)}", "Exited", contextProfileName);
        }

        private static void SwapAlias(string contextProfileName)
        {
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(SwapAlias)}", "Entered", contextProfileName);
            var indexExists = Client.IndexExists(SearchConfiguration.LiveIndexAliasStore).Exists;
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(SwapAlias)}", $"indexExists: {indexExists}", contextProfileName);
            Client.Alias(aliases =>
            {
                Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(SwapAlias)}", "Checking Alias...", contextProfileName);
                if (indexExists)
                {
                    Logger.LogInformation(
                        $"{nameof(StoreIndexer)}.${nameof(SwapAlias)}",
                        "Index exists, so adding an old index alias",
                        contextProfileName);
                    aliases.Add(a => a.Alias(SearchConfiguration.OldIndexAliasStore).Index(SearchConfiguration.LiveIndexAliasStore));
                }
                Logger.LogInformation(
                    $"{nameof(StoreIndexer)}.${nameof(SwapAlias)}",
                    "Swapping live index to the latest content and marking the old one with the old index name pattern",
                    contextProfileName);
                return aliases
                    .Remove(a => a.Alias(SearchConfiguration.LiveIndexAliasStore).Index("*"))
                    .Add(a => a.Alias(SearchConfiguration.LiveIndexAliasStore).Index(CurrentIndexName));
            });
            var oldIndices = Client.GetIndicesPointingToAlias(SearchConfiguration.OldIndexAliasStore)
                .OrderByDescending(name => name)
                .Skip(2);
            foreach (var oldIndex in oldIndices)
            {
                Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(SwapAlias)}", $"Deleting old index '{oldIndex}'", contextProfileName);
                Client.DeleteIndex(oldIndex);
                Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(SwapAlias)}", $"Old index '{oldIndex}' Deleted", contextProfileName);
            }
            Logger.LogInformation($"{nameof(StoreIndexer)}.${nameof(SwapAlias)}", "Exited", contextProfileName);
        }
    }
}
