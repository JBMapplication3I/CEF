// <copyright file="IDiscountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDiscountModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for discount model.</summary>
    public partial interface IDiscountModel
    {
        /// <summary>Gets or sets a value indicating whether we can combine.</summary>
        /// <value>True if we can combine, false if not.</value>
        bool CanCombine { get; set; }

        /// <summary>Gets or sets a value indicating whether this IDiscountModel is automatic applied.</summary>
        /// <value>True if this IDiscountModel is automatic applied, false if not.</value>
        bool IsAutoApplied { get; set; }

        /// <summary>Gets or sets the rounding operation.</summary>
        /// <value>The rounding operation.</value>
        int RoundingOperation { get; set; }

        /// <summary>Gets or sets the usage limit per account.</summary>
        /// <value>The usage limit per account.</value>
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

        /// <summary>Gets or sets the discount compare operator.</summary>
        /// <value>The discount compare operator.</value>
        Enums.CompareOperator DiscountCompareOperator { get; set; }

        /// <summary>Gets or sets the identifier of the discount type.</summary>
        /// <value>The identifier of the discount type.</value>
        int DiscountTypeID { get; set; } // NOTE: Not a foreign key

        /// <summary>Gets or sets the type of the value.</summary>
        /// <value>The type of the value.</value>
        int ValueType { get; set; } // NOTE: Not a foreign key

        /// <summary>Gets or sets the type of the rounding.</summary>
        /// <value>The type of the rounding.</value>
        int? RoundingType { get; set; } // NOTE: Not a foreign key

        /// <summary>Gets or sets the priority.</summary>
        /// <value>The priority.</value>
        int? Priority { get; set; }

        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }

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

        #region Associated Objects
        /// <summary>Gets or sets the codes.</summary>
        /// <value>The codes.</value>
        List<IDiscountCodeModel>? Codes { get; set; }

        /// <summary>Gets or sets the countries.</summary>
        /// <value>The countries.</value>
        List<IDiscountCountryModel>? Countries { get; set; }

        /// <summary>Gets or sets the user roles.</summary>
        /// <value>The user roles.</value>
        List<IDiscountUserRoleModel>? UserRoles { get; set; }

        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        List<IDiscountCategoryModel>? Categories { get; set; }

        /// <summary>Gets or sets a list of types of the accounts.</summary>
        /// <value>A list of types of the accounts.</value>
        List<IDiscountAccountTypeModel>? AccountTypes { get; set; }

        /// <summary>Gets or sets a list of types of the products.</summary>
        /// <value>A list of types of the products.</value>
        List<IDiscountProductTypeModel>? ProductTypes { get; set; }

        /// <summary>Gets or sets the ship carrier methods.</summary>
        /// <value>The ship carrier methods.</value>
        List<IDiscountShipCarrierMethodModel>? ShipCarrierMethods { get; set; }
        #endregion

        #region Convenience Properties
        /// <summary>Gets or sets the discount total.</summary>
        /// <value>The discount total.</value>
        decimal DiscountTotal { get; set; }
        #endregion
    }
}
