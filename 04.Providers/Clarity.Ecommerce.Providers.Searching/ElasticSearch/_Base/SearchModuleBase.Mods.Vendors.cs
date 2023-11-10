// <copyright file="SearchModuleBase.Mods.Vendors.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. vendors class</summary>
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
        protected const string NestedNameForRecordVendors = "record-vendors";
        protected const string NestedNameForRecordVendorSingle = "record-single-vendor";
        protected const string NestedNameForRecordVendorsAny = "record-vendors-any";
        protected const string NestedNameForRecordVendorsAllPrefix = "record-vendors-all-";
        protected const string TermsNameForRecordVendorIDs = "record-vendors-ids";
        protected const string TermsNameForRecordVendorKeys = "record-vendors-keys";
        protected const string TermsNameForRecordVendorNames = "record-vendors-names";
        #endregion

        /// <summary>Vendors single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer VendorsSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.VendorID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordVendorSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleVendor)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Vendors)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.Vendors!.First().ID.Suffix(Keyword), form.VendorID)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Vendors any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer VendorsAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.VendorIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForRecordVendorsAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyVendors)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Vendors)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.Vendors!.First().ID.Suffix(Keyword)).Terms(form.VendorIDsAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Vendors all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer VendorsAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.VendorIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.VendorIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordVendorsAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllVendors)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Vendors)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.Vendors!.First().ID.Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for vendors.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForVendors(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.VendorIDs = result.Aggregations.Nested(NestedNameForRecordVendors)
                ////?.Aggregations
                ?.Terms(TermsNameForRecordVendorIDs)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }

        /// <summary>Appends the aggregations for vendors.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForVendors(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Nested(NestedNameForRecordVendors, n => n
                .Path(p => p.Vendors)
                .Aggregations(ap => ap
                    .Terms(TermsNameForRecordVendorIDs, ts => ts
                        .Field(p => p.Vendors!.First().ID.Suffix(Keyword))
                        .Missing(0).Size(30)
                    )
                    .Terms(TermsNameForRecordVendorKeys, ts => ts
                        .Field(p => p.Vendors!.First().Key.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                    .Terms(TermsNameForRecordVendorNames, ts => ts
                        .Field(p => p.Vendors!.First().Name.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                )
            );
            return returnQuery;
        }
    }
}
