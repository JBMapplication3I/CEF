// <copyright file="DumpReaderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the dump reader base class</summary>
namespace Clarity.Ecommerce.Providers.Searching.ElasticSearch
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Searching;
    using MoreLinq;
    using Nest;
    using Utilities;

    /// <summary>A dump reader base.</summary>
    /// <typeparam name="TIndexableModel">Type of the indexable model.</typeparam>
    /// <seealso cref="LoggingBase"/>
    internal abstract class DumpReaderBase<TIndexableModel> : LoggingBase
        where TIndexableModel : IndexableModelBase
    {
        #region Constants
        /// <summary>(Immutable) The core weight multiplier.</summary>
        protected const decimal CoreWeightMultiplier = 100m;

        /// <summary>(Immutable) The role for anonymous.</summary>
        protected const string RoleForAnonymous = "Anonymous";

        /// <summary>(Immutable) The undefined.</summary>
        protected const string Undefined = "undefined";

        /// <summary>(Immutable) The attribute name for SKU restrictions.</summary>
        protected const string AttributeNameForSKURestrictions = "SKU-Restrictions";

        /// <summary>(Immutable) The attribute value for SKU restrictions.</summary>
        protected const string AttributeValueForSKURestrictions = "RestrictShipFlag\":\"Y\"";

        /// <summary>(Immutable) The pipe.</summary>
        protected const string Pipe = "|";
        #endregion

        /// <summary>Gets the records in this collection.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="ct">                The cancellation token.</param>
        /// <returns>An enumerator that allows foreach to be used to process the records in this collection.</returns>
        public abstract IEnumerable<TIndexableModel> GetRecords(
            string? contextProfileName,
            CancellationToken ct);

        /// <summary>Loads filterable attributes.</summary>
        /// <param name="context">           The context.</param>
        /// <param name="useSkuRestrictions">True to use SKU restrictions.</param>
        /// <returns>The filterable attributes.</returns>
        protected virtual Dictionary<string, AttrModel> LoadFilterableAttributes(
            IClarityEcommerceEntities context,
            bool useSkuRestrictions = false)
        {
            var query = context.GeneralAttributes.AsNoTracking().FilterByActive(true);
            if (useSkuRestrictions)
            {
                query = query
                    .Where(x => !x.IsMarkup
                        && !x.HideFromStorefront
                        && (x.IsFilter || x.CustomKey == AttributeNameForSKURestrictions));
            }
            else
            {
                query = query
                    .Where(x => !x.IsMarkup
                        && !x.HideFromStorefront
                        && x.IsFilter);
            }
            return query
                .Select(x => new AttrModel
                {
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Active = true,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    SortOrder = x.SortOrder,
                    IsFilter = true,
                    TypeID = x.TypeID,
                })
                .ToDictionary(x => x.CustomKey!, x => x);
        }

        /// <summary>Loads initial type data.</summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="context">The context.</param>
        /// <returns>The initial type data.</returns>
        protected virtual Dictionary<int, TypeModel> LoadInitialTypeData<TEntity>(
                IClarityEcommerceEntities context)
            where TEntity : class, ITypableBase
        {
            return context.Set<TEntity>()
                .FilterByActive(true)
                .Select(x => new TypeModel
                {
                    ID = x.ID,
                    Name = x.DisplayName ?? x.Name,
                    SortOrder = x.SortOrder ?? 0,
                })
                .ToDictionary(x => x.ID, x => x);
        }

        /// <summary>Loads initial category data.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The initial category data.</returns>
        protected virtual Dictionary<int, CatModel> LoadInitialCategoryData(
            IClarityEcommerceEntities context)
        {
            return context.Categories
                .Where(x => x.Active)
                .Select(x => new CatModel
                {
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    ParentID = x.ParentID,
                })
                .ToDictionary(x => x.ID, x => x);
        }

        /// <summary>Loads initial region data.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The initial region data.</returns>
        protected virtual Dictionary<int, IndexableRegionModel> LoadInitialRegionData(
            IClarityEcommerceEntities context)
        {
            return context.Regions
                .Where(x => x.Active)
                .Select(x => new IndexableRegionModel
                {
                    ID = x.ID,
                    Key = x.CustomKey,
                    Name = x.Name,
                })
                .ToDictionary(x => x.ID, x => x);
        }

        /// <summary>Assign category data.</summary>
        /// <param name="categories">    The categories.</param>
        /// <param name="categoriesRead">The categories read.</param>
        /// <returns>A <see cref="List{IndexableCategoryFilter}"/>.</returns>
        protected virtual List<IndexableCategoryFilter> AssignCategoryData(
            Dictionary<int, CatModel> categories,
            IEnumerable<IndexableCategoryFilterRead> categoriesRead)
        {
            List<IndexableCategoryFilter> indexes = new();
            foreach (var read in categoriesRead)
            {
                var toAdd = new IndexableCategoryFilter
                {
                    CategoryName = read.Name,
                };
                if (!Contract.CheckValidID(read.ParentID))
                {
                    indexes.Add(toAdd);
                    continue;
                }
                if (!categories.ContainsKey(read.ParentID!.Value))
                {
                    // Category isn't visible
                    indexes.Add(toAdd);
                    continue;
                }
                var parents = new[]
                {
                    categories[read.ParentID.Value],
                    null,
                    null,
                    null,
                    null,
                    null,
                };
                // ReSharper disable PossibleInvalidOperationException
                var i = 0;
                toAdd.CategoryParent1Name = parents[i]!.Name + Pipe + parents[i]!.CustomKey;
                if (!Contract.CheckValidID(parents[i]!.ParentID))
                {
                    indexes.Add(toAdd);
                    continue;
                }
                parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                toAdd.CategoryParent2Name = parents[i]!.Name + Pipe + parents[i]!.CustomKey;
                if (!Contract.CheckValidID(parents[i]!.ParentID))
                {
                    indexes.Add(toAdd);
                    continue;
                }
                parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                toAdd.CategoryParent3Name = parents[i]!.Name + Pipe + parents[i]!.CustomKey;
                if (!Contract.CheckValidID(parents[i]!.ParentID))
                {
                    indexes.Add(toAdd);
                    continue;
                }
                parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                toAdd.CategoryParent4Name = parents[i]!.Name + Pipe + parents[i]!.CustomKey;
                if (!Contract.CheckValidID(parents[i]!.ParentID))
                {
                    indexes.Add(toAdd);
                    continue;
                }
                parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                toAdd.CategoryParent5Name = parents[i]!.Name + Pipe + parents[i]!.CustomKey;
                if (!Contract.CheckValidID(parents[i]!.ParentID))
                {
                    indexes.Add(toAdd);
                    continue;
                }
                parents[++i] = categories[parents[i - 1]!.ParentID!.Value];
                toAdd.CategoryParent6Name = parents[i]!.Name + Pipe + parents[i]!.CustomKey;
                if (!Contract.CheckValidID(parents[i]!.ParentID))
                {
                    indexes.Add(toAdd);
                    continue;
                }
                indexes.Add(toAdd);
                // ReSharper restore PossibleInvalidOperationException
            }
            return indexes;
        }

        /// <summary>Handles the category data.</summary>
        /// <param name="categories">    The categories.</param>
        /// <param name="categoriesRead">The categories read.</param>
        /// <param name="indexableModel">The indexable model.</param>
        protected virtual void HandleCategoryData(
            Dictionary<int, CatModel> categories,
            List<IndexableCategoryFilterRead> categoriesRead,
            TIndexableModel indexableModel)
        {
            indexableModel.Categories = AssignCategoryData(
                categories,
                categoriesRead
                    .Select(y => new IndexableCategoryFilterRead
                    {
                        ID = y.ID,
                        Name = y.Name,
                        DisplayName = y.DisplayName,
                        ParentID = y.ParentID,
                    })
                    .DistinctBy(x => x.ID));
            var categoryCount = categoriesRead.Count;
            // Reverse the order since they were reversed for the category tree
            indexableModel.CategoryNamePrimary = categoryCount > 0 ? categoriesRead.ElementAt(categoryCount - 1).Name : null;
            indexableModel.CategoryNameSecondary = categoryCount > 1 ? categoriesRead.ElementAt(categoryCount - 2).Name : null;
            indexableModel.CategoryNameTertiary = categoryCount > 2 ? categoriesRead.ElementAt(categoryCount - 3).Name : null;
            if (Contract.CheckValidKey(indexableModel.CategoryNamePrimary))
            {
                indexableModel.SuggestedByCategory = new CompletionField
                {
                    Input = new List<string>(indexableModel.CategoryNamePrimary!.Trim().Split(' ')) { indexableModel.CategoryNamePrimary.Trim() },
                    Weight = (int)CoreWeightMultiplier,
                };
            }
        }

        protected virtual void HandleFilterableAttributes(
            string? versionFilterableJsonAttributes,
            Dictionary<string, AttrModel> filterableAttributes,
            TIndexableModel indexableModel,
            bool useSkuRestrictions = false)
        {
            indexableModel.FilterableAttributes = !string.IsNullOrEmpty(versionFilterableJsonAttributes)
                ? versionFilterableJsonAttributes.DeserializeAttributesDictionary()
                    .Where(x => Contract.CheckValidKey(x.Key)
                        && x.Key != Undefined
                        && Contract.CheckValidKey(x.Value?.Value)
                        && (filterableAttributes.ContainsKey(x.Key) || useSkuRestrictions && x.Key == AttributeNameForSKURestrictions))
                    .Select(x => new IndexableAttributeObjectFilter
                    {
                        ID = x.Value.ID > 0 ? x.Value.ID : filterableAttributes[x.Key].ID,
                        Key = x.Value.Key ?? x.Key,
                        SortOrder = x.Value.SortOrder ?? filterableAttributes[x.Key].SortOrder,
                        Value = CollapseWhitespace(x.Value.Value),
                        UofM = x.Value.UofM,
                    })
                    .Where(x => Contract.CheckValidKey(x.Value))
                : null;
        }

        /// <summary>Handles the queryable attributes.</summary>
        /// <param name="versionsQueryableJsonAttributes">The versions queryable JSON attributes.</param>
        /// <param name="indexableModel">                 The indexable model.</param>
        /// <param name="queryableAttributeKeyList">      List of queryable attribute keys.</param>
        protected virtual void HandleQueryableAttributes(
            string? versionsQueryableJsonAttributes,
            TIndexableModel indexableModel,
            string[] queryableAttributeKeyList)
        {
            var allAttributes = versionsQueryableJsonAttributes.DeserializeAttributesDictionary().ToArray();
            if (!Contract.CheckNotEmpty(allAttributes) || !Contract.CheckNotEmpty(queryableAttributeKeyList))
            {
                return;
            }
            var queryableAttributesToUse = allAttributes
                .Where(x => queryableAttributeKeyList.Contains(x.Value.Key)
                    && Contract.CheckValidKey(x.Value?.Value))
                .ToDictionary(x => x.Key, x => x.Value);
            if (!Contract.CheckNotEmpty(queryableAttributesToUse))
            {
                return;
            }
            indexableModel.SuggestedByQueryableSerializableAttributes = new CompletionField
            {
                Input = queryableAttributesToUse
                    .SelectMany(x => CollapseWhitespace(x.Value.Value)!.Split(' '))
                    .Union(queryableAttributesToUse.Select(x => x.Value.Value))
                    .ToList(),
                Weight = 1,
            };
            indexableModel.QueryableAttributes = queryableAttributesToUse
                .Select(x => new IndexableAttributeObjectFilter
                {
                    ID = x.Value.ID > 0 ? x.Value.ID : queryableAttributesToUse[x.Key].ID,
                    Key = x.Value.Key ?? x.Key,
                    SortOrder = x.Value.SortOrder ?? queryableAttributesToUse[x.Key].SortOrder,
                    Value = CollapseWhitespace(x.Value.Value),
                    UofM = x.Value.UofM,
                });
            indexableModel.QueryableSerializableAttributeValuesAggregate = CollapseWhitespace(
                queryableAttributesToUse
                    .Select(x => x.Value.Value)
                    .Aggregate((c, n) => c + "," + n));
        }

        /// <summary>Handles the roles.</summary>
        /// <param name="requiresRoles"> The requires roles.</param>
        /// <param name="indexableModel">The indexable model.</param>
        protected virtual void HandleRoles(string? requiresRoles, TIndexableModel indexableModel)
        {
            indexableModel.RequiresRolesList = requiresRoles
                ?.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(str => new IndexableRoleFilter { RoleName = str })
                .ToList()
                ?? new List<IndexableRoleFilter> { new() { RoleName = RoleForAnonymous } };
        }

        /// <summary>Handles the standard suggests described by indexableModel.</summary>
        /// <param name="indexableModel">The indexable model.</param>
        /// <param name="weightModifier">The weight modifier.</param>
        protected virtual void HandleStandardSuggests(TIndexableModel indexableModel, decimal weightModifier = 1m)
        {
            indexableModel.SuggestedByName = new CompletionField
            {
                Input = new List<string>(indexableModel.Name!.Trim().Split(' ')) { indexableModel.Name },
                Weight = Math.Max(1, (int)(weightModifier * 1m * CoreWeightMultiplier)),
            };
            indexableModel.SuggestedByCustomKey = new CompletionField
            {
                Input = new List<string>((indexableModel.CustomKey ?? indexableModel.Name)!.Trim().Split(' ')) { indexableModel.CustomKey ?? indexableModel.Name },
                Weight = Math.Max(1, (int)(weightModifier * 2m * CoreWeightMultiplier)),
            };
            if (Contract.CheckValidKey(indexableModel.Description))
            {
                indexableModel.SuggestedByDescription = new CompletionField
                {
                    Input = new List<string>(indexableModel.Description!.Trim().Split(' ')) { indexableModel.Description.Trim() },
                    Weight = Math.Max(1, (int)(weightModifier * 0.25m * CoreWeightMultiplier)),
                };
            }
        }

        /// <summary>Collapse whitespace.</summary>
        /// <param name="source">                        Source for the.</param>
        /// <param name="additionalReplacementsToSpaces">A variable-length parameters list containing additional
        ///                                              replacements to spaces.</param>
        /// <returns>A string?</returns>
        protected virtual string? CollapseWhitespace(
            string? source,
            params char[] additionalReplacementsToSpaces)
        {
            if (source is null)
            {
                return null;
            }
            var temp = source.Trim();
            if (Contract.CheckNotEmpty(additionalReplacementsToSpaces))
            {
                temp = additionalReplacementsToSpaces
                    .Aggregate(temp, (c, r) => c.Replace(r, ' '));
            }
            while (temp.Contains("  "))
            {
                temp = temp.Replace("  ", " ");
            }
            return temp;
        }
    }
}
