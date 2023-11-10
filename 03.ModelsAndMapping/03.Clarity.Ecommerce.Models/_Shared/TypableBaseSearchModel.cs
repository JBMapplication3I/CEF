// <copyright file="TypableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the typable base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the typable base search.</summary>
    /// <seealso cref="DisplayableBaseSearchModel"/>
    /// <seealso cref="ITypableBaseSearchModel"/>
    public class TypableBaseSearchModel
        : DisplayableBaseSearchModel,
            ITypableBaseSearchModel,
            IAmFilterableByStoreSearchModel,
            IAmFilterableByBrandSearchModel
    {
        #region IAmFilterableByStoreSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Store ID For Search, Note: This will be overridden on data calls automatically")]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        public bool? StoreIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Store Key for objects")]
        public string? StoreKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Store Name for objects")]
        public string? StoreName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreSeoUrl), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Store SEO URL for objects")]
        public string? StoreSeoUrl { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreCountryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store within this country")]
        public int? StoreCountryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreRegionID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store within this region")]
        public int? StoreRegionID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreCity), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Match a store within this city")]
        public string? StoreCity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyCountryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any contact within this country")]
        public int? StoreAnyCountryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyRegionID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any contact within this region")]
        public int? StoreAnyRegionID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyCity), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any contact within this city")]
        public string? StoreAnyCity { get; set; }
        #endregion

        #region IAmFilterableByBrandSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Brand ID For Search, Note: This will be overridden on data calls automatically")]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? BrandIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Brand Key for objects")]
        public string? BrandKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Brand Name for objects")]
        public string? BrandName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? BrandNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? BrandNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandCategoryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a brand which uses this category")]
        public int? BrandCategoryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyDistrictID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any district")]
        public int? StoreAnyDistrictID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyZipCode), DataType = "string?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any zip code")]
        public string? StoreAnyZipCode { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyLatitude), DataType = "double?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any latitude")]
        public double? StoreAnyLatitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyLongitude), DataType = "double?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any longitude")]
        public double? StoreAnyLongitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyRadius), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any radius")]
        public int? StoreAnyRadius { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreAnyUnits), DataType = "string?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any units")]
        public Enums.LocatorUnits? StoreAnyUnits { get; set; }
        #endregion
    }
}
