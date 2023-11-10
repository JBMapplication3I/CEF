// <copyright file="PriceRule.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IPriceRule
        : INameableBase,
            IAmFilterableByAccount<PriceRuleAccount>,
            IAmFilterableByManufacturer<PriceRuleManufacturer>,
            IAmFilterableByProduct<PriceRuleProduct>,
            IAmFilterableByBrand<PriceRuleBrand>,
            IAmFilterableByFranchise<PriceRuleFranchise>,
            IAmFilterableByStore<PriceRuleStore>,
            IAmFilterableByVendor<PriceRuleVendor>
    {
        #region PriceRule Properties
        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the price adjustment.</summary>
        /// <value>The price adjustment.</value>
        decimal PriceAdjustment { get; set; }

        /// <summary>Gets or sets a value indicating whether this IPriceRule is percentage.</summary>
        /// <value>True if this IPriceRule is percentage, false if not.</value>
        bool IsPercentage { get; set; }

        /// <summary>Gets or sets a value indicating whether this IPriceRule use price base.</summary>
        /// <value>True if use price base, false if not.</value>
        bool UsePriceBase { get; set; }

        /// <summary>Gets or sets a value indicating whether this IPriceRule is exclusive.</summary>
        /// <value>True if this IPriceRule is exclusive, false if not.</value>
        bool IsExclusive { get; set; }

        /// <summary>Gets or sets a value indicating whether this IPriceRule is markup (true) or markdown (false).</summary>
        /// <value>True if this IPriceRule is markup, false if not.</value>
        bool IsMarkup { get; set; }

        /// <summary>Gets or sets a value indicating whether this IPriceRule is only for anonymous users.</summary>
        /// <value>True if this IPriceRule is only for anonymous users, false if not.</value>
        bool IsOnlyForAnonymousUsers { get; set; }

        /// <summary>Gets or sets the minimum quantity.</summary>
        /// <value>The minimum quantity.</value>
        decimal? MinQuantity { get; set; }

        /// <summary>Gets or sets the maximum quantity.</summary>
        /// <value>The maximum quantity.</value>
        decimal? MaxQuantity { get; set; }

        /// <summary>Gets or sets the priority.</summary>
        /// <value>The priority.</value>
        int? Priority { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        Currency? Currency { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the price rule countries.</summary>
        /// <value>The price rule countries.</value>
        ICollection<PriceRuleCountry>? PriceRuleCountries { get; set; }

        /// <summary>Gets or sets the price rule user roles.</summary>
        /// <value>The price rule user roles.</value>
        ICollection<PriceRuleUserRole>? PriceRuleUserRoles { get; set; }

        /// <summary>Gets or sets the categories the price rule belongs to.</summary>
        /// <value>The price rule categories.</value>
        ICollection<PriceRuleCategory>? PriceRuleCategories { get; set; }

        /// <summary>Gets or sets a list of types of the price rule accounts.</summary>
        /// <value>A list of types of the price rule accounts.</value>
        ICollection<PriceRuleAccountType>? PriceRuleAccountTypes { get; set; }

        /// <summary>Gets or sets a list of types of the price rule products.</summary>
        /// <value>A list of types of the price rule products.</value>
        ICollection<PriceRuleProductType>? PriceRuleProductTypes { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Pricing", "PriceRule")]
    public class PriceRule : NameableBase, IPriceRule
    {
        private ICollection<PriceRuleBrand>? brands;
        private ICollection<PriceRuleStore>? stores;
        private ICollection<PriceRuleVendor>? vendors;
        private ICollection<PriceRuleAccount>? accounts;
        private ICollection<PriceRuleProduct>? products;
        private ICollection<PriceRuleFranchise>? franchises;
        private ICollection<PriceRuleManufacturer>? manufacturers;

        private ICollection<PriceRuleCountry>? priceRuleCountries;
        private ICollection<PriceRuleUserRole>? priceRuleUserRoles;
        private ICollection<PriceRuleCategory>? priceRuleCategories;
        private ICollection<PriceRuleAccountType>? priceRuleAccountTypes;
        private ICollection<PriceRuleProductType>? priceRuleProductTypes;

        public PriceRule()
        {
            // IAmFilterableByProduct
            products = new HashSet<PriceRuleProduct>();
            // IAmFilterableByBrand
            brands = new HashSet<PriceRuleBrand>();
            // IAmFilterableByFranchise
            franchises = new HashSet<PriceRuleFranchise>();
            // IAmFilterableByStore
            stores = new HashSet<PriceRuleStore>();
            // IAmFilterableByVendor
            vendors = new HashSet<PriceRuleVendor>();
            // IAmFilterableByManufacturer
            manufacturers = new HashSet<PriceRuleManufacturer>();
            // IAmFilterableByAccount
            accounts = new HashSet<PriceRuleAccount>();
            // Price Rule Properties
            priceRuleCountries = new HashSet<PriceRuleCountry>();
            priceRuleUserRoles = new HashSet<PriceRuleUserRole>();
            priceRuleCategories = new HashSet<PriceRuleCategory>();
            priceRuleAccountTypes = new HashSet<PriceRuleAccountType>();
            priceRuleProductTypes = new HashSet<PriceRuleProductType>();
        }

        #region PriceRule Properties
        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? StartDate { get; set; } = null;

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? EndDate { get; set; } = null;

        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false)]
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal PriceAdjustment { get; set; } = 0m;

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinQuantity { get; set; } = null;

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaxQuantity { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsPercentage { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsMarkup { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool UsePriceBase { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsExclusive { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsOnlyForAnonymousUsers { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? Priority { get; set; } = null;
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Currency)), DefaultValue(null)]
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DefaultValue(null)]
        public virtual Currency? Currency { get; set; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleProduct>? Products { get => products; set => products = value; }
        #endregion

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleBrand>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleFranchise>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleStore>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByAccount
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleAccount>? Accounts { get => accounts; set => accounts = value; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleVendor>? Vendors { get => vendors; set => vendors = value; }
        #endregion

        #region IAmFilterableByManufacturer
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleManufacturer>? Manufacturers { get => manufacturers; set => manufacturers = value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleCountry>? PriceRuleCountries { get => priceRuleCountries; set => priceRuleCountries = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleUserRole>? PriceRuleUserRoles { get => priceRuleUserRoles; set => priceRuleUserRoles = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleCategory>? PriceRuleCategories { get => priceRuleCategories; set => priceRuleCategories = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleAccountType>? PriceRuleAccountTypes { get => priceRuleAccountTypes; set => priceRuleAccountTypes = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<PriceRuleProductType>? PriceRuleProductTypes { get => priceRuleProductTypes; set => priceRuleProductTypes = value; }
        #endregion
    }
}
