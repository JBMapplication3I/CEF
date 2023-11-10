// <copyright file="IProductRestrictionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductRestrictionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for product restriction model.</summary>
    public partial interface IProductRestrictionModel
    {
        /// <summary>Gets or sets a value indicating whether we can purchase internationally.</summary>
        /// <value>True if we can purchase internationally, false if not.</value>
        bool CanPurchaseInternationally { get; set; }

        /// <summary>Gets or sets a value indicating whether we can purchase domestically.</summary>
        /// <value>True if we can purchase domestically, false if not.</value>
        bool CanPurchaseDomestically { get; set; }

        /// <summary>Gets or sets a value indicating whether we can purchase intra region.</summary>
        /// <value>True if we can purchase intra region, false if not.</value>
        bool CanPurchaseIntraRegion { get; set; }

        /// <summary>Gets or sets a value indicating whether we can ship internationally.</summary>
        /// <value>True if we can ship internationally, false if not.</value>
        bool CanShipInternationally { get; set; }

        /// <summary>Gets or sets a value indicating whether we can ship domestically.</summary>
        /// <value>True if we can ship domestically, false if not.</value>
        bool CanShipDomestically { get; set; }

        /// <summary>Gets or sets a value indicating whether we can ship intra region.</summary>
        /// <value>True if we can ship intra region, false if not.</value>
        bool CanShipIntraRegion { get; set; }

        /// <summary>Gets or sets the restrictions apply to city.</summary>
        /// <value>The restrictions apply to city.</value>
        string? RestrictionsApplyToCity { get; set; }

        /// <summary>Gets or sets the restrictions apply to postal code.</summary>
        /// <value>The restrictions apply to postal code.</value>
        string? RestrictionsApplyToPostalCode { get; set; }

        /// <summary>Gets or sets the identifier of the restrictions apply to country.</summary>
        /// <value>The identifier of the restrictions apply to country.</value>
        int? RestrictionsApplyToCountryID { get; set; }

        /// <summary>Gets or sets the restrictions apply to country key.</summary>
        /// <value>The restrictions apply to country key.</value>
        string? RestrictionsApplyToCountryKey { get; set; }

        /// <summary>Gets or sets the name of the restrictions apply to country.</summary>
        /// <value>The name of the restrictions apply to country.</value>
        string? RestrictionsApplyToCountryName { get; set; }

        /// <summary>Gets or sets the restrictions apply to country.</summary>
        /// <value>The restrictions apply to country.</value>
        ICountryModel? RestrictionsApplyToCountry { get; set; }

        /// <summary>Gets or sets the identifier of the restrictions apply to region.</summary>
        /// <value>The identifier of the restrictions apply to region.</value>
        int? RestrictionsApplyToRegionID { get; set; }

        /// <summary>Gets or sets the restrictions apply to region key.</summary>
        /// <value>The restrictions apply to region key.</value>
        string? RestrictionsApplyToRegionKey { get; set; }

        /// <summary>Gets or sets the name of the restrictions apply to region.</summary>
        /// <value>The name of the restrictions apply to region.</value>
        string? RestrictionsApplyToRegionName { get; set; }

        /// <summary>Gets or sets the restrictions apply to region.</summary>
        /// <value>The restrictions apply to region.</value>
        IRegionModel? RestrictionsApplyToRegion { get; set; }

        /// <summary>Gets or sets the override with roles.</summary>
        /// <value>The override with roles.</value>
        string? OverrideWithRoles { get; set; }

        /// <summary>Gets or sets the identifier of the override with account type.</summary>
        /// <value>The identifier of the override with account type.</value>
        int? OverrideWithAccountTypeID { get; set; }

        /// <summary>Gets or sets the override with account type key.</summary>
        /// <value>The override with account type key.</value>
        string? OverrideWithAccountTypeKey { get; set; }

        /// <summary>Gets or sets the name of the override with account type.</summary>
        /// <value>The name of the override with account type.</value>
        string? OverrideWithAccountTypeName { get; set; }

        /// <summary>Gets or sets the type of the override with account.</summary>
        /// <value>The type of the override with account.</value>
        ITypeModel? OverrideWithAccountType { get; set; }
    }
}
