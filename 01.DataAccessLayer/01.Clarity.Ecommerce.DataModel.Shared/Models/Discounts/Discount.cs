// <copyright file="Discount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IDiscount
        : INameableBase,
            IAmFilterableByAccount<DiscountAccount>,
            IAmFilterableByProduct<DiscountProduct>,
            IAmFilterableByManufacturer<DiscountManufacturer>,
            IAmFilterableByBrand<DiscountBrand>,
            IAmFilterableByFranchise<DiscountFranchise>,
            IAmFilterableByStore<DiscountStore>,
            IAmFilterableByUser<DiscountUser>,
            IAmFilterableByVendor<DiscountVendor>
    {
        #region Discount Properties
        /// <summary>Gets or sets a value indicating whether we can combine.</summary>
        /// <value>True if we can combine, false if not.</value>
        bool CanCombine { get; set; }

        /// <summary>Gets or sets a value indicating whether this IDiscount is automatic applied.</summary>
        /// <value>True if this IDiscount is automatic applied, false if not.</value>
        bool IsAutoApplied { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        decimal Value { get; set; }

        /// <summary>Gets or sets the threshold amount.</summary>
        /// <value>The threshold amount.</value>
        decimal ThresholdAmount { get; set; }

        /// <summary>Gets or sets the buy x coordinate value.</summary>
        /// <value>The buy x coordinate value.</value>
        decimal? BuyXValue { get; set; }

        /// <summary>Gets or sets the get y coordinate value.</summary>
        /// <value>The get y coordinate value.</value>
        decimal? GetYValue { get; set; }

        /// <summary>Gets or sets the rounding operation.</summary>
        /// <value>The rounding operation.</value>
        int RoundingOperation { get; set; }

        /// <summary>Gets or sets the discount compare operator.</summary>
        /// <value>The discount compare operator.</value>
        int? DiscountCompareOperator { get; set; }

        /// <summary>Gets or sets the usage limit.</summary>
        /// <value>The usage limit.</value>
        int? UsageLimitPerAccount { get; set; }

        /// <summary>Gets or sets the usage limit per user.</summary>
        /// <value>The usage limit per user.</value>
        int? UsageLimitPerUser { get; set; }

        /// <summary>Gets or sets the usage limit per cart.</summary>
        /// <value>The usage limit per cart.</value>
        int? UsageLimitPerCart { get; set; }

        /// <summary>Gets or sets the usage limit globally.</summary>
        /// <value>The usage limit globally.</value>
        int? UsageLimitGlobally { get; set; }

        /// <summary>Gets or sets the type of the rounding.</summary>
        /// <value>The type of the rounding.</value>
        int? RoundingType { get; set; }

        /// <summary>Gets or sets the identifier of the discount type.</summary>
        /// <value>The identifier of the discount type.</value>
        int DiscountTypeID { get; set; }

        /// <summary>Gets or sets the type of the value.</summary>
        /// <value>The type of the value.</value>
        int ValueType { get; set; }

        /// <summary>Gets or sets the priority.</summary>
        /// <value>The priority.</value>
        int? Priority { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the codes.</summary>
        /// <value>The codes.</value>
        ICollection<DiscountCode>? Codes { get; set; }

        /// <summary>Gets or sets the countries.</summary>
        /// <value>The countries.</value>
        ICollection<DiscountCountry>? Countries { get; set; }

        /// <summary>Gets or sets the user roles.</summary>
        /// <value>The user roles.</value>
        ICollection<DiscountUserRole>? UserRoles { get; set; }

        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        ICollection<DiscountCategory>? Categories { get; set; }

        /// <summary>Gets or sets a list of types of the accounts.</summary>
        /// <value>A list of types of the accounts.</value>
        ICollection<DiscountAccountType>? AccountTypes { get; set; }

        /// <summary>Gets or sets a list of types of the products.</summary>
        /// <value>A list of types of the products.</value>
        ICollection<DiscountProductType>? ProductTypes { get; set; }

        /// <summary>Gets or sets the ship carrier methods.</summary>
        /// <value>The ship carrier methods.</value>
        ICollection<DiscountShipCarrierMethod>? ShipCarrierMethods { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Discounts", "Discount")]
    public class Discount : NameableBase, IDiscount
    {
        private ICollection<DiscountCode>? codes;
        private ICollection<DiscountUser>? users;
        private ICollection<DiscountBrand>? brands;
        private ICollection<DiscountFranchise>? franchises;
        private ICollection<DiscountStore>? stores;
        private ICollection<DiscountVendor>? vendors;
        private ICollection<DiscountAccount>? accounts;
        private ICollection<DiscountProduct>? products;
        private ICollection<DiscountCountry>? countries;
        private ICollection<DiscountUserRole>? userRoles;
        private ICollection<DiscountCategory>? categories;
        private ICollection<DiscountAccountType>? accountTypes;
        private ICollection<DiscountProductType>? productTypes;
        private ICollection<DiscountManufacturer>? manufacturers;
        private ICollection<DiscountShipCarrierMethod>? shipCarrierMethods;

        public Discount()
        {
            codes = new HashSet<DiscountCode>();
            users = new HashSet<DiscountUser>();
            brands = new HashSet<DiscountBrand>();
            stores = new HashSet<DiscountStore>();
            vendors = new HashSet<DiscountVendor>();
            accounts = new HashSet<DiscountAccount>();
            products = new HashSet<DiscountProduct>();
            countries = new HashSet<DiscountCountry>();
            userRoles = new HashSet<DiscountUserRole>();
            categories = new HashSet<DiscountCategory>();
            franchises = new HashSet<DiscountFranchise>();
            accountTypes = new HashSet<DiscountAccountType>();
            productTypes = new HashSet<DiscountProductType>();
            manufacturers = new HashSet<DiscountManufacturer>();
            shipCarrierMethods = new HashSet<DiscountShipCarrierMethod>();
        }

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountProduct>? Products { get => products; set => products = value; }
        #endregion

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountBrand>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual ICollection<DiscountFranchise>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual ICollection<DiscountStore>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByAccount
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<DiscountAccount>? Accounts { get => accounts; set => accounts = value; }
        #endregion

        #region IAmFilterableByUser
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<DiscountUser>? Users { get => users; set => users = value; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual ICollection<DiscountVendor>? Vendors { get => vendors; set => vendors = value; }
        #endregion

        #region IAmFilterableByManufacturer
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual ICollection<DiscountManufacturer>? Manufacturers { get => manufacturers; set => manufacturers = value; }
        #endregion

        #region Discount Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool CanCombine { get; set; } = false;

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsAutoApplied { get; set; } = false;

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Value { get; set; } = 0;

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal ThresholdAmount { get; set; } = 0;

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? BuyXValue { get; set; } = null;

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? GetYValue { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int RoundingOperation { get; set; } = 0;

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int? UsageLimitPerAccount { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int? UsageLimitPerUser { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int? UsageLimitPerCart { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int? UsageLimitGlobally { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? DiscountCompareOperator { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int DiscountTypeID { get; set; } = 0;

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int ValueType { get; set; } = 0;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? RoundingType { get; set; } = null;

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? Priority { get; set; } = null;

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? EndDate { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountCode>? Codes { get => codes; set => codes = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountCountry>? Countries { get => countries; set => countries = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountUserRole>? UserRoles { get => userRoles; set => userRoles = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual ICollection<DiscountCategory>? Categories { get => categories; set => categories = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountAccountType>? AccountTypes { get => accountTypes; set => accountTypes = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountProductType>? ProductTypes { get => productTypes; set => productTypes = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<DiscountShipCarrierMethod>? ShipCarrierMethods { get => shipCarrierMethods; set => shipCarrierMethods = value; }
        #endregion
    }
}
