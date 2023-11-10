// <copyright file="SearchModuleBase.Mods.Categories.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search module base. mods. categories class</summary>
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
        protected const string NestedNameForRecordCategories = "record-categories";
        protected const string NestedNameForRecordCategorySingle = "record-single-category";
        protected const string NestedNameForRecordCategoriesAny = "record-categories-any";
        protected const string NestedNameForRecordCategoriesAllPrefix = "record-categories-all-";
        protected const string TermsNameForCategoryParent = "category-names-parent";
        protected const string TermsNameForCategory5 = "category-names-5";
        protected const string TermsNameForCategory4 = "category-names-4";
        protected const string TermsNameForCategory3 = "category-names-3";
        protected const string TermsNameForCategory2 = "category-names-2";
        protected const string TermsNameForCategory1 = "category-names-1";
        protected const string TermsNameForCategory0 = "category-names-0";
        #endregion

        /// <summary>Categories single query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer CategoriesSingleQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (!Contract.CheckValidKey(form.Category))
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n
                    .Name(NestedNameForRecordCategorySingle)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsSingleCategory)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Categories)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Term(p => p.Categories!.First().CategoryName.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.Categories!.First().CategoryDisplayName.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.Categories!.First().CategoryParent1Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.Categories!.First().CategoryParent2Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.Categories!.First().CategoryParent3Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.Categories!.First().CategoryParent4Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.Categories!.First().CategoryParent5Name.Suffix(Keyword), form.Category),
                                s => s.Term(p => p.Categories!.First().CategoryParent6Name.Suffix(Keyword), form.Category)
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Categories any query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer CategoriesAnyQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.CategoriesAny?.Any() != true)
            {
                return returnQuery;
            }
            returnQuery &= +q
                .Nested(n => n.Name(NestedNameForRecordCategoriesAny)
                    .Boost(ElasticSearchingProviderConfig.SearchingBoostsAnyCategory)
                    .InnerHits(i => i.Explain())
                    .Path(p => p.Categories)
                    .Query(nq => +nq
                        .Bool(b => b
                            .Should(
                                s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryName.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryDisplayName.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent1Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent2Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent3Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent4Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent5Name.Suffix(Keyword)).Terms(form.CategoriesAny)),
                                s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent6Name.Suffix(Keyword)).Terms(form.CategoriesAny))
                            ).MinimumShouldMatch(MinimumShouldMatch.Fixed(1))
                        )
                    )
                    .IgnoreUnmapped()
                );
            return returnQuery;
        }

        /// <summary>Categories all query modification.</summary>
        /// <param name="q">          The query descriptor to process.</param>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="form">       The form.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>A QueryContainer.</returns>
        protected virtual QueryContainer CategoriesAllQueryModification(
            QueryContainerDescriptor<TIndexModel> q,
            QueryContainer returnQuery,
            TSearchForm form,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            if (form.CategoriesAll?.Any() != true)
            {
                return returnQuery;
            }
            var filterList = form.CategoriesAll.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (filterList.Count == 0)
            {
                return returnQuery;
            }
            filterList.ForEach(filter =>
            {
                returnQuery &= +q
                    .Nested(n => n
                        .Name($"{NestedNameForRecordCategoriesAllPrefix}{filter}")
                        .Boost(ElasticSearchingProviderConfig.SearchingBoostsAllCategories)
                        .InnerHits(i => i.Explain())
                        .Path(p => p.Categories)
                        .Query(nq => +nq
                            .Bool(b => b.MinimumShouldMatch(MinimumShouldMatch.Percentage(100d)) // 100%
                                .Should(
                                    // resharper disable PossiblyMistakenUseOfParamsMethod
                                    s => s.Terms(t => t.Field(p => p.Categories!.First().CategoryName.Suffix(Keyword)).Terms(filter))
                                        || s.Terms(t => t.Field(p => p.Categories!.First().CategoryDisplayName.Suffix(Keyword)).Terms(filter))
                                        || s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent1Name.Suffix(Keyword)).Terms(filter))
                                        || s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent2Name.Suffix(Keyword)).Terms(filter))
                                        || s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent3Name.Suffix(Keyword)).Terms(filter))
                                        || s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent4Name.Suffix(Keyword)).Terms(filter))
                                        || s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent5Name.Suffix(Keyword)).Terms(filter))
                                        || s.Terms(t => t.Field(p => p.Categories!.First().CategoryParent6Name.Suffix(Keyword)).Terms(filter))
                                    // resharper remanufacturer PossiblyMistakenUseOfParamsMethod
                                )
                            )
                        ).IgnoreUnmapped()
                    );
            });
            return returnQuery;
        }

        /// <summary>Searches for the first view model additional assignments for categories.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="result"> The result.</param>
        /// <param name="setting">True to setting.</param>
        protected virtual void SearchViewModelAdditionalAssignmentsForCategories(
            TSearchViewModel model,
            ISearchResponse<TIndexModel> result,
            bool setting)
        {
            if (!setting)
            {
                return;
            }
            var categoriesTree = new AggregateTree
            {
                DocCountErrorUpperBound = result.Aggregations.Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).DocCountErrorUpperBound,
                SumOtherDocCount = result.Aggregations.Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).SumOtherDocCount,
                HasChildren = result.Aggregations.Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets.Count > 0,
                Children = result.Aggregations.Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
