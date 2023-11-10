// <autogenerated>
// <copyright file="InventoryLocationSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Inventory Location search.</summary>
    public partial class InventoryLocationSearchModel
        : NameableBaseSearchModel
        , IInventoryLocationSearchModel
    {
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
        #region IAmFilterableByFranchiseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(FranchiseID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Franchise ID For Search, Note: This will be overridden on data calls automatically")]
        public int? FranchiseID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FranchiseIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? FranchiseIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FranchiseKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Franchise Key for objects")]
        public string? FranchiseKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FranchiseName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Franchise Name for objects")]
        public string? FranchiseName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FranchiseNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? FranchiseNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FranchiseNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? FranchiseNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FranchiseCategoryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a Franchise which uses this category")]
        public int? FranchiseCategoryID { get; set; }
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
        #region IHaveAContactBase
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Contact ID for search")]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Key for search")]
        public string? ContactKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactKeyStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactKeyStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactKeyIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactKeyIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFirstName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact First Name for search")]
        public string? ContactFirstName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFirstNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactFirstNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFirstNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactFirstNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactLastName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Last Name for search")]
        public string? ContactLastName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactLastNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactLastNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactLastNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactLastNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactPhone), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Phone for search")]
        public string? ContactPhone { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactPhoneStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactPhoneStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactPhoneIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactPhoneIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFax), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Fax for search")]
        public string? ContactFax { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFaxStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactFaxStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactFaxIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactFaxIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactEmail), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Contact Email for search")]
        public string? ContactEmail { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactEmailStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactEmailStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ContactEmailIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? ContactEmailIncludeNull { get; set; }
        #endregion
    }
}
