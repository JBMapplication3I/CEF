// <copyright file="CategoryCRUDWorkflow.extended.Trees.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category workflow. trees class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using LinqKit;
    using Models;
    using Utilities;

    public partial class CategoryWorkflow
    {
        /// <summary>The map category mids to category models.</summary>
        private static readonly Func<CategoryMid1, CategoryModel> MapCategoryMidsToCategoryModels = c1 => new()
        {
            // Base Properties
            ID = c1.ID,
            CustomKey = c1.CustomKey,
            Active = c1.Active,
            CreatedDate = c1.CreatedDate,
            SerializableAttributes = c1.JsonAttributes.DeserializeAttributesDictionary(),
            // NameableBase Properties
            Name = c1.Name,
            // IHaveSeoBase Properties
            SeoUrl = c1.SeoUrl ?? c1.Name,
            // Category Properties
            DisplayName = c1.DisplayName,
            IsVisible = c1.IsVisible,
            IncludeInMenu = c1.IncludeInMenu,
            SortOrder = c1.SortOrder,
            RequiresRoles = c1.RequiresRoles,
            RequiresRolesAlt = c1.RequiresRolesAlt,
            // IHaveATypeBase Properties
            TypeID = c1.TypeID,
            // IHaveImagesBase Properties
            PrimaryImageFileName = c1.PrimaryImageFileName,
            // IHaveAParentBase Properties
            HasChildren = c1.Children!.Any(),
            ParentID = c1.ParentID,
            ParentKey = c1.ParentKey,
            ParentName = c1.ParentName,
            ParentSeoUrl = c1.ParentSeoUrl,
            Children = c1.Children!
                .Select(c2 => new CategoryModel
                {
                    // Base Properties
                    ID = c2.ID,
                    CustomKey = c2.CustomKey,
                    Active = c2.Active,
                    CreatedDate = c2.CreatedDate,
                    SerializableAttributes = c2.JsonAttributes.DeserializeAttributesDictionary(),
                    // NameableBase Properties
                    Name = c2.Name,
                    // IHaveSeoBase Properties
                    SeoUrl = (c1.SeoUrl ?? c1.Name) + "/" + (c2.SeoUrl ?? c2.Name),
                    // Category Properties
                    DisplayName = c2.DisplayName,
                    IsVisible = c2.IsVisible,
                    IncludeInMenu = c2.IncludeInMenu,
                    SortOrder = c2.SortOrder,
                    RequiresRoles = c2.RequiresRoles,
                    RequiresRolesAlt = c2.RequiresRolesAlt,
                    // IHaveATypeBase Properties
                    TypeID = c2.TypeID,
                    // IHaveImagesBase Properties
                    PrimaryImageFileName = c2.PrimaryImageFileName,
                    // IHaveAParentBase Properties
                    HasChildren = c2.Children!.Any(),
                    ParentID = c2.ParentID,
                    ParentKey = c2.ParentKey,
                    ParentName = c2.ParentName,
                    ParentSeoUrl = c2.ParentSeoUrl,
                    Children = c2.Children!
                        .Select(c3 => new CategoryModel
                        {
                            // Base Properties
                            ID = c3.ID,
                            CustomKey = c3.CustomKey,
                            Active = c3.Active,
                            CreatedDate = c3.CreatedDate,
                            SerializableAttributes = c3.JsonAttributes.DeserializeAttributesDictionary(),
                            // NameableBase Properties
                            Name = c3.Name,
                            // IHaveSeoBase Properties
                            SeoUrl = (c1.SeoUrl ?? c1.Name) + "/" + (c2.SeoUrl ?? c2.Name) + "/" + (c3.SeoUrl ?? c3.Name),
                            // Category Properties
                            DisplayName = c3.DisplayName,
                            IsVisible = c3.IsVisible,
                            IncludeInMenu = c3.IncludeInMenu,
                            SortOrder = c3.SortOrder,
                            RequiresRoles = c3.RequiresRoles,
                            RequiresRolesAlt = c3.RequiresRolesAlt,
                            // IHaveATypeBase Properties
                            TypeID = c3.TypeID,
                            // IHaveImagesBase Properties
                            PrimaryImageFileName = c3.PrimaryImageFileName,
                            // IHaveAParentBase Properties
                            HasChildren = c3.Children!.Any(),
                            ParentID = c3.ParentID,
                            ParentKey = c3.ParentKey,
                            ParentName = c3.ParentName,
                            ParentSeoUrl = c3.ParentSeoUrl,
                            Children = c3.Children!
                                .Select(c4 => new CategoryModel
                                {
                                    // Base Properties
                                    ID = c4.ID,
                                    CustomKey = c4.CustomKey,
                                    Active = c4.Active,
                                    CreatedDate = c4.CreatedDate,
                                    SerializableAttributes = c4.JsonAttributes.DeserializeAttributesDictionary(),
                                    // NameableBase Properties
                                    Name = c4.Name,
                                    // IHaveSeoBase Properties
                                    SeoUrl = (c1.SeoUrl ?? c1.Name) + "/" + (c2.SeoUrl ?? c2.Name) + "/" + (c3.SeoUrl ?? c3.Name) + "/" + (c4.SeoUrl ?? c4.Name),
                                    // Category Properties
                                    DisplayName = c4.DisplayName,
                                    IsVisible = c4.IsVisible,
                                    IncludeInMenu = c4.IncludeInMenu,
                                    SortOrder = c4.SortOrder,
                                    RequiresRoles = c4.RequiresRoles,
                                    RequiresRolesAlt = c4.RequiresRolesAlt,
                                    // IHaveATypeBase Properties
                                    TypeID = c4.TypeID,
                                    // IHaveImagesBase Properties
                                    PrimaryImageFileName = c4.PrimaryImageFileName,
                                    // IHaveAParentBase Properties
                                    HasChildren = c4.Children!.Any(),
                                    ParentID = c4.ParentID,
                                    ParentKey = c4.ParentKey,
                                    ParentName = c4.ParentName,
                                    ParentSeoUrl = c4.ParentSeoUrl,
                                    Children = c4.Children!
                                        .Select(c5 => new CategoryModel
                                        {
                                            // Base Properties
                                            ID = c5.ID,
                                            CustomKey = c5.CustomKey,
                                            Active = c5.Active,
                                            CreatedDate = c5.CreatedDate,
                                            SerializableAttributes = c5.JsonAttributes.DeserializeAttributesDictionary(),
                                            // NameableBase Properties
                                            Name = c5.Name,
                                            // IHaveSeoBase Properties
                                            SeoUrl = (c1.SeoUrl ?? c1.Name) + "/" + (c2.SeoUrl ?? c2.Name) + "/" + (c3.SeoUrl ?? c3.Name) + "/" + (c4.SeoUrl ?? c4.Name) + "/" + (c5.SeoUrl ?? c5.Name),
                                            // Category Properties
                                            DisplayName = c5.DisplayName,
                                            IsVisible = c5.IsVisible,
                                            IncludeInMenu = c5.IncludeInMenu,
                                            SortOrder = c5.SortOrder,
                                            RequiresRoles = c5.RequiresRoles,
                                            RequiresRolesAlt = c5.RequiresRolesAlt,
                                            // IHaveATypeBase Properties
                                            TypeID = c5.TypeID,
                                            // IHaveImagesBase Properties
                                            PrimaryImageFileName = c5.PrimaryImageFileName,
                                            // IHaveAParentBase Properties
                                            ParentID = c5.ParentID,
                                            ParentKey = c5.ParentKey,
                                            ParentName = c5.ParentName,
                                            ParentSeoUrl = c5.ParentSeoUrl,
                                        })
                                        .OrderBy(c5 => c5.SortOrder)
                                        .ThenBy(c5 => c5.DisplayName ?? c5.Name)
                                        .ToList(),
                                })
                                .OrderBy(c4 => c4.SortOrder)
                                .ThenBy(c4 => c4.DisplayName ?? c4.Name)
                                .ToList(),
                        })
                        .OrderBy(c3 => c3.SortOrder)
                        .ThenBy(c3 => c3.DisplayName ?? c3.Name)
                        .ToList(),
                })
                .OrderBy(c2 => c2.SortOrder)
                .ThenBy(c2 => c2.DisplayName ?? c2.Name)
                .ToList(),
        };

        /// <summary>The map category mids to category models.</summary>
        private static readonly Func<CategoryMid1, MenuCategoryModel> MapCategoryMidsToMenuCategoryModels = c1 => new()
        {
            ID = c1.ID,
            CustomKey = c1.CustomKey,
            Name = c1.Name,
            DisplayName = c1.DisplayName,
            SeoUrl = c1.SeoUrl ?? c1.Name,
            SortOrder = c1.SortOrder,
            HasChildren = c1.Children!.Any(),
            PrimaryImageFileName = c1.PrimaryImageFileName,
            Children = c1.Children!
                .Select(c2 => new MenuCategoryModel
                {
                    ID = c2.ID,
                    CustomKey = c2.CustomKey,
                    Name = c2.Name,
                    DisplayName = c2.DisplayName,
                    SeoUrl = (c1.SeoUrl ?? c1.Name) + "/" + (c2.SeoUrl ?? c2.Name),
                    SortOrder = c2.SortOrder,
                    PrimaryImageFileName = c1.PrimaryImageFileName,
                    HasChildren = c2.Children!.Any(),
                    Children = c2.Children!
                        .Select(c3 => new MenuCategoryModel
                        {
                            ID = c3.ID,
                            CustomKey = c3.CustomKey,
                            Name = c3.Name,
                            DisplayName = c3.DisplayName,
                            SeoUrl = (c1.SeoUrl ?? c1.Name) + "/" + (c2.SeoUrl ?? c2.Name) + "/" + (c3.SeoUrl ?? c3.Name),
                            SortOrder = c3.SortOrder,
                            HasChildren = c3.Children!.Any(),
                            PrimaryImageFileName = c1.PrimaryImageFileName,
                            Children = c3.Children!
                                .Select(c4 => new MenuCategoryModel
                                {
                                    ID = c4.ID,
                                    CustomKey = c4.CustomKey,
                                    Name = c4.Name,
                                    DisplayName = c4.DisplayName,
                                    SeoUrl = (c1.SeoUrl ?? c1.Name) + "/" + (c2.SeoUrl ?? c2.Name) + "/" + (c3.SeoUrl ?? c3.Name) + "/" + (c4.SeoUrl ?? c4.Name),
                                    SortOrder = c4.SortOrder,
                                    HasChildren = c4.Children!.Any(),
                                    PrimaryImageFileName = c1.PrimaryImageFileName,
                                    Children = c4.Children!
                                        .Select(c5 => new MenuCategoryModel
                                        {
                                            ID = c5.ID,
                                            CustomKey = c5.CustomKey,
                                            Name = c5.Name,
                                            DisplayName = c5.DisplayName,
                                            SeoUrl = (c1.SeoUrl ?? c1.Name) + "/" + (c2.SeoUrl ?? c2.Name) + "/" + (c3.SeoUrl ?? c3.Name) + "/" + (c4.SeoUrl ?? c4.Name) + "/" + (c5.SeoUrl ?? c5.Name),
                                            SortOrder = c5.SortOrder,
                                            PrimaryImageFileName = c1.PrimaryImageFileName,
                                        })
                                        .OrderBy(c5 => c5.SortOrder)
                                        .ThenBy(c5 => c5.DisplayName ?? c5.Name)
                                        .ToList(),
                                })
                                .OrderBy(c4 => c4.SortOrder)
                                .ThenBy(c4 => c4.DisplayName ?? c4.Name)
                                .ToList(),
                        })
                        .OrderBy(c3 => c3.SortOrder)
                        .ThenBy(c3 => c3.DisplayName ?? c3.Name)
                        .ToList(),
                })
                .OrderBy(c2 => c2.SortOrder)
                .ThenBy(c2 => c2.DisplayName ?? c2.Name)
                .ToList(),
        };

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForMenuCategoriesThreeLevelsAsync(
            ICategorySearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetLastModifiedForMenuCategoriesThreeLevelsAsync(search, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForMenuCategoriesThreeLevelsAsync(
            ICategorySearchModel search,
            IClarityEcommerceEntities context)
        {
            var query = context.Categories
                .AsNoTracking()
                .FilterByActive(search.Active)
                .FilterIHaveAParentBasesBySearchModel(search)
                .FilterByHaveATypeSearchModel<Category, CategoryType>(search)
                .FilterIAmFilterableByMasterBrandsBySearchModel<Category, BrandCategory>(search)
                .FilterCategoriesByRequiresRoles(search.CurrentRoles?.ToList())
                .OrderBy(x => x.UpdatedDate ?? x.CreatedDate);
            if (!search.IncludeChildrenInResults)
            {
                return await query.Select(x => x.UpdatedDate ?? x.CreatedDate).FirstOrDefaultAsync().ConfigureAwait(false);
            }
            var enumerated = query.Select(ProductCategoryDateTimeSelectorWithChildrenFunc()).ToList();
            var level1 = enumerated
                .Select(x => x.UpdatedDate)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level2 = enumerated
                .SelectMany(x => x.Children!.Select(y => y.UpdatedDate))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level3 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.Select(z => z.UpdatedDate)))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level4 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.SelectMany(z => z.Children!.Select(a => a.UpdatedDate))))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level5 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.SelectMany(z => z.Children!.SelectMany(a => a.Children!.Select(b => b.UpdatedDate)))))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level6 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.SelectMany(z => z.Children!.SelectMany(a => a.Children!.Select(b => b.UpdatedDate)))))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level7 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.SelectMany(z => z.Children!.SelectMany(a => a.Children!.SelectMany(b => b.Children!.Select(c => c.UpdatedDate))))))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var max = new[] { level1, level2, level3, level4, level5, level6, level7 }.Max();
            return max == DateTime.MinValue ? null : max;
        }

        /// <inheritdoc/>
        public async Task<List<IMenuCategoryModel>> GetMenuCategoriesThreeLevelsAsync(
            ICategorySearchModel search,
            List<string?>? roles,
            string? contextProfileName)
        {
            roles ??= new();
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Categories
                .AsNoTracking()
                .AsExpandable()
                .FilterByNameableBaseSearchModel(search)
                .FilterByHaveATypeSearchModel<Category, CategoryType>(search)
                .FilterIAmFilterableByMasterBrandsBySearchModel<Category, BrandCategory>(search)
                .FilterCategoriesByIsVisible(true)
                .FilterCategoriesByIncludeInMenu(search.IncludeInMenu)
                .FilterCategoriesByHasProductsUnderAnotherCategory(
                    search.HasProductsUnderAnotherCategoryID,
                    search.HasProductsUnderAnotherCategoryKey,
                    search.HasProductsUnderAnotherCategoryName,
                    true)
                .FilterCategoriesByRequiresRoles(roles);
            var useChildAttributes = search.ChildJsonAttributes?.Count > 0;
            var useIncludeInMenu = string.IsNullOrWhiteSpace(search.Name) && !useChildAttributes;
            if (useIncludeInMenu)
            {
                query = query.Where(c => c.ParentID == null);
            }
            else if (useChildAttributes)
            {
                query = query.FilterCategoriesByChildCategoriesJsonAttributesByValues(search.ChildJsonAttributes);
            }
            else
            {
                return new();
            }
            var childrenWherePredicate = BuildCategoryChildrenPredicate(search, useIncludeInMenu, useChildAttributes);
            return (await query
                    .Select(CategoryThreeLevelsSelector(childrenWherePredicate!))
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(MapCategoryMidsToMenuCategoryModels!)
                .Select(x => RemoveHiddenFromStorefrontAttributes(x, contextProfileName))
                .Distinct()
                .OrderBy(c => c!.SortOrder)
                .ThenBy(c => c!.DisplayName ?? c.Name)!
                .ToList<IMenuCategoryModel>();
        }

        /// <inheritdoc/>
        public async Task<List<ICategoryModel?>?> GetCategoriesThreeLevelsAsync(
            ICategorySearchModel search,
            List<string?>? roles,
            string? contextProfileName)
        {
            roles ??= new();
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Categories
                .AsNoTracking()
                .AsExpandable()
                .FilterByNameableBaseSearchModel(search)
                .FilterByHaveATypeSearchModel<Category, CategoryType>(search)
                .FilterIAmFilterableByMasterBrandsBySearchModel<Category, BrandCategory>(search)
                .FilterCategoriesByIsVisible(true)
                .FilterCategoriesByIncludeInMenu(search.IncludeInMenu)
                .FilterCategoriesByHasProductsUnderAnotherCategory(
                    search.HasProductsUnderAnotherCategoryID,
                    search.HasProductsUnderAnotherCategoryKey,
                    search.HasProductsUnderAnotherCategoryName,
                    true)
                .FilterCategoriesByRequiresRoles(roles)
                .FilterCategoriesByChildCategoriesJsonAttributesByValues(search.ChildJsonAttributes);
            var useIncludeInMenu = search.IncludeInMenu.HasValue && search.IncludeInMenu.Value;
            if (useIncludeInMenu)
            {
                query = query.Where(c => c.ParentID == null);
            }
            var childrenWherePredicate = BuildCategoryChildrenPredicate(
                search,
                useIncludeInMenu,
                search.ChildJsonAttributes?.Any() == true);
            var results = (await query
                    .Select(CategoryThreeLevelsSelector(childrenWherePredicate!))
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(MapCategoryMidsToCategoryModels!)
                .Select(x => RemoveHiddenFromStorefrontAttributes(x, contextProfileName))
                .Distinct()
                .OrderBy(c => c!.SortOrder)
                .ThenBy(c => c!.DisplayName ?? c.Name)
                .ToList();
            if (!useIncludeInMenu)
            {
                return results;
            }
            results = results.Where(x => x!.IncludeInMenu).ToList();
            foreach (var result in results.Where(x => Contract.CheckNotEmpty(x?.Children)))
            {
                result!.Children = result.Children!.Where(x => x.IncludeInMenu).ToList();
                foreach (var result2 in result.Children.Where(x => Contract.CheckNotEmpty(x?.Children)))
                {
                    result2.Children = result2.Children!.Where(x => x.IncludeInMenu).ToList();
                    foreach (var result3 in result2.Children.Where(x => Contract.CheckNotEmpty(x?.Children)))
                    {
                        result3.Children = result3.Children!.Where(x => x.IncludeInMenu).ToList();
                        foreach (var result4 in result3.Children.Where(x => Contract.CheckNotEmpty(x?.Children)))
                        {
                            result4.Children = result4.Children!.Where(x => x.IncludeInMenu).ToList();
                            foreach (var result5 in result4.Children.Where(x => Contract.CheckNotEmpty(x?.Children)))
                            {
                                result5.Children = result5.Children!.Where(x => x.IncludeInMenu).ToList();
                                foreach (var result6 in result5.Children.Where(x => Contract.CheckNotEmpty(x?.Children)))
                                {
                                    result6.Children = result6.Children!.Where(x => x.IncludeInMenu).ToList();
                                    foreach (var result7 in result6.Children.Where(x => Contract.CheckNotEmpty(x?.Children)))
                                    {
                                        result7.Children = null; // Unsupported
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /*
            for (var i = 0; i < results.Count; i++)
            {
                if (!results[i].IncludeInMenu)
                {
                    results.RemoveAt(i);
                    continue;
                }
                if (Contract.CheckEmpty(results[i].Children))
                {
                    i++;
                    continue;
                }
                var children1 = results[i].Children;
                for (var j = 0; j < children1.Count;)
                {
                    if (!children1[j].IncludeInMenu)
                    {
                        children1.RemoveAt(j);
                        continue;
                    }
                    if (Contract.CheckEmpty(children1[j].Children))
                    {
                        j++;
                        continue;
                    }
                    var children2 = children1[j].Children;
                    for (var k = 0; k < children2.Count;)
                    {
                        if (!children2[k].IncludeInMenu)
                        {
                            children2.RemoveAt(k);
                            continue;
                        }
                        if (Contract.CheckEmpty(children2[k].Children))
                        {
                            k++;
                            continue;
                        }
                        var children3 = children2[k].Children;
                        for (var l = 0; l < children3.Count;)
                        {
                            if (!children3[l].IncludeInMenu)
                            {
                                children3.RemoveAt(l);
                                continue;
                            }
                            if (Contract.CheckEmpty(children3[l].Children))
                            {
                                l++;
                                continue;
                            }
                            var children4 = children3[l].Children;
                            for (var m = 0; m < children4.Count;)
                            {
                                if (!children4[m].IncludeInMenu)
                                {
                                    children4.RemoveAt(m);
                                    continue;
                                }
                                if (Contract.CheckEmpty(children4[m].Children))
                                {
                                    m++;
                                    continue;
                                }
                                var children5 = children4[m].Children;
                                for (var n = 0; n < children5.Count;)
                                {
                                    if (!children5[n].IncludeInMenu)
                                    {
                                        children5.RemoveAt(n);
                                        continue;
                                    }
                                    if (Contract.CheckEmpty(children5[n].Children))
                                    {
                                        n++;
                                        continue;
                                    }
                                    var children6 = children5[n].Children;
                                    for (var o = 0; o < children6.Count;)
                                    {
                                        if (!children6[o].IncludeInMenu)
                                        {
                                            children6.RemoveAt(o);
                                            continue;
                                        }
                                        if (Contract.CheckEmpty(children6[o].Children))
                                        {
                                            o++;
                                            continue;
                                        }
                                        var children7 = children6[o].Children;
                                        for (var p = 0; p < children7.Count;)
                                        {
                                            if (!children7[p].IncludeInMenu)
                                            {
                                                children7.RemoveAt(p);
                                                continue;
                                            }
                                            if (Contract.CheckEmpty(children7[p].Children))
                                            {
                                                p++;
                                                continue;
                                            }
                                            children7[p].Children = null; // Unsupported
                                            p++;
                                        }
                                        o++;
                                    }
                                    n++;
                                }
                                m++;
                            }
                            l++;
                        }
                        k++;
                    }
                    j++;
                }
                i++;
            }
            */
            return results;
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForResultTreeAsync(
            ICategorySearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetLastModifiedForResultTreeAsync(search, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForResultTreeAsync(
            ICategorySearchModel search,
            IClarityEcommerceEntities context)
        {
            var query = context.Categories
                .AsNoTracking()
                .FilterByActive(search.Active)
                .FilterIHaveAParentBasesBySearchModel(search)
                .FilterByHaveATypeSearchModel<Category, CategoryType>(search)
                .FilterIAmFilterableByMasterBrandsBySearchModel<Category, BrandCategory>(search)
                .FilterCategoriesByRequiresRoles(search.CurrentRoles?.ToList())
                .OrderBy(x => x.UpdatedDate ?? x.CreatedDate);
            if (!search.IncludeChildrenInResults)
            {
                return await query.Select(x => x.UpdatedDate ?? x.CreatedDate).FirstOrDefaultAsync().ConfigureAwait(false);
            }
            var enumerated = query.Select(ProductCategoryDateTimeSelectorWithChildrenFunc()).ToList();
            var level1 = enumerated
                .Select(x => x.UpdatedDate)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level2 = enumerated
                .SelectMany(x => x.Children!.Select(y => y.UpdatedDate))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level3 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.Select(z => z.UpdatedDate)))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level4 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.SelectMany(z => z.Children!.Select(a => a.UpdatedDate))))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level5 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.SelectMany(z => z.Children!.SelectMany(a => a.Children!.Select(b => b.UpdatedDate)))))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level6 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.SelectMany(z => z.Children!.SelectMany(a => a.Children!.Select(b => b.UpdatedDate)))))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var level7 = enumerated
                .SelectMany(x => x.Children!.SelectMany(y => y.Children!.SelectMany(z => z.Children!.SelectMany(a => a.Children!.SelectMany(b => b.Children!.Select(c => c.UpdatedDate))))))
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();
            var max = new[] { level1, level2, level3, level4, level5, level6, level7 }.Max();
            return max == DateTime.MinValue ? null : max;
        }

        /// <inheritdoc/>
        public Task<List<IProductCategorySelectorModel>> GetCategoryTreeAsync(
            ICategorySearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Categories
                .AsNoTracking()
                .FilterByActive(search.Active)
                .FilterIHaveAParentBasesBySearchModel(search)
                .FilterByHaveATypeSearchModel<Category, CategoryType>(search)
                .FilterIAmFilterableByMasterBrandsBySearchModel<Category, BrandCategory>(search)
                .FilterCategoriesByRequiresRoles(search.CurrentRoles?.ToList())
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.DisplayName ?? x.Name);
            return search.IncludeChildrenInResults
                ? Task.FromResult(query.Select(ProductCategorySelectorWithChildrenFunc(search.ProductID, search.BrandID)).ToList())
                : Task.FromResult(query.Select(ProductCategorySelectorFunc(search.ProductID, search.BrandID)).ToList());
        }

        /// <summary>Gets attribute keys that should be hidden.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The attribute keys that should be hidden.</returns>
        private static List<string> GetAttributeKeysThatShouldBeHidden(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.GeneralAttributes
                .AsNoTracking()
                .FilterByActive(true)
                .FilterGeneralAttributesByHideFromStorefront(true)
                .Select(x => x.CustomKey!)
                .ToList();
        }

        /// <summary>Removes the hidden from storefront attributes.</summary>
        /// <param name="category">          The category.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ICategoryModel.</returns>
        private static ICategoryModel? RemoveHiddenFromStorefrontAttributes(
            ICategoryModel? category,
            string? contextProfileName)
        {
            if (category == null)
            {
                return null;
            }
            var keys = GetAttributeKeysThatShouldBeHidden(contextProfileName);
            if (keys.Any(key => category.SerializableAttributes.ContainsKey(key)
                    && !category.SerializableAttributes.TryRemove(key, out _)))
            {
                throw new SecurityException("Cannot remove attribute key");
            }
            // ReSharper disable once InvertIf
            if (category.ProductCategories?.Count > 0)
            {
                if ((
                        from productCategory in category.ProductCategories
                        from key in keys
                        where productCategory.Slave?.SerializableAttributes != null
                            && productCategory.Slave.SerializableAttributes.ContainsKey(key)
                            && !productCategory.Slave.SerializableAttributes.TryRemove(key, out _)
                        select productCategory)
                    .Any())
                {
                    throw new SecurityException("Cannot remove attribute key");
                }
            }
            // Return a clean category
            return category;
        }

        /// <summary>Removes the hidden from storefront attributes.</summary>
        /// <param name="category">The category.</param>
        /// <returns>An IMenuCategoryModel.</returns>
        private static IMenuCategoryModel? RemoveHiddenFromStorefrontAttributes(
            IMenuCategoryModel? category,
            string? contextProfileName)
        {
            return category;
        }

        /// <summary>Category three levels selector.</summary>
        /// <param name="childrenWherePredicate">The children where predicate.</param>
        /// <returns>An Expression{Func{Category,CategoryMid1}}.</returns>
        private static Expression<Func<Category?, CategoryMid1?>> CategoryThreeLevelsSelector(
            Expression<Func<Category?, bool>> childrenWherePredicate)
        {
            var getPrimaryImageFileName = CategorySQLExtensions.GetPrimaryImageFileName();
            return c1 => c1 == null ? null : new CategoryMid1
            {
                // Base Properties
                ID = c1.ID,
                CustomKey = c1.CustomKey,
                Active = c1.Active,
                CreatedDate = c1.CreatedDate,
                JsonAttributes = c1.JsonAttributes,
                ////SerializableAttributes = c.SerializableAttributes, // Can't create in these functions
                // NameableBase Properties
                Name = c1.Name,
                // Category Properties
                DisplayName = c1.DisplayName,
                IsVisible = c1.IsVisible,
                IncludeInMenu = c1.IncludeInMenu,
                SortOrder = c1.SortOrder,
                RequiresRoles = c1.RequiresRoles,
                RequiresRolesAlt = c1.RequiresRolesAlt,
                // IHaveSeoBase Properties
                SeoUrl = c1.SeoUrl,
                // IHaveATypeBase Properties
                TypeID = c1.TypeID,
                // IHaveImagesBase Properties
                PrimaryImageFileName = getPrimaryImageFileName.Invoke(c1.Images),
                // IHaveAParentBase Properties
                ParentID = c1.ParentID,
                ParentKey = c1.Parent != null ? c1.Parent.CustomKey : null,
                ParentName = c1.Parent != null ? c1.Parent.Name : null,
                ParentSeoUrl = c1.Parent != null ? c1.Parent.SeoUrl : null,
                Children = c1.Children!.AsQueryable()
                    .Where(childrenWherePredicate.Compile())
                    .Select(c2 => new CategoryMid2
                    {
                        // Base Properties
                        ID = c2.ID,
                        CustomKey = c2.CustomKey,
                        Active = c2.Active,
                        CreatedDate = c2.CreatedDate,
                        JsonAttributes = c2.JsonAttributes,
                        ////SerializableAttributes = cc.SerializableAttributes, // Can't create in these functions
                        // NameableBase Properties
                        Name = c2.Name,
                        // Category Properties
                        DisplayName = c2.DisplayName,
                        IsVisible = c2.IsVisible,
                        IncludeInMenu = c2.IncludeInMenu,
                        SortOrder = c2.SortOrder,
                        RequiresRoles = c2.RequiresRoles,
                        RequiresRolesAlt = c2.RequiresRolesAlt,
                        // IHaveSeoBase Properties
                        SeoUrl = c2.SeoUrl,
                        // IHaveATypeBase Properties
                        TypeID = c2.TypeID,
                        // IHaveImagesBase Properties
                        PrimaryImageFileName = getPrimaryImageFileName.Invoke(c2.Images),
                        // IHaveAParentBase Properties
                        ParentID = c1.ID,
                        ParentKey = c1.CustomKey,
                        ParentName = c1.Name,
                        ParentSeoUrl = c1.SeoUrl,
                        Children = c2.Children!.AsQueryable()
                            .Where(childrenWherePredicate.Compile())
                            .Select(c3 => new CategoryMid3
                            {
                                // Base Properties
                                ID = c3.ID,
                                CustomKey = c3.CustomKey,
                                Active = c3.Active,
                                CreatedDate = c3.CreatedDate,
                                JsonAttributes = c3.JsonAttributes,
                                ////SerializableAttributes = ccc.SerializableAttributes, // Can't create in these functions
                                // NameableBase Properties
                                Name = c3.Name,
                                // Category Properties
                                DisplayName = c3.DisplayName,
                                IsVisible = c3.IsVisible,
                                IncludeInMenu = c3.IncludeInMenu,
                                SortOrder = c3.SortOrder,
                                RequiresRoles = c3.RequiresRoles,
                                RequiresRolesAlt = c3.RequiresRolesAlt,
                                // IHaveSeoBase Properties
                                SeoUrl = c3.SeoUrl,
                                // IHaveATypeBase Properties
                                TypeID = c3.TypeID,
                                // IHaveImagesBase Properties
                                PrimaryImageFileName = getPrimaryImageFileName.Invoke(c3.Images),
                                // IHaveAParentBase Properties
                                ParentID = c2.ID,
                                ParentKey = c2.CustomKey,
                                ParentName = c2.Name,
                                ParentSeoUrl = c2.SeoUrl,
                                Children = c3.Children!.AsQueryable()
                                    .Where(childrenWherePredicate.Compile())
                                    .Select(c4 => new CategoryMid4
                                    {
                                        // Base Properties
                                        ID = c4.ID,
                                        CustomKey = c4.CustomKey,
                                        Active = c4.Active,
                                        CreatedDate = c4.CreatedDate,
                                        JsonAttributes = c4.JsonAttributes,
                                        ////SerializableAttributes = ccc.SerializableAttributes, // Can't create in these functions
                                        // NameableBase Properties
                                        Name = c4.Name,
                                        // Category Properties
                                        DisplayName = c4.DisplayName,
                                        IsVisible = c4.IsVisible,
                                        IncludeInMenu = c4.IncludeInMenu,
                                        SortOrder = c4.SortOrder,
                                        RequiresRoles = c4.RequiresRoles,
                                        RequiresRolesAlt = c4.RequiresRolesAlt,
                                        // IHaveSeoBase Properties
                                        SeoUrl = c4.SeoUrl,
                                        // IHaveATypeBase Properties
                                        TypeID = c4.TypeID,
                                        // IHaveImagesBase Properties
                                        PrimaryImageFileName = getPrimaryImageFileName.Invoke(c4.Images),
                                        // IHaveAParentBase Properties
                                        ParentID = c3.ID,
                                        ParentKey = c3.CustomKey,
                                        ParentName = c3.Name,
                                        ParentSeoUrl = c3.SeoUrl,
                                        Children = c4.Children!.AsQueryable()
                                            .Where(childrenWherePredicate.Compile())
                                            .Select(c5 => new CategoryMid5
                                            {
                                                // Base Properties
                                                ID = c5.ID,
                                                CustomKey = c5.CustomKey,
                                                Active = c5.Active,
                                                CreatedDate = c5.CreatedDate,
                                                JsonAttributes = c5.JsonAttributes,
                                                ////SerializableAttributes = ccc.SerializableAttributes, // Can't create in these functions
                                                // NameableBase Properties
                                                Name = c5.Name,
                                                // Category Properties
                                                DisplayName = c5.DisplayName,
                                                IsVisible = c5.IsVisible,
                                                IncludeInMenu = c5.IncludeInMenu,
                                                SortOrder = c5.SortOrder,
                                                RequiresRoles = c5.RequiresRoles,
                                                RequiresRolesAlt = c5.RequiresRolesAlt,
                                                // IHaveSeoBase Properties
                                                SeoUrl = c5.SeoUrl,
                                                // IHaveATypeBase Properties
                                                TypeID = c5.TypeID,
                                                // IHaveImagesBase Properties
                                                PrimaryImageFileName = getPrimaryImageFileName.Invoke(c5.Images),
                                                // IHaveAParentBase Properties
                                                ParentID = c4.ID,
                                                ParentKey = c4.CustomKey,
                                                ParentName = c4.Name,
                                                ParentSeoUrl = c4.SeoUrl,
                                            }),
                                    }),
                            }),
                    }),
            };
        }

        /// <summary>Builds category children predicate.</summary>
        /// <param name="search">           The search.</param>
        /// <param name="useIncludeInMenu"> true to use include in menu.</param>
        /// <param name="useChildAttribute">true to use child attribute.</param>
        /// <returns>An Expression{Func{Category,bool}}.</returns>
        private static Expression<Func<Category, bool>> BuildCategoryChildrenPredicate(
            ICategorySearchModel search,
            bool useIncludeInMenu,
            bool useChildAttribute)
        {
            var predicate = PredicateBuilder.New<Category>(true).And(c => c.Active && c.IsVisible);
            if (useIncludeInMenu)
            {
                predicate.And(c => c.IncludeInMenu);
            }
            if (useChildAttribute)
            {
                predicate.And(search.ChildJsonAttributes!.BuildJsonAttributePredicateForValues<Category>());
            }
            return predicate;
        }

        /// <summary>Product category selector with children function.</summary>
        /// <param name="productID">Identifier for the product.</param>
        /// <param name="brandID">  Identifier for the brand.</param>
        /// <returns>A Func{Category,IProductCategorySelectorModel}.</returns>
        private static Func<Category, IProductCategorySelectorModel> ProductCategorySelectorWithChildrenFunc(int? productID, int? brandID)
        {
            var useProduct = Contract.CheckValidID(productID);
            var useBrand = Contract.CheckValidID(brandID);
            return x => new ProductCategorySelectorModel
            {
                // Base Properties
                ID = x.ID,
                CustomKey = x.CustomKey,
                Active = x.Active,
                CreatedDate = x.CreatedDate,
                SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                // NameableBase Properties
                Name = x.DisplayName ?? x.Name,
                // IHaveSeoBase Properties
                SeoUrl = x.SeoUrl,
                // Category Properties
                SortOrder = x.SortOrder,
                // IHaveAParentBase Properties
                IsSelfSelected = useProduct
                    ? x.Products!.Any(y => y.Active && y.MasterID == productID!.Value)
                    : useBrand && x.Brands!.Any(y => y.Active && y.MasterID == brandID!.Value),
                IsChildSelected = false,
                HasChildren = x.Children!.Count > 0,
                Children = x.Children
                    .Where(y => y.Active)
                    .Select(ProductCategorySelectorWithChildrenFunc(productID, brandID))
                    .Cast<ProductCategorySelectorModel>()
                    .ToList(),
            };
        }

        /// <summary>Product category date time selector with children function.</summary>
        /// <returns>A Func{Category,IProductCategorySelectorModel}.</returns>
        private static Func<Category, IProductCategorySelectorModel> ProductCategoryDateTimeSelectorWithChildrenFunc()
        {
            return x => new ProductCategorySelectorModel
            {
                // Base Properties
                UpdatedDate = x.UpdatedDate ?? x.CreatedDate,
                Children = x.Children!
                    .Where(y => y.Active)
                    .Select(ProductCategoryDateTimeSelectorWithChildrenFunc())
                    .Cast<ProductCategorySelectorModel>()
                    .ToList(),
            };
        }

        /// <summary>Product category selector function.</summary>
        /// <param name="productID">Identifier for the product.</param>
        /// <param name="brandID">  Identifier for the brand.</param>
        /// <returns>A Func{Category,IProductCategorySelectorModel}.</returns>
        private static Func<Category, IProductCategorySelectorModel> ProductCategorySelectorFunc(int? productID, int? brandID)
        {
            var useProduct = Contract.CheckValidID(productID);
            var useBrand = Contract.CheckValidID(brandID);
            return x => new ProductCategorySelectorModel
            {
                // Base Properties
                ID = x.ID,
                Active = x.Active,
                CustomKey = x.CustomKey,
                CreatedDate = x.CreatedDate,
                SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                // NameableBase Properties
                Name = x.DisplayName ?? x.Name,
                // IHaveSeoBase Properties
                SeoUrl = x.SeoUrl,
                // Category Properties
                SortOrder = x.SortOrder,
                // IHaveAParentBase Properties
                IsSelfSelected = useProduct
                    ? x.Products!.Any(y => y.Active && y.MasterID == productID!.Value)
                    : useBrand && x.Brands!.Any(y => y.Active && y.MasterID == brandID!.Value),
                IsChildSelected = false,
                HasChildren = x.Children!.Count > 0,
                Children = null,
            };
        }

        [PublicAPI]
        private abstract class CategoryMidBase
        {
            public int ID { get; set; }

            public string? CustomKey { get; set; }

            public string? Name { get; set; }

            public string? DisplayName { get; set; }

            public int TypeID { get; set; }

            public bool IsVisible { get; set; }

            public bool IncludeInMenu { get; set; }

            public string? SeoUrl { get; set; }

            public int? SortOrder { get; set; }

            public bool Active { get; set; }

            public DateTime CreatedDate { get; set; }

            public string? PrimaryImageFileName { get; set; }

            public string? JsonAttributes { get; set; }

            //// public SerializableAttributesDictionary SerializableAttributes { get; set; } // Can't create in these functions

            public int? ParentID { get; set; }

            public string? ParentKey { get; set; }

            public string? ParentName { get; set; }

            public string? ParentSeoUrl { get; set; }

            public string? RequiresRoles { get; set; }

            public string? RequiresRolesAlt { get; set; }
        }

        // ReSharper disable BadDeclarationBracesLineBreaks
        private class CategoryMid1 : CategoryMidBase
        {
            public IEnumerable<CategoryMid2>? Children { get; set; }
        }

        private class CategoryMid2 : CategoryMidBase
        {
            public IEnumerable<CategoryMid3>? Children { get; set; }
        }

        private class CategoryMid3 : CategoryMidBase
        {
            public IEnumerable<CategoryMid4>? Children { get; set; }
        }

        private class CategoryMid4 : CategoryMidBase
        {
            public IEnumerable<CategoryMid5>? Children { get; set; }
        }

        private class CategoryMid5 : CategoryMidBase
        {
        }
        // ReSharper restore BadDeclarationBracesLineBreaks
    }
}
