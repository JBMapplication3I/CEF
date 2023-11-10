// <copyright file="SalesCollectionBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales collection base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the sales collection base search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="ISalesCollectionBaseSearchModel"/>
    public abstract class SalesCollectionBaseSearchModel : BaseSearchModel, ISalesCollectionBaseSearchModel
    {
        #region IHaveATypeBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeID), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The Type ID for objects")]
        public int? TypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeIDs), DataType = "int?[]", ParameterType = "query", IsRequired = false, Description = "The Type IDs for objects to specifically include")]
        public int?[]? TypeIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeID), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The Type ID for objects to specifically exclude")]
        public int? ExcludedTypeID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeIDs), DataType = "int?[]", ParameterType = "query", IsRequired = false, Description = "The Type IDs for objects to specifically exclude")]
        public int?[]? ExcludedTypeIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Type Key for objects")]
        public string? TypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeKeys), DataType = "string[]", ParameterType = "query", IsRequired = false, Description = "The Type Keys for objects to specifically include")]
        public string?[]? TypeKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Type Key for objects to specifically exclude")]
        public string? ExcludedTypeKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeKeys), DataType = "string[]", ParameterType = "query", IsRequired = false, Description = "The Type Keys for objects to specifically exclude")]
        public string?[]? ExcludedTypeKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Type Name for objects")]
        public string? TypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeNames), DataType = "string[]", ParameterType = "query", IsRequired = false, Description = "The Type Names for objects to specifically include")]
        public string?[]? TypeNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Type Name for objects to specifically exclude")]
        public string? ExcludedTypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeNames), DataType = "string[]", ParameterType = "query", IsRequired = false, Description = "The Type Names for objects to specifically exclude")]
        public string?[]? ExcludedTypeNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Type Display Name for objects")]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeDisplayNames), DataType = "string[]", ParameterType = "query", IsRequired = false, Description = "The Type Display Names for objects to specifically include")]
        public string?[]? TypeDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeDisplayName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Type Display Name for objects to specifically exclude")]
        public string? ExcludedTypeDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedTypeDisplayNames), DataType = "string[]", ParameterType = "query", IsRequired = false, Description = "The Type Display Names for objects to specifically exclude")]
        public string?[]? ExcludedTypeDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Display Name for objects to specifically exclude")]
        public string? TypeTranslationKey { get; set; }
        #endregion

        #region IHaveAStatusBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusID), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The Status ID for objects")]
        public int? StatusID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusIDs), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The Status IDs for objects to specifically include")]
        public int?[]? StatusIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStatusID), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The Status ID for objects to specifically exclude")]
        public int? ExcludedStatusID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStatusIDs), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The Status IDs for objects to specifically exclude")]
        public int?[]? ExcludedStatusIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Key for objects")]
        public string? StatusKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusKeys), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Keys for objects to specifically include")]
        public string?[]? StatusKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStatusKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Key for objects to specifically exclude")]
        public string? ExcludedStatusKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStatusKeys), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Keys for objects to specifically exclude")]
        public string?[]? ExcludedStatusKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Name for objects")]
        public string? StatusName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusNames), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Names for objects to specifically include")]
        public string?[]? StatusNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStatusName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Name for objects to specifically exclude")]
        public string? ExcludedStatusName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStatusNames), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Names for objects to specifically exclude")]
        public string?[]? ExcludedStatusNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusDisplayName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Display Name for objects")]
        public string? StatusDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusDisplayNames), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Display Names for objects to specifically include")]
        public string?[]? StatusDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStatusDisplayName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Display Name for objects to specifically exclude")]
        public string? ExcludedStatusDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStatusDisplayNames), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The Status Display Names for objects to specifically exclude")]
        public string?[]? ExcludedStatusDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusTranslationKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Display Name for objects to specifically exclude")]
        public string? StatusTranslationKey { get; set; }
        #endregion

        #region IHaveAStateBaseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateID), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The State ID for objects")]
        public int? StateID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateIDs), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The State IDs for objects to specifically include")]
        public int?[]? StateIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStateID), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The State ID for objects to specifically exclude")]
        public int? ExcludedStateID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStateIDs), DataType = "int?", ParameterType = "query", IsRequired = false, Description = "The State IDs for objects to specifically exclude")]
        public int?[]? ExcludedStateIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Key for objects")]
        public string? StateKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateKeys), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Keys for objects to specifically include")]
        public string?[]? StateKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStateKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Key for objects to specifically exclude")]
        public string? ExcludedStateKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStateKeys), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Keys for objects to specifically exclude")]
        public string?[]? ExcludedStateKeys { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Name for objects")]
        public string? StateName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateNames), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Names for objects to specifically include")]
        public string?[]? StateNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStateName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Name for objects to specifically exclude")]
        public string? ExcludedStateName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStateNames), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Names for objects to specifically exclude")]
        public string?[]? ExcludedStateNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateDisplayName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Display Name for objects")]
        public string? StateDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateDisplayNames), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Display Names for objects to specifically include")]
        public string?[]? StateDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStateDisplayName), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Display Name for objects to specifically exclude")]
        public string? ExcludedStateDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludedStateDisplayNames), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Display Names for objects to specifically exclude")]
        public string?[]? ExcludedStateDisplayNames { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateTranslationKey), DataType = "string", ParameterType = "query", IsRequired = false, Description = "The State Display Name for objects to specifically exclude")]
        public string? StateTranslationKey { get; set; }
        #endregion

        #region IAmFilterableByAccountSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Account ID For Search")]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        public bool? AccountIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Account Key for objects")]
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Account Name for objects")]
        public string? AccountName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? AccountNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? AccountNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountIDOrCustomKeyOrNameOrDescription), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "General search against Account assigned to the object")]
        public string? AccountIDOrCustomKeyOrNameOrDescription { get; set; }
        #endregion

        #region IAmFilterableByUserSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "User ID assigned to the object")]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        public bool? UserIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserIDOrCustomKeyOrUserName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "General search against User assigned to the object (includes UserName even though the name of the property doesn't say it)")]
        public string? UserIDOrCustomKeyOrUserName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserExternalID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Username of the User assigned to the object")]
        public string? UserExternalID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserKey), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserUsername), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? UserUsername { get; set; }
        #endregion

        #region IAmFilterableByBrandSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Brand ID For Search, Note: This will be overridden on data calls automatically")]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
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
            Description = "Match a store which uses this category")]
        public int? BrandCategoryID { get; set; }
        #endregion

        #region IAmFilterableByFranchiseSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(FranchiseID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Franchise ID For Search, Note: This will be overridden on data calls automatically")]
        public int? FranchiseID { get; set; }

        /// <inheritdoc/>
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
            Description = "Match a store which uses this category")]
        public int? FranchiseCategoryID { get; set; }
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
        [ApiMember(Name = nameof(MinDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "The minimum date for the object (uses UpdatedDate if set, otherwise CreatedDate)")]
        public DateTime? MinDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MaxDate), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "The maximum date for the object (uses UpdatedDate if set, otherwise CreatedDate)")]
        public DateTime? MaxDate { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Phone), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? Phone { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FirstName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? FirstName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(LastName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? LastName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? Email { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PostalCode), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? PostalCode { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ProductKey), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? ProductKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ProductIDs), DataType = "List<int>", ParameterType = "query", IsRequired = false)]
        public List<int>? ProductIDs { get; set; }
    }
}
