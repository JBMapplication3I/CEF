// <copyright file="SearchModuleBase.Mods.Attributes.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. attributes class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Providers.Searching;
    using Nest;

    internal abstract partial class SearchModuleBase<TSearchViewModel, TSearchForm, TIndexModel>
        where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>, new()
        where TSearchForm : SearchFormBase, new()
        where TIndexModel : IndexableModelBase
    {
        #region Constant Strings
        protected const string NestedNameForRecordAttributes = "record-attributes";
        protected const string NestedNameForRecordAttributesAny = "record-attributes-any";
        protected const string NestedNameForRecordAttributesAllPrefix = "record-attributes-all-";
        protected const string TermsNameForAttributeKeys = "attribute-keys";
        protected const string TermsNameForAttributeValues = "attribute-values";
        #endregion

        /// <summary>Attribute values any bool query descriptor.</summary>
        /// <param name="b">   The BoolQueryDescriptor{TIndexModel} to process.</param>
        /// <param name="form">The form.</param>
        /// <returns>A BoolQueryDescriptor{TIndexModel}.</returns>
        protected virtual BoolQueryDescriptor<TIndexModel> AttributeValuesAnyBoolQueryDescriptor(
            BoolQueryDescriptor<TIndexModel> b,
            TSearchForm form)
        {
            // Must have attributes to filter by
            if (form.AttributesAny == null)
            {
                return b;
            }
            var filterList = form.AttributesAny
                .Where(x => !string.IsNullOrWhiteSpace(x.Key) && x.Value?.All(string.IsNullOrWhiteSpace) == false)
                .ToDictionary(x => x.Key, x => x.Value.Where(y => !string.IsNullOrWhiteSpace(y)))
                .ToList();
            if (filterList.Count == 0)
            {
                return b;
            }
            return new BoolQueryDescriptor<TIndexModel>()
                .MinimumShouldMatch(MinimumShouldMatch.Percentage(1.00d)) // 100%
                .Should(filterList
                    .Select(x => new QueryContainerDescriptor<TIndexModel>()
                        .Bool(b1 => b1
                            .Should(m => m.Term(p => p.FilterableAttributes!.First().Key.Suffix(Keyword), x.Key)
                                && m.Terms(t => t.Field(p => p.FilterableAttributes!.First().Value.Suffix(Keyword)).Terms(x.Value))
                            )
                        )
                    ).ToArray()
                );
        }

        /// <summary>Attributes any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer AttributesAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form)
        {
            if (form.AttributesAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordAttributesAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyAttributes)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.FilterableAttributes)
                    .Query(nq => +nq.Bool(b => AttributeValuesAnyBoolQueryDescriptor(b, form)))
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Attributes all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer AttributesAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form)
        {
            if (form.AttributesAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.AttributesAll
                .Where(x => !string.IsNullOrWhiteSpace(x.Key)
                    && x.Value?.All(string.IsNullOrWhiteSpace) == false)
                .ToDictionary(x => x.Key, x => x.Value.Where(y => !string.IsNullOrWhiteSpace(y)))
                .ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordAttributesAllPrefix}{filter.Key.ToLower().Replace(" ", "-")}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllAttributes)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.FilterableAttributes)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(s => s.Term(p => p.FilterableAttributes!.First().Key.Suffix(Keyword), filter.Key)
                                    && s.Terms(t => t.Field(p => p.FilterableAttributes!.First().Value.Suffix(Keyword)).Terms(filter.Value))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for attributes.</summary>
        /// <param name="model"> The model.</param>
        /// <param name="result">The result.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForAttributes(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result)
        {
            model.AttributesDict = result.Aggregations.Nested(NestedNameForRecordAttributes)
                .Terms(TermsNameForAttributeKeys)
                .Buckets
                .ToDictionary(
                    k => k.Key,
                    v => ((List<IBucket>)((BucketAggregate)result.Aggregations.Nested(NestedNameForRecordAttributes)
                                .Terms(TermsNameForAttributeKeys)
                                .Buckets
                                .First(x => x.Key == v.Key)
                                .First()
                                .Value)
                            .Items)
                        .Cast<KeyedBucket<object>>()
                        .ToDictionary(k2 => (k2.Key as string)!, v2 => v2.DocCount));
        }

        /// <summary>Appends the aggregations for attributes.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForAttributes(
            AggregationContainerDescriptor<TIndexModel> returnQuery)
        {
            returnQuery &= returnQuery.Nested(NestedNameForRecordAttributes, n => n
                .Path(p => p.FilterableAttributes)
                .Aggregations(aa => aa
                    .Terms(TermsNameForAttributeKeys, tsKey => tsKey
                        .Field(p => p.FilterableAttributes!.First().Key.Suffix(Keyword))
                        .Missing(NotAvailable)
                        .Aggregations(aaa => aaa
                            .Terms(TermsNameForAttributeValues, tsValue => tsValue
                                .Field(p => p.FilterableAttributes!.First().Value.Suffix(Keyword))
                                .Missing(NotAvailable)
                                .Size(100)
                            )
                        ).Size(100)
                    )
                )
            );
            return returnQuery;
        }
    }
}
