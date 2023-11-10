// <copyright file="SearchModuleBase.Mods.RatingRanges.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. rating ranges class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    internal abstract partial class SearchModuleBase<TSearchViewModel, TSearchForm, TIndexModel>
        where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>, new()
        where TSearchForm : SearchFormBase, new()
        where TIndexModel : IndexableModelBase
    {
        #region Constant Strings
        protected const string RangeNameForRecordRatingRanges = "record-rating-ranges";
        protected const string RangeNameForRecordRatingRangesMin = "record-rating-ranges-min";
        protected const string RangeNameForRecordRatingRangesMax = "record-rating-ranges-max";
        #endregion

        /// <summary>Rating ranges query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer RatingRangesQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.RatingRanges?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b.Name(RangeNameForRecordRatingRanges)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsRatingRanges)
                    .Should(CreateRatingRangeQueryLambdas(form))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                );
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for rating ranges.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForRatingRanges(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            if (result.Aggregations.Range(RangeNameForRecordRatingRanges)?.Buckets == null)
            {
                return;
            }
            var ratingRanges = result.Aggregations.Range(RangeNameForRecordRatingRanges)
                .Buckets
                .Select(x => new AggregateRatingRange
                {
                    From = x.From,
                    To = x.To,
                    DocCount = x.DocCount,
                    Label = x.Key,
                })
                .ToList();
            model.RatingRanges = ratingRanges;
        }

        /// <summary>Appends the aggregations for rating ranges.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForRatingRanges(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Range(
                RangeNameForRecordRatingRanges,
                rr => rr
                    .Field(f => f.RatingToIndexAs)
                    .Ranges(CreateRatingRangeLambdas())
                    .Aggregations(aa => aa
                        .Max(
                            RangeNameForRecordRatingRangesMax,
                            ma => ma.Field(f => f.RatingToIndexAs).Missing(0))
                        .Min(
                            RangeNameForRecordRatingRangesMin,
                            ma => ma.Field(f => f.RatingToIndexAs).Missing(0))));
            return returnQuery;
        }

        /// <summary>Rating range query descriptor.</summary>
        /// <param name="form">   The form.</param>
        /// <returns>A NumericRangeQueryDescriptor{TIndexModel}.</returns>
        protected virtual Func<QueryContainerDescriptor<TIndexModel>, QueryContainer>[] CreateRatingRangeQueryLambdas(
            TSearchForm form)
        {
            var ratingRangeQueries = new List<Func<QueryContainerDescriptor<TIndexModel>, QueryContainer>>();
            if (form.RatingRanges?.All(string.IsNullOrWhiteSpace) != false)
            {
                return ratingRangeQueries.ToArray();
            }
            var regex = new Regex(@"^\s*(?<from>\d+(?:\.\d+)?)\s*-\s*(?<to>\d+(?:\.\d+)?)\s*$");
            ratingRangeQueries.AddRange(
                from ratingRange in form.RatingRanges
                let match = regex.Match(ratingRange)
                select (Func<QueryContainerDescriptor<TIndexModel>, QueryContainer>)(s => s.Range(
                    c => c.Name(ratingRange)
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsRatingRanges)
                        .Field(p => p.RatingToIndexAs)
                        .GreaterThanOrEquals(double.Parse(match.Groups["from"].Value) + 0.0001d)
                        .LessThanOrEquals(double.Parse(match.Groups["to"].Value) + 0.0001d)
                        .Relation(RangeRelation.Within))));
            return ratingRangeQueries.ToArray();
        }

        /// <summary>Creates rating range aggregates based on increment config values.</summary>
        /// <returns>A new array of <see cref="Func{AggregationRangeDescriptor, IAggregationRange}"/>.</returns>
        protected virtual Func<AggregationRangeDescriptor, IAggregationRange>[] CreateRatingRangeLambdas()
        {
            List<Func<AggregationRangeDescriptor, IAggregationRange>> results = new();
            for (var i = 0; i < Contract.RequiresNotEmpty(ElasticSearchingProviderConfig.SearchingRatingRangesIncrements).Count(); i++)
            {
                var r = ElasticSearchingProviderConfig.SearchingRatingRangesIncrements![i];
                if (i == 0)
                {
                    // First
                    results.Add(x => x.To(r.To + 0.0001d).Key($"{r.From + 0.01d} - {r.To}"));
                }
                else if (i == ElasticSearchingProviderConfig.SearchingRatingRangesIncrements.Length - 1)
                {
                    // Last
                    results.Add(x => x.From(r.From + 0.0001d).Key($"{r.From + 0.01d} - {r.To}"));
                }
                else
                {
                    // Middle
                    results.Add(x => x.From(r.From + 0.0001d).To(r.To + 0.0001d).Key($"{r.From + 0.01d} - {r.To}"));
                }
            }
            return results.ToArray();
        }
    }
}
