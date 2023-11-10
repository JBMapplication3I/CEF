// <copyright file="IPriceRuleModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPriceRuleModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    public partial interface IPriceRuleModel
    {
        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        DateTime? EndDate { get; set; }

        /// <summary>Gets or sets the minimum quantity.</summary>
        /// <value>The minimum quantity.</value>
        decimal? MinQuantity { get; set; }

        /// <summary>Gets or sets the maximum quantity.</summary>
        /// <value>The maximum quantity.</value>
        decimal? MaxQuantity { get; set; }

        /// <summary>Gets or sets the price adjustment.</summary>
        /// <value>The price adjustment.</value>
        decimal PriceAdjustment { get; set; }

        /// <summary>Gets or sets a value indicating whether this PriceRule is percentage.</summary>
        /// <value>True if this PriceRule is percentage, false if not.</value>
        bool IsPercentage { get; set; }

        /// <summary>Gets or sets a value indicating whether this PriceRule is markup.</summary>
        /// <value>True if this PriceRule is markup, false if not.</value>
        bool IsMarkup { get; set; }

        /// <summary>Gets or sets a value indicating whether this PriceRule use price base.</summary>
        /// <value>True if use price base, false if not.</value>
        bool UsePriceBase { get; set; }

        /// <summary>Gets or sets a value indicating whether this PriceRule is exclusive.</summary>
        /// <value>True if this PriceRule is exclusive, false if not.</value>
        bool IsExclusive { get; set; }

        /// <summary>Gets or sets a value indicating whether this PriceRule is only for anonymous users.</summary>
        /// <value>True if this PriceRule is only for anonymous users, false if not.</value>
        bool IsOnlyForAnonymousUsers { get; set; }

        /// <summary>Gets or sets the priority.</summary>
        /// <value>The priority.</value>
        int? Priority { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the currency.</summary>
        /// <value>The name of the currency.</value>
        string? CurrencyName { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        ICurrencyModel? Currency { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the price rule countries.</summary>
        /// <value>The price rule countries.</value>
        List<IPriceRuleCountryModel>? PriceRuleCountries { get; set; }

        /// <summary>Gets or sets the categories the price rule belongs to.</summary>
        /// <value>The price rule categories.</value>
        List<IPriceRuleCategoryModel>? PriceRuleCategories { get; set; }

        /// <summary>Gets or sets the price rule user roles.</summary>
        /// <value>The price rule user roles.</value>
        List<IPriceRuleUserRoleModel>? PriceRuleUserRoles { get; set; }

        /// <summary>Gets or sets a list of types of the price rule accounts.</summary>
        /// <value>A list of types of the price rule accounts.</value>
        List<IPriceRuleAccountTypeModel>? PriceRuleAccountTypes { get; set; }

        /// <summary>Gets or sets a list of types of the price rule products.</summary>
        /// <value>A list of types of the price rule products.</value>
        List<IPriceRuleProductTypeModel>? PriceRuleProductTypes { get; set; }
        #endregion
    }
}
