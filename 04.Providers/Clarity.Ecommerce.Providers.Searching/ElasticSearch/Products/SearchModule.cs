// <copyright file="SearchModule.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product search module class</summary>
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

    /// <summary>A product search module.</summary>
    /// <seealso cref="SearchModuleBase{ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel}"/>
    internal partial class ProductSearchModule
        : SearchModuleBase<ProductSearchViewModel, ProductCatalogSearchForm, ProductIndexableModel>
    {
        /// <summary>(Immutable) List of names of the terms name for record brands.</summary>
        protected const string TermsNameForRecordBrandNamesAgg = "record-brand-names-agg";

        /// <inheritdoc/>
        protected override IPromise<IList<ISort>> Sort(
            SortDescriptor<ProductIndexableModel> sort,
            ProductCatalogSearchForm form)
        {
            return form.Sort switch
            {
                Enums.SearchSort.PricingAscending => sort.Ascending(p => p.PricingToIndexAs),
                Enums.SearchSort.PricingDescending => sort.Descending(p => p.PricingToIndexAs),
                Enums.SearchSort.Defined => sort.Field(sortField => sortField.Field(p => p.SortOrder).MissingLast().Ascending()),
                Enums.SearchSort.Popular => sort.Descending(p => p.TotalPurchasedQuantity),
                Enums.SearchSort.RatingAscending => sort.Ascending(p => p.RatingToIndexAs),
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
            AggregationContainerDescriptor<ProductIndexableModel> a,
            ProductCatalogSearchForm form)
        {
            var returnQuery = a;
            returnQuery = AppendAggregationsForAttributes(returnQuery);
            returnQuery = AppendAggregationsForBrandNames(returnQuery);
            returnQuery = AppendAggregationsForBrands(returnQuery, CEFConfigDictionary.BrandsEnabled);
            returnQuery = AppendAggregationsForCategories(returnQuery, CEFConfigDictionary.CategoriesEnabled);
            returnQuery = AppendAggregationsForFranchises(returnQuery, CEFConfigDictionary.FranchisesEnabled);
            returnQuery = AppendAggregationsForManufacturers(returnQuery, CEFConfigDictionary.ManufacturersEnabled);
            returnQuery = AppendAggregationsForPricingRanges(returnQuery, ElasticSearchingProviderConfig.SearchingPricingRangesEnabled);
            // returnQuery = AppendAggregationsForProducts(returnQuery, true/*CEFConfigDictionary.ProductsEnabled*/);
            returnQuery = AppendAggregationsForRatingRanges(returnQuery, ElasticSearchingProviderConfig.SearchingRatingRangesEnabled);
            returnQuery = AppendAggregationsForStores(returnQuery, CEFConfigDictionary.StoresEnabled);
            returnQuery = AppendAggregationsForTypes(returnQuery, true/*CEFConfigDictionary.TypesEnabled*/);
            returnQuery = AppendAggregationsForVendors(returnQuery, CEFConfigDictionary.VendorsEnabled);
            return returnQuery;
        }

        /// <inheritdoc/>
        protected override QueryContainer Query(
            QueryContainerDescriptor<ProductIndexableModel> q,
            ProductCatalogSearchForm form)
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
                || q.Match(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingProductIndexBoostsBrandNameMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingProductIndexBoostsBrandNamePrefixKeyword)
                    .Value(form.Query)
                )
                || q.Term(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingProductIndexBoostsBrandNameTermKeyword)
                    .Value(form.Query)
                )
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
            #endregion
            #region Match & Prefix by CategoryNameSecondary, with a massive boost
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
            #endregion
            #region Match & Prefix by CategoryNameTertiary, with a massive boost
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
                                .Field(p => p.ManufacturerPartNumber.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingProductIndexBoostsManufacturerPartKeywordNumberFunctionScoreFactor)
                                .Field(p => p.BrandName.Suffix(Keyword), ElasticSearchingProviderConfig.SearchingProductIndexBoostsBrandNameKeywordFunctionScoreFactor)
                                .Field(p => p.BrandName, ElasticSearchingProviderConfig.SearchingProductIndexBoostsBrandNameFunctionScoreFactor)
                                .Field(p => p.ShortDescription, ElasticSearchingProviderConfig.SearchingProductIndexBoostsShortDescriptionFunctionScoreFactor)
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
            if (Contract.CheckValidKey(form.BrandName))
            {
                returnQuery &= +q.Match(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost(ElasticSearchingProviderConfig.SearchingProductIndexBoostsBrandNamePrefixKeyword)
                    .Query(form.BrandName)
                );
            }
            returnQuery = AttributesAnyQueryModification(q, returnQuery, form);
            returnQuery = AttributesAllQueryModification(q, returnQuery, form);
            returnQuery = BrandsSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.BrandsEnabled);
            returnQuery = BrandsAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.BrandsEnabled);
            returnQuery = BrandsAllQueryModification(q, returnQuery, form, CEFConfigDictionary.BrandsEnabled);
            returnQuery = CategoriesSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.CategoriesEnabled);
            returnQuery = CategoriesAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.CategoriesEnabled);
            returnQuery = CategoriesAllQueryModification(q, returnQuery, form, CEFConfigDictionary.CategoriesEnabled);
            returnQuery = FranchisesSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.FranchisesEnabled);
            returnQuery = FranchisesAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.FranchisesEnabled);
            returnQuery = FranchisesAllQueryModification(q, returnQuery, form, CEFConfigDictionary.FranchisesEnabled);
            returnQuery = ManufacturersSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.ManufacturersEnabled);
            returnQuery = ManufacturersAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.ManufacturersEnabled);
            returnQuery = ManufacturersAllQueryModification(q, returnQuery, form, CEFConfigDictionary.ManufacturersEnabled);
            returnQuery = PricingRangesQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingPricingRangesEnabled);
            /*
            returnQuery = ProductsSingleQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.ProductsEnabled*//*);
            returnQuery = ProductsAnyQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.ProductsEnabled*//*);
            returnQuery = ProductsAllQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.ProductsEnabled*//*);
            */
            returnQuery = ProductsOnHandQueryModification(q, returnQuery, form);
            returnQuery = RatingRangesQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingRatingRangesEnabled);
            returnQuery = RolesSingleQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles);
            returnQuery = RolesAnyQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles);
            returnQuery = RolesAllQueryModification(q, returnQuery, form, ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles);
            returnQuery = StoresSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.StoresEnabled);
            returnQuery = StoresAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.StoresEnabled);
            returnQuery = StoresAllQueryModification(q, returnQuery, form, CEFConfigDictionary.StoresEnabled);
            returnQuery = TypesSingleQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.TypesEnabled*/);
            returnQuery = TypesAnyQueryModification(q, returnQuery, form, true/*CEFConfigDictionary.TypesEnabled*/);
            returnQuery = VendorsSingleQueryModification(q, returnQuery, form, CEFConfigDictionary.VendorsEnabled);
            returnQuery = VendorsAnyQueryModification(q, returnQuery, form, CEFConfigDictionary.VendorsEnabled);
            returnQuery = VendorsAllQueryModification(q, returnQuery, form, CEFConfigDictionary.VendorsEnabled);
            return returnQuery;
        }

        /// <inheritdoc/>
        protected override void SearchViewModelAdditionalAssignments(
            ProductSearchViewModel model,
            ISearchResponse<ProductIndexableModel> result)
        {
            SearchViewModelAdditionalAssignmentsForAttributes(model, result);
            SearchViewModelAdditionalAssignmentsForBrands(model, result, CEFConfigDictionary.BrandsEnabled);
            SearchViewModelAdditionalAssignmentsForBrandNames(model, result);
            SearchViewModelAdditionalAssignmentsForCategories(model, result, CEFConfigDictionary.CategoriesEnabled);
            SearchViewModelAdditionalAssignmentsForFranchises(model, result, CEFConfigDictionary.FranchisesEnabled);
            SearchViewModelAdditionalAssignmentsForManufacturers(model, result, CEFConfigDictionary.ManufacturersEnabled);
            SearchViewModelAdditionalAssignmentsForPricingRanges(model, result, ElasticSearchingProviderConfig.SearchingPricingRangesEnabled);
            // SearchViewModelAdditionalAssignmentsForProducts(model, result, true/*CEFConfigDictionary.ProductsEnabled*/);
            SearchViewModelAdditionalAssignmentsForRatingRanges(model, result, ElasticSearchingProviderConfig.SearchingRatingRangesEnabled);
            SearchViewModelAdditionalAssignmentsForRoles(model, result, ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles);
            SearchViewModelAdditionalAssignmentsForStores(model, result, CEFConfigDictionary.StoresEnabled);
            SearchViewModelAdditionalAssignmentsForTypes(model, result, true/*CEFConfigDictionary.StoresEnabled*/);
            SearchViewModelAdditionalAssignmentsForVendors(model, result, CEFConfigDictionary.VendorsEnabled);
        }

        protected virtual AggregationContainerDescriptor<ProductIndexableModel> AppendAggregationsForBrandNames(
            AggregationContainerDescriptor<ProductIndexableModel> returnQuery)
        {
            #region BrandNames
            returnQuery &= returnQuery
                .Terms(TermsNameForRecordBrandNamesAgg, ts => ts
                    .Field(p => p.BrandNameAgg.Suffix(Keyword))
                    .Missing(string.Empty)
                    .Size(128)
                );
            #endregion
            return returnQuery;
        }

        protected virtual void SearchViewModelAdditionalAssignmentsForBrandNames(
            ProductSearchViewModel model,
            ISearchResponse<ProductIndexableModel> result)
        {
            model.BrandNames = result.Aggregations.Terms(TermsNameForRecordBrandNamesAgg)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }
    }
}
