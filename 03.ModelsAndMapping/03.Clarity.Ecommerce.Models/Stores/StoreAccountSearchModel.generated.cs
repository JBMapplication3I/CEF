// <autogenerated>
// <copyright file="StoreAccountSearchModel.generated.cs" company="clarity-ventures.com">
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

    /// <summary>A data model for the Store Account search.</summary>
    public partial class StoreAccountSearchModel
        : AmARelationshipTableBaseSearchModel
        , IStoreAccountSearchModel
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
        #region IAmFilterableByAccountSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Account ID For Search, Note: This will be overridden on data calls automatically")]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, allow matches to null for AccountID field")]
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
        [ApiMember(Name = nameof(AccountNameStrict), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, the value of the AccountName field must match exactly, otherwise, a case-insentive contains check is run.")]
        public bool? AccountNameStrict { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountNameIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, allow matches to null for AccountName field")]
        public bool? AccountNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(AccountIDOrCustomKeyOrNameOrDescription), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? AccountIDOrCustomKeyOrNameOrDescription { get; set; }
        #endregion
        #region IAmFilterableByStoreSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Store ID For Search, Note: This will be overridden on data calls automatically")]
        public int? StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? StoreIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Store Key for objects")]
        public string? StoreKey { get => MasterKey; set => MasterKey = value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Store Name for objects")]
        public string? StoreName { get => MasterName; set => MasterName = value; }

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
        [ApiMember(Name = nameof(HasAccessToStore), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? HasAccessToStore { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PricePointID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? PricePointID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(PricePointIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? PricePointIDIncludeNull { get; set; }
    }
}
