// <autogenerated>
// <copyright file="ProductAssociationSearchModel.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SearchModel Classes generated to provide base setups.</summary>
// <remarks>This file was auto-generated by SearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable MissingXmlDoc, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective, UnusedMember.Global
#nullable enable
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the Product Association search.</summary>
    public partial class ProductAssociationSearchModel
        : AmARelationshipTableBaseSearchModel
        , IProductAssociationSearchModel
    {
        #region IAmARelationshipTableBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MasterName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Name of the Master Record [Optional]")]
        public string? MasterName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SlaveName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Name of the Slave Record [Optional]")]
        public string? SlaveName { get; set; }
        #endregion
        #region IHaveATypeBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Type ID for objects")]
        public int? TypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeIDs), DataType = "int?[]", ParameterType = "query", IsRequired = false,
            Description = "The Type IDs for objects to specifically include")]
        public int?[]? TypeIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Type ID for objects to specifically exclude")]
        public int? ExcludedTypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeIDs), DataType = "int?[]", ParameterType = "query", IsRequired = false,
            Description = "The Type IDs for objects to specifically exclude")]
        public int?[]? ExcludedTypeIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Key for objects")]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKeys), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Keys for objects to specifically include")]
        public string?[]? TypeKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Key for objects to specifically exclude")]
        public string? ExcludedTypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeKeys), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Keys for objects to specifically exclude")]
        public string?[]? ExcludedTypeKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Name for objects")]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Names for objects to specifically include")]
        public string?[]? TypeNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Name for objects to specifically exclude")]
        public string? ExcludedTypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Names for objects to specifically exclude")]
        public string?[]? ExcludedTypeNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Name for objects")]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Names for objects to specifically include")]
        public string?[]? TypeDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeDisplayName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Name for objects to specifically exclude")]
        public string? ExcludedTypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeDisplayNames), DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Names for objects to specifically exclude")]
        public string?[]? ExcludedTypeDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Translation Key for objects")]
        public string? TypeTranslationKey { get; set; }
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
        #endregion
        #region IAmFilterableByStoreSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Store ID For Search, Note: This will be overridden on data calls automatically")]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
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
        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinQuantity), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MinQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxQuantity), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MaxQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchQuantity), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? MatchQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchQuantityIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchQuantityIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MinSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MinSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MaxSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchSortOrder), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? MatchSortOrder { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MatchSortOrderIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? MatchSortOrderIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UnitOfMeasure), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UnitOfMeasureStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? UnitOfMeasureStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UnitOfMeasureIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? UnitOfMeasureIncludeNull { get; set; }
    }
}
