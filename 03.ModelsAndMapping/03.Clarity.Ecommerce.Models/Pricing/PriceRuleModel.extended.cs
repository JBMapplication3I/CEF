// <copyright file="PriceRuleModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the price rule.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IPriceRuleModel"/>
    public partial class PriceRuleModel
    {
        /// <inheritdoc/>
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDate { get; set; }

        /// <inheritdoc/>
        public decimal PriceAdjustment { get; set; }

        /// <inheritdoc/>
        public decimal? MinQuantity { get; set; }

        /// <inheritdoc/>
        public decimal? MaxQuantity { get; set; }

        /// <inheritdoc/>
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public bool IsPercentage { get; set; }

        /// <inheritdoc/>
        public bool IsMarkup { get; set; }

        /// <inheritdoc/>
        public bool UsePriceBase { get; set; }

        /// <inheritdoc/>
        public bool IsExclusive { get; set; }

        /// <inheritdoc/>
        public bool IsOnlyForAnonymousUsers { get; set; }

        /// <inheritdoc/>
        public int? Priority { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? CurrencyName { get; set; }

        /// <inheritdoc cref="IPriceRuleModel.Currency"/>
        public CurrencyModel? Currency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IPriceRuleModel.Currency { get => Currency; set => Currency = (CurrencyModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IPriceRuleModel.PriceRuleCountries"/>
        public List<PriceRuleCountryModel>? PriceRuleCountries { get; set; }

        /// <inheritdoc cref="IPriceRuleModel.PriceRuleUserRoles"/>
        public List<PriceRuleUserRoleModel>? PriceRuleUserRoles { get; set; }

        /// <inheritdoc cref="IPriceRuleModel.PriceRuleCategories"/>
        public List<PriceRuleCategoryModel>? PriceRuleCategories { get; set; }

        /// <inheritdoc cref="IPriceRuleModel.PriceRuleAccountTypes"/>
        public List<PriceRuleAccountTypeModel>? PriceRuleAccountTypes { get; set; }

        /// <inheritdoc cref="IPriceRuleModel.PriceRuleProductTypes"/>
        public List<PriceRuleProductTypeModel>? PriceRuleProductTypes { get; set; }

        /// <inheritdoc/>
        List<IPriceRuleCountryModel>? IPriceRuleModel.PriceRuleCountries { get => PriceRuleCountries?.ToList<IPriceRuleCountryModel>(); set => PriceRuleCountries = value?.Cast<PriceRuleCountryModel>().ToList(); }

        /// <inheritdoc/>
        List<IPriceRuleUserRoleModel>? IPriceRuleModel.PriceRuleUserRoles { get => PriceRuleUserRoles?.ToList<IPriceRuleUserRoleModel>(); set => PriceRuleUserRoles = value?.Cast<PriceRuleUserRoleModel>().ToList(); }

        /// <inheritdoc/>
        List<IPriceRuleCategoryModel>? IPriceRuleModel.PriceRuleCategories { get => PriceRuleCategories?.ToList<IPriceRuleCategoryModel>(); set => PriceRuleCategories = value?.Cast<PriceRuleCategoryModel>().ToList(); }

        /// <inheritdoc/>
        List<IPriceRuleAccountTypeModel>? IPriceRuleModel.PriceRuleAccountTypes { get => PriceRuleAccountTypes?.ToList<IPriceRuleAccountTypeModel>(); set => PriceRuleAccountTypes = value?.Cast<PriceRuleAccountTypeModel>().ToList(); }

        /// <inheritdoc/>
        List<IPriceRuleProductTypeModel>? IPriceRuleModel.PriceRuleProductTypes { get => PriceRuleProductTypes?.ToList<IPriceRuleProductTypeModel>(); set => PriceRuleProductTypes = value?.Cast<PriceRuleProductTypeModel>().ToList(); }
        #endregion
    }
}
