// <copyright file="ICategoryWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICategoryWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Models;

    /// <summary>Interface for category workflow.</summary>
    public partial interface ICategoryWorkflow
    {
        /// <summary>Gets with option to exclude product categories.</summary>
        /// <param name="id">                      The identifier.</param>
        /// <param name="excludeProductCategories">Categories the exclude product belongs to.</param>
        /// <param name="contextProfileName">      Name of the context profile.</param>
        /// <returns>The with option.</returns>
        Task<ICategoryModel?> GetWithOptionAsync(
            int id,
            bool? excludeProductCategories,
            string? contextProfileName);

        /// <summary>Gets with option to exclude product categories.</summary>
        /// <param name="key">                     The key.</param>
        /// <param name="excludeProductCategories">Categories the exclude product belongs to.</param>
        /// <param name="contextProfileName">      Name of the context profile.</param>
        /// <returns>The with option.</returns>
        Task<ICategoryModel?> GetWithOptionAsync(
            string key,
            bool? excludeProductCategories,
            string? contextProfileName);

        /// <summary>Gets the last modified for by seo URL for meta data result.</summary>
        /// <param name="seoUrl">            URL of the seo.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for by seo URL for meta data result.</returns>
        Task<DateTime?> GetLastModifiedForBySeoUrlForMetaDataResultAsync(string seoUrl, string? contextProfileName);

        /// <summary>Gets category by seo URL for meta data.</summary>
        /// <param name="seoUrl">            URL of the seo.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The category by seo URL for meta data.</returns>
        Task<ICategoryModel?> GetCategoryBySeoUrlForMetaDataAsync(string seoUrl, string? contextProfileName);

        /// <summary>Gets categories three levels.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="roles">            The current user roles.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The categories three levels.</returns>
        Task<List<ICategoryModel?>?> GetCategoriesThreeLevelsAsync(
            ICategorySearchModel search,
            List<string?>? roles,
            string? contextProfileName);

        /// <summary>Gets the last modified for result tree.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for result tree.</returns>
        Task<DateTime?> GetLastModifiedForMenuCategoriesThreeLevelsAsync(
            ICategorySearchModel search,
            string? contextProfileName);

        /// <summary>Gets the last modified for result tree.</summary>
        /// <param name="search"> The search.</param>
        /// <param name="context">The context.</param>
        /// <returns>The last modified for result tree.</returns>
        Task<DateTime?> GetLastModifiedForMenuCategoriesThreeLevelsAsync(
            ICategorySearchModel search,
            IClarityEcommerceEntities context);

        /// <summary>Gets categories three levels.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="roles">            The current user roles.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The categories three levels.</returns>
        Task<List<IMenuCategoryModel>> GetMenuCategoriesThreeLevelsAsync(
            ICategorySearchModel search,
            List<string?>? roles,
            string? contextProfileName);

        /// <summary>Gets the last modified for result tree.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for result tree.</returns>
        Task<DateTime?> GetLastModifiedForResultTreeAsync(
            ICategorySearchModel search,
            string? contextProfileName);

        /// <summary>Gets the last modified for result tree.</summary>
        /// <param name="search"> The search.</param>
        /// <param name="context">The context.</param>
        /// <returns>The last modified for result tree.</returns>
        Task<DateTime?> GetLastModifiedForResultTreeAsync(
            ICategorySearchModel search,
            IClarityEcommerceEntities context);

        /// <summary>Gets category tree.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The category tree.</returns>
        Task<List<IProductCategorySelectorModel>> GetCategoryTreeAsync(
            ICategorySearchModel search,
            string? contextProfileName);

        /// <summary>Generates a category site map content.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The category site map content.</returns>
        Task<string> GenerateCategorySiteMapContentAsync(string? contextProfileName);

        /// <summary>Generates a custom site map content.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The custom site map content.</returns>
        Task<string> GenerateCustomSiteMapContentAsync(string? contextProfileName);

        /// <summary>Saves a category site map.</summary>
        /// <param name="siteMapContent">The site map content.</param>
        /// <param name="appSetting">    The application setting.</param>
        /// <param name="fileName">      Filename of the file.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> SaveCategorySiteMapAsync(
            string siteMapContent,
            string appSetting,
            string fileName = "CategorySiteMap.xml");
    }
}
