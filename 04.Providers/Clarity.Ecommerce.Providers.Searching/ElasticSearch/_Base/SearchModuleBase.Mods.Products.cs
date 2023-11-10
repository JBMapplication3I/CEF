// <copyright file="SearchModuleBase.Mods.Products.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. products class</summary>
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
        protected const string NestedNameForRecordProducts = "record-products";
        protected const string NestedNameForRecordProductSingle = "record-single-product";
        protected const string NestedNameForRecordProductsAny = "record-products-any";
        protected const string NestedNameForRecordProductsAllPrefix = "record-products-all-";
        protected const string TermsNameForRecordProductIDs = "record-products-ids";
        protected const string TermsNameForRecordProductKeys = "record-products-keys";
        protected const string TermsNameForRecordProductNames = "record-products-names";
        #endregion

        /// <summary>Products single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer ProductsSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidID(form.ProductID))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordProductSingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleProduct)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Products)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.Products!.First().ID.Suffix(Keyword), form.ProductID)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Products any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer ProductsAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.ProductIDsAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForRecordProductsAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyProducts)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Products)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.Products!.First().ID.Suffix(Keyword)).Terms(form.ProductIDsAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Products all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer ProductsAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.ProductIDsAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.ProductIDsAll.Where(x => Contract.CheckValidID(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordProductsAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllProducts)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Products)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    s => s.Terms(t => t.Field(p => p.Products!.First().ID.Suffix(Keyword)).Terms(filter))
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Products single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer ProductsOnHandQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form)
        {
            if (form.OnHand)
            {
                returnQuery &= +q.Range(m => m
                    .Field(p => p.StockQuantity)
                    .GreaterThan(0)
                );
            }
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for products.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForProducts(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            model.ProductIDs = result.Aggregations.Nested(NestedNameForRecordProducts)
                ////?.Aggregations
                ?.Terms(TermsNameForRecordProductIDs)
                ?.Buckets
                .ToDictionary(x => x.Key, x => x.DocCount);
        }

        /// <summary>Appends the aggregations for products.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForProducts(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            returnQuery &= returnQuery.Nested(NestedNameForRecordProducts, n => n
                .Path(p => p.Products)
                .Aggregations(ap => ap
                    .Terms(TermsNameForRecordProductIDs, ts => ts
                        .Field(p => p.Products!.First().ID.Suffix(Keyword))
                        .Missing(0).Size(30)
                    )
                    .Terms(TermsNameForRecordProductKeys, ts => ts
                        .Field(p => p.Products!.First().Key.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                    .Terms(TermsNameForRecordProductNames, ts => ts
                        .Field(p => p.Products!.First().Name.Suffix(Keyword))
                        .Missing(string.Empty).Size(30)
                    )
                )
            );
            return returnQuery;
        }
    }
}
