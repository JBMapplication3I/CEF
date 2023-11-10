// <copyright file="CategoryCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using LinqKit;
    using Mapper;
    using Models;
    using Utilities;

    public partial class CategoryWorkflow
    {
        /// <inheritdoc/>
        public async Task<ICategoryModel?> GetWithOptionAsync(
            int id,
            bool? excludeProductCategories,
            string? contextProfileName)
        {
            Contract.RequiresValidID(id);
            if (excludeProductCategories is null or false)
            {
                return await GetAsync(id, contextProfileName).ConfigureAwait(false);
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetWithOptionInnerAsync(
                    context.Categories.FilterByID(id),
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ICategoryModel?> GetWithOptionAsync(
            string key,
            bool? excludeProductCategories,
            string? contextProfileName)
        {
            Contract.RequiresValidKey(key);
            if (excludeProductCategories is null or false)
            {
                return await GetAsync(key, contextProfileName).ConfigureAwait(false);
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetWithOptionInnerAsync(
                    context.Categories.FilterByActive(true).FilterByCustomKey(key, true),
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetLastModifiedForBySeoUrlForMetaDataResultAsync(
            string seoUrl,
            string? contextProfileName)
        {
            Contract.RequiresValidKey<InvalidOperationException>(seoUrl, "SEO URL is required");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entityID = await CheckExistsBySeoUrlAsync(seoUrl, context).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            return await context.Categories
                .AsNoTracking()
                .FilterByID(entityID)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ICategoryModel?> GetCategoryBySeoUrlForMetaDataAsync(
            string seoUrl,
            string? contextProfileName)
        {
            Contract.RequiresValidKey<InvalidOperationException>(seoUrl, "SEO URL is required");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entityID = await CheckExistsBySeoUrlAsync(seoUrl, context).ConfigureAwait(false);
            if (Contract.CheckInvalidID(entityID))
            {
                return null;
            }
            var entity = await context.Categories
                .AsNoTracking()
                .FilterByID(entityID)
                .Select(x => new
                {
                    x.ID,
                    x.SeoMetaData,
                    x.SeoPageTitle,
                    x.SeoDescription,
                    x.SeoKeywords,
                    x.SeoUrl,
                    x.Name,
                    x.Description,
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var model = RegistryLoaderWrapper.GetInstance<ICategoryModel>(contextProfileName);
            model.ID = entity.ID;
            model.Name = entity.Name;
            model.Description = entity.Description;
            model.SeoMetaData = entity.SeoMetaData;
            model.SeoPageTitle = entity.SeoPageTitle;
            model.SeoDescription = entity.SeoDescription;
            model.SeoKeywords = entity.SeoKeywords;
            model.SeoUrl = entity.SeoUrl;
            return model;
        }

        /// <inheritdoc/>
        public override async Task<(List<ICategoryModel> results, int totalPages, int totalCount)> SearchAsync(
            ICategorySearchModel search,
            bool asListing,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            (List<ICategoryModel> results, int totalPages, int totalCount) results;
            var query = context.Categories
                .AsNoTracking()
                .AsExpandable()
                .FilterCategoriesBySearchModel(search)
                .FilterCategoriesByRequiresRoles(search.CurrentRoles?.ToList())
                .FilterCategoriesByRequiresRolesAlt(search.CurrentRoles?.ToList())
                .FilterCategoriesByHasProductsUnderAnotherCategory(
                    search.HasProductsUnderAnotherCategoryID,
                    search.HasProductsUnderAnotherCategoryKey,
                    search.HasProductsUnderAnotherCategoryName,
                    true)
                .FilterCategoriesByChildCategoriesJsonAttributesByValues(search.ChildJsonAttributes)
                .ApplySorting(search.Sorts, search.Groupings, contextProfileName)
                .FilterByPaging(search.Paging, out results.totalPages, out results.totalCount);
            var getPrimaryImageFileName = CategorySQLExtensions.GetPrimaryImageFileName();
            if (asListing)
            {
                results.results = (await query
                        .Select(c => new
                        {
                            // IBase
                            c.ID,
                            c.CustomKey,
                            c.Active,
                            c.CreatedDate,
                            c.Hash,
                            c.JsonAttributes,
                            // INameableBase
                            c.Name,
                            // IHaveSeoBase
                            c.SeoUrl,
                            // IHaveImagesBase
                            PrimaryImageFileName = getPrimaryImageFileName.Invoke(c.Images),
                            // IHaveAParentBase
                            HasChildren = c.Children!.Count > 0,
                            c.ParentID,
                            Children = c.Children,
                            Parent = c.Parent == null
                                ? null
                                : new
                                {
                                    c.Parent.CustomKey,
                                    c.Parent.Name,
                                    c.Parent.SeoUrl,
                                },
                            // IHaveATypeBase
                            c.TypeID,
                            Type = c.Type == null
                                ? null
                                : new
                                {
                                    c.Type.CustomKey,
                                    c.Type.Name,
                                    c.Type.DisplayName,
                                    c.Type.SortOrder,
                                    c.Type.TranslationKey,
                                },
                            // Category
                            c.DisplayName,
                            c.SortOrder,
                            c.IsVisible,
                            c.IncludeInMenu,
                            c.RequiresRoles,
                            c.RequiresRolesAlt,
                        })
                        .ToListAsync()
                        .ConfigureAwait(false))
                    .Select(c => new CategoryModel
                    {
                        // IBase
                        ID = c.ID,
                        CustomKey = c.CustomKey,
                        Active = c.Active,
                        //CreatedDate = c.CreatedDate,
                        Hash = c.Hash,
                        SerializableAttributes = c.JsonAttributes.DeserializeAttributesDictionary(),
                        // INameableBase
                        Name = c.Name,
                        // IHaveSeoBase
                        SeoUrl = c.SeoUrl,
                        // IHaveImagesBase
                        PrimaryImageFileName = c.PrimaryImageFileName,
                        // IHaveAParentBase
                        HasChildren = c.HasChildren,
                        ParentID = c.ParentID,
                        ParentKey = c.Parent?.CustomKey,
                        ParentName = c.Parent?.Name,
                        ParentSeoUrl = c.Parent?.SeoUrl,
                        // IHaveATypeBase
                        TypeID = c.TypeID,
                        //TypeKey = c.Type?.CustomKey,
                        //TypeName = c.Type?.Name,
                        //TypeDisplayName = c.Type?.DisplayName,
                        //TypeSortOrder = c.Type?.SortOrder,
                        // Category
                        //DisplayName = c.DisplayName,
                        //SortOrder = c.SortOrder,
                        IsVisible = c.IsVisible,
                        IncludeInMenu = c.IncludeInMenu,
                        RequiresRoles = c.RequiresRoles,
                        RequiresRolesAlt = c.RequiresRolesAlt,
                        Children = c.Children == null
                            ? null
                            : c.Children!.Where(x => x.Active).Select(ch => new CategoryModel
                            {
                                // IBase
                                ID = ch.ID,
                                CustomKey = ch.CustomKey,
                                Name = ch.Name,
                                CreatedDate = ch.CreatedDate,
                            }).ToList(),
                    })
                    .ToList<ICategoryModel>();
                return results;
            }
            results.results = (await query
                    .Select(c => new
                    {
                        // IBase
                        c.ID,
                        c.CustomKey,
                        c.Active,
                        c.CreatedDate,
                        c.UpdatedDate,
                        c.Hash,
                        c.JsonAttributes,
                        // INameableBase
                        c.Name,
                        c.Description,
                        // IHaveSeoBase
                        c.SeoUrl,
                        c.SeoPageTitle,
                        c.SeoDescription,
                        c.SeoKeywords,
                        // IHaveImagesBase
                        PrimaryImageFileName = getPrimaryImageFileName.Invoke(c.Images),
                        // IHaveAParentBase
                        HasChildren = c.Children!.Count > 0,
                        c.ParentID,
                        Parent = c.Parent == null
                            ? null
                            : new
                            {
                                c.Parent.CustomKey,
                                c.Parent.Name,
                                c.Parent.SeoUrl,
                            },
                        // IHaveATypeBase
                        c.TypeID,
                        Type = c.Type == null
                            ? null
                            : new
                            {
                                c.Type.CustomKey,
                                c.Type.Name,
                                c.Type.DisplayName,
                                c.Type.SortOrder,
                                c.Type.TranslationKey,
                            },
                        // Category
                        c.DisplayName,
                        c.SortOrder,
                        c.IsVisible,
                        c.IncludeInMenu,
                        c.RequiresRoles,
                        c.RequiresRolesAlt,
                        c.HeaderContent,
                        c.SidebarContent,
                        c.FooterContent,
                        c.HandlingCharge,
                        c.MinimumOrderDollarAmount,
                        c.MinimumOrderDollarAmountAfter,
                        c.MinimumOrderDollarAmountOverrideFeeIsPercent,
                        c.MinimumOrderDollarAmountOverrideFeeWarningMessage,
                        c.MinimumOrderDollarAmountWarningMessage,
                        c.MinimumOrderQuantityAmount,
                        c.MinimumOrderQuantityAmountAfter,
                        c.MinimumOrderQuantityAmountOverrideFeeIsPercent,
                        c.MinimumOrderQuantityAmountOverrideFeeWarningMessage,
                        c.MinimumOrderQuantityAmountWarningMessage,
                        c.RestockingFeeAmount,
                        c.RestockingFeePercent,
                        // Related Objects
                        c.MinimumOrderDollarAmountBufferCategoryID,
                        c.MinimumOrderDollarAmountBufferProductID,
                        c.MinimumOrderQuantityAmountBufferCategoryID,
                        c.MinimumOrderQuantityAmountBufferProductID,
                        c.RestockingFeeAmountCurrencyID,
                        RestockingFeeAmountCurrencyKey = c.RestockingFeeAmountCurrency != null ? c.RestockingFeeAmountCurrency.CustomKey : null,
                        RestockingFeeAmountCurrencyName = c.RestockingFeeAmountCurrency != null ? c.RestockingFeeAmountCurrency.Name : null,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(c => new CategoryModel
                {
                    // IBase
                    ID = c.ID,
                    CustomKey = c.CustomKey,
                    Active = c.Active,
                    //CreatedDate = c.CreatedDate,
                    //UpdatedDate = c.UpdatedDate,
                    Hash = c.Hash,
                    SerializableAttributes = c.JsonAttributes.DeserializeAttributesDictionary(),
                    // INameableBase
                    Name = c.Name,
                    //Description = c.Description,
                    // IHaveSeoBase
                    SeoUrl = c.SeoUrl,
                    //SeoPageTitle = c.SeoPageTitle,
                    //SeoDescription = c.SeoDescription,
                    //SeoKeywords = c.SeoKeywords,
                    // IHaveImagesBase
                    PrimaryImageFileName = c.PrimaryImageFileName,
                    // IHaveAParentBase
                    HasChildren = c.HasChildren,
                    ParentID = c.ParentID,
                    ParentKey = c.Parent?.CustomKey,
                    ParentName = c.Parent?.Name,
                    ParentSeoUrl = c.Parent?.SeoUrl,
                    // IHaveATypeBase
                    TypeID = c.TypeID,
                    //TypeKey = c.Type?.CustomKey,
                    //TypeName = c.Type?.Name,
                    //TypeDisplayName = c.Type?.DisplayName,
                    //TypeSortOrder = c.Type?.SortOrder,
                    // Category
                    //DisplayName = c.DisplayName,
                    SortOrder = c.SortOrder,
                    IsVisible = c.IsVisible,
                    IncludeInMenu = c.IncludeInMenu,
                    RequiresRoles = c.RequiresRoles,
                    RequiresRolesAlt = c.RequiresRolesAlt,
                    HeaderContent = c.HeaderContent,
                    SidebarContent = c.SidebarContent,
                    FooterContent = c.FooterContent,
                    HandlingCharge = c.HandlingCharge,
                    MinimumOrderDollarAmount = c.MinimumOrderDollarAmount,
                    MinimumOrderDollarAmountAfter = c.MinimumOrderDollarAmountAfter,
                    MinimumOrderDollarAmountOverrideFeeIsPercent = c.MinimumOrderDollarAmountOverrideFeeIsPercent,
                    MinimumOrderDollarAmountOverrideFeeWarningMessage = c.MinimumOrderDollarAmountOverrideFeeWarningMessage,
                    MinimumOrderDollarAmountWarningMessage = c.MinimumOrderDollarAmountWarningMessage,
                    MinimumOrderQuantityAmount = c.MinimumOrderQuantityAmount,
                    MinimumOrderQuantityAmountAfter = c.MinimumOrderQuantityAmountAfter,
                    MinimumOrderQuantityAmountOverrideFeeIsPercent = c.MinimumOrderQuantityAmountOverrideFeeIsPercent,
                    MinimumOrderQuantityAmountOverrideFeeWarningMessage = c.MinimumOrderQuantityAmountOverrideFeeWarningMessage,
                    MinimumOrderQuantityAmountWarningMessage = c.MinimumOrderQuantityAmountWarningMessage,
                    RestockingFeeAmount = c.RestockingFeeAmount,
                    RestockingFeePercent = c.RestockingFeePercent,
                    // Related Objects
                    MinimumOrderDollarAmountBufferCategoryID = c.MinimumOrderDollarAmountBufferCategoryID,
                    MinimumOrderDollarAmountBufferProductID = c.MinimumOrderDollarAmountBufferProductID,
                    MinimumOrderQuantityAmountBufferCategoryID = c.MinimumOrderQuantityAmountBufferCategoryID,
                    MinimumOrderQuantityAmountBufferProductID = c.MinimumOrderQuantityAmountBufferProductID,
                    RestockingFeeAmountCurrencyID = c.RestockingFeeAmountCurrencyID,
                    RestockingFeeAmountCurrencyKey = c.RestockingFeeAmountCurrencyKey,
                    RestockingFeeAmountCurrencyName = c.RestockingFeeAmountCurrencyName,
                })
                .ToList<ICategoryModel>();
            return results;
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<ICategoryModel>> SearchForConnectAsync(
            ICategorySearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult<IEnumerable<ICategoryModel>>(
                context.Categories
                    .AsNoTracking()
                    .AsExpandable()
                    .FilterCategoriesBySearchModel(search)
                    .FilterCategoriesByRequiresRoles(search.CurrentRoles?.ToList())
                    .FilterCategoriesByRequiresRolesAlt(search.CurrentRoles?.ToList())
                    .FilterCategoriesByHasProductsUnderAnotherCategory(
                        search.HasProductsUnderAnotherCategoryID,
                        search.HasProductsUnderAnotherCategoryKey,
                        search.HasProductsUnderAnotherCategoryName,
                        true)
                    .FilterCategoriesByChildCategoriesJsonAttributesByValues(search.ChildJsonAttributes)
                    .OrderCategoriesByDefault()
                    .SelectForConnect(contextProfileName)
                    .ToList());
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ICategory entity,
            ICategoryModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            if (Contract.CheckValidID(model.ParentID)
                && !(await CheckExistsAsync(model.ParentID!.Value, context).ConfigureAwait(false)).HasValue)
            {
                throw new ArgumentException("If ParentID is set, it must match a record in the database");
            }
            if (model.ParentID == 0)
            {
                model.ParentID = null;
            }
            int? parentID = null;
            if (!string.IsNullOrWhiteSpace(model.ParentKey)
                && !(parentID = await CheckExistsAsync(model.ParentKey!, context).ConfigureAwait(false)).HasValue)
            {
                parentID = (await CreateAsync(
                            new CategoryModel
                            {
                                Active = true,
                                CustomKey = model.ParentKey,
                                Name = model.ParentKey,
                                SeoUrl = model.ParentKey!.ToSeoUrl(),
                                TypeID = 1,
                            },
                            context)
                        .ConfigureAwait(false))
                    .Result;
            }
            entity.ParentID = model.ParentID ?? parentID;
            if (model.Parent != null)
            {
                await RelateParentAsync(entity, model, context.ContextProfileName).ConfigureAwait(false);
            }
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            // NOTE: Must run after relates as the category data refreshes from the db when assigning the relates, which could be to itself
            entity.UpdateCategoryFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Category? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse, InvertIf
            if (context.CategoryImages is not null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.CategoryImages.Count(x => x.MasterID == entity.ID);)
                {
                    context.CategoryImages!.Remove(context.CategoryImages.First(x => x.MasterID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Gets with option inner.</summary>
        /// <param name="query">The query.</param>
        /// <returns>The with option inner.</returns>
        private static async Task<ICategoryModel?> GetWithOptionInnerAsync(
            IQueryable<Category> query,
            string? contextProfileName)
        {
            var getPrimaryImageFileName = CategorySQLExtensions.GetPrimaryImageFileName();
            return (await query
                    .AsExpandable()
                    .Select(x => new
                    {
                        // IBase
                        x.ID,
                        x.CustomKey,
                        x.Active,
                        x.CreatedDate,
                        x.UpdatedDate,
                        x.Hash,
                        x.JsonAttributes,
                        // INameableBase
                        x.Name,
                        x.Description,
                        // HaveSeoBase
                        x.SeoUrl,
                        x.SeoKeywords,
                        x.SeoMetaData,
                        x.SeoDescription,
                        x.SeoPageTitle,
                        // IHaveAParentBase
                        x.ParentID,
                        HasChildren = x.Children!.Count > 0,
                        // IHaveATypeBase
                        x.TypeID,
                        x.Type,
                        // IAmFilterableByBrand
                        Brands = x.Brands!.Where(y => y.Active),
                        // IHaveRequiresRolesBase
                        x.RequiresRoles,
                        x.RequiresRolesAlt,
                        // x.RequiresRolesList, // Not Mapped
                        // IHaveReviewsBase
                        // Reviews = x.Reviews.Where(y => y.Active), // Skipped
                        // IHaveImagesBase
                        Images = x.Images!.Where(y => y.Active),
                        PrimaryImageFileName = getPrimaryImageFileName.Invoke(x.Images),
                        // IHaveStoredFilesBase
                        StoredFiles = x.StoredFiles!.Where(y => y.Active),
                        // Category
                        x.DisplayName,
                        x.SortOrder,
                        x.IsVisible,
                        x.IncludeInMenu,
                        x.HeaderContent,
                        x.SidebarContent,
                        x.FooterContent,
                        x.HandlingCharge,
                        x.RestockingFeePercent,
                        x.RestockingFeeAmount,
                        // Related Objects
                        // Skipped
                        // Associated Objects
                        // Skipped
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new CategoryModel
                {
                    // IBase
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Hash = x.Hash,
                    SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                    // INameableBase
                    Name = x.Name,
                    Description = x.Description,
                    // HaveSeoBase
                    SeoUrl = x.SeoUrl,
                    SeoKeywords = x.SeoKeywords,
                    SeoMetaData = x.SeoMetaData,
                    SeoDescription = x.SeoDescription,
                    SeoPageTitle = x.SeoPageTitle,
                    // IHaveAParentBase
                    ParentID = x.ParentID,
                    HasChildren = x.HasChildren,
                    // IHaveATypeBase
                    TypeID = x.TypeID,
                    Type = (TypeModel?)x.Type.CreateCategoryTypeModelFromEntityLite(contextProfileName),
                    // IAmFilterableByBrand
                    Brands = x.Brands.Select(y => y.CreateBrandCategoryModelFromEntityList(contextProfileName)).Cast<BrandCategoryModel>().ToList(),
                    // IHaveRequiresRolesBase
                    RequiresRoles = x.RequiresRoles,
                    RequiresRolesAlt = x.RequiresRolesAlt,
                    // x.RequiresRolesList, // Not Mapped
                    // IHaveReviewsBase
                    // Reviews = x.Reviews, Skipped
                    // IHaveImagesBase
                    Images = x.Images.Select(y => y.CreateCategoryImageModelFromEntityList(contextProfileName)).Cast<CategoryImageModel>().ToList(),
                    PrimaryImageFileName = x.PrimaryImageFileName,
                    // IHaveStoredFilesBase
                    StoredFiles = x.StoredFiles.Select(y => y.CreateCategoryFileModelFromEntityList(contextProfileName)).Cast<CategoryFileModel>().ToList(),
                    // Category
                    DisplayName = x.DisplayName,
                    SortOrder = x.SortOrder,
                    IsVisible = x.IsVisible,
                    IncludeInMenu = x.IncludeInMenu,
                    HeaderContent = x.HeaderContent,
                    SidebarContent = x.SidebarContent,
                    FooterContent = x.FooterContent,
                    HandlingCharge = x.HandlingCharge,
                    RestockingFeePercent = x.RestockingFeePercent,
                    RestockingFeeAmount = x.RestockingFeeAmount,
                    // Related Objects
                    // Skipped
                    // Associated Objects
                    // Skipped
                })
                .FirstOrDefault();
        }

        /// <summary>Relate parent.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task RelateParentAsync(ICategory entity, ICategoryModel model, string? contextProfileName)
        {
            if (model.Parent == null)
            {
                return;
            }
            var parentID = model.Parent.ParentID;
            if (parentID != null || string.IsNullOrEmpty(model.Parent.CustomKey))
            {
                return;
            }
            var cat = await GetAsync((model.ParentKey ?? model.Parent.CustomKey)!, contextProfileName).ConfigureAwait(false);
            if (cat != null)
            {
                entity.ParentID = cat.ID;
            }
        }
    }
}
