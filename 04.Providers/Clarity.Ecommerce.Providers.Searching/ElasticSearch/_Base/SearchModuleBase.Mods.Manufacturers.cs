// <copyright file="SearchModuleBase.Mods.Manufacturers.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. manufacturers class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.Linq;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    internal abstract partial class SearchModuleBase<TSearchViewModel, TSearchForm, TIndexModel>
        where TSearchViewModel : SearchViewModelBase<TSearchForm, TIndexModel>, new()
        where TSearchForm : SearchFormBase, new()
        where TIndexModel : IndexableModelBase
    {
        #region Constant Strings
        protected const string NestedNameForRecordManufacturers = "record-manufacturers";
        protected const string NestedNameForRecordManufacturerSingle = "record-single-manufacturer";
        protected const string NestedNameForRecordManufacturersAny = "record-manufacturers-any";
        protected const string NestedNameForRecordManufacturersAllPrefix = "record-manufacturers-all-";
        protected const string TermsNameForRecordManufacturerIDs = "record-manufacturers-ids";
        protected const string TermsNameForRecordManufacturerKeys = "record-manufacturers-keys";
        protected const string TermsNameForRecordManufacturerNames = "record-manufacturers-names";
        #endregion

        /// <summary>Manufacturers single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer ManufacturersSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.ManufacturerID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordManufacturerSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleManufacturer)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Manufacturers)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.Manufacturers!.First().ID.Suffix(Keyword), form.ManufacturerID)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Manufacturers any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer ManufacturersAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.ManufacturerIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForRecordManufacturersAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyManufacturers)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Manufacturers)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.Manufacturers!.First().ID.Suffix(Keyword)).Terms(form.ManufacturerIDsAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Manufacturers all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer ManufacturersAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.ManufacturerIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.ManufacturerIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordManufacturersAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllManufacturers)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Manufacturers)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.Manufacturers!.First().ID.Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for manufacturers.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForManufacturers(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.ManufacturerIDs = result.Aggregations.Nested(NestedNameForRecordManufacturers)
                ////?.Aggregations
                ?.Terms(TermsNameForRecordManufacturerIDs)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }

        /// <summary>Appends the aggregations for manufacturers.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForManufacturers(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Nested(NestedNameForRecordManufacturers, n => n
                .Path(p => p.Manufacturers)
                .Aggregations(ap => ap
                    .Terms(TermsNameForRecordManufacturerIDs, ts => ts
                        .Field(p => p.Manufacturers!.First().ID.Suffix(Keyword))
                        .Missing(0).Size(30)
                    )
                    .Terms(TermsNameForRecordManufacturerKeys, ts => ts
                        .Field(p => p.Manufacturers!.First().Key.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                    .Terms(TermsNameForRecordManufacturerNames, ts => ts
                        .Field(p => p.Manufacturers!.First().Name.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                )
            );
            return returnQuery;
        }
    }
}
