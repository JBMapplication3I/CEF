// <copyright file="SearchFormBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search form base class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using JetBrains.Annotations;
    using ServiceStack;

    /// <summary>A search form base.</summary>
    [PublicAPI]
    public abstract class SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="SearchFormBase"/> class.</summary>
        protected SearchFormBase()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="SearchFormBase"/> class.</summary>
        /// <param name="other">The other.</param>
        protected SearchFormBase(SearchFormBase other)
        {
#pragma warning disable CA2214
            CopyFrom(other);
#pragma warning restore CA2214
        }

        #region Page data
        /// <summary>Gets or sets the page.</summary>
        /// <value>The page.</value>
        public int Page { get; set; } = 1;

        /// <summary>Gets or sets the size of the page.</summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; } = 9;

        /// <summary>Gets or sets the size of the page set.</summary>
        /// <value>The size of the page set.</value>
        public int PageSetSize { get; set; } = 5;

        /// <summary>Gets or sets the page format.</summary>
        /// <value>The page format.</value>
        public string? PageFormat { get; set; } = "grid";
        #endregion

        #region Sort Data
        /// <summary>Gets or sets the sort.</summary>
        /// <value>The sort.</value>
        public Enums.SearchSort Sort { get; set; }
        #endregion

        #region The raw query strings
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string? Name { get; set; }

        /// <summary>Gets or sets the query.</summary>
        /// <value>The query.</value>
        [ApiMember(Name = nameof(Query), DataType = "string", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string? Query { get; set; }
        #endregion

        #region Attributes
        /// <summary>Gets or sets the attributes any.</summary>
        /// <value>The attributes any.</value>
        [ApiMember(Name = nameof(AttributesAny), DataType = "Dictionary<string, string[]>", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public Dictionary<string, string[]>? AttributesAny { get; set; }

        /// <summary>Gets or sets the attributes all.</summary>
        /// <value>The attributes all.</value>
        [ApiMember(Name = nameof(AttributesAll), DataType = "Dictionary<string, string[]>", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public Dictionary<string, string[]>? AttributesAll { get; set; }
        #endregion

        #region Brands
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [ApiMember(Name = nameof(BrandID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the brand IDs any.</summary>
        /// <value>The brand IDs any.</value>
        [ApiMember(Name = nameof(BrandIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? BrandIDsAny { get; set; }

        /// <summary>Gets or sets the brand IDs all.</summary>
        /// <value>The brand IDs all.</value>
        [ApiMember(Name = nameof(BrandIDsAll), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? BrandIDsAll { get; set; }
        #endregion

        #region Categories
        /// <summary>Gets or sets the category the forced belongs to.</summary>
        /// <value>The forced category.</value>
        [ApiMember(Name = nameof(ForcedCategory), DataType = "string", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string? ForcedCategory { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        [ApiMember(Name = nameof(Category), DataType = "string", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string? Category { get; set; }

        /// <summary>Gets or sets the categories any.</summary>
        /// <value>The categories any.</value>
        [ApiMember(Name = nameof(CategoriesAny), DataType = "string[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string[]? CategoriesAny { get; set; }

        /// <summary>Gets or sets the categories all.</summary>
        /// <value>The categories all.</value>
        [ApiMember(Name = nameof(CategoriesAll), DataType = "string[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string[]? CategoriesAll { get; set; }
        #endregion

        #region Districts
        /// <summary>Gets or sets the identifier of the district.</summary>
        /// <value>The identifier of the district.</value>
        [ApiMember(Name = nameof(DistrictID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? DistrictID { get; set; }

        /// <summary>Gets or sets the district IDs any.</summary>
        /// <value>The district IDs any.</value>
        [ApiMember(Name = nameof(DistrictIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? DistrictIDsAny { get; set; }

        /// <summary>Gets or sets the district IDs all.</summary>
        /// <value>The district IDs all.</value>
        [ApiMember(Name = nameof(DistrictIDsAll), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? DistrictIDsAll { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [ApiMember(Name = nameof(City), DataType = "string", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string? City { get; set; }
        #endregion

        #region Franchises
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        [ApiMember(Name = nameof(FranchiseID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? FranchiseID { get; set; }

        /// <summary>Gets or sets the franchise IDs any.</summary>
        /// <value>The franchise IDs any.</value>
        [ApiMember(Name = nameof(FranchiseIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? FranchiseIDsAny { get; set; }

        /// <summary>Gets or sets the franchise IDs all.</summary>
        /// <value>The franchise IDs all.</value>
        [ApiMember(Name = nameof(FranchiseIDsAll), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? FranchiseIDsAll { get; set; }
        #endregion

        #region Manufacturers
        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        [ApiMember(Name = nameof(ManufacturerID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer identifiers any.</summary>
        /// <value>The manufacturer identifiers any.</value>
        [ApiMember(Name = nameof(ManufacturerIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? ManufacturerIDsAny { get; set; }

        /// <summary>Gets or sets the manufacturer identifiers all.</summary>
        /// <value>The manufacturer identifiers all.</value>
        [ApiMember(Name = nameof(ManufacturerIDsAll), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? ManufacturerIDsAll { get; set; }
        #endregion

        #region Pricing Ranges
        /// <summary>Gets or sets the pricing ranges.</summary>
        /// <value>The pricing ranges.</value>
        [ApiMember(Name = nameof(PricingRanges), DataType = "string[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string[]? PricingRanges { get; set; }
        #endregion

        #region Products
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        [ApiMember(Name = nameof(ProductID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? ProductID { get; set; }

        /// <summary>Gets or sets the product identifiers any.</summary>
        /// <value>The product identifiers any.</value>
        [ApiMember(Name = nameof(ProductIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? ProductIDsAny { get; set; }

        /// <summary>Gets or sets the product identifiers all.</summary>
        /// <value>The product identifiers all.</value>
        [ApiMember(Name = nameof(ProductIDsAll), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? ProductIDsAll { get; set; }

        /// <summary>Gets or sets a value indicating whether the products that are stocked on hand.</summary>
        /// <value>The products that are stocked on hand.</value>
        public bool OnHand { get; set; }

        /// <summary>Gets or sets the price lists of the product.</summary>
        /// <value>The price lists.</value>
        public IEnumerable<string>? PriceListsAny { get; set; }
        #endregion

        #region Ratings Ranges
        /// <summary>Gets or sets the rating ranges.</summary>
        /// <value>The rating ranges.</value>
        [ApiMember(Name = nameof(RatingRanges), DataType = "string[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string[]? RatingRanges { get; set; }
        #endregion

        #region Regions
        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        [ApiMember(Name = nameof(RegionID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? RegionID { get; set; }

        /// <summary>Gets or sets the region IDs any.</summary>
        /// <value>The region IDs any.</value>
        [ApiMember(Name = nameof(RegionIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? RegionIDsAny { get; set; }
        #endregion

        #region Roles
        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        [ApiMember(Name = nameof(Role), DataType = "string", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string? Role { get; set; }

        /// <summary>Gets or sets the roles any.</summary>
        /// <value>The roles any.</value>
        [ApiMember(Name = nameof(RolesAny), DataType = "string[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string[]? RolesAny { get; set; }

        /// <summary>Gets or sets the roles all.</summary>
        /// <value>The roles all.</value>
        [ApiMember(Name = nameof(RolesAll), DataType = "string[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public string[]? RolesAll { get; set; }
        #endregion

        #region Stores
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the store identifiers any.</summary>
        /// <value>The store identifiers any.</value>
        [ApiMember(Name = nameof(StoreIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? StoreIDsAny { get; set; }

        /// <summary>Gets or sets the store identifiers all.</summary>
        /// <value>The store identifiers all.</value>
        [ApiMember(Name = nameof(StoreIDsAll), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? StoreIDsAll { get; set; }
        #endregion

        #region Types
        /// <summary>Gets or sets the identifier of the type.</summary>
        /// <value>The identifier of the type.</value>
        [ApiMember(Name = nameof(TypeID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? TypeID { get; set; }

        /// <summary>Gets or sets the type identifiers any.</summary>
        /// <value>The type i ds any.</value>
        [ApiMember(Name = nameof(TypeIDsAny), DataType = "Dictionary<string, string[]>", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? TypeIDsAny { get; set; }
        #endregion

        #region Vendors
        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        [ApiMember(Name = nameof(VendorID), DataType = "int?", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor identifiers any.</summary>
        /// <value>The vendor identifiers any.</value>
        [ApiMember(Name = nameof(VendorIDsAny), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? VendorIDsAny { get; set; }

        /// <summary>Gets or sets the vendor identifiers all.</summary>
        /// <value>The vendor identifiers all.</value>
        [ApiMember(Name = nameof(VendorIDsAll), DataType = "int[]", ParameterType = "query", IsRequired = false),
            DefaultValue(null)]
        public int[]? VendorIDsAll { get; set; }
        #endregion

        /// <summary>Copies from described by other.</summary>
        /// <param name="other">Another instance to copy.</param>
        public virtual void CopyFrom(SearchFormBase other)
        {
            this.Page = other.Page;
            this.PageSize = other.PageSize;
            this.PageSetSize = other.PageSetSize;
            this.PageFormat = other.PageFormat;
            this.Sort = other.Sort;
            this.Query = other.Query;
            this.AttributesAny = other.AttributesAny;
            this.AttributesAll = other.AttributesAll;
            this.BrandID = other.BrandID;
            this.BrandIDsAny = other.BrandIDsAny;
            this.BrandIDsAll = other.BrandIDsAll;
            this.ForcedCategory = other.ForcedCategory;
            this.Category = other.Category;
            this.CategoriesAny = other.CategoriesAny;
            this.CategoriesAll = other.CategoriesAll;
            this.City = other.City;
            this.DistrictID = other.DistrictID;
            this.DistrictIDsAny = other.DistrictIDsAny;
            this.DistrictIDsAll = other.DistrictIDsAll;
            this.FranchiseID = other.FranchiseID;
            this.FranchiseIDsAny = other.FranchiseIDsAny;
            this.FranchiseIDsAll = other.FranchiseIDsAll;
            this.ManufacturerID = other.ManufacturerID;
            this.ManufacturerIDsAny = other.ManufacturerIDsAny;
            this.ManufacturerIDsAll = other.ManufacturerIDsAll;
            this.OnHand = other.OnHand;
            this.PricingRanges = other.PricingRanges;
            this.ProductID = other.ProductID;
            this.ProductIDsAny = other.ProductIDsAny;
            this.ProductIDsAll = other.ProductIDsAll;
            this.RatingRanges = other.RatingRanges;
            this.RegionID = other.RegionID;
            this.RegionIDsAny = other.RegionIDsAny;
            this.Role = other.Role;
            this.RolesAny = other.RolesAny;
            this.RolesAll = other.RolesAll;
            this.StoreID = other.StoreID;
            this.StoreIDsAny = other.StoreIDsAny;
            this.StoreIDsAll = other.StoreIDsAll;
            this.TypeID = other.TypeID;
            this.TypeIDsAny = other.TypeIDsAny;
            this.VendorID = other.VendorID;
            this.VendorIDsAny = other.VendorIDsAny;
            this.VendorIDsAll = other.VendorIDsAll;
        }
    }
}
