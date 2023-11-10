// <copyright file="SalesCollectionBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    public partial class SalesCollectionBaseSearchModel : BaseSearchModel
    {
        [ApiMember(Name = "TypeID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Type ID for objects")]
        public int? TypeID { get; set; }

        [ApiMember(Name = "TypeIDs", DataType = "int?[]", ParameterType = "query", IsRequired = false,
            Description = "The Type IDs for objects to specifically include")]
        public int?[]? TypeIDs { get; set; }

        [ApiMember(Name = "ExcludedTypeID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Type ID for objects to specifically exclude")]
        public int? ExcludedTypeID { get; set; }

        [ApiMember(Name = "ExcludedTypeIDs", DataType = "int?[]", ParameterType = "query", IsRequired = false,
            Description = "The Type IDs for objects to specifically exclude")]
        public int?[]? ExcludedTypeIDs { get; set; }

        [ApiMember(Name = "TypeKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Key for objects")]
        public string? TypeKey { get; set; }

        [ApiMember(Name = "TypeKeys", DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Keys for objects to specifically include")]
        public string[]? TypeKeys { get; set; }

        [ApiMember(Name = "ExcludedTypeKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Key for objects to specifically exclude")]
        public string? ExcludedTypeKey { get; set; }

        [ApiMember(Name = "ExcludedTypeKeys", DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Keys for objects to specifically exclude")]
        public string[]? ExcludedTypeKeys { get; set; }

        [ApiMember(Name = "TypeName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Name for objects")]
        public string? TypeName { get; set; }

        [ApiMember(Name = "TypeNames", DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Names for objects to specifically include")]
        public string[]? TypeNames { get; set; }

        [ApiMember(Name = "ExcludedTypeName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Name for objects to specifically exclude")]
        public string? ExcludedTypeName { get; set; }

        [ApiMember(Name = "ExcludedTypeNames", DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Names for objects to specifically exclude")]
        public string[]? ExcludedTypeNames { get; set; }

        [ApiMember(Name = "TypeDisplayName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Name for objects")]
        public string? TypeDisplayName { get; set; }

        [ApiMember(Name = "TypeDisplayNames", DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Names for objects to specifically include")]
        public string[]? TypeDisplayNames { get; set; }

        [ApiMember(Name = "ExcludedTypeDisplayName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Name for objects to specifically exclude")]
        public string? ExcludedTypeDisplayName { get; set; }

        [ApiMember(Name = "ExcludedTypeDisplayNames", DataType = "string[]", ParameterType = "query", IsRequired = false,
            Description = "The Type Display Names for objects to specifically exclude")]
        public string[]? ExcludedTypeDisplayNames { get; set; }

        [ApiMember(Name = "TypeTranslationKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Display Name for objects to specifically exclude")]
        public string? TypeTranslationKey { get; set; }

        [ApiMember(Name = "StatusID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Status ID for objects")]
        public int? StatusID { get; set; }

        [ApiMember(Name = "StatusIDs", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Status IDs for objects to specifically include")]
        public int?[]? StatusIDs { get; set; }

        [ApiMember(Name = "ExcludedStatusID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Status ID for objects to specifically exclude")]
        public int? ExcludedStatusID { get; set; }

        [ApiMember(Name = "ExcludedStatusIDs", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The Status IDs for objects to specifically exclude")]
        public int?[]? ExcludedStatusIDs { get; set; }

        [ApiMember(Name = "StatusKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Key for objects")]
        public string? StatusKey { get; set; }

        [ApiMember(Name = "StatusKeys", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Keys for objects to specifically include")]
        public string[]? StatusKeys { get; set; }

        [ApiMember(Name = "ExcludedStatusKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Key for objects to specifically exclude")]
        public string? ExcludedStatusKey { get; set; }

        [ApiMember(Name = "ExcludedStatusKeys", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Keys for objects to specifically exclude")]
        public string[]? ExcludedStatusKeys { get; set; }

        [ApiMember(Name = "StatusName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Name for objects")]
        public string? StatusName { get; set; }

        [ApiMember(Name = "StatusNames", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Names for objects to specifically include")]
        public string[]? StatusNames { get; set; }

        [ApiMember(Name = "ExcludedStatusName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Name for objects to specifically exclude")]
        public string? ExcludedStatusName { get; set; }

        [ApiMember(Name = "ExcludedStatusNames", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Names for objects to specifically exclude")]
        public string[]? ExcludedStatusNames { get; set; }

        [ApiMember(Name = "StatusDisplayName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Display Name for objects")]
        public string? StatusDisplayName { get; set; }

        [ApiMember(Name = "StatusDisplayNames", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Display Names for objects to specifically include")]
        public string[]? StatusDisplayNames { get; set; }

        [ApiMember(Name = "ExcludedStatusDisplayName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Display Name for objects to specifically exclude")]
        public string? ExcludedStatusDisplayName { get; set; }

        [ApiMember(Name = "ExcludedStatusDisplayNames", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Status Display Names for objects to specifically exclude")]
        public string[]? ExcludedStatusDisplayNames { get; set; }

        [ApiMember(Name = "StatusTranslationKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Display Name for objects to specifically exclude")]
        public string? StatusTranslationKey { get; set; }

        [ApiMember(Name = "StateID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The State ID for objects")]
        public int? StateID { get; set; }

        [ApiMember(Name = "StateIDs", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The State IDs for objects to specifically include")]
        public int?[]? StateIDs { get; set; }

        [ApiMember(Name = "ExcludedStateID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The State ID for objects to specifically exclude")]
        public int? ExcludedStateID { get; set; }

        [ApiMember(Name = "ExcludedStateIDs", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The State IDs for objects to specifically exclude")]
        public int?[]? ExcludedStateIDs { get; set; }

        [ApiMember(Name = "StateKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Key for objects")]
        public string? StateKey { get; set; }

        [ApiMember(Name = "StateKeys", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Keys for objects to specifically include")]
        public string[]? StateKeys { get; set; }

        [ApiMember(Name = "ExcludedStateKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Key for objects to specifically exclude")]
        public string? ExcludedStateKey { get; set; }

        [ApiMember(Name = "ExcludedStateKeys", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Keys for objects to specifically exclude")]
        public string[]? ExcludedStateKeys { get; set; }

        [ApiMember(Name = "StateName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Name for objects")]
        public string? StateName { get; set; }

        [ApiMember(Name = "StateNames", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Names for objects to specifically include")]
        public string[]? StateNames { get; set; }

        [ApiMember(Name = "ExcludedStateName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Name for objects to specifically exclude")]
        public string? ExcludedStateName { get; set; }

        [ApiMember(Name = "ExcludedStateNames", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Names for objects to specifically exclude")]
        public string[]? ExcludedStateNames { get; set; }

        [ApiMember(Name = "StateDisplayName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Display Name for objects")]
        public string? StateDisplayName { get; set; }

        [ApiMember(Name = "StateDisplayNames", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Display Names for objects to specifically include")]
        public string[]? StateDisplayNames { get; set; }

        [ApiMember(Name = "ExcludedStateDisplayName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Display Name for objects to specifically exclude")]
        public string? ExcludedStateDisplayName { get; set; }

        [ApiMember(Name = "ExcludedStateDisplayNames", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Display Names for objects to specifically exclude")]
        public string[]? ExcludedStateDisplayNames { get; set; }

        [ApiMember(Name = "StateTranslationKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The State Display Name for objects to specifically exclude")]
        public string? StateTranslationKey { get; set; }

        [ApiMember(Name = "AccountID", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Account ID For Search")]
        public int? AccountID { get; set; }

        public bool? AccountIDIncludeNull { get; set; }

        [ApiMember(Name = "AccountKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Account Key for objects")]
        public string? AccountKey { get; set; }

        [ApiMember(Name = "AccountName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Account Name for objects")]
        public string? AccountName { get; set; }

        [ApiMember(Name = "AccountNameStrict", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "")]
        public bool? AccountNameStrict { get; set; }

        [ApiMember(Name = "AccountNameIncludeNull", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "")]
        public bool? AccountNameIncludeNull { get; set; }

        [ApiMember(Name = "AccountIDOrCustomKeyOrNameOrDescription", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "General search against Account assigned to the object")]
        public string? AccountIDOrCustomKeyOrNameOrDescription { get; set; }

        [ApiMember(Name = "UserID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "User ID assigned to the object")]
        public int? UserID { get; set; }

        public bool? UserIDIncludeNull { get; set; }

        [ApiMember(Name = "UserIDOrCustomKeyOrUserName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "General search against User assigned to the object (includes UserName even though the name of the property doesn't say it)")]
        public string? UserIDOrCustomKeyOrUserName { get; set; }

        [ApiMember(Name = "UserExternalID", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Username of the User assigned to the object")]
        public string? UserExternalID { get; set; }

        [ApiMember(Name = "UserKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "")]
        public string? UserKey { get; set; }

        [ApiMember(Name = "UserUsername", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "")]
        public string? UserUsername { get; set; }

        [ApiMember(Name = "BrandID", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Brand ID For Search, Note: This will be overridden on data calls automatically")]
        public int? BrandID { get; set; }

        public bool? BrandIDIncludeNull { get; set; }

        [ApiMember(Name = "BrandKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Brand Key for objects")]
        public string? BrandKey { get; set; }

        [ApiMember(Name = "BrandName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Brand Name for objects")]
        public string? BrandName { get; set; }

        [ApiMember(Name = "BrandNameStrict", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "")]
        public bool? BrandNameStrict { get; set; }

        [ApiMember(Name = "BrandNameIncludeNull", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "")]
        public bool? BrandNameIncludeNull { get; set; }

        [ApiMember(Name = "BrandCategoryID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store which uses this category")]
        public int? BrandCategoryID { get; set; }

        [ApiMember(Name = "FranchiseID", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Franchise ID For Search, Note: This will be overridden on data calls automatically")]
        public int? FranchiseID { get; set; }

        public bool? FranchiseIDIncludeNull { get; set; }

        [ApiMember(Name = "FranchiseKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Franchise Key for objects")]
        public string? FranchiseKey { get; set; }

        [ApiMember(Name = "FranchiseName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Franchise Name for objects")]
        public string? FranchiseName { get; set; }

        [ApiMember(Name = "FranchiseNameStrict", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "")]
        public bool? FranchiseNameStrict { get; set; }

        [ApiMember(Name = "FranchiseNameIncludeNull", DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "")]
        public bool? FranchiseNameIncludeNull { get; set; }

        [ApiMember(Name = "FranchiseCategoryID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store which uses this category")]
        public int? FranchiseCategoryID { get; set; }

        [ApiMember(Name = "StoreID", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Store ID For Search, Note: This will be overridden on data calls automatically")]
        public int? StoreID { get; set; }

        public bool? StoreIDIncludeNull { get; set; }

        [ApiMember(Name = "StoreKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Store Key for objects")]
        public string? StoreKey { get; set; }

        [ApiMember(Name = "StoreName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Store Name for objects")]
        public string? StoreName { get; set; }

        [ApiMember(Name = "StoreSeoUrl", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Store SEO URL for objects")]
        public string? StoreSeoUrl { get; set; }

        [ApiMember(Name = "StoreCountryID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store within this country")]
        public int? StoreCountryID { get; set; }

        [ApiMember(Name = "StoreRegionID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store within this region")]
        public int? StoreRegionID { get; set; }

        [ApiMember(Name = "StoreCity", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Match a store within this city")]
        public string? StoreCity { get; set; }

        [ApiMember(Name = "StoreAnyCountryID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any contact within this country")]
        public int? StoreAnyCountryID { get; set; }

        [ApiMember(Name = "StoreAnyRegionID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any contact within this region")]
        public int? StoreAnyRegionID { get; set; }

        [ApiMember(Name = "StoreAnyCity", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any contact within this city")]
        public string? StoreAnyCity { get; set; }

        [ApiMember(Name = "StoreAnyDistrictID", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any district")]
        public int? StoreAnyDistrictID { get; set; }

        [ApiMember(Name = "StoreAnyZipCode", DataType = "string?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any zip code")]
        public string? StoreAnyZipCode { get; set; }

        [ApiMember(Name = "StoreAnyLatitude", DataType = "double?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any latitude")]
        public double? StoreAnyLatitude { get; set; }

        [ApiMember(Name = "StoreAnyLongitude", DataType = "double?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any longitude")]
        public double? StoreAnyLongitude { get; set; }

        [ApiMember(Name = "StoreAnyRadius", DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any radius")]
        public int? StoreAnyRadius { get; set; }

        [ApiMember(Name = "StoreAnyUnits", DataType = "string?", ParameterType = "query", IsRequired = false,
            Description = "Match a store with any units")]
        public LocatorUnits? StoreAnyUnits { get; set; }

        [ApiMember(Name = "MinDate", DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "The minimum date for the object (uses UpdatedDate if set, otherwise CreatedDate)")]
        public DateTime? MinDate { get; set; }

        [ApiMember(Name = "MaxDate", DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "The maximum date for the object (uses UpdatedDate if set, otherwise CreatedDate)")]
        public DateTime? MaxDate { get; set; }

        [ApiMember(Name = "Phone", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "")]
        public string? Phone { get; set; }

        [ApiMember(Name = "FirstName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "")]
        public string? FirstName { get; set; }

        [ApiMember(Name = "LastName", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "")]
        public string? LastName { get; set; }

        [ApiMember(Name = "Email", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "")]
        public string? Email { get; set; }

        [ApiMember(Name = "PostalCode", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "")]
        public string? PostalCode { get; set; }

        [ApiMember(Name = "ProductKey", DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "")]
        public string? ProductKey { get; set; }

        [ApiMember(Name = "ProductIDs", DataType = "List<int>", ParameterType = "query", IsRequired = false,
            Description = "")]
        public List<int>? ProductIDs { get; set; }
    }

}
