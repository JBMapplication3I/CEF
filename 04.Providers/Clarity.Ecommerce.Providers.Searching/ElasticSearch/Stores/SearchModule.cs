// <copyright file="SearchModule.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store search module class</summary>
// ReSharper disable BadControlBracesLineBreaks, BadMemberAccessSpaces, CyclomaticComplexity, FunctionComplexityOverflow
// ReSharper disable MissingLinebreak, MissingSpace, MultipleStatementsOnOneLine, RedundantCaseLabel
// ReSharper disable RegionWithinTypeMemberBody, UseNullPropagationWhenPossible
// ReSharper disable StyleCop.SA1002, StyleCop.SA1009, StyleCop.SA1019, StyleCop.SA1025, StyleCop.SA1111
// ReSharper disable StyleCop.SA1114, StyleCop.SA1115, StyleCop.SA1116, StyleCop.SA1123
#pragma warning disable CA1822
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Providers.Searching;
    using JSConfigs;
    using Nest;
    using Utilities;

    /// <summary>A store search module.</summary>
    /// <seealso cref="SearchModuleBase{StoreSearchViewModel,StoreCatalogSearchForm,StoreIndexableModel}"/>
    internal partial class StoreSearchModule
        : SearchModuleBase<StoreSearchViewModel, StoreCatalogSearchForm, StoreIndexableModel>
    {
        #region Constant Strings
        protected const string NestedNameForRecordBadges = "record-badges";
        protected const string NestedNameForRecordBadgeSingle = "record-single-badge";
        protected const string NestedNameForRecordBadgesAny = "record-badges-any";
        protected const string NestedNameForRecordBadgesAllPrefix = "record-badges-all-";
        protected const string TermsNameForRecordBadgeIDs = "record-badges-ids";
        protected const string TermsNameForRecordBadgeKeys = "record-badges-keys";
        protected const string TermsNameForRecordBadgeNames = "record-badges-names";
        #endregion

        /// <inheritdoc/>
        protected override IPromise<IList<ISort>> Sort(
            SortDescriptor<StoreIndexableModel> sort,
            StoreCatalogSearchForm form)
        {
            return form.Sort switch
            {
                Enums.SearchSort.RatingAscending => sort.Descending(p => p.RatingToIndexAs),
                Enums.SearchSort.RatingDescending => sort.Descending(p => p.RatingToIndexAs),
                Enums.SearchSort.NameAscending => sort.Ascending(p => p.Name.Suffix(Raw)),
                Enums.SearchSort.NameDescending => sort.Descending(p => p.Name.Suffix(Raw)),
                Enums.SearchSort.Recent => sort.Field(sortField => sortField.Field(p => p.UpdatedDate).MissingLast().Descending()),
                Enums.SearchSort.Relevance => sort.Descending(SortSpecialField.Score).Field(sortField => sortField.Field(p => p.SortOrder).MissingLast().Ascending()),
                _ => sort.Descending(SortSpecialField.Score).Field(sortField => sortField.Field(p => p.SortOrder).MissingLast().Ascending()),
            };
        }

        /// <inheritdoc/>
        protected override IAggregationContainer Aggregations(
            AggregationContainerDescriptor<StoreIndexableModel> a,
            StoreCatalogSearchForm form)
        {
            var returnQuery = a;
            returnQuery = AppendAggregationsForAttributes(returnQuery);
            // returnQuery = AppendAggregationsForBrandNames(returnQuery);
            returnQuery = AppendAggregationsForBadges(returnQuery, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeBadges);
            returnQuery = AppendAggregationsForBrands(returnQuery, CEFConfigDictionary.BrandsEnabled);
            returnQuery = AppendAggregationsForCategories(returnQuery, CEFConfigDictionary.CategoriesEnabled);
            returnQuery = AppendAggregationsForCities(returnQuery, true/*CEFConfigDictionary.CitiesEnabled*/);
            returnQuery = AppendAggregationsForDistricts(returnQuery, true/*CEFConfigDictionary.DistrictsEnabled*/);
            returnQuery = AppendAggregationsForFranchises(returnQuery, CEFConfigDictionary.FranchisesEnabled);
            returnQuery = AppendAggregationsForManufacturers(returnQuery, CEFConfigDictionary.ManufacturersEnabled);
            returnQuery = AppendAggregationsForNames(returnQuery, true/*CEFConfigDictionary.NamesEnabled*/);
            // returnQuery = AppendAggregationsForPricingRanges(returnQuery, ElasticSearchingProviderConfig.SearchingPricingRangesEnabled);
            returnQuery = AppendAggregationsForProducts(returnQuery, true/*CEFConfigDictionary.ProductsEnabled*/);
            returnQuery = AppendAggregationsForRatingRanges(returnQuery, ElasticSearchingProviderConfig.SearchingRatingRangesEnabled);
            returnQuery = AppendAggregationsForRegions(returnQuery, true/*CEFConfigDictionary.RegionsEnabled*/);
            // returnQuery = AppendAggregationsForStores(returnQuery, CEFConfigDictionary.StoresEnabled);
            returnQuery = AppendAggregationsForTypes(returnQuery, true/*CEFConfigDictionary.TypesEnabled*/);
            returnQuery = AppendAggregationsForVendors(returnQuery, CEFConfigDictionary.VendorsEnabled);
            return returnQuery;
        }

        /// <inheritdoc/>
        protected override QueryContainer Query(
            QueryContainerDescriptor<StoreIndexableModel> q,
            StoreCatalogSearchForm form)
        {
            var returnQuery = q
            #region Match & Prefix by Name, with a massive boost
                .Match(m => m
                    .Field(p => p.Name.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsNameMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.Name.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsNamePrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.Name.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsNameTermKeyword)
                    .Value(form.Query)
                )
            #endregion
            #region Match & Prefix by CustomKey/SKU, with a massive boost
                || q.Match(m => m
                    .Field(p => p.CustomKey.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCustomKeyMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CustomKey.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCustomKeyPrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.CustomKey.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCustomKeyTermKeyword)
                    .Value(form.Query)
                )
            #endregion
            #region Match by BrandName, with a massive boost
            /*
                || q.Match(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingStoreIndexBoostsBrandNameMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingStoreIndexBoostsBrandNamePrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingStoreIndexBoostsBrandNameTermKeyword)
                    .Value(form.Query)
                )
            */
            #endregion
            #region Match & Prefix by SeoKeywords, with a massive boost
                || q.Match(m => m
                    .Field(p => p.SeoKeywords.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.SeoKeywords.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsPrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.SeoKeywords.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsTermKeyword)
                    .Value(form.Query)
                )
            #endregion
            #region Match & Prefix by CategoryNamePrimary, with a massive boost
            /*
                || q.Match(m => m
                    .Field(p => p.CategoryNamePrimary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CategoryNamePrimary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryPrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.CategoryNamePrimary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryTermKeyword)
                    .Value(form.Query)
                )
            */
            #endregion
            #region Match & Prefix by CategoryNameSecondary, with a massive boost
            /*
                || q.Match(m => m
                    .Field(p => p.CategoryNameSecondary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CategoryNameSecondary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryPrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.CategoryNameSecondary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryTermKeyword)
                    .Value(form.Query)
                )
            */
            #endregion
            #region Match & Prefix by CategoryNameTertiary, with a massive boost
            /*
                || q.Match(m => m
                    .Field(p => p.CategoryNameTertiary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CategoryNameTertiary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryPrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.CategoryNameTertiary.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryTermKeyword)
                    .Value(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CategoryNameTertiary.Suffix(Keyword))
                    .Boost(250)
                    .Value(form.Query)
                )
            */
            #endregion
            #region Match & Prefix by QueryableSerializableAttributeValuesAggregate, with a medium boost
                || q.Match(m => m
                    .Field(p => p.QueryableSerializableAttributeValuesAggregate.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsQueryableSerializableAttributeValueMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.QueryableSerializableAttributeValuesAggregate.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsQueryableSerializableAttributeValuePrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.QueryableSerializableAttributeValuesAggregate.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsQueryableSerializableAttributeValueTermKeyword)
                    .Value(form.Query)
                )
            #endregion
            #region FunctionScore
                || q.FunctionScore(fs => fs
                    .MaxBoost(ElasticSearchingProviderConfig.SearchingBoostsFunctionScoreMaxBoost)
                    .Functions(ff => ff
                        .Weight(1.0)
                    /*
                    .FieldValueFactor(fvf => fvf
                        .Field(p => p.TotalPurchasedQuantity)
                        .Factor(ElasticSearchingProviderConfig.SearchingBoostsTotalPurchasedQuantityFunctionScoreFactor)
                    )
                    */
                    )
                    .Query(query => query
                        .MultiMatch(m => m
                            .Fields(f => f
                                .Field(p => p.CustomKey.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsCustomKeyKeywordFunctionScoreFactor)
                                .Field(p => p.CustomKey, ElasticSearchingProviderConfig.SearchingBoostsCustomKeyFunctionScoreFactor)
                                .Field(p => p.Name.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsNameKeywordFunctionScoreFactor)
                                .Field(p => p.Name, ElasticSearchingProviderConfig.SearchingBoostsNameFunctionScoreFactor)
                                .Field(p => p.SeoKeywords.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsKeywordFunctionScoreFactor)
                                .Field(p => p.SeoKeywords, ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsFunctionScoreFactor)
                                .Field(p => p.QueryableSerializableAttributeValuesAggregate.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsSerializableAttributeValuesKeywordFunctionScoreFactor)
                                .Field(p => p.QueryableSerializableAttributeValuesAggregate, ElasticSearchingProviderConfig.SearchingBoostsSerializableAttributeValuesFunctionScoreFactor)
                                .Field(p => p.Description, ElasticSearchingProviderConfig.SearchingBoostsDescriptionFunctionScoreFactor)
                                .Field(p => p.CategoryNamePrimary.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryKeywordFunctionScoreFactor)
                                .Field(p => p.CategoryNamePrimary, ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryFunctionScoreFactor)
                                .Field(p => p.CategoryNameSecondary.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryKeywordFunctionScoreFactor)
                                .Field(p => p.CategoryNameSecondary, ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryFunctionScoreFactor)
                                .Field(p => p.CategoryNameTertiary.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryKeywordFunctionScoreFactor)
                                .Field(p => p.CategoryNameTertiary, ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryFunctionScoreFactor)
                            // .Field(p => p.ManufacturerPartNumber.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsManufacturerPartKeywordNumberFunctionScoreFactor)
                            // .Field(p => p.BrandName.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingBoostsBrandNameKeywordFunctionScoreFactor)
                            // .Field(p => p.BrandName, ElasticSearchingProviderConfig.SearchingBoostsBrandNameFunctionScoreFactor)
                            // .Field(p => p.ShortDescription, ElasticSearchingProviderConfig.SearchingBoostsShortDescriptionFunctionScoreFactor)
                            )
                            .Fuzziness(Fuzziness.Auto)
                            .Operator(Operator.Or)
                            .Analyzer(ProductIndexer.AnalyzerNameForProductName)
                            .Query(form.Query)
                        )
                    )
                )
            #endregion
                ;
            /*
            if (Contract.CheckValidKey(form.BrandName))
            {
                returnQuery &= +q.Match(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingStoreIndexBoostsBrandNamePrefixKeyword)
                    .Query(form.BrandName)
                );
            }
            */
            returnQuery = AttributesAnyQueryModification(q, returnQuery, form);
            returnQuery = AttributesAllQueryModification(q, returnQuery, form);
            returnQuery = BadgesAnyQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeBadges);
            returnQuery = BadgesAllQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeBadges);
            returnQuery = BadgesSingleQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeBadges);
            returnQuery = BrandsSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.BrandsEnabled);
            returnQuery = BrandsAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.BrandsEnabled);
            returnQuery = BrandsAllQueryModification(q, returnQuery, form, CEFConfigDictionary.BrandsEnabled);
            returnQuery = CategoriesSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.CategoriesEnabled);
            returnQuery = CategoriesAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.CategoriesEnabled);
            returnQuery = CategoriesAllQueryModification(q, returnQuery, form, CEFConfigDictionary.CategoriesEnabled);
            returnQuery = CitiesSingleQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.CitiesEnabled*/);
            returnQuery = DistrictsSingleQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.DistrictsEnabled*/);
            returnQuery = DistrictsAnyQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.DistrictsEnabled*/);
            returnQuery = DistrictsAllQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.DistrictsEnabled*/);
            returnQuery = FranchisesSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.FranchisesEnabled);
            returnQuery = FranchisesAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.FranchisesEnabled);
            returnQuery = FranchisesAllQueryModification(q, returnQuery, form, CEFConfigDictionary.FranchisesEnabled);
            returnQuery = ManufacturersSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.ManufacturersEnabled);
            returnQuery = ManufacturersAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.ManufacturersEnabled);
            returnQuery = ManufacturersAllQueryModification(q, returnQuery, form, CEFConfigDictionary.ManufacturersEnabled);
            /*
            returnQuery = PricingRangesQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingPricingRangesEnabled);
            */
            returnQuery = ProductsSingleQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.ProductsEnabled*/);
            returnQuery = ProductsAnyQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.ProductsEnabled*/);
            returnQuery = ProductsAllQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.ProductsEnabled*/);
            returnQuery = RatingRangesQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingRatingRangesEnabled);
            returnQuery = RegionsSingleQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.RegionsEnabled*/);
            returnQuery = RegionsAnyQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.RegionsEnabled*/);
            /*
            returnQuery = RolesSingleQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeRoles);
            returnQuery = RolesAnyQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeRoles);
            returnQuery = RolesAllQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeRoles);
            */
            /*
            returnQuery = StoresSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.StoresEnabled);
            returnQuery = StoresAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.StoresEnabled);
            returnQuery = StoresAllQueryModification(q, returnQuery, form, CEFConfigDictionary.StoresEnabled);
            */
            returnQuery = TypesSingleQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.TypesEnabled*/);
            returnQuery = TypesAnyQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.TypesEnabled*/);
            returnQuery = VendorsSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.VendorsEnabled);
            returnQuery = VendorsAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.VendorsEnabled);
            returnQuery = VendorsAllQueryModification(q, returnQuery, form, CEFConfigDictionary.VendorsEnabled);
            return returnQuery;
        }

        /// <inheritdoc/>
        protected override void SearchViewModelAdditionalAssignments(
            StoreSearchViewModel model,
            ISearchResponse<StoreIndexableModel> result)
        {
            SearchViewModelAdditionalAssignmentsForAttributes(model, result);
            SearchViewModelAdditionalAssignmentsForBadges(model, result, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeBadges);
            SearchViewModelAdditionalAssignmentsForBrands(model, result, CEFConfigDictionary.BrandsEnabled);
            SearchViewModelAdditionalAssignmentsForCategories(model, result, CEFConfigDictionary.CategoriesEnabled);
            SearchViewModelAdditionalAssignmentsForCities(model, result, true/*CEFConfigDictionary.CitiesEnabled*/);
            SearchViewModelAdditionalAssignmentsForDistricts(model, result, true/*CEFConfigDictionary.DistrictsEnabled*/);
            SearchViewModelAdditionalAssignmentsForFranchises(model, result, CEFConfigDictionary.FranchisesEnabled);
            SearchViewModelAdditionalAssignmentsForManufacturers(model, result, CEFConfigDictionary.ManufacturersEnabled);
            SearchViewModelAdditionalAssignmentsForNames(model, result, true/*CEFConfigDictionary.NamesEnabled*/);
            // SearchViewModelAdditionalAssignmentsForPricingRanges(model, result, ElasticSearchingProviderConfig.SearchingPricingRangesEnabled);
            SearchViewModelAdditionalAssignmentsForProducts(model, result, true/*CEFConfigDictionary.ProductsEnabled*/);
            // SearchViewModelAdditionalAssignmentsForRoles(model, result, ElasticSearchingProviderConfig.SearchingStoreIndexFiltersIncludeRoles);
            SearchViewModelAdditionalAssignmentsForRatingRanges(model, result, ElasticSearchingProviderConfig.SearchingRatingRangesEnabled);
            SearchViewModelAdditionalAssignmentsForRegions(model, result, true/*CEFConfigDictionary.RegionsEnabled*/);
            // SearchViewModelAdditionalAssignmentsForStores(model, result, CEFConfigDictionary.StoresEnabled);
            SearchViewModelAdditionalAssignmentsForTypes(model, result, true/*CEFConfigDictionary.StoresEnabled*/);
            SearchViewModelAdditionalAssignmentsForVendors(model, result, CEFConfigDictionary.VendorsEnabled);
        }

        /// <summary>Badges single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer BadgesSingleQueryModification(
            QueryContainerDescriptor<StoreIndexableModel> q,
            QueryContainer returnQuery,
            StoreCatalogSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.BadgeID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordBadgeSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingStoreIndexBoostsSingleBadge)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Badges)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(s => s.Term(p => p.Badges!.First().ID.Suffix(Keyword), form.BadgeID))
                            .MinimumShouldMatch(MinimumShouldMatch.Fixed(1))))
                    .IgnoreUnmapped());
            return returnQuery;
        }

        /// <summary>Badges any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer BadgesAnyQueryModification(
            QueryContainerDescriptor<StoreIndexableModel> q,
            QueryContainer returnQuery,
            StoreCatalogSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.BadgeIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForRecordBadgesAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingStoreIndexBoostsAnyBadges)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Badges)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(s => s.Terms(t => t.Field(p => p.Badges!.First().ID.Suffix(Keyword)).Terms(form.BadgeIDsAny)))
                            .MinimumShouldMatch(MinimumShouldMatch.Fixed(1))))
                    .IgnoreUnmapped());
            return returnQuery;
        }

        /// <summary>Badges all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer BadgesAllQueryModification(
            QueryContainerDescriptor<StoreIndexableModel> q,
            QueryContainer returnQuery,
            StoreCatalogSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.BadgeIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.BadgeIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordBadgesAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingStoreIndexBoostsAllBadges)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Badges)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(s => s.Terms(t => t.Field(p => p.Badges!.First().ID.Suffix(Keyword)).Terms(filter)))))
                        .IgnoreUnmapped());
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for badges.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForBadges(
            StoreSearchViewModel model,
            ISearchResponse<StoreIndexableModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.BadgeIDs = result.Aggregations.Nested(NestedNameForRecordBadges)
                ////?.Aggregations
                ?.Terms(TermsNameForRecordBadgeIDs)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }

        /// <summary>Appends the aggregations for badges.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{StoreIndexableModel}.</returns>
        protected virtual AggregationContainerDescriptor<StoreIndexableModel> AppendAggregationsForBadges(
            AggregationContainerDescriptor<StoreIndexableModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Nested(NestedNameForRecordBadges, n => n
                .Path(p => p.Badges)
                .Aggregations(ap => ap
                    .Terms(TermsNameForRecordBadgeIDs, ts => ts
                        .Field(p => p.Badges!.First().ID.Suffix(Keyword))
                        .Missing(0).Size(30))
                    .Terms(TermsNameForRecordBadgeKeys, ts => ts
                        .Field(p => p.Badges!.First().Key.Suffix(Keyword))
                        .Missing(string.Empty).Size(30))
                    .Terms(TermsNameForRecordBadgeNames, ts => ts
                        .Field(p => p.Badges!.First().Name.Suffix(Keyword))
                        .Missing(string.Empty).Size(30))));
            return returnQuery;
        }
    }
}
