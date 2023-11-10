// <copyright file="SearchModuleBase.Mods.PricingRanges.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. pricing ranges class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Interfaces.Providers.Searching;
    using Nest;

    internal abstract partial class SearchModuleBase<TSearchViewModel, TSearchForm, TIndexModel>
        where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>, new()
        where TSearchForm : SearchFormBase, new()
        where TIndexModel : IndexableModelBase
    {
        #region Constant Strings
        protected const string RangeNameForRecordPricingRanges = "record-pricing-ranges";
        protected const string RangeNameForRecordPricingRangesMin = "record-pricing-ranges-min";
        protected const string RangeNameForRecordPricingRangesMax = "record-pricing-ranges-max";
        #endregion

        /// <summary>Pricing ranges query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer PricingRangesQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.PricingRanges?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b.Name(RangeNameForRecordPricingRanges)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsPricingRanges)
                    .Should(CreatePricingRangeQueryLambdas(form))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                );
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for pricing ranges.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForPricingRanges(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            if (result.Aggregations.Range(RangeNameForRecordPricingRanges)?.Buckets == null)
            {
                return;
            }
            var pricingRanges = result.Aggregations.Range(RangeNameForRecordPricingRanges)
                .Buckets
                .Select(x => new AggregatePricingRange
                {
                    From = x.From,
                    To = x.To,
                    DocCount = x.DocCount,
                    Label = x.Key,
                })
                .ToList();
            model.PricingRanges = pricingRanges;
        }

        /// <summary>Appends the aggregations for pricing ranges.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForPricingRanges(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Range(RangeNameForRecordPricingRanges, rr => rr
                .Field(f => f.PricingToIndexAs)
                .Ranges(
                    CreatePricingRangeLambdas()
                ).Aggregations(aa => aa
                    .Max(RangeNameForRecordPricingRangesMax, ma => ma
                        .Field(f => f.PricingToIndexAs)
                        .Missing(0)
                    )
                    .Min(RangeNameForRecordPricingRangesMin, ma => ma
                        .Field(f => f.PricingToIndexAs)
                        .Missing(0)
                    )
                )
            );
            return returnQuery;
        }

        /// <summary>Pricing range query descriptor.</summary>
        /// <param name="form">The form.</param>
        /// <returns>A NumericRangeQueryDescriptor{TIndexModel}.</returns>
        protected virtual Func<QueryContainerDescriptor<TIndexModel>, QueryContainer>[] CreatePricingRangeQueryLambdas(
            TSearchForm form)
        {
            var pricingRangeQueries = new List<Func<QueryContainerDescriptor<TIndexModel>, QueryContainer>>();
            if (form.PricingRanges?.All(string.IsNullOrWhiteSpace) != false)
            {
                return pricingRangeQueries.ToArray();
            }
            var regex = new Regex("[\\d.]+");
            foreach (var pricingRange in form.PricingRanges)
            {
                var tempPricingRange = pricingRange;
                // Less than first increment
                if (pricingRange.Contains("<"))
                {
                    pricingRangeQueries.Add(s => s
                        .Range(c => c
                            .Name(tempPricingRange)
                            .Boost(ElasticSearchingProviderConfig.SearchingBoostsPricingRanges)
                            .Field(p => p.PricingToIndexAs)
                            .LessThan(ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Amount)
                            .Relation(RangeRelation.Within)));
                }
                // Greater than last increment stop
                else if (pricingRange.Contains("+"))
                {
                    var lastIncrementStop = ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Stop
                        ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Stop
                        ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Stop
                        ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Stop;
                    pricingRangeQueries.Add(s => s
                        .Range(c => c
                            .Name(tempPricingRange)
                            .Boost(ElasticSearchingProviderConfig.SearchingBoostsPricingRanges)
                            .Field(p => p.PricingToIndexAs)
                            .GreaterThanOrEquals(lastIncrementStop)
                            .Relation(RangeRelation.Within)));
                }
                else
                {
                    var match = regex.Match(pricingRange);
                    var amount = double.Parse(match.Value);
                    switch (amount)
                    {
                        case var x when x < ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Stop:
                        {
                            pricingRangeQueries.Add(s => s
                                .Range(c => c
                                    .Name(tempPricingRange)
                                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsPricingRanges)
                                    .Field(p => p.PricingToIndexAs)
                                    .GreaterThanOrEquals(amount)
                                    .LessThan(amount + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Amount)
                                    .Relation(RangeRelation.Within)));
                            break;
                        }
                        case var x when x < ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Stop:
                        {
                            pricingRangeQueries.Add(s => s
                                .Range(c => c
                                    .Name(tempPricingRange)
                                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsPricingRanges)
                                    .Field(p => p.PricingToIndexAs)
                                    .GreaterThanOrEquals(amount)
                                    .LessThan(amount + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Amount)
                                    .Relation(RangeRelation.Within)));
                            break;
                        }
                        case var x when x < ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Stop:
                        {
                            pricingRangeQueries.Add(s => s
                                .Range(c => c
                                    .Name(tempPricingRange)
                                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsPricingRanges)
                                    .Field(p => p.PricingToIndexAs)
                                    .GreaterThanOrEquals(amount)
                                    .LessThan(amount + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Amount)
                                    .Relation(RangeRelation.Within)));
                            break;
                        }
                        case var x when x < ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Stop:
                        {
                            pricingRangeQueries.Add(s => s
                                .Range(c => c
                                    .Name(tempPricingRange)
                                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsPricingRanges)
                                    .Field(p => p.PricingToIndexAs)
                                    .GreaterThanOrEquals(amount)
                                    .LessThan(amount + (ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Amount ?? 0))
                                    .Relation(RangeRelation.Within)));
                            break;
                        }
                    }
                }
            }
            return pricingRangeQueries.ToArray();
        }

        /// <summary>Creates pricing range aggregates based on increment config values.</summary>
        /// <returns>A new array of <see cref="Func{AggregationRangeDescriptor, IAggregationRange}"/>.</returns>
        protected virtual Func<AggregationRangeDescriptor, IAggregationRange>[] CreatePricingRangeLambdas()
        {
            var pricingRanges = new List<Func<AggregationRangeDescriptor, IAggregationRange>>
            {
                // < ${first increment amount}
                x => x
                    .To(ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Amount)
                    .Key($"< ${ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Amount}"),
            };
            // Create possible increment amount values
            for (var i = ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Amount;
                    i < ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Stop;
                    i += ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Amount)
            {
                var from = i;
                pricingRanges.Add(x => x
                    .From(from)
                    .To(from + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Amount - 0.0001)
                    .Key($"${from} - ${from + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Amount - 0.01}"));
            }
            if (ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Amount.HasValue
                && ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Stop.HasValue)
            {
                for (double? i = ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Stop;
                        i < ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Stop;
                        i += ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Amount)
                {
                    var from = i;
                    pricingRanges.Add(x => x
                        .From(from)
                        .To(from + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Amount - 0.0001)
                        .Key($"${from} - ${from + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Amount - 0.01}"));
                }
                if (ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Amount.HasValue
                    && ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Stop.HasValue)
                {
                    for (var i = ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Stop;
                            i < ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Stop;
                            i += ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Amount)
                    {
                        var from = i;
                        pricingRanges.Add(x => x
                            .From(from)
                            .To(from + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Amount - 0.0001)
                            .Key($"${from} - ${from + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Amount - 0.01}"));
                    }
                    if (ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Amount.HasValue
                        && ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Stop.HasValue)
                    {
                        for (var i = ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Stop;
                                i < ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Stop;
                                i += ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Amount)
                        {
                            var from = i;
                            pricingRanges.Add(x => x
                                .From(from)
                                .To(from + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Amount - 0.0001)
                                .Key($"${from} - ${from + ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Amount - 0.01}"));
                        }
                    }
                }
            }
            // ${last increment amount} +
            pricingRanges.Add(x => x.From(ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Stop
                ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Stop
                ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Stop
                ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Stop)
                .Key("$" + (ElasticSearchingProviderConfig.SearchingPricingRangesIncrement4Stop
                    ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement3Stop
                    ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement2Stop
                    ?? ElasticSearchingProviderConfig.SearchingPricingRangesIncrement1Stop) + " +"));
            return pricingRanges.ToArray();
        }
    }
}
