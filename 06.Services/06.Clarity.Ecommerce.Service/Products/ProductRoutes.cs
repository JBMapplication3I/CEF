// <copyright file="ProductRoutes.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product routes class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    public partial class GetProductByID
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The store the user has selected if present")]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [ApiMember(Name = nameof(BrandID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The brand the user has selected if present")]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the is vendor admin.</summary>
        /// <value>The is vendor admin.</value>
        [ApiMember(Name = nameof(IsVendorAdmin), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "A flag indicating that this is a vendor admin request. This can only be set by the server.")]
        public bool? IsVendorAdmin { get; set; }

        /// <summary>Gets or sets the identifier of the vendor admin.</summary>
        /// <value>The identifier of the vendor admin.</value>
        [ApiMember(Name = nameof(VendorAdminID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The identifier of the vendor which is logged in. This can only be set by the server.")]
        public int? VendorAdminID { get; set; }

        /// <summary>Gets or sets the identifier of the preview.</summary>
        /// <value>The identifier of the preview.</value>
        [ApiMember(Name = nameof(PreviewID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The identifier of the preview version to load.")]
        public int? PreviewID { get; set; }
    }

    public partial class GetProductByKey
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The store the user has selected if present")]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [ApiMember(Name = nameof(BrandID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The brand the user has selected if present")]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the is vendor admin.</summary>
        /// <value>The is vendor admin.</value>
        [ApiMember(Name = nameof(IsVendorAdmin), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "A flag indicating that this is a vendor admin request. This can only be set by the server.")]
        public bool? IsVendorAdmin { get; set; }

        /// <summary>Gets or sets the identifier of the vendor admin.</summary>
        /// <value>The identifier of the vendor admin.</value>
        [ApiMember(Name = nameof(VendorAdminID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The identifier of the vendor which is logged in. This can only be set by the server.")]
        public int? VendorAdminID { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Products/Product/Full/ID/{ID}", "GET",
            Summary = "Get Full Product By ID")]
    public partial class AdminGetProductFull : ImplementsIDBase, IReturn<ProductModel>
    {
    }

    [PublicAPI,
        Route("/Products/Products/ByIDs", "GET",
            Summary = "Provides the same results as calling GetProductByID multiple times with separate IDs. WARNING: You should not use this endpoint to get a large number of products, limit to a page size.")]
    public partial class GetProductsByIDs : IReturn<List<ProductModel>>
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The store the user has selected if present")]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [ApiMember(Name = nameof(BrandID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The brand the user has selected if present")]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the is vendor admin.</summary>
        /// <value>The is vendor admin.</value>
        [ApiMember(Name = nameof(IsVendorAdmin), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "A flag indicating that this is a vendor admin request. This can only be set by the server.")]
        public bool? IsVendorAdmin { get; set; }

        /// <summary>Gets or sets the identifier of the vendor admin.</summary>
        /// <value>The identifier of the vendor admin.</value>
        [ApiMember(Name = nameof(VendorAdminID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The identifier of the vendor which is logged in. This can only be set by the server.")]
        public int? VendorAdminID { get; set; }

        /// <summary>Gets or sets the product IDs.</summary>
        /// <value>The product IDs.</value>
        [ApiMember(Name = nameof(IDs), DataType = "List<int>", ParameterType = "query", IsRequired = true,
            Description = "The identifiers of products to read out")]
        // ReSharper disable once InconsistentNaming
        public List<int> IDs { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Products/Product/CheckProductInMyStore/{ID}", "GET",
            Summary = "Check if the product or product parents are in current user's store")]
    public partial class CheckProductInMyStore : ImplementsIDBase, IReturn<bool>
    {
    }

    [PublicAPI,
        Route("/Products/GetProductsByCategory", "GET", Summary = "GET Product By category")]
    public partial class GetProductsByCategory : IReturn<QuickOrderFormProductsModel>
    {
        [ApiMember(Name = nameof(ProductTypeIDs), DataType = "int[]", ParameterType = "query", IsRequired = true,
            Description = "Product type ID")]
        public List<int> ProductTypeIDs { get; set; } = null!;
    }

    [PublicAPI,
        Route("/Products/GetProductsByPreviouslyOrdered", "POST", Summary = "GET Product By category")]
    public partial class GetProductsByPreviouslyOrdered : ProductSearchModel, IReturn<PreviouslyOrderedProductPagedResults>
    {
    }

    [PublicAPI,
        Route("/Products/Product/URL", "GET",
            Summary = "Get Product By SEO URL. Includes Quantity and StoreID to affect pricing")]
    public partial class GetProductByURL : IReturn<ProductModel>
    {
        /// <summary>Gets or sets the seo url.</summary>
        /// <value>The seo url.</value>
        [ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The SEO URL of the Product to locate")]
        public string SeoUrl { get; set; } = null!;

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [ApiMember(Name = nameof(Quantity), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The store the user has selected if present. Defaults to 1")]
        public int? Quantity { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The store the user has selected if present")]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [ApiMember(Name = nameof(BrandID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The brand the user has selected if present")]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the is vendor admin.</summary>
        /// <value>The is vendor admin.</value>
        [ApiMember(Name = nameof(IsVendorAdmin), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "A flag indicating that this is a vendor admin request. This can only be set by the server.")]
        public bool? IsVendorAdmin { get; set; }

        /// <summary>Gets or sets the identifier of the vendor admin.</summary>
        /// <value>The identifier of the vendor admin.</value>
        [ApiMember(Name = nameof(VendorAdminID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The identifier of the vendor which is logged in. This can only be set by the server.")]
        public int? VendorAdminID { get; set; }
    }

    [Obsolete("Use GetProductMetadataByURL instead"),
        PublicAPI,
        Route("/Products/Product/Metadata", "GET",
            Summary = "Get Product by SEO URL for just the SEO Metadata")]
    public partial class GetProductForMetaData : IReturn<ProductModel>
    {
        /// <summary>Gets or sets the seo url.</summary>
        /// <value>The seo url.</value>
        [ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The SEO URL of the Product to locate")]
        public string SeoUrl { get; set; } = null!;
    }

    [PublicAPI,
        Route("/Products/Product/URL/Metadata", "GET",
            Summary = @"Get Product Metadata By SEO URL.")]
    public partial class GetProductMetadataByURL : IReturn<SerializableAttributesDictionary>
    {
        [ApiMember(Name = nameof(SeoUrl), DataType = "string", ParameterType = "query", IsRequired = true,
        Description = "The SEO URL to perform the lookup with")]
        public string SeoUrl { get; set; } = null!;
    }

    [PublicAPI,
        Route("/Products/BestSellers", "GET",
            Summary = "Use to get best selling products in the system")]
    public partial class GetBestSellersProducts : IReturn<List<ProductModel>>
    {
        [ApiMember(Name = nameof(Count), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "Count of Products to Pull")]
        public int Count { get; set; }

        [ApiMember(Name = nameof(Days), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "Number of days back from today to use for calculations")]
        public int Days { get; set; }

        [ApiMember(Name = nameof(CategorySeoUrl), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CategorySeoUrl { get; set; }
    }

    [PublicAPI,
        Route("/Products/Trending", "POST",
            Summary = "Use to get trending products in the system")]
    public partial class GetTrendingProducts : IReturn<List<ProductModel>>
    {
        [ApiMember(Name = nameof(Count), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "Count of Products to Pull")]
        public int Count { get; set; }

        [ApiMember(Name = nameof(Days), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "Number of days back from today to use for calculations")]
        public int Days { get; set; }

        [ApiMember(Name = nameof(CategorySeoUrl), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CategorySeoUrl { get; set; }
    }

    [PublicAPI,
        Route("/Products/CustomerFavorites", "GET",
            Summary = "Use to get most customer favorited products in the system")]
    public partial class GetCustomerFavoritesProducts : IReturn<List<ProductModel>>
    {
        [ApiMember(Name = nameof(Count), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "Count of Products to Pull")]
        public int Count { get; set; }

        [ApiMember(Name = nameof(Days), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "Number of days back from today to use for calculations")]
        public int Days { get; set; }

        [ApiMember(Name = nameof(CategorySeoUrl), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CategorySeoUrl { get; set; }
    }

    [PublicAPI,
        Route("/Products/Latest", "GET",
            Summary = "Use to get latest products in the system")]
    public partial class GetLatestProducts : IReturn<List<ProductModel>>
    {
        [ApiMember(Name = nameof(Count), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "Count of Products to Pull")]
        public int Count { get; set; }

        [ApiMember(Name = nameof(Days), DataType = "int", ParameterType = "query", IsRequired = true,
            Description = "Number of days back from today to use for calculations")]
        public int Days { get; set; }

        [ApiMember(Name = nameof(CategorySeoUrl), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CategorySeoUrl { get; set; }
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Products/ExportToExcel", "GET",
            Summary = "Use to export all products in the system")]
    public partial class GetProductsAsExcelDoc : IReturn<DownloadFileResult>
    {
    }

    [Route("/Products/Product/InventoryHistory/{ID}", "GET", Summary = "Use to get attributes")]
    public partial class GetProductInventoryHistory : ImplementsIDBase, IReturn<List<ProductInventoryLocationSectionModel>>
    {
    }

    public partial class PreviouslyOrderedProductPagedResults : PagedResultsBase<ProductModel>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Products/SiteMap", "GET", Summary = "Get the product site map without replacing it")]
    public partial class GetProductSiteMapContent : IReturn<DownloadFileResult>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Products/SiteMap/Regenerate", "POST",
            Summary = "Generates a new product site map and then replaces the existing one per the DropPath value from the web config")]
    public partial class RegenerateProductSiteMap : IReturn<bool>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Products/CurrentStore/Products", "GET", Priority = 1,
            Summary = "Use to get a list of products (for the current store only)")]
    public partial class GetProductsForCurrentStore : ProductSearchModel, IReturn<ProductPagedResults>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Products/Admin/Portal/Products", "GET", Priority = 1,
            Summary = "Use to get a list of products (for the current x-portal we can administrate only)")]
    public partial class AdminGetProductsForPortal : ProductSearchModel, IReturn<ProductPagedResults>
    {
    }

    [PublicAPI,
        Route("/Products/Personalization/ForCurrentUser", "GET")]
    public partial class GetPersonalizationProductsForCurrentUser : IReturn<List<ProductModel>>
    {
    }

    [PublicAPI,
        Route("/Categories/Personalization/ForCurrentUser", "GET")]
    public partial class GetPersonalizedCategoriesForCurrentUser : IReturn<List<CategoryModel>>
    {
    }

    [PublicAPI,
        Route("/Products/Personalization/Feed/ForCurrentUser", "GET")]
    public partial class GetPersonalizedCategoryAndProductFeedForCurrentUser : IReturn<List<KeyValuePair<CategoryModel, List<ProductModel>>>>
    {
    }

    [PublicAPI,
        Route("/Products/ProductPrimaryImage/ID/{ProductID}", "GET", Priority = 1)]
    public partial class GetPrimaryImageForProductID : ImplementsIDBase, IReturn<ProductImageModel>
    {
    }

    [PublicAPI, Route("/Products/ProductNotifications", "POST")]
    public partial class ProcessProductNotifications : IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, Route("/Products/Product/ExistsNonNull/Key/{Key*}", "GET", Priority = 1)]
    public partial class CheckProductExistsNonNullByKey : ImplementsKeyBase, IReturn<DigestModel>
    {
    }
}
