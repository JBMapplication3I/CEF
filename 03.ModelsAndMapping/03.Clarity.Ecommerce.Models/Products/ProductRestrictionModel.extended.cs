// <copyright file="ProductRestrictionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product restriction model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;

    public partial class ProductRestrictionModel
    {
        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool CanPurchaseInternationally { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool CanPurchaseDomestically { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool CanPurchaseIntraRegion { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool CanShipInternationally { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool CanShipDomestically { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool CanShipIntraRegion { get; set; } = true;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? RestrictionsApplyToCity { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? RestrictionsApplyToPostalCode { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? RestrictionsApplyToCountryID { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? RestrictionsApplyToCountryKey { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? RestrictionsApplyToCountryName { get; set; } = null;

        /// <inheritdoc cref="IProductRestrictionModel.RestrictionsApplyToCountry"/>
        [DefaultValue(null)]
        public CountryModel? RestrictionsApplyToCountry { get; set; }

        /// <inheritdoc/>
        ICountryModel? IProductRestrictionModel.RestrictionsApplyToCountry { get => RestrictionsApplyToCountry; set => RestrictionsApplyToCountry = (CountryModel?)value; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? RestrictionsApplyToRegionID { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? RestrictionsApplyToRegionKey { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? RestrictionsApplyToRegionName { get; set; } = null;

        /// <inheritdoc cref="IProductRestrictionModel.RestrictionsApplyToRegion"/>
        [DefaultValue(null)]
        public RegionModel? RestrictionsApplyToRegion { get; set; }

        /// <inheritdoc/>
        IRegionModel? IProductRestrictionModel.RestrictionsApplyToRegion { get => RestrictionsApplyToRegion; set => RestrictionsApplyToRegion = (RegionModel?)value; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? OverrideWithRoles { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? OverrideWithAccountTypeID { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? OverrideWithAccountTypeKey { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? OverrideWithAccountTypeName { get; set; } = null;

        /// <inheritdoc cref="IProductRestrictionModel.OverrideWithAccountType"/>
        [DefaultValue(null)]
        public TypeModel? OverrideWithAccountType { get; set; }

        /// <inheritdoc/>
        ITypeModel? IProductRestrictionModel.OverrideWithAccountType { get => OverrideWithAccountType; set => OverrideWithAccountType = (TypeModel?)value; }
    }
}
