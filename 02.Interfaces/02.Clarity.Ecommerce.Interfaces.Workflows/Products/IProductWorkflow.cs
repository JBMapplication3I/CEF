// <copyright file="IProductWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    public partial interface IProductWorkflow
    {
        /// <summary>Creates legacy product with key.</summary>
        /// <param name="productKey">        The product key.</param>
        /// <param name="productName">       Name of the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new legacy product with key.</returns>
        Task<int?> CreateLegacyProductWithKeyAsync(string productKey, string? productName, string? contextProfileName);

        /// <summary>Has children for tree view.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Dictionary{int,bool}.</returns>
        Task<Dictionary<int, bool>> HasChildrenForTreeViewAsync(int id, string? contextProfileName);

        /// <summary>Gets children images for tree view.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The children images for tree view.</returns>
        Task<Dictionary<int, List<IProductImageModel>>> GetChildrenImagesForTreeViewAsync(
            int id,
            string? contextProfileName);

        /// <summary>Check product in my store.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="currentUser">       The current user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> CheckProductInMyStoreAsync(int productID, IUserModel currentUser, string? contextProfileName);

        /// <summary>Product update notifications.</summary>
        /// <param name="days">                 The days.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="categorySeoUrl">       SEO URL of the category.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ProductUpdateNotificationsAsync(
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName);

        #region Read
        /// <summary>Gets a full.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The full record.</returns>
        Task<IProductModel?> GetFullAsync(int id, string? contextProfileName);

        /// <summary>Gets a full.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The full record.</returns>
        Task<IProductModel?> GetFullAsync(string key, string? contextProfileName);

        /// <summary>Gets the record.</summary>
        /// <param name="id">                 The identifier.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="isVendorAdmin">      True if this  is vendor admin.</param>
        /// <param name="vendorAdminID">      Identifier for the vendor admin.</param>
        /// <param name="previewID">          Identifier for the preview.</param>
        /// <returns>An IProductModel.</returns>
        Task<IProductModel?> GetAsync(
            int id,
            string? contextProfileName,
            bool isVendorAdmin,
            int? vendorAdminID,
            int? previewID);

        /// <summary>Gets the record.</summary>
        /// <param name="id">                 The identifier.</param>
        /// <param name="context">            The context.</param>
        /// <param name="isVendorAdmin">      True if this  is vendor admin.</param>
        /// <param name="vendorAdminID">      Identifier for the vendor admin.</param>
        /// <param name="previewID">          Identifier for the preview.</param>
        /// <returns>An IProductModel.</returns>
        Task<IProductModel?> GetAsync(
            int id,
            IClarityEcommerceEntities context,
            bool isVendorAdmin,
            int? vendorAdminID,
            int? previewID);

        /// <summary>Gets the record.</summary>
        /// <param name="key">                The key.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="isVendorAdmin">      True if this  is vendor admin.</param>
        /// <param name="vendorAdminID">      Identifier for the vendor admin.</param>
        /// <returns>An IProductModel.</returns>
        Task<IProductModel?> GetAsync(
            string key,
            string? contextProfileName,
            bool isVendorAdmin,
            int? vendorAdminID);

        /// <summary>Gets the record.</summary>
        /// <param name="key">                The key.</param>
        /// <param name="context">            The context.</param>
        /// <param name="isVendorAdmin">      True if this  is vendor admin.</param>
        /// <param name="vendorAdminID">      Identifier for the vendor admin.</param>
        /// <returns>An IProductModel.</returns>
        Task<IProductModel?> GetAsync(
            string key,
            IClarityEcommerceEntities context,
            bool isVendorAdmin,
            int? vendorAdminID);

        /// <summary>Gets by name.</summary>
        /// <param name="name">                 The name.</param>
        /// <param name="context">              The context.</param>
        /// <param name="isVendorAdmin">        True if this  is vendor admin.</param>
        /// <param name="vendorAdminID">        Identifier for the vendor admin.</param>
        /// <returns>The by name.</returns>
        Task<IProductModel?> GetByNameAsync(
            string name,
            IClarityEcommerceEntities context,
            bool isVendorAdmin,
            int? vendorAdminID);

        /// <summary>Gets product by SEO URL.</summary>
        /// <param name="seoUrl">             The SEO URL.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <param name="isVendorAdmin">      True if this  is vendor admin.</param>
        /// <param name="vendorAdminID">      Identifier for the vendor admin.</param>
        /// <returns>The product by SEO URL.</returns>
        Task<IProductModel?> GetProductBySeoUrlAsync(
            string seoUrl,
            string? contextProfileName,
            bool isVendorAdmin,
            int? vendorAdminID);

        /// <summary>Gets the last modified for by seo URL for meta data result.</summary>
        /// <param name="seoUrl">            URL of the seo.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for by seo URL for meta data result.</returns>
        Task<DateTime?> GetLastModifiedForBySeoUrlForMetaDataResultAsync(string seoUrl, string? contextProfileName);

        /// <summary>Gets product by seo URL for meta data.</summary>
        /// <param name="seoUrl">            URL of the seo.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product by seo URL for meta data.</returns>
        Task<IProductModel?> GetProductBySeoUrlForMetaDataAsync(string seoUrl, string? contextProfileName);

        /// <summary>Gets by manufacturer number.</summary>
        /// <param name="manufacturerNumber">The manufacturer number.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by manufacturer number.</returns>
        Task<IProductModel?> GetByManufacturerNumberAsync(string manufacturerNumber, string? contextProfileName);

        /// <summary>Removes the hidden from storefront attributes.</summary>
        /// <param name="item">              The item.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IProductModel.</returns>
        Task<IProductModel?> RemoveHiddenFromStorefrontAttributesAsync(IProductModel item, string? contextProfileName);

        /// <summary>Removes the hidden from storefront attributes.</summary>
        /// <param name="item">   The item.</param>
        /// <param name="context">The context.</param>
        /// <returns>An IProductModel.</returns>
        Task<IProductModel?> RemoveHiddenFromStorefrontAttributesAsync(
            IProductModel item,
            IClarityEcommerceEntities context);

        /// <summary>Searches for the first recently viewed products.</summary>
        /// <param name="productIDs">           The product IDs.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The found recently viewed products.</returns>
        Task<List<IProductModel>> SearchRecentlyViewedProductsAsync(
            List<int> productIDs,
            string? contextProfileName);

        /// <summary>Searches for the first previously ordered.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="asListing">         True to as listing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The found previously ordered.</returns>
        Task<(List<IProductModel> results, int totalPages, int totalCount)> SearchPreviouslyOrderedAsync(
            IProductSearchModel search,
            bool asListing,
            string? contextProfileName);

        /// <summary>Gets latest products last modified.</summary>
        /// <param name="days">              The days.</param>
        /// <param name="categorySeoUrl">    SEO URL of the category.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The latest products last modified.</returns>
        Task<DateTime?> GetLatestProductsLastModifiedAsync(
            int days,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets latest products.</summary>
        /// <param name="count">              Number of.</param>
        /// <param name="days">               The days.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="categorySeoUrl">     SEO URL of the category.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <returns>The latest products.</returns>
        Task<List<IProductModel>> GetLatestProductsAsync(
            int count,
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets updated products last modified.</summary>
        /// <param name="days">              The days.</param>
        /// <param name="categorySeoUrl">    SEO URL of the category.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The updated products last modified.</returns>
        Task<DateTime?> GetUpdatedProductsLastModifiedAsync(
            int days,
            string categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets updated products.</summary>
        /// <param name="days">               The days.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="categorySeoUrl">     SEO URL of the category.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <returns>The updated products.</returns>
        Task<List<IProductModel>> GetUpdatedProductsAsync(
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets customers favorite products last modified.</summary>
        /// <param name="days">              The days.</param>
        /// <param name="categorySeoUrl">    SEO URL of the category.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The customers favorite products last modified.</returns>
        Task<DateTime?> GetCustomersFavoriteProductsLastModifiedAsync(
            int days,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets customers favorite products.</summary>
        /// <param name="count">              Number of.</param>
        /// <param name="days">               The days.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="categorySeoUrl">     SEO URL of the category.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <returns>The customers favorite products.</returns>
        Task<List<IProductModel>> GetCustomersFavoriteProductsAsync(
            int count,
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets best selling products last modified.</summary>
        /// <param name="days">              The days.</param>
        /// <param name="categorySeoUrl">    SEO URL of the category.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The best selling products last modified.</returns>
        Task<DateTime?> GetBestSellingProductsLastModifiedAsync(
            int days,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets best selling products.</summary>
        /// <param name="count">              Number of.</param>
        /// <param name="days">               The days.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="categorySeoUrl">     SEO URL of the category.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <returns>The best selling products.</returns>
        Task<List<IProductModel>> GetBestSellingProductsAsync(
            int count,
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets trending products last modified.</summary>
        /// <param name="days">              The days.</param>
        /// <param name="categorySeoUrl">    SEO URL of the category.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The trending products last modified.</returns>
        Task<DateTime?> GetTrendingProductsLastModifiedAsync(
            int days,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets trending products.</summary>
        /// <param name="count">              Number of.</param>
        /// <param name="days">               The days.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="categorySeoUrl">     SEO URL of the category.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <returns>The trending products.</returns>
        Task<List<IProductModel>> GetTrendingProductsAsync(
            int count,
            int days,
            IPricingFactoryContextModel pricingFactoryContext,
            string? categorySeoUrl,
            string? contextProfileName);

        /// <summary>Gets all active as listing.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>all active as listing.</returns>
        Task<List<IProductModel>> GetAllActiveAsListingAsync(string? contextProfileName);

        /// <summary>Gets products by category.</summary>
        /// <param name="productTypeIDs">     The product type IDs.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <returns>The products by category.</returns>
        Task<IQuickOrderFormProductsModel?> GetProductsByCategoryAsync(
            List<int>? productTypeIDs,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Gets key words.</summary>
        /// <param name="term">              The term.</param>
        /// <param name="types">             The types.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The key words.</returns>
        Task<List<string>> GetKeyWordsAsync(string? term, List<string>? types, string? contextProfileName);

        /// <summary>Gets product review information last modified.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product review information last modified.</returns>
        Task<DateTime?> GetProductReviewInformationLastModifiedAsync(
            int productID,
            string? contextProfileName);

        /// <summary>Gets product review information.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product review information.</returns>
        Task<IProductReviewInformationModel> GetProductReviewInformationAsync(
            int productID,
            string? contextProfileName);

        /// <summary>Creates product review.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> CreateProductReviewAsync(IReviewModel request, string? contextProfileName);

        /// <summary>Gets inventory location history.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The inventory location history.</returns>
        Task<List<IProductInventoryLocationSectionModel>> GetInventoryLocationHistoryAsync(
            int id,
            string? contextProfileName);

        /// <summary>Gets product metadata by SEO URL.</summary>
        /// <param name="seoUrl">            URL of the seo.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product metadata by SEO URL.</returns>
        Task<SerializableAttributesDictionary> GetProductMetadataBySeoUrlAsync(
            string seoUrl,
            string? contextProfileName);

        /// <summary>Gets product metadata by identifier.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product metadata by identifier.</returns>
        Task<SerializableAttributesDictionary> GetProductMetadataByIDAsync(
            int id,
            string? contextProfileName);

        /// <summary>Gets primary image last modified.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The primary image last modified.</returns>
        Task<DateTime?> GetPrimaryImageLastModifiedAsync(int productID, string? contextProfileName);

        /// <summary>Gets primary image.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The primary image.</returns>
        Task<IProductImageModel?> GetPrimaryImageAsync(int productID, string? contextProfileName);
        #endregion

        #region Special
        /// <summary>Export to excel.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A DataSet.</returns>
        Task<DataSet> ExportToExcelAsync(string? contextProfileName);

        /// <summary>Generates a product site map content.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The product site map content.</returns>
        Task<string> GenerateProductSiteMapContentAsync(string? contextProfileName);

        /// <summary>Saves a product site map.</summary>
        /// <param name="siteMapContent">The site map content.</param>
        /// <param name="dropPath">      Full pathname of the drop folder.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> SaveProductSiteMapAsync(string siteMapContent, string dropPath);

        /// <summary>Gets personalization products for user last modified.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The personalization products for user last modified.</returns>
        Task<DateTime?> GetPersonalizationProductsForUserLastModifiedAsync(
            int? userID,
            string? contextProfileName);

        /// <summary>Gets personalization products for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The personalization products for user.</returns>
        Task<List<IProductModel?>> GetPersonalizationProductsForUserAsync(
            int? userID,
            string? contextProfileName);

        /// <summary>Gets personalized categories for user last modified.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The personalized categories for user last modified.</returns>
        Task<DateTime?> GetPersonalizedCategoriesForUserLastModifiedAsync(
            int? userID,
            string? contextProfileName);

        /// <summary>Gets personalized categories for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The personalized categories for user.</returns>
        Task<List<ICategoryModel>> GetPersonalizedCategoriesForUserAsync(
            int? userID,
            string? contextProfileName);

        /// <summary>Gets personalized category and product feed for user identifier last modified.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The personalized category and product feed for user identifier last modified.</returns>
        Task<DateTime?> GetPersonalizedCategoryAndProductFeedForUserIDLastModifiedAsync(
            int? userID,
            string? contextProfileName);

        /// <summary>Gets personalized category and product feed for user identifier.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The personalized category and product feed for user identifier.</returns>
        Task<Dictionary<ICategoryModel, List<IProductModel?>>> GetPersonalizedCategoryAndProductFeedForUserIDAsync(
            int? userID,
            string? contextProfileName);

        /// <summary>Searches for SKU restrictions in JsonAttributes, returns true if a restriction is found.</summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="state">     User State location.</param>
        /// <param name="city">      User City location.</param>
        /// <returns>True if shipping restricted, false if not.</returns>
        Task<bool> IsShippingRestrictedAsync(
            SerializableAttributesDictionary? attributes,
            string? state,
            string? city);
        #endregion

        #region Raw Prices
        /// <summary>Gets raw prices.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The raw prices.</returns>
        Task<CEFActionResponse<IRawProductPricesModel>> GetRawPricesAsync(int id, string? contextProfileName);

        /// <summary>Updates the raw prices.</summary>
        /// <param name="rawPricesToPush">   The raw prices to push.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> UpdateRawPricesAsync(
            IRawProductPricesModel rawPricesToPush,
            string? contextProfileName);

        /// <summary>Bulk update raw prices.</summary>
        /// <param name="rawPricesToPush">   The raw prices to push.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> BulkUpdateRawPricesAsync(
            List<IRawProductPricesModel> rawPricesToPush,
            string? contextProfileName);
        #endregion

        /// <summary>Gets the last modified for by IDs result.</summary>
        /// <param name="productIDs">        The product IDs.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="isVendorAdmin">     True if this request is for a vendor admin.</param>
        /// <param name="vendorAdminID">     Identifier for the vendor admin.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for by IDs result.</returns>
        Task<DateTime?> GetLastModifiedForByIDsResultAsync(
            List<int> productIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName);

        /// <summary>Gets by IDs.</summary>
        /// <param name="productIDs">        The product IDs.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="isVendorAdmin">     True if this request is for a vendor admin.</param>
        /// <param name="vendorAdminID">     Identifier for the vendor admin.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by IDs.</returns>
        Task<List<IProductModel>> GetByIDsAsync(
            List<int> productIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName);

        /// <summary>Clean products.</summary>
        /// <param name="productIDs">The product IDs.</param>
        /// <param name="context">   The context.</param>
        /// <returns>A Task.</returns>
        Task CleanProductsAsync(int[] productIDs, IClarityEcommerceEntities context);

        /// <summary>Calculates the minimum and maximum prices of variants for a product.</summary>
        /// <param name="productID">The variant master to check prices for.</param>
        /// <param name="pricingFactoryContext">The pricing factory context.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>The minimum and maximum price of variants for the given product.</returns>
        Task<IMinMaxCalculatedPrices?> CalculatePriceRangeForVariantsAsync(
            int productID,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);
    }
}
