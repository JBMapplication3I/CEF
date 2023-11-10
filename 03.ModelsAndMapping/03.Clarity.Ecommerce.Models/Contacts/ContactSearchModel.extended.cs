// <copyright file="ContactSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contact search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data model for the contact search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="IContactSearchModel"/>
    public class ContactSearchModel : NameableBaseSearchModel, IContactSearchModel
    {
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
        public int? AddressID { get; set; }

        /// <inheritdoc/>
        public bool? AddressIDIncludeNull { get; set; }

        /// <inheritdoc/>
        public string? Email1 { get; set; }

        /// <inheritdoc/>
        public bool? Email1Strict { get; set; }

        /// <inheritdoc/>
        public bool? Email1IncludeNull { get; set; }

        /// <inheritdoc/>
        public string? Fax1 { get; set; }

        /// <inheritdoc/>
        public bool? Fax1Strict { get; set; }

        /// <inheritdoc/>
        public bool? Fax1IncludeNull { get; set; }

        /// <inheritdoc/>
        public string? FirstName { get; set; }

        /// <inheritdoc/>
        public bool? FirstNameStrict { get; set; }

        /// <inheritdoc/>
        public bool? FirstNameIncludeNull { get; set; }

        /// <inheritdoc/>
        public string? FullName { get; set; }

        /// <inheritdoc/>
        public bool? FullNameStrict { get; set; }

        /// <inheritdoc/>
        public bool? FullNameIncludeNull { get; set; }

        /// <inheritdoc/>
        public string? LastName { get; set; }

        /// <inheritdoc/>
        public bool? LastNameStrict { get; set; }

        /// <inheritdoc/>
        public bool? LastNameIncludeNull { get; set; }

        /// <inheritdoc/>
        public string? MiddleName { get; set; }

        /// <inheritdoc/>
        public bool? MiddleNameStrict { get; set; }

        /// <inheritdoc/>
        public bool? MiddleNameIncludeNull { get; set; }

        /// <inheritdoc/>
        public string? Phone1 { get; set; }

        /// <inheritdoc/>
        public bool? Phone1Strict { get; set; }

        /// <inheritdoc/>
        public bool? Phone1IncludeNull { get; set; }

        /// <inheritdoc/>
        public string? Phone2 { get; set; }

        /// <inheritdoc/>
        public bool? Phone2Strict { get; set; }

        /// <inheritdoc/>
        public bool? Phone2IncludeNull { get; set; }

        /// <inheritdoc/>
        public string? Phone3 { get; set; }

        /// <inheritdoc/>
        public bool? Phone3Strict { get; set; }

        /// <inheritdoc/>
        public bool? Phone3IncludeNull { get; set; }

        /// <inheritdoc/>
        public string? Website1 { get; set; }

        /// <inheritdoc/>
        public bool? Website1Strict { get; set; }

        /// <inheritdoc/>
        public bool? Website1IncludeNull { get; set; }
    }
}