#pragma warning disable SA1305 // Field names should not use Hungarian notation
                    .Select(vP => new AggregateTree
#pragma warning restore SA1305 // Field names should not use Hungarian notation
                    {
                        Key = vP.Key,
                        DocCount = vP.DocCount,
                        DocCountErrorUpperBound = vP.DocCountErrorUpperBound,
                        HasChildren = result.Aggregations
                            .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                            .Count > 0,
                        Children = result.Aggregations
                            .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                            .Select(v5 => new AggregateTree
                            {
                                Key = v5.Key,
                                DocCount = v5.DocCount,
                                DocCountErrorUpperBound = v5.DocCountErrorUpperBound,
                                HasChildren = result.Aggregations
                                    .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                    .Count > 0,
                                Children = result.Aggregations
                                    .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                    .Select(v4 => new AggregateTree
                                    {
                                        Key = v4.Key,
                                        DocCount = v4.DocCount,
                                        DocCountErrorUpperBound = v4.DocCountErrorUpperBound,
                                        HasChildren = result.Aggregations
                                            .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                            .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                            .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                            .Count > 0,
                                        Children = result.Aggregations
                                            .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                            .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                            .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                            .Select(v3 => new AggregateTree
                                            {
                                                Key = v3.Key,
                                                DocCount = v3.DocCount,
                                                DocCountErrorUpperBound = v3.DocCountErrorUpperBound,
                                                HasChildren = result.Aggregations
                                                    .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                    .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                    .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                    .Count > 0,
                                                Children = result.Aggregations
                                                    .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                    .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                    .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                    .Select(v2 => new AggregateTree
                                                    {
                                                        Key = v2.Key,
                                                        DocCount = v2.DocCount,
                                                        DocCountErrorUpperBound = v2.DocCountErrorUpperBound,
                                                        HasChildren = result.Aggregations
                                                            .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                            .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                            .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                            .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                            .FirstOrDefault(x1 => x1.Key == v2.Key)?.Terms(TermsNameForCategory1).Buckets
                                                            .Count > 0,
                                                        Children = result.Aggregations
                                                            .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                                            .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                            .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                            .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                            .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                            .FirstOrDefault(x1 => x1.Key == v2.Key)?.Terms(TermsNameForCategory1).Buckets
                                                            .Select(v1 => new AggregateTree
                                                            {
                                                                Key = v1.Key,
                                                                DocCount = v1.DocCount,
                                                                DocCountErrorUpperBound = v1.DocCountErrorUpperBound,
                                                                HasChildren = result.Aggregations
                                                                    .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                                    .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                                    .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                                    .FirstOrDefault(x1 => x1.Key == v2.Key)?.Terms(TermsNameForCategory1).Buckets
                                                                    .FirstOrDefault(x0 => x0.Key == v1.Key)?.Terms(TermsNameForCategory0).Buckets
                                                                    .Count > 0,
                                                                Children = result.Aggregations
                                                                    .Nested(NestedNameForRecordCategories).Terms(TermsNameForCategoryParent).Buckets
                                                                    .FirstOrDefault(x5 => x5.Key == vP.Key)?.Terms(TermsNameForCategory5).Buckets
                                                                    .FirstOrDefault(x4 => x4.Key == v5.Key)?.Terms(TermsNameForCategory4).Buckets
                                                                    .FirstOrDefault(x3 => x3.Key == v4.Key)?.Terms(TermsNameForCategory3).Buckets
                                                                    .FirstOrDefault(x2 => x2.Key == v3.Key)?.Terms(TermsNameForCategory2).Buckets
                                                                    .FirstOrDefault(x1 => x1.Key == v2.Key)?.Terms(TermsNameForCategory1).Buckets
                                                                    .FirstOrDefault(x0 => x0.Key == v1.Key)?.Terms(TermsNameForCategory0).Buckets
                                                                    .Select(v0 => new AggregateTree
                                                                    {
                                                                        Key = v0.Key,
                                                                        DocCount = v0.DocCount,
                                                                        DocCountErrorUpperBound = v0.DocCountErrorUpperBound,
                                                                    })
                                                                    .ToList(),
                                                            })
                                                            .ToList(),
                                                    })
                                                    .ToList(),
                                            })
                                            .ToList(),
                                    })
                                    .ToList(),
                            })
                            .ToList(),
                    })
                    .ToList(),
            };
            model.CategoriesTree = categoriesTree;
            while (model.CategoriesTree.Children!.All(x => x.Key == NotAvailable))
            {
                // Go in a level
                model.CategoriesTree = model.CategoriesTree.Children?.FirstOrDefault();
                if (model.CategoriesTree == null)
                {
                    break;
                }
            }
            if (model.CategoriesTree is not { HasChildren: true })
            {
                return;
            }
            var changeMade = false;
            while (model.CategoriesTree.Children!.Any(x => x.Key == NotAvailable))
            {
                var root = model.CategoriesTree;
                MergeNAChildrenUpOneLevel(model.CategoriesTree, root);
                model.CategoriesTree = root;
                changeMade = true;
            }
            if (!changeMade)
            {
                return;
            }
            var mergeMade = false;
            // Update the aggregates that ended up with duplicate values
            foreach (var ap in model.CategoriesTree.Children!)
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
            if (!mergeMade)
            {
                return;
            }
            // Redo the counts at all levels
            foreach (var ap in model.CategoriesTree.Children)
            {
                if (ap.HasChildren)
                {
                    foreach (var a5 in ap.Children!)
                    {
                        if (a5.HasChildren)
                        {
                            foreach (var a4 in a5.Children!)
                            {
                                if (a4.HasChildren)
                                {
                                    foreach (var a3 in a4.Children!)
                                    {
                                        if (a3.HasChildren)
                                        {
                                            foreach (var a2 in a3.Children!)
                                            {
                                                if (a2.HasChildren)
                                                {
                                                    foreach (var a1 in a2.Children!)
                                                    {
                                                        if (a1.HasChildren)
                                                        {
                                                            foreach (var a0 in a1.Children!)
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
            // thisLevel.Children = thisLevel.Children.OrderByDescending(x => x.DocCount).ToList();
            // thisLevel.DocCount = thisLevel.Children.Sum(x => x.DocCount);
        }

        /// <summary>Appends the aggregations for categories.</summary>
        /// <param name="returnQuery">The return query.</param>
        /// <param name="setting">    True to setting.</param>
        /// <returns>An AggregationContainerDescriptor{TIndexModel}.</returns>
        protected virtual AggregationContainerDescriptor<TIndexModel> AppendAggregationsForCategories(
            AggregationContainerDescriptor<TIndexModel> returnQuery,
            bool setting)
        {
            if (!setting)
            {
                return returnQuery;
            }
            // This puts the categories into a tree up to 7 levels deep. If there's only 2 levels, then you will have 'N/A' om the first 4 levels and then the 2 actual levels all the way in
            returnQuery &= returnQuery
                .Nested(NestedNameForRecordCategories, n => n
                    .Path(p => p.Categories)
                    .Aggregations(ap => ap
                        .Terms(TermsNameForCategoryParent, ts6 => ts6
                            .Field(p => p.Categories!.First().CategoryParent6Name.Suffix(Keyword))
                            .Missing(NotAvailable).Size(500)
                            .Aggregations(a6 => a6
                                .Terms(TermsNameForCategory5, ts5 => ts5
                                    .Field(p => p.Categories!.First().CategoryParent5Name.Suffix(Keyword))
                                    .Missing(NotAvailable).Size(500)
                                    .Aggregations(a5 => a5
                                        .Terms(TermsNameForCategory4, ts4 => ts4
                                            .Field(p => p.Categories!.First().CategoryParent4Name.Suffix(Keyword))
                                            .Missing(NotAvailable).Size(500)
                                            .Aggregations(a4 => a4
                                                .Terms(TermsNameForCategory3, ts3 => ts3
                                                    .Field(p => p.Categories!.First().CategoryParent3Name.Suffix(Keyword))
                                                    .Missing(NotAvailable).Size(500)
                                                    .Aggregations(a3 => a3
                                                        .Terms(TermsNameForCategory2, ts2 => ts2
                                                            .Field(p => p.Categories!.First().CategoryParent2Name.Suffix(Keyword))
                                                            .Missing(NotAvailable).Size(500)
                                                            .Aggregations(a2 => a2
                                                                .Terms(TermsNameForCategory1, ts1 => ts1
                                                                    .Field(p => p.Categories!.First().CategoryParent1Name.Suffix(Keyword))
                                                                    .Missing(NotAvailable).Size(500)
                                                                    .Aggregations(a1 => a1
                                                                        .Terms(TermsNameForCategory0, ts0 => ts0
                                                                            .Field(p => p.Categories!.First().CategoryName.Suffix(Keyword))
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
            return returnQuery;
        }

        /// <summary>Merge N/A children up one level.</summary>
        /// <param name="modelInstance">The model instance.</param>
        /// <param name="thisLevel">    This level.</param>
        protected virtual void MergeNAChildrenUpOneLevel(AggregateTree? modelInstance, AggregateTree thisLevel)
        {
            if (modelInstance is null)
            {
                return;
            }
            var theNAChild = modelInstance.Children!.First(x => x.Key == NotAvailable);
            thisLevel.Children!.Remove(theNAChild);
            for (var i = 0; i < theNAChild.Children!.Count;)
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
                rootsMatchingChild.Children!.AddRange(currentChild.Children);
            }
        }

        /// <summary>Merge duplicate keys for level.</summary>
        /// <param name="aggregate">The aggregate.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual bool MergeDuplicateKeysForLevel(AggregateTree aggregate)
        {
            if (Contract.CheckNull(aggregate.Children))
            {
                return false;
            }
            var grouped = aggregate.Children!.GroupBy(x => x.Key).ToList();
            if (!grouped.Any(x => x.Count() > 1))
            {
                return false;
            }
            // There is at least one duplicate, get the list of duplicates
            var keysToMerge = grouped.Where(x => x.Count() > 1).Select(x => x.Key).ToList();
            for (var i = 0; i < aggregate.Children!.Count;)
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
                    aggregate.Children[i].Children ??= new();
                    if (aggregate.Children[j].Children == null)
                    {
                        aggregate.Children[i].SumOtherDocCount = aggregate.Children[j].DocCount;
                    }
                    else
                    {
                        aggregate.Children[i].Children!.AddRange(aggregate.Children[j].Children!);
                    }
                    aggregate.Children.RemoveAt(j);
                }
                i++;
            }
            return true;
        }
    }
}
