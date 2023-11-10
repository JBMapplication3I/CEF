// <copyright file="ProductRestriction.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product restriction class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for product restriction.</summary>
    public interface IProductRestriction : IAmFilterableByProduct
    {
        #region ProductRestriction
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

        /// <summary>Gets or sets the override with roles.</summary>
        /// <value>The override with roles.</value>
        string? OverrideWithRoles { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the override with account type.</summary>
        /// <value>The identifier of the override with account type.</value>
        int? OverrideWithAccountTypeID { get; set; }

        /// <summary>Gets or sets the type of the override with account.</summary>
        /// <value>The type of the override with account.</value>
        AccountType? OverrideWithAccountType { get; set; }

        /// <summary>Gets or sets the identifier of the restrictions apply to country.</summary>
        /// <value>The identifier of the restrictions apply to country.</value>
        int? RestrictionsApplyToCountryID { get; set; }

        /// <summary>Gets or sets the restrictions apply to country.</summary>
        /// <value>The restrictions apply to country.</value>
        Country? RestrictionsApplyToCountry { get; set; }

        /// <summary>Gets or sets the identifier of the restrictions apply to region.</summary>
        /// <value>The identifier of the restrictions apply to region.</value>
        int? RestrictionsApplyToRegionID { get; set; }

        /// <summary>Gets or sets the restrictions apply to region.</summary>
        /// <value>The restrictions apply to region.</value>
        Region? RestrictionsApplyToRegion { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    /// <summary>A product restriction.</summary>
    /// <seealso cref="IProductRestriction"/>
    [SqlSchema("Products", "ProductRestriction")]
    public class ProductRestriction : Base, IProductRestriction
    {
        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Product)), DefaultValue(0)]
        public int ProductID { get; set; } = 0;

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Product? Product { get; set; }
        #endregion

        #region ProductRestriction Properties
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
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? RestrictionsApplyToCity { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(24), DefaultValue(null)]
        public string? RestrictionsApplyToPostalCode { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(1024), DefaultValue(null)]
        public string? OverrideWithRoles { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(OverrideWithAccountType)), DefaultValue(null)]
        public int? OverrideWithAccountTypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AccountType? OverrideWithAccountType { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(RestrictionsApplyToCountry)), DefaultValue(null)]
        public int? RestrictionsApplyToCountryID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Country? RestrictionsApplyToCountry { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(RestrictionsApplyToRegion)), DefaultValue(null)]
        public int? RestrictionsApplyToRegionID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Region? RestrictionsApplyToRegion { get; set; }
        #endregion
    }
}
