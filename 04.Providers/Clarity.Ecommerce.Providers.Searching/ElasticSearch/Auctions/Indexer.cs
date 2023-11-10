// <copyright file="Indexer.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product indexer class</summary>
// ReSharper disable StyleCop.SA1009, StyleCop.SA1111, StyleCop.SA1115, StyleCop.SA1116
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Linq;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A product indexer.</summary>
    internal class AuctionIndexer : IndexerBase<AuctionDumpReader, AuctionIndexableModel>
    {
        // <inheritdoc/>
        protected override string GenIndexName()
            => $"{ElasticSearchingProviderConfig.SearchingAuctionIndex}.{DateTime.UtcNow:yyyy-MM-dd.HH-mm-ss}";

        // <inheritdoc/>
        protected override string GetLiveIndexNameFromSetting() => ElasticSearchingProviderConfig.SearchingAuctionIndex;

        // <inheritdoc/>
        protected override string GetOldIndexNameFromSetting() => ElasticSearchingProviderConfig.SearchingAuctionIndexOld;

        // <inheritdoc/>
        protected override TypeMappingDescriptor<AuctionIndexableModel> Map(TypeMappingDescriptor<AuctionIndexableModel> map) => map
            .AutoMap()
            .Properties(ps => ps
                .Text(t => t.Name(p => p.Name).Analyzer(AnalyzerNameForProductName).Fields(f => f.Text(p => p.Name(Keyword).Analyzer(AnalyzerNameForProductKeyword)).Keyword(p => p.Name(Raw))))
                .Text(t => t.Name(p => p.CustomKey).Analyzer(AnalyzerNameForProductName).Fields(f => f.Text(p => p.Name(Keyword).Analyzer(AnalyzerNameForProductKeyword)).Keyword(p => p.Name(Raw))))
                .Text(t => t.Name(p => p.QueryableSerializableAttributeValuesAggregate).Analyzer(AnalyzerNameForProductName).Fields(f => f.Text(p => p.Name(Keyword).Analyzer(AnalyzerNameForProductKeyword)).Keyword(p => p.Name(Raw))))
                .Nested<IndexableAttributeObjectFilter>(n => n
                    .Name(p => p.FilterableAttributes!.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Fields(fs => fs.Text(ss => ss.Name(Raw)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Key).Analyzer(AnalyzerNameForAttributeName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForAttributeKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.UofM).Fields(fs => fs.Text(ss => ss.Name(Raw)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Value).Fields(fs => fs.Text(ss => ss.Name(Raw)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                // NOTE: Intentionally not adding p.QueryableAttributes like p.FilterableAttributes
                .Nested<IndexableRoleFilter>(n => n
                    .Name(p => p.RequiresRolesList!.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.RoleName).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableBrandFilter>(n => n
                    .Name(p => p.Brands!.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Key).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Name).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableCategoryFilter>(n => n
                    .Name(p => p.Categories!.First())
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
                .Nested<IndexableFranchiseFilter>(n => n
                    .Name(p => p.Franchises!.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Key).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Name).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableManufacturerFilter>(n => n
                    .Name(p => p.Manufacturers!.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Key).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Name).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableProductFilter>(n => n
                    .Name(p => p.Products!.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Key).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Name).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableStoreFilter>(n => n
                    .Name(p => p.Stores!.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Key).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Name).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Nested<IndexableVendorFilter>(n => n
                    .Name(p => p.Vendors!.First())
                    .AutoMap()
                    .Properties(props => props
                        .Text(t => t.Name(a => a.ID).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Key).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                        .Text(t => t.Name(a => a.Name).Analyzer(AnalyzerNameForProductName).Fields(fs => fs.Text(ss => ss.Name(Raw).Analyzer(AnalyzerNameForProductKeyword)).Keyword(ss => ss.Name(Keyword))))
                    )
                )
                .Completion(c => c.Name(p => p.SuggestedByName).Analyzer(SuggestAnalyzerNameForProductKeyword))
                .Completion(c => c.Name(p => p.SuggestedByCustomKey).Analyzer(SuggestAnalyzerNameForProductKeyword))
                .Completion(c => c.Name(p => p.SuggestedByCategory).Analyzer(SuggestAnalyzerNameForProductKeyword))
                .Completion(c => c.Name(p => p.SuggestedByQueryableSerializableAttributes).Analyzer(SuggestAnalyzerNameForProductKeyword))
            );
    }
}
