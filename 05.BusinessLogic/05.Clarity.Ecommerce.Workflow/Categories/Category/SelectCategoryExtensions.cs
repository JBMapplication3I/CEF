// <copyright file="SelectCategoryExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the select category extensions class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Collections.Generic;
    using System.Linq;
    using DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A select category extensions.</summary>
    public static class SelectCategoryExtensions
    {
        /// <summary>Enumerates select for connect in this collection.</summary>
        /// <param name="query">             The query to act on.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process select for connect in this collection.</returns>
        public static IEnumerable<ICategoryModel> SelectForConnect(this IQueryable<Category> query, string? contextProfileName)
        {
            Contract.RequiresNotNull(query);
            return query
                .Select(c => new CategoryModel
                {
                    // Base Properties
                    ID = c.ID,
                    Active = c.Active,
                    CustomKey = c.CustomKey,
                    // NameableBase Properties
                    Name = c.Name,
                    Description = c.Description,
                    // SEO Properties
                    SeoUrl = c.SeoUrl,
                    SeoPageTitle = c.SeoPageTitle,
                    SeoDescription = c.SeoDescription,
                    SeoKeywords = c.SeoKeywords,
                    // Category Properties
                    DisplayName = c.DisplayName,
                    SortOrder = c.SortOrder,
                    IsVisible = c.IsVisible,
                    IncludeInMenu = c.IncludeInMenu,
                    RequiresRoles = c.RequiresRoles,
                    RequiresRolesAlt = c.RequiresRolesAlt,
                    // Related Objects
                    TypeID = c.TypeID,
                    TypeKey = c.Type != null ? c.Type.CustomKey : null,
                    TypeName = c.Type != null ? c.Type.Name : null,
                    ParentID = c.ParentID,
                    ParentKey = c.Parent != null ? c.Parent.CustomKey : null,
                    ParentName = c.Parent != null ? c.Parent.Name : null,
                    ParentSeoUrl = c.Parent != null ? c.Parent.SeoUrl : null,
                    HasChildren = c.Children!.Count > 0,
                    Hash = c.Hash,
                    // Attributes
                    SerializableAttributes = c.SerializableAttributes,
                    ////SerializableAttributes = c.SerializableAttributes,
                    // Files
                    Images = c.Images!.Where(x => x.Active).Select(x => ModelMapperForCategoryImage.CreateCategoryImageModelFromEntityList(x, contextProfileName)).Cast<CategoryImageModel>().ToList(),
                    StoredFiles = c.StoredFiles!.Where(x => x.Active).Select(x => ModelMapperForCategoryFile.CreateCategoryFileModelFromEntityList(x, contextProfileName)).Cast<CategoryFileModel>().ToList(),
                })
                .ToList();
        }
    }
}
