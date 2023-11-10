// <copyright file="StoreSearchModule.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store search module class</summary>
// ReSharper disable MissingLinebreak, StyleCop.SA1002, StyleCop.SA1009, StyleCop.SA1111, StyleCop.SA1115, StyleCop.SA1116, StyleCop.SA1123
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Web.Search
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Providers.Searching;
    using Nest;

    /// <summary>A store search module.</summary>
    /// <seealso cref="SearchModuleBase{StoreSearchViewModel,StoreCatalogSearchForm,IndexableStoreModel}"/>
    internal class StoreSearchModule
        : SearchModuleBase<StoreSearchViewModel, StoreCatalogSearchForm, IndexableStoreModel>
    {
        /// <inheritdoc/>
        protected override IPromise<IList<ISort>> Sort(SortDescriptor<IndexableStoreModel> sort)
        {
            switch (Form.Sort)
            {
                case Enums.SearchSort.NameAscending:
                {
                    return sort.Ascending(p => p.Name.Suffix("raw"));
                }
                case Enums.SearchSort.NameDescending:
                {
                    return sort.Descending(p => p.Name.Suffix("raw"));
                }
                case Enums.SearchSort.Recent:
                {
                    return sort.Field(sortField => sortField.Field(p => p.UpdatedDate ?? p.CreatedDate).Descending());
                }
                // ReSharper disable RedundantCaseLabel
                case Enums.SearchSort.Relevance:
                case Enums.SearchSort.PriceAscending:
                case Enums.SearchSort.PriceDescending:
                case Enums.SearchSort.Defined:
                case Enums.SearchSort.Popular:
                // ReSharper restore RedundantCaseLabel
                default:
                    return sort.Descending("_score");
            }
        }

        /// <inheritdoc/>
        protected override IAggregationContainer Aggregations(AggregationContainerDescriptor<IndexableStoreModel> a)
        {
            // TODO: Aggregations with multiple search properties (especially attributes)
            return a
                    #region Attributes
                    .Nested("store-attributes", n => n
                        .Path(p => p.Attributes)
                        .Aggregations(aa => aa
                            .Terms("attribute-keys", tsKey => tsKey
                                .Field(p => p.Attributes.First().Key.Suffix("keyword"))
                                .Missing("N/A")
                                .Aggregations(aaa => aaa
                                    .Terms("attribute-values", tsValue => tsValue
                                        .Field(p => p.Attributes.First().Value.Suffix("keyword"))
                                        .Missing("N/A")
                                        .Size(100)
                                    )
                                ).Size(100)
                            )
                        )
                    )
                #endregion
                ;
        }

        /// <inheritdoc/>
        protected override QueryContainer Query(QueryContainerDescriptor<IndexableStoreModel> q)
        {
            var returnQuery = q
                #region Match by Name first, with a massive boost
                .Match(m => m
                    .Field(p => p.Name.Suffix("keyword"))
                    .Boost(1000)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(Form.Query)
                )
                #endregion
                #region Match by CustomKey/SKU second, with a massive boost
                || q.Match(m => m
                    .Field(p => p.CustomKey.Suffix("keyword"))
                    .Boost(1000)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(Form.Query)
                )
                #endregion
                #region FunctionScore
                || q.FunctionScore(fs => fs
                    .MaxBoost(10)
                    .Functions(ff => ff)
                    .Query(query => query
                        .MultiMatch(m => m
                            .Fields(f => f
                                 .Field(p => p.ID.Suffix("keyword"), 1.5)
                                 ////.Field(p => p.ID, 1.5)
                                 .Field(p => p.CustomKey.Suffix("keyword"), 2)
                                 .Field(p => p.CustomKey, 2)
                                 .Field(p => p.Name.Suffix("keyword"), 14)
                                 .Field(p => p.Name, 6)
                                 ////.Field(p => p.Description, 0.25)
                            )
                            .Operator(Operator.And)
                            .Fuzziness(Fuzziness.Auto)
                            .Query(Form.Query)
                        )
                    )
                )
                #endregion
                #region Attributes (Any)
                && +q.Nested(n => n
                    .Name("store-attributes-any")
                    .Boost(1.1)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Attributes)
                    .Query(nq => +nq.Bool(AttributeValuesAnyBoolQueryDescriptor))
                    .IgnoreUnmapped()
                )
                #endregion
                ;
            #region Attributes (All)
            // Must have attributes to filter by
            if (Form.AttributesAll == null)
            {
                return q;
            }
            var filterList = Form.AttributesAll
                .Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Value?.All(string.IsNullOrWhiteSpace) == false)
                .ToDictionary(x => x.Key, x => x.Value.Where(y => !string.IsNullOrWhiteSpace(y)))
                .ToList();
            if (!filterList.Any())
            {
                return q;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"store-attributes-all-{filter.Key.ToLower().Replace(" ", "-")}")
                        .Boost(1.1)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Attributes)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(s => s.Term(p => p.Attributes.First().Key.Suffix("keyword"), filter.Key)
                                          && s.Terms(t => t.Field(p => p.Attributes.First().Value.Suffix("keyword")).Terms(filter.Value))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            #endregion
            return returnQuery;
        }

        /// <inheritdoc/>
        // ReSharper disable once CyclomaticComplexity
        protected override void SearchViewModelAdditionalAssignments(StoreSearchViewModel model, ISearchResponse<IndexableStoreModel> result)
        {
            #region Attributes
            if (result.Aggregations?.Nested("store-attributes") == null)
            {
                return;
            }
            var attributes = result.Aggregations.Nested("store-attributes")
                .Terms("attribute-keys")
                .Buckets
                .ToDictionary(
                    k => k.Key,
                    v => ((List<IBucket>)((BucketAggregate)result.Aggregations.Nested("store-attributes")
                                .Terms("attribute-keys")
                                .Buckets
                                .First(x => x.Key == v.Key)
                                .First()
                                .Value)
                            .Items)
                        .Cast<KeyedBucket<object>>()
                        .ToDictionary(k2 => k2.Key as string, v2 => v2.DocCount)
                );
            model.Attributes = attributes;
            #endregion
        }

        private BoolQueryDescriptor<IndexableStoreModel> AttributeValuesAnyBoolQueryDescriptor(BoolQueryDescriptor<IndexableStoreModel> b)
        {
            // Must have attributes to filter by
            if (Form.AttributesAny == null)
            {
                return b;
            }
            var filterList = Form.AttributesAny
                .Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Value?.All(string.IsNullOrWhiteSpace) == false)
                .ToDictionary(x => x.Key, x => x.Value.Where(y => !string.IsNullOrWhiteSpace(y)))
                .ToList();
            if (!filterList.Any())
            {
                return b;
            }
            return new BoolQueryDescriptor<IndexableStoreModel>()
                .MinimumShouldMatch(MinimumShouldMatch.Percentage(1.00d)) // 100%
                .Should(filterList
                    .Select(x => new QueryContainerDescriptor<IndexableStoreModel>()
                        .Bool(b1 => b1
                            .Should(m => m.Term(p => p.Attributes.First().Key.Suffix("keyword"), x.Key)
                                      && m.Terms(t => t.Field(p => p.Attributes.First().Value.Suffix("keyword")).Terms(x.Value))
                            )
                        )
                    ).ToArray()
                );
        }
    }
}
