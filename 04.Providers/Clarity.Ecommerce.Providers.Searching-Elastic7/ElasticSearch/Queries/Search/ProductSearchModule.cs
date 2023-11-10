// <copyright file="ProductSearchModule.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product search module class</summary>
// ReSharper disable BadControlBracesLineBreaks, BadMemberAccessSpaces, CyclomaticComplexity, FunctionComplexityOverflow
// ReSharper disable MissingLinebreak, MissingSpace, MultipleStatementsOnOneLine, RedundantCaseLabel
// ReSharper disable RegionWithinTypeMemberBody, UseNullPropagationWhenPossible
// ReSharper disable StyleCop.SA1002, StyleCop.SA1009, StyleCop.SA1019, StyleCop.SA1025, StyleCop.SA1111
// ReSharper disable StyleCop.SA1114, StyleCop.SA1115, StyleCop.SA1116, StyleCop.SA1123
#pragma warning disable SA1002, SA1009, SA1019, SA1025, SA1111, SA1114, SA1115, SA1116, SA1123
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch.Web.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Interfaces.Providers.Searching;
    using Nest;
    using Utilities;

    /// <summary>A product search module.</summary>
    /// <seealso cref="SearchModuleBase{ProductSearchViewModel, ProductCatalogSearchForm, IndexableProductModel}"/>
    public partial class ProductSearchModule
        : SearchModuleBase<ProductSearchViewModel, ProductCatalogSearchForm, IndexableProductModel>
    {
        #region Constant Strings
        // General
        private const string Keyword = "keyword";
        private const string Raw = "raw";
        private const string NotAvailable = "N/A";
        // Categories
        private const string NestedNameForProductCategories = "product-categories";
        private const string NestedNameForProductCategorySingle = "product-single-category";
        private const string NestedNameForProductCategoriesAny = "product-categories-any";
        private const string NestedNameForProductCategoriesAllPrefix = "product-categories-all-";
        private const string TermsNameForCategoryParent = "category-names-parent";
        private const string TermsNameForCategory5 = "category-names-5";
        private const string TermsNameForCategory4 = "category-names-4";
        private const string TermsNameForCategory3 = "category-names-3";
        private const string TermsNameForCategory2 = "category-names-2";
        private const string TermsNameForCategory1 = "category-names-1";
        private const string TermsNameForCategory0 = "category-names-0";
        // Attributes
        private const string NestedNameForProductAttributes = "product-attributes";
        private const string NestedNameForProductAttributesAny = " product-attributes-any";
        private const string NestedNameForProductAttributesAllPrefix = "product-attributes-all-";
        private const string TermsNameForAttributeKeys = " attribute-keys";
        private const string TermsNameForAttributeValues = " attribute-values";
        // Price Ranges
        private const string RangeNameForProductPriceRanges = "product-price-ranges";
        private const string RangeNameForProductPriceRangesMin = "product-price-ranges-min";
        private const string RangeNameForProductPriceRangesMax = "product-price-ranges-max";
        // Brands
        private const string NestedNameForProductBrands = "product-brands";
        private const string NestedNameForProductBrandSingle = "product-single-brand";
        private const string NestedNameForProductBrandsAny = " product-brands-any";
        private const string NestedNameForProductBrandsAllPrefix = "product-brands-all-";
        private const string TermsNameForProductBrandIDs = "product-brands-ids";
        private const string TermsNameForProductBrandKeys = "product-brands-keys";
        private const string TermsNameForProductBrandNames = "product-brands-names";
        // Stores
        private const string NestedNameForProductStores = "product-stores";
        private const string NestedNameForProductStoreSingle = "product-single-store";
        private const string NestedNameForProductStoresAny = "product-stores-any";
        private const string NestedNameForProductStoresAllPrefix = "product-stores-all-";
        private const string TermsNameForProductStoreIDs = "product-stores-ids";
        private const string TermsNameForProductStoreKeys = "product-stores-keys";
        private const string TermsNameForProductStoreNames = "product-stores-names";
        // Roles
        private const string NestedNameForProductRoleSingle = "product-single-role";
        private const string NestedNameForProductRolesAny = " product-roles-any";
        private const string NestedNameForProductRolesAllPrefix = "product-roles-all-";
        private const string AnonymousRole = "Anonymous";
        #endregion
    }

    public partial class ProductSearchModule
    {
        /// <inheritdoc/>
        protected override IPromise<IList<ISort>> Sort(
            SortDescriptor<IndexableProductModel> sort,
            ProductCatalogSearchForm form)
        {
            switch (form.Sort)
            {
                case Enums.SearchSort.NameAscending:
                {
                    return sort.Ascending(p => p.Name.Suffix(Raw));
                }
                case Enums.SearchSort.NameDescending:
                {
                    return sort.Descending(p => p.Name.Suffix(Raw));
                }
                case Enums.SearchSort.PriceAscending:
                {
                    return sort.Ascending(p => p.FinalPrice);
                }
                case Enums.SearchSort.PriceDescending:
                {
                    return sort.Descending(p => p.FinalPrice);
                }
                case Enums.SearchSort.Defined:
                {
                    return sort.Field(sortField => sortField.Field(p => p.SortOrder).MissingLast().Ascending());
                }
                case Enums.SearchSort.Popular:
                {
                    return sort.Descending(p => p.TotalPurchasedQuantity);
                }
                case Enums.SearchSort.Recent:
                {
                    return sort.Field(sortField => sortField.Field(p => p.UpdatedDate).MissingLast().Descending());
                }
                case Enums.SearchSort.Relevance:
                default:
                {
                    return sort
                        .Descending(SortSpecialField.Score)
                        .Field(sortField => sortField.Field(p => p.SortOrder).MissingLast().Ascending());
                }
            }
        }

        /// <inheritdoc/>
        protected override IAggregationContainer Aggregations(
            AggregationContainerDescriptor<IndexableProductModel> a,
            ProductCatalogSearchForm form)
        {
            var returnQuery = a;
            #region Categories
            // This puts the categories into a tree up to 7 levels deep. If there's only 2 levels, then you will have 'N/A' om the first 4 levels and then the 2 actual levels all the way in
            returnQuery &= returnQuery
                .Nested(NestedNameForProductCategories, n => n
                    .Path(p => p.ProductCategories)
                    .Aggregations(ap => ap
                        .Terms(TermsNameForCategoryParent, ts6 => ts6
                            .Field(p => p.ProductCategories.First().CategoryParent6Name.Suffix(Keyword))
                            .Missing(NotAvailable).Size(500)
                            .Aggregations(a6 => a6
                                .Terms(TermsNameForCategory5, ts5 => ts5
                                    .Field(p => p.ProductCategories.First().CategoryParent5Name.Suffix(Keyword))
                                    .Missing(NotAvailable).Size(500)
                                    .Aggregations(a5 => a5
                                        .Terms(TermsNameForCategory4, ts4 => ts4
                                            .Field(p => p.ProductCategories.First().CategoryParent4Name.Suffix(Keyword))
                                            .Missing(NotAvailable).Size(500)
                                            .Aggregations(a4 => a4
                                                .Terms(TermsNameForCategory3, ts3 => ts3
                                                    .Field(p => p.ProductCategories.First().CategoryParent3Name.Suffix(Keyword))
                                                    .Missing(NotAvailable).Size(500)
                                                    .Aggregations(a3 => a3
                                                        .Terms(TermsNameForCategory2, ts2 => ts2
                                                            .Field(p => p.ProductCategories.First().CategoryParent2Name.Suffix(Keyword))
                                                            .Missing(NotAvailable).Size(500)
                                                            .Aggregations(a2 => a2
                                                                .Terms(TermsNameForCategory1, ts1 => ts1
                                                                    .Field(p => p.ProductCategories.First().CategoryParent1Name.Suffix(Keyword))
                                                                    .Missing(NotAvailable).Size(500)
                                                                    .Aggregations(a1 => a1
                                                                        .Terms(TermsNameForCategory0, ts0 => ts0
                                                                            .Field(p => p.ProductCategories.First().CategoryName.Suffix(Keyword))
                                                                            .Missing(NotAvailable).Size(500)
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                );
            #endregion
            #region Price Ranges
            returnQuery &= returnQuery.Range(RangeNameForProductPriceRanges, rr => rr
                .Field(f => f.FinalPrice)
                .Ranges(
                    CreatePriceRangeLambdas()
                ).Aggregations(aa => aa
                    .Max(RangeNameForProductPriceRangesMax, ma => ma
                        .Field(f => f.FinalPrice)
                        .Missing(0)
                    )
                    .Min(RangeNameForProductPriceRangesMin, ma => ma
                        .Field(f => f.FinalPrice)
                        .Missing(0)
                    )
                )
            );
            #endregion
            #region Attributes
            returnQuery &= returnQuery.Nested(NestedNameForProductAttributes, n => n
                .Path(p => p.Attributes)
                .Aggregations(aa => aa
                    .Terms(TermsNameForAttributeKeys, tsKey => tsKey
                        .Field(p => p.Attributes.First().Key.Suffix(Keyword))
                        .Missing(NotAvailable)
                        .Aggregations(aaa => aaa
                            .Terms(TermsNameForAttributeValues, tsValue => tsValue
                                .Field(p => p.Attributes.First().Value.Suffix(Keyword))
                                .Missing(NotAvailable)
                                .Size(100)
                            )
                        ).Size(100)
                    )
                )
            );
            #endregion
            #region Brands
            if (ElasticSearchingProviderConfig.SearchingProductIndexRequiresABrand)
            {
                returnQuery &= returnQuery.Nested(NestedNameForProductBrands, n => n
                    .Path(p => p.ProductBrands)
                    .Aggregations(ap => ap
                        .Terms(TermsNameForProductBrandIDs, ts => ts
                            .Field(p => p.ProductBrands.First().BrandID.Suffix(Keyword))
                            .Missing(0).Size(30)
                        )
                        .Terms(TermsNameForProductBrandKeys, ts => ts
                            .Field(p => p.ProductBrands.First().BrandKey.Suffix(Keyword))
                            .Missing(string.Empty).Size(30)
                        )
                        .Terms(TermsNameForProductBrandNames, ts => ts
                            .Field(p => p.ProductBrands.First().BrandName.Suffix(Keyword))
                            .Missing(string.Empty).Size(30)
                        )
                    )
                );
            }
            #endregion
            #region Stores
            if (ElasticSearchingProviderConfig.SearchingProductIndexRequiresAStore)
            {
                returnQuery &= returnQuery.Nested(NestedNameForProductStores, n => n
                    .Path(p => p.ProductStores)
                    .Aggregations(ap => ap
                        .Terms(TermsNameForProductStoreIDs, ts => ts
                            .Field(p => p.ProductStores.First().StoreID.Suffix(Keyword))
                            .Missing(0).Size(30)
                        )
                        .Terms(TermsNameForProductStoreKeys, ts => ts
                            .Field(p => p.ProductStores.First().StoreKey.Suffix(Keyword))
                            .Missing(string.Empty).Size(30)
                        )
                        .Terms(TermsNameForProductStoreNames, ts => ts
                            .Field(p => p.ProductStores.First().StoreName.Suffix(Keyword))
                            .Missing(string.Empty).Size(30)
                        )
                    )
                );
            }
            #endregion
            return returnQuery;
        }

        /// <inheritdoc/>
        protected override QueryContainer Query(
            QueryContainerDescriptor<IndexableProductModel> q,
            ProductCatalogSearchForm form)
        {
            var returnQuery = q
                #region Match & Prefix by Name, with a massive boost
                .Match(m => m
                    .Field(p => p.Name.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsNameMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.Name.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsNamePrefixKeyword)
                    .Value(form.Query)
                )
                #endregion
                #region Match & Prefix by CustomKey/SKU, with a massive boost
                || q.Match(m => m
                    .Field(p => p.CustomKey.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsCustomKeyMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CustomKey.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsCustomKeyPrefixKeyword)
                    .Value(form.Query)
                )
                #endregion
                #region Match by BrandName, with a massive boost
                || q.Match(m => m
                    .Field(p => p.BrandName.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsBrandNameMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                #endregion
                #region Match & Prefix by SeoKeywords, with a massive boost
                || q.Match(m => m
                    .Field(p => p.SeoKeywords.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.SeoKeywords.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsPrefixKeyword)
                    .Value(form.Query)
                )
                #endregion
                #region Match & Prefix by CategoryNamePrimary, with a massive boost
                || q.Match(m => m
                    .Field(p => p.CategoryNamePrimary.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CategoryNamePrimary.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryPrefixKeyword)
                    .Value(form.Query)
                )
                #endregion
                #region Match & Prefix by CategoryNameSecondary, with a massive boost
                || q.Match(m => m
                    .Field(p => p.CategoryNameSecondary.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CategoryNameSecondary.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryPrefixKeyword)
                    .Value(form.Query)
                )
                #endregion
                #region Match & Prefix by CategoryNameTertiary, with a massive boost
                || q.Match(m => m
                    .Field(p => p.CategoryNameTertiary.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryMatchKeyword)
                    .Fuzziness(Fuzziness.Auto)
                    .Query(form.Query)
                )
                || q.Prefix(m => m
                    .Field(p => p.CategoryNameTertiary.Suffix(Keyword))
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryPrefixKeyword)
                    .Value(form.Query)
                )
                #endregion
                #region FunctionScore
                || q.FunctionScore(fs => fs
                    .MaxBoost((double)ElasticSearchingProviderConfig.SearchingBoostsFunctionScoreMaxBoost)
                    .Functions(ff => ff
                        .FieldValueFactor(fvf => fvf
                            .Field(p => p.TotalPurchasedQuantity)
                            .Factor((double)ElasticSearchingProviderConfig.SearchingBoostsTotalPurchasedQuantityFunctionScoreFactor)
                        )
                    )
                    .Query(query => query
                        .MultiMatch(m => m
                            .Fields(f => f
                                .Field(p => p.ManufacturerPartNumber.Suffix(Keyword), (double)ElasticSearchingProviderConfig.SearchingBoostsManufacturerPartKeywordNumberFunctionScoreFactor)
                                .Field(p => p.CustomKey.Suffix(Keyword), (double)ElasticSearchingProviderConfig.SearchingBoostsCustomKeyKeywordFunctionScoreFactor)
                                .Field(p => p.CustomKey, (double)ElasticSearchingProviderConfig.SearchingBoostsCustomKeyFunctionScoreFactor)
                                .Field(p => p.Name.Suffix(Keyword), (double)ElasticSearchingProviderConfig.SearchingBoostsNameKeywordFunctionScoreFactor)
                                .Field(p => p.Name, (double)ElasticSearchingProviderConfig.SearchingBoostsNameFunctionScoreFactor)
                                .Field(p => p.BrandName.Suffix(Keyword), (double)ElasticSearchingProviderConfig.SearchingBoostsBrandNameKeywordFunctionScoreFactor)
                                .Field(p => p.BrandName, (double)ElasticSearchingProviderConfig.SearchingBoostsBrandNameFunctionScoreFactor)
                                .Field(p => p.SeoKeywords.Suffix(Keyword), (double)ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsKeywordFunctionScoreFactor)
                                .Field(p => p.SeoKeywords, (double)ElasticSearchingProviderConfig.SearchingBoostsSeoKeywordsFunctionScoreFactor)
                                .Field(p => p.ShortDescription, (double)ElasticSearchingProviderConfig.SearchingBoostsShortDescriptionFunctionScoreFactor)
                                .Field(p => p.Description, (double)ElasticSearchingProviderConfig.SearchingBoostsDescriptionFunctionScoreFactor)
                                .Field(p => p.CategoryNamePrimary.Suffix(Keyword), (double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryKeywordFunctionScoreFactor)
                                .Field(p => p.CategoryNamePrimary, (double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNamePrimaryFunctionScoreFactor)
                                .Field(p => p.CategoryNameSecondary.Suffix(Keyword), (double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryKeywordFunctionScoreFactor)
                                .Field(p => p.CategoryNameSecondary, (double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNameSecondaryFunctionScoreFactor)
                                .Field(p => p.CategoryNameTertiary.Suffix(Keyword), (double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryKeywordFunctionScoreFactor)
                                .Field(p => p.CategoryNameTertiary, (double)ElasticSearchingProviderConfig.SearchingBoostsCategoryNameTertiaryFunctionScoreFactor)
                            )
                            .Fuzziness(Fuzziness.Auto)
                            .Operator(Operator.And)
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
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsBrandNamePrefixKeyword)
                    .Query(form.BrandName)
                );
            }
            returnQuery = CategoriesSingleQueryModification(q, returnQuery, form);
            returnQuery = CategoriesAnyQueryModification(q, returnQuery, form);
            returnQuery = CategoriesAllQueryModification(q, returnQuery, form);
            returnQuery = RolesSingleQueryModification(q, returnQuery, form);
            returnQuery = RolesAnyQueryModification(q, returnQuery, form);
            returnQuery = RolesAllQueryModification(q, returnQuery, form);
            returnQuery = BrandsSingleQueryModification(q, returnQuery, form);
            returnQuery = BrandsAnyQueryModification(q, returnQuery, form);
            returnQuery = BrandsAllQueryModification(q, returnQuery, form);
            returnQuery = StoresSingleQueryModification(q, returnQuery, form);
            returnQuery = StoresAnyQueryModification(q, returnQuery, form);
            returnQuery = StoresAllQueryModification(q, returnQuery, form);
            returnQuery = PriceRangesQueryModification(q, returnQuery, form);
            returnQuery = AttributesAnyQueryModification(q, returnQuery, form);
            returnQuery = AttributesAllQueryModification(q, returnQuery, form);
            return returnQuery;
        }

        /// <inheritdoc/>
        protected override void SearchViewModelAdditionalAssignments(
            ProductSearchViewModel model,
            ISearchResponse<IndexableProductModel> result)
        {
            SearchViewModelAdditionalAssignmentsForCategories(model, result);
            SearchViewModelAdditionalAssignmentsForStores(model, result);
            SearchViewModelAdditionalAssignmentsForBrands(model, result);
            SearchViewModelAdditionalAssignmentsForPriceRanges(model, result);
            SearchViewModelAdditionalAssignmentsForAttributes(model, result);
            SearchViewModelAdditionalAssignmentsForRoles(model, result);
        }
    }

    public partial class ProductSearchModule
    {
        #region Categories
        /// <summary>Categories single query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer CategoriesSingleQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (!Contract.CheckValidKey(form.Category))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForProductCategorySingle)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsSingleCategory)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.ProductCategories)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.ProductCategories.First().CategoryName.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.ProductCategories.First().CategoryDisplayName.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.ProductCategories.First().CategoryParent1Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.ProductCategories.First().CategoryParent2Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.ProductCategories.First().CategoryParent3Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.ProductCategories.First().CategoryParent4Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.ProductCategories.First().CategoryParent5Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.ProductCategories.First().CategoryParent6Name.Suffix(Keyword), form.Category)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Categories any query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer CategoriesAnyQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.CategoriesAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForProductCategoriesAny)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAnyCategory)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.ProductCategories)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryName.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryDisplayName.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent1Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent2Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent3Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent4Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent5Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent6Name.Suffix(Keyword)).Terms(form.CategoriesAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Categories all query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer CategoriesAllQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.CategoriesAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.CategoriesAll.Where(x => Contract.CheckValidKey(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForProductCategoriesAllPrefix}{filter}")
                        .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAllCategories)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.ProductCategories)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryName.Suffix(Keyword)).Terms(filter))
                                      || s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryDisplayName.Suffix(Keyword)).Terms(filter))
                                      || s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent1Name.Suffix(Keyword)).Terms(filter))
                                      || s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent2Name.Suffix(Keyword)).Terms(filter))
                                      || s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent3Name.Suffix(Keyword)).Terms(filter))
                                      || s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent4Name.Suffix(Keyword)).Terms(filter))
                                      || s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent5Name.Suffix(Keyword)).Terms(filter))
                                      || s.Terms(t => t.Field(p => p.ProductCategories.First().CategoryParent6Name.Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for categories.</summary>
        /// <param name="model"> The model.</param>
        /// <param name="result">The result.</param>
        protected void SearchViewModelAdditionalAssignmentsForCategories(
            ProductSearchViewModel model,
            ISearchResponse<IndexableProductModel> result)
        {
            var categoriesTree = new TreeAggregate
            {
                DocCountErrorUpperBound = result.Aggregations.Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).DocCountErrorUpperBound,
                SumOtherDocCount = result.Aggregations.Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).SumOtherDocCount,
                HasChildren = result.Aggregations.Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets.Count > 0,
                Children = result.Aggregations.Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                    .Select(vP => new TreeAggregate
                    {
                        Key = vP.Key,
                        DocCount = vP.DocCount,
                        DocCountErrorUpperBound = vP.DocCountErrorUpperBound,
                        HasChildren = result.Aggregations
                                          .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                          .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                          .Count > 0,
                        Children = result.Aggregations
                            .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                            .Select(v5 => new TreeAggregate
                            {
                                Key = v5.Key,
                                DocCount = v5.DocCount,
                                DocCountErrorUpperBound = v5.DocCountErrorUpperBound,
                                HasChildren = result.Aggregations
                                                  .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                                  .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                  .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                  .Count > 0,
                                Children = result.Aggregations
                                    .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                    .Select(v4 => new TreeAggregate
                                    {
                                        Key = v4.Key,
                                        DocCount = v4.DocCount,
                                        DocCountErrorUpperBound = v4.DocCountErrorUpperBound,
                                        HasChildren = result.Aggregations
                                                          .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                                          .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                          .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                          .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                          .Count > 0,
                                        Children = result.Aggregations
                                            .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                            .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                            .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                            .Select(v3 => new TreeAggregate
                                            {
                                                Key = v3.Key,
                                                DocCount = v3.DocCount,
                                                DocCountErrorUpperBound = v3.DocCountErrorUpperBound,
                                                HasChildren = result.Aggregations
                                                                  .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                                                  .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                                  .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                                  .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                                  .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                                  .Count > 0,
                                                Children = result.Aggregations
                                                    .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                    .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                    .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                    .Select(v2 => new TreeAggregate
                                                    {
                                                        Key = v2.Key,
                                                        DocCount = v2.DocCount,
                                                        DocCountErrorUpperBound = v2.DocCountErrorUpperBound,
                                                        HasChildren = result.Aggregations
                                                                          .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                                                          .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                                          .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                                          .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                                          .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                                          .FirstOrDefault(x1 => x1.Key == v2.Key)?.Terms(TermsNameForCategory1).Buckets
                                                                          .Count > 0,
                                                        Children = result.Aggregations
                                                            .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                            .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                            .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                            .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                            .FirstOrDefault(x1 => x1.Key == v2.Key)?.Terms(TermsNameForCategory1).Buckets
                                                            .Select(v1 => new TreeAggregate
                                                            {
                                                                Key = v1.Key,
                                                                DocCount = v1.DocCount,
                                                                DocCountErrorUpperBound = v1.DocCountErrorUpperBound,
                                                                HasChildren = result.Aggregations
                                                                                  .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                                                                  .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                                                  .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                                                  .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                                                  .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                                                  .FirstOrDefault(x1 => x1.Key == v2.Key)?.Terms(TermsNameForCategory1).Buckets
                                                                                  .FirstOrDefault(x0 => x0.Key == v1.Key)?.Terms(TermsNameForCategory0).Buckets
                                                                                  .Count > 0,
                                                                Children = result.Aggregations
                                                                    .Nested(NestedNameForProductCategories).Terms(TermsNameForCategoryParent).Buckets
                                                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                                    .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                                    .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                                    .FirstOrDefault(x1 => x1.Key == v2.Key)?.Terms(TermsNameForCategory1).Buckets
                                                                    .FirstOrDefault(x0 => x0.Key == v1.Key)?.Terms(TermsNameForCategory0).Buckets
                                                                    .Select(v0 => new TreeAggregate
                                                                    {
                                                                        Key = v0.Key,
                                                                        DocCount = v0.DocCount,
                                                                        DocCountErrorUpperBound = v0.DocCountErrorUpperBound,
                                                                    }).ToList(),
                                                            }).ToList(),
                                                    }).ToList(),
                                            }).ToList(),
                                    }).ToList(),
                            }).ToList(),
                    }).ToList(),
            };
            model.CategoriesTree = categoriesTree;
            while (model.CategoriesTree.Children.All(x => x.Key == NotAvailable))
            {
                // Go in a level
                model.CategoriesTree = model.CategoriesTree.Children?.FirstOrDefault();
                if (model.CategoriesTree == null)
                {
                    break;
                }
            }
            if (model.CategoriesTree is { HasChildren: true })
            {
                var changeMade = false;
                while (model.CategoriesTree.Children.Any(x => x.Key == NotAvailable))
                {
                    var root = model.CategoriesTree;
                    MergeNAChildrenUpOneLevel(model, root);
                    model.CategoriesTree = root;
                    changeMade = true;
                }
                if (changeMade)
                {
                    var mergeMade = false;
                    // Update the aggregates that ended up with duplicate values
                    foreach (var ap in model.CategoriesTree.Children)
                    {
                        if (ap.Children?.Any() != true)
                        {
                            continue;
                        }
                        ap.HasChildren = true;
                        mergeMade |= MergeDuplicateKeysForLevel(ap);
                        foreach (var a5 in ap.Children)
                        {
                            if (a5.Children?.Any() != true)
                            {
                                continue;
                            }
                            a5.HasChildren = true;
                            mergeMade |= MergeDuplicateKeysForLevel(a5);
                            foreach (var a4 in a5.Children)
                            {
                                if (a4.Children?.Any() != true)
                                {
                                    continue;
                                }
                                a4.HasChildren = true;
                                mergeMade |= MergeDuplicateKeysForLevel(a4);
                                foreach (var a3 in a4.Children)
                                {
                                    if (a3.Children?.Any() != true)
                                    {
                                        continue;
                                    }
                                    a3.HasChildren = true;
                                    mergeMade |= MergeDuplicateKeysForLevel(a3);
                                    foreach (var a2 in a3.Children)
                                    {
                                        if (a2.Children?.Any() != true)
                                        {
                                            continue;
                                        }
                                        a2.HasChildren = true;
                                        mergeMade |= MergeDuplicateKeysForLevel(a2);
                                        foreach (var a1 in a2.Children)
                                        {
                                            if (a1.Children?.Any() != true)
                                            {
                                                continue;
                                            }
                                            a1.HasChildren = true;
                                            mergeMade |= MergeDuplicateKeysForLevel(a1);
                                            foreach (var a0 in a1.Children)
                                            {
                                                mergeMade |= MergeDuplicateKeysForLevel(a0);
                                                // a0 should never have children so we stop here
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (mergeMade)
                    {
                        // Redo the counts at all levels
                        foreach (var ap in model.CategoriesTree.Children)
                        {
                            if (ap.HasChildren)
                            {
                                foreach (var a5 in ap.Children)
                                {
                                    if (a5.HasChildren)
                                    {
                                        foreach (var a4 in a5.Children)
                                        {
                                            if (a4.HasChildren)
                                            {
                                                foreach (var a3 in a4.Children)
                                                {
                                                    if (a3.HasChildren)
                                                    {
                                                        foreach (var a2 in a3.Children)
                                                        {
                                                            if (a2.HasChildren)
                                                            {
                                                                foreach (var a1 in a2.Children)
                                                                {
                                                                    if (a1.HasChildren)
                                                                    {
                                                                        foreach (var a0 in a1.Children)
                                                                        {
                                                                            a0.OriginalDocCount = a0.DocCount;
                                                                            a0.SuspectedThisLevelDocCount = a0.Children?.Any() == true ? a1.Children.Sum(x => x.DocCount + (x.SumOtherDocCount ?? 0)) : 0;
                                                                            var newCount0 = a0.HasChildren ? a1.SuspectedThisLevelDocCount : a0.DocCount;
                                                                            a0.MergedDocCount = a0.DocCount + newCount0;
                                                                        }
                                                                    }
                                                                    a1.OriginalDocCount = a1.DocCount;
                                                                    a1.SuspectedThisLevelDocCount = a1.Children?.Any() == true ? a1.Children.Sum(x => x.DocCount + (x.SumOtherDocCount ?? 0)) : 0;
                                                                    var newCount1 = a1.HasChildren ? a1.SuspectedThisLevelDocCount : a1.DocCount;
                                                                    a1.MergedDocCount = a1.DocCount + newCount1;
                                                                }
                                                            }
                                                            a2.OriginalDocCount = a2.DocCount;
                                                            a2.SuspectedThisLevelDocCount = a2.Children?.Any() == true ? a2.Children.Sum(x => x.DocCount + (x.SumOtherDocCount ?? 0)) : 0;
                                                            var newCount2 = a2.HasChildren ? a2.SuspectedThisLevelDocCount : a2.DocCount;
                                                            a2.MergedDocCount = a2.DocCount + newCount2;
                                                        }
                                                    }
                                                    a3.OriginalDocCount = a3.DocCount;
                                                    a3.SuspectedThisLevelDocCount = a3.Children?.Any() == true ? a3.Children.Sum(x => x.DocCount + (x.SumOtherDocCount ?? 0)) : 0;
                                                    var newCount3 = a3.HasChildren ? a3.SuspectedThisLevelDocCount : a3.DocCount;
                                                    a3.MergedDocCount = a3.DocCount + newCount3;
                                                }
                                            }
                                            a4.OriginalDocCount = a4.DocCount;
                                            a4.SuspectedThisLevelDocCount = a4.Children?.Any() == true ? a4.Children.Sum(x => x.DocCount + (x.SumOtherDocCount ?? 0)) : 0;
                                            var newCount4 = a4.HasChildren ? a4.SuspectedThisLevelDocCount : a4.DocCount;
                                            a4.MergedDocCount = a4.DocCount + newCount4;
                                        }
                                    }
                                    a5.OriginalDocCount = a5.DocCount;
                                    a5.SuspectedThisLevelDocCount = a5.Children?.Any() == true ? a5.Children.Sum(x => x.DocCount + (x.SumOtherDocCount ?? 0)) : 0;
                                    var newCount5 = a5.HasChildren ? a5.SuspectedThisLevelDocCount : a5.DocCount;
                                    a5.MergedDocCount = a5.DocCount + newCount5;
                                }
                            }
                            ap.OriginalDocCount = ap.DocCount;
                            ap.SuspectedThisLevelDocCount = ap.Children?.Any() == true ? ap.Children.Sum(x => x.DocCount + (x.SumOtherDocCount ?? 0)) : 0;
                            var newCountP = ap.HasChildren ? ap.SuspectedThisLevelDocCount : ap.DocCount;
                            ap.MergedDocCount = ap.DocCount + newCountP;
                        }
                        model.CategoriesTree.OriginalDocCount = model.CategoriesTree.DocCount;
                        model.CategoriesTree.SuspectedThisLevelDocCount = model.CategoriesTree.Children?.Any() == true ? model.CategoriesTree.Children.Sum(x => x.DocCount + (x.SumOtherDocCount ?? 0)) : 0;
                        var newCount = model.CategoriesTree.HasChildren ? model.CategoriesTree.SuspectedThisLevelDocCount : model.CategoriesTree.DocCount;
                        model.CategoriesTree.MergedDocCount = model.CategoriesTree.DocCount + newCount;
                    }
                }
            }
            // thisLevel.Children = thisLevel.Children.OrderByDescending(x => x.DocCount).ToList();
            // thisLevel.DocCount = thisLevel.Children.Sum(x => x.DocCount);
        }

        /// <summary>Merge duplicate keys for level.</summary>
        /// <param name="aggregate">The aggregate.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private static bool MergeDuplicateKeysForLevel(TreeAggregate aggregate)
        {
            var grouped = aggregate.Children.GroupBy(x => x.Key).ToList();
            if (!grouped.Any(x => x.Count() > 1))
            {
                return false;
            }
            // There is at least one duplicate, get the list of duplicates
            var keysToMerge = grouped.Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            for (var i = 0; i < aggregate.Children.Count;)
            {
                // Is this one that needs to merge? if not, move next
                if (!keysToMerge.Contains(aggregate.Children[i].Key))
                {
                    i++;
                    continue;
                }
                // Since we didn't see it earlier in the list, we can look later in
                // the list to find the dupe to merge to this one
                for (var j = i + 1; j < aggregate.Children.Count;)
                {
                    if (aggregate.Children[j].Key != aggregate.Children[i].Key)
                    {
                        j++;
                        continue;
                    }
                    aggregate.Children[i].Children ??= new List<TreeAggregate>();
                    if (aggregate.Children[j].Children == null)
                    {
                        aggregate.Children[i].SumOtherDocCount = aggregate.Children[j].DocCount;
                    }
                    else
                    {
                        aggregate.Children[i].Children.AddRange(aggregate.Children[j].Children);
                    }
                    aggregate.Children.RemoveAt(j);
                }
                i++;
            }
            return true;
        }

        /// <summary>Merge N/A children up one level.</summary>
        /// <param name="model">    The model.</param>
        /// <param name="thisLevel">this level.</param>
        private static void MergeNAChildrenUpOneLevel(ProductSearchViewModel model, TreeAggregate thisLevel)
        {
            var theNAChild = model.CategoriesTree.Children.First(x => x.Key == NotAvailable);
            thisLevel.Children.Remove(theNAChild);
            for (var i = 0; i < theNAChild.Children.Count;)
            {
                var currentChild = theNAChild.Children[0];
                theNAChild.Children.Remove(currentChild);
                ////if (currentChild.Key == NotAvailable)
                ////{
                ////    // This is an NA so we need to look at it's children in the next wrap around
                ////    return;
                ////}
                if (thisLevel.Children.All(x => x.Key != currentChild.Key))
                {
                    // No match at thisLevel, add to thisLevel
                    thisLevel.Children.Add(currentChild);
                    continue;
                }
                // Match at thisLevel, merge with thisLevel's match
                var rootsMatchingChild = thisLevel.Children.First(x => x.Key == currentChild.Key);
                if (currentChild.Children == null)
                {
                    continue;
                }
                rootsMatchingChild.Children?.AddRange(currentChild.Children);
            }
        }
        #endregion
    }

    public partial class ProductSearchModule
    {
        #region Roles
        /// <summary>Roles single query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer RolesSingleQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (string.IsNullOrWhiteSpace(form.Role))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForProductRoleSingle)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsSingleRole)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.RequiresRolesList)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.RequiresRolesList.First().Suffix(Keyword), form.Category)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Roles any query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer RolesAnyQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (!ElasticSearchingProviderConfig.SearchingProductIndexFiltersIncludeRoles)
            {
                return returnQuery;
            }
            if (form.RolesAny?.Any() != true)
            {
                form.RolesAny = new[] { AnonymousRole };
            }
            else if (!form.RolesAny.Contains(AnonymousRole))
            {
                var roles = form.RolesAny.ToList();
                roles.Add(AnonymousRole);
                form.RolesAny = roles.ToArray();
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForProductRolesAny)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAnyRoles)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.RequiresRolesList)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.RequiresRolesList.First().RoleName.Suffix(Keyword)).Terms(form.RolesAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Roles all query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer RolesAllQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.RolesAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.RolesAll.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForProductRolesAllPrefix}{filter}")
                        .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAllRoles)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.RequiresRolesList)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.RequiresRolesList.First().Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for roles.</summary>
        /// <param name="model"> The model.</param>
        /// <param name="result">The result.</param>
        protected void SearchViewModelAdditionalAssignmentsForRoles(
            ProductSearchViewModel model,
            ISearchResponse<IndexableProductModel> result)
        {
            model.Form.RolesAny = null;
        }
        #endregion
    }

    public partial class ProductSearchModule
    {
        #region Brands
        /// <summary>Brands single query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer BrandsSingleQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (!Contract.CheckValidID(form.BrandID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForProductBrandSingle)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsSingleBrand)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.ProductBrands)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.ProductBrands.First().BrandID.Suffix(Keyword), form.BrandID)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Brands any query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer BrandsAnyQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.BrandIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForProductBrandsAny)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAnyBrands)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.ProductBrands)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.ProductBrands.First().BrandID.Suffix(Keyword)).Terms(form.BrandIDsAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Brands all query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer BrandsAllQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.BrandIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.BrandIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForProductBrandsAllPrefix}{filter}")
                        .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAllBrands)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.ProductBrands)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.ProductBrands.First().BrandID.Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for brands.</summary>
        /// <param name="model"> The model.</param>
        /// <param name="result">The result.</param>
        protected void SearchViewModelAdditionalAssignmentsForBrands(
            ProductSearchViewModel model,
            ISearchResponse<IndexableProductModel> result)
        {
            #region Brands
            model.BrandIDs = result.Aggregations.Nested(NestedNameForProductBrands)
                ////?.Aggregations
                ?.Terms(TermsNameForProductBrandIDs)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
            #endregion
        }
        #endregion
    }

    public partial class ProductSearchModule
    {
        #region Stores
        /// <summary>Stores single query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer StoresSingleQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (!Contract.CheckValidID(form.StoreID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForProductStoreSingle)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsSingleStore)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.ProductStores)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.ProductStores.First().StoreID.Suffix(Keyword), form.StoreID)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Stores any query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer StoresAnyQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.StoreIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForProductStoresAny)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAnyStores)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.ProductStores)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.ProductStores.First().StoreID.Suffix(Keyword)).Terms(form.StoreIDsAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Stores all query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer StoresAllQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.StoreIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.StoreIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForProductStoresAllPrefix}{filter}")
                        .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAllStores)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.ProductStores)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.ProductStores.First().StoreID.Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for stores.</summary>
        /// <param name="model"> The model.</param>
        /// <param name="result">The result.</param>
        protected void SearchViewModelAdditionalAssignmentsForStores(
            ProductSearchViewModel model,
            ISearchResponse<IndexableProductModel> result)
        {
            model.StoreIDs = result.Aggregations.Nested(NestedNameForProductStores)
                ////?.Aggregations
                ?.Terms(TermsNameForProductStoreIDs)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }
        #endregion
    }

    public partial class ProductSearchModule
    {
        #region Attributes
        /// <summary>Attributes any query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer AttributesAnyQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.AttributesAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForProductAttributesAny)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAnyAttributes)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Attributes)
                    .Query(nq => +nq.Bool(b => AttributeValuesAnyBoolQueryDescriptor(b, form)))
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Attributes all query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer AttributesAllQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
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
                        .Name($"{NestedNameForProductAttributesAllPrefix}{filter.Key.ToLower().Replace(" ", "-")}")
                        .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsAllAttributes)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Attributes)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(s => s.Term(p => p.Attributes.First().Key.Suffix(Keyword), filter.Key)
                                          && s.Terms(t => t.Field(p => p.Attributes.First().Value.Suffix(Keyword)).Terms(filter.Value))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Attribute values any bool query descriptor.</summary>
        /// <param name="b">   The BoolQueryDescriptor{IndexableProductModel} to process.</param>
        /// <param name="form">The form.</param>
        /// <returns>A BoolQueryDescriptor{IndexableProductModel}.</returns>
        protected BoolQueryDescriptor<IndexableProductModel> AttributeValuesAnyBoolQueryDescriptor(
            BoolQueryDescriptor<IndexableProductModel> b,
            ProductCatalogSearchForm form)
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
            return new BoolQueryDescriptor<IndexableProductModel>()
                .MinimumShouldMatch(MinimumShouldMatch.Percentage(1.00d)) // 100%
                .Should(filterList
                    .Select(x => new QueryContainerDescriptor<IndexableProductModel>()
                        .Bool(b1 => b1
                            .Should(m => m.Term(p => p.Attributes.First().Key.Suffix(Keyword), x.Key)
                                      && m.Terms(t => t.Field(p => p.Attributes.First().Value.Suffix(Keyword)).Terms(x.Value))
                            )
                        )
                    ).ToArray()
                );
        }

        /// <summary>Searches for the first view model additional assignments for attributes.</summary>
        /// <param name="model"> The model.</param>
        /// <param name="result">The result.</param>
        protected void SearchViewModelAdditionalAssignmentsForAttributes(
            ProductSearchViewModel model,
            ISearchResponse<IndexableProductModel> result)
        {
            model.AttributesDict = result.Aggregations.Nested(NestedNameForProductAttributes)
                .Terms(TermsNameForAttributeKeys)
                .Buckets
                .ToDictionary(
                    k => k.Key,
                    v => ((List<IBucket>)((BucketAggregate)result.Aggregations.Nested(NestedNameForProductAttributes)
                                .Terms(TermsNameForAttributeKeys)
                                .Buckets
                                .First(x => x.Key == v.Key)
                                .First()
                                .Value)
                            .Items)
                        .Cast<KeyedBucket<object>>()
                        .ToDictionary(k2 => k2.Key as string, v2 => v2.DocCount));
        }
        #endregion
    }

    public partial class ProductSearchModule
    {
        #region Price Ranges
        /// <summary>Price ranges query modification.</summary>
        /// <param name="q">          The QueryContainerDescriptor{IndexableProductModel} to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected QueryContainer PriceRangesQueryModification(
            QueryContainerDescriptor<IndexableProductModel> q,
            QueryContainer returnQuery,
            ProductCatalogSearchForm form)
        {
            if (form.PriceRanges?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Bool(b => b.Name(RangeNameForProductPriceRanges)
                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsPriceRanges)
                    .Should(CreatePriceRangeQueryLambdas(form))
                    .MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                );
            return returnQuery;
        }

        /// <summary>Price range query descriptor.</summary>
        /// <param name="form">The form.</param>
        /// <returns>A NumericRangeQueryDescriptor{IndexableProductModel}.</returns>
        protected Func<QueryContainerDescriptor<IndexableProductModel>, QueryContainer>[] CreatePriceRangeQueryLambdas(
            ProductCatalogSearchForm form)
        {
            var priceRangeQueries = new List<Func<QueryContainerDescriptor<IndexableProductModel>, QueryContainer>>();
            if (form.PriceRanges?.All(string.IsNullOrWhiteSpace) != false)
            {
                return priceRangeQueries.ToArray();
            }
            var regex = new Regex("[\\d.]+");
            foreach (var priceRange in form.PriceRanges)
            {
                var tempPriceRange = priceRange;
                // Less than first increment
                if (priceRange.Contains("<"))
                {
                    priceRangeQueries.Add(s => s
                        .Range(c => c
                            .Name(tempPriceRange)
                            .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsPriceRanges)
                            .Field(p => p.FinalPrice)
                            .LessThan((double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Amount)
                            .Relation(RangeRelation.Within)));
                }
                // Greater than last increment stop
                else if (priceRange.Contains("+"))
                {
                    var lastIncrementStop = ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Stop
                        ?? ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Stop
                        ?? ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Stop
                        ?? ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Stop;
                    priceRangeQueries.Add(s => s
                        .Range(c => c
                            .Name(tempPriceRange)
                            .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsPriceRanges)
                            .Field(p => p.FinalPrice)
                            .GreaterThanOrEquals((double)lastIncrementStop)
                            .Relation(RangeRelation.Within)));
                }
                else
                {
                    var match = regex.Match(priceRange);
                    var amount = double.Parse(match.Value);
                    switch (amount)
                    {
                        case var x when x < (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Stop:
                        {
                            priceRangeQueries.Add(s => s
                                .Range(c => c
                                    .Name(tempPriceRange)
                                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsPriceRanges)
                                    .Field(p => p.FinalPrice)
                                    .GreaterThanOrEquals(amount)
                                    .LessThan(amount + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Amount)
                                    .Relation(RangeRelation.Within)));
                            break;
                        }
                        case var x when x < (double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Stop:
                        {
                            priceRangeQueries.Add(s => s
                                .Range(c => c
                                    .Name(tempPriceRange)
                                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsPriceRanges)
                                    .Field(p => p.FinalPrice)
                                    .GreaterThanOrEquals(amount)
                                    .LessThan(amount + (double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Amount)
                                    .Relation(RangeRelation.Within)));
                            break;
                        }
                        case var x when x < (double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Stop:
                        {
                            priceRangeQueries.Add(s => s
                                .Range(c => c
                                    .Name(tempPriceRange)
                                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsPriceRanges)
                                    .Field(p => p.FinalPrice)
                                    .GreaterThanOrEquals(amount)
                                    .LessThan(amount + (double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Amount)
                                    .Relation(RangeRelation.Within)));
                            break;
                        }
                        case var x when x < (double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Stop:
                        {
                            priceRangeQueries.Add(s => s
                                .Range(c => c
                                    .Name(tempPriceRange)
                                    .Boost((double)ElasticSearchingProviderConfig.SearchingBoostsPriceRanges)
                                    .Field(p => p.FinalPrice)
                                    .GreaterThanOrEquals(amount)
                                    .LessThan(amount + ((double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Amount ?? 0))
                                    .Relation(RangeRelation.Within)));
                            break;
                        }
                    }
                }
            }
            return priceRangeQueries.ToArray();
        }

        /// <summary>Searches for the first view model additional assignments for price ranges.</summary>
        /// <param name="model"> The model.</param>
        /// <param name="result">The result.</param>
        protected void SearchViewModelAdditionalAssignmentsForPriceRanges(
            ProductSearchViewModel model,
            ISearchResponse<IndexableProductModel> result)
        {
            if (result.Aggregations.Range(RangeNameForProductPriceRanges)?.Buckets != null)
            {
                var priceRanges = result.Aggregations.Range(RangeNameForProductPriceRanges)
                    .Buckets
                    .Select(x => new AggregatePriceRange
                    {
                        From = x.From,
                        To = x.To,
                        DocCount = x.DocCount,
                        Label = x.Key,
                    }).ToList();
                model.PriceRanges = priceRanges;
            }
        }

        /// <summary>Creates price range aggregates based on increment config values.</summary>
        /// <returns>A new array of func{ aggregation range descriptor, i aggregation range}.</returns>
        private static Func<AggregationRangeDescriptor, IAggregationRange>[] CreatePriceRangeLambdas()
        {
            var priceRanges = new List<Func<AggregationRangeDescriptor, IAggregationRange>>
            {
                // < ${first increment amount}
                x => x
                    .To((double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Amount)
                    .Key($"< ${ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Amount}"),
            };
            // Create possible increment amount values
            for (var i = (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Amount;
                 i < (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Stop;
                 i += (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Amount)
            {
                var from = i;
                priceRanges.Add(x => x
                    .From(from)
                    .To(from + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Amount - 0.0001)
                    .Key($"${from} - ${from + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Amount - 0.01}"));
            }
            if (ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Amount.HasValue
                && ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Stop.HasValue)
            {
                for (double? i = (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Stop;
                    i < (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Stop;
                    i += (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Amount)
                {
                    var from = i;
                    priceRanges.Add(x => x
                        .From(from)
                        .To(from + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Amount - 0.0001)
                        .Key($"${from} - ${from + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Amount - 0.01}"));
                }
                if (ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Amount.HasValue
                    && ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Stop.HasValue)
                {
                    for (var i = (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Stop;
                         i < (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Stop;
                         i += (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Amount)
                    {
                        var from = i;
                        priceRanges.Add(x => x
                            .From(from)
                            .To(from + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Amount - 0.0001)
                            .Key($"${from} - ${from + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Amount - 0.01}"));
                    }
                    if (ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Amount.HasValue
                        && ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Stop.HasValue)
                    {
                        for (var i = (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Stop;
                             i < (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Stop;
                             i += (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Amount)
                        {
                            var from = i;
                            priceRanges.Add(x => x
                                .From(from)
                                .To(from + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Amount - 0.0001)
                                .Key($"${from} - ${from + (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Amount - 0.01}"));
                        }
                    }
                }
            }
            // ${last increment amount} +
            priceRanges.Add(x => x.From((double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Stop
                ?? (double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Stop
                ?? (double?)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Stop
                ?? (double)ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Stop)
                .Key("$" + (ElasticSearchingProviderConfig.SearchingPriceRangesIncrement4Stop
                            ?? ElasticSearchingProviderConfig.SearchingPriceRangesIncrement3Stop
                            ?? ElasticSearchingProviderConfig.SearchingPriceRangesIncrement2Stop
                            ?? ElasticSearchingProviderConfig.SearchingPriceRangesIncrement1Stop) + " +"));
            return priceRanges.ToArray();
        }
        #endregion
    }
}
