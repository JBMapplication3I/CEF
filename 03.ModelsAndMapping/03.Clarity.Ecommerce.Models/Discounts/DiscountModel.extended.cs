// <copyright file="DiscountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the discount.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IDiscountModel"/>
    public partial class DiscountModel
    {
        #region Discount Properties
        /// <inheritdoc/>
        public bool CanCombine { get; set; }

        /// <inheritdoc/>
        public bool IsAutoApplied { get; set; }

        /// <inheritdoc/>
        public int RoundingOperation { get; set; }

        /// <inheritdoc/>
        public int? UsageLimitPerAccount { get; set; }

        /// <inheritdoc/>
        public int? UsageLimitPerUser { get; set; }

        /// <inheritdoc/>
        public int? UsageLimitPerCart { get; set; }

        /// <inheritdoc/>
        public int? UsageLimitGlobally { get; set; }

        /// <inheritdoc/>
        public Enums.CompareOperator DiscountCompareOperator { get; set; }

        /// <inheritdoc/>
        public int DiscountTypeID { get; set; }

        /// <inheritdoc/>
        public int ValueType { get; set; }

        /// <inheritdoc/>
        public int? RoundingType { get; set; }

        /// <inheritdoc/>
        public int? Priority { get; set; }

        /// <inheritdoc/>
        public DateTime? StartDate { get; set; }

        /// <inheritdoc/>
        public DateTime? EndDate { get; set; }

        /// <inheritdoc/>
        public decimal Value { get; set; }

        /// <inheritdoc/>
        public decimal ThresholdAmount { get; set; }

        /// <inheritdoc/>
        public decimal? BuyXValue { get; set; }

        /// <inheritdoc/>
        public decimal? GetYValue { get; set; }
        #endregion

        #region AssociatedObjects
        /// <inheritdoc cref="IDiscountModel.Codes"/>
        public List<DiscountCodeModel>? Codes { get; set; }

        /// <inheritdoc/>
        List<IDiscountCodeModel>? IDiscountModel.Codes { get => Codes?.ToList<IDiscountCodeModel>(); set => Codes = value?.Cast<DiscountCodeModel>().ToList(); }

        /// <inheritdoc cref="IDiscountModel.Countries"/>
        public List<DiscountCountryModel>? Countries { get; set; }

        /// <inheritdoc/>
        List<IDiscountCountryModel>? IDiscountModel.Countries { get => Countries?.ToList<IDiscountCountryModel>(); set => Countries = value?.Cast<DiscountCountryModel>().ToList(); }

        /// <inheritdoc cref="IDiscountModel.UserRoles"/>
        public List<DiscountUserRoleModel>? UserRoles { get; set; }

        /// <inheritdoc/>
        List<IDiscountUserRoleModel>? IDiscountModel.UserRoles { get => UserRoles?.ToList<IDiscountUserRoleModel>(); set => UserRoles = value?.Cast<DiscountUserRoleModel>().ToList(); }

        /// <inheritdoc cref="IDiscountModel.Categories"/>
        public List<DiscountCategoryModel>? Categories { get; set; }

        /// <inheritdoc/>
        List<IDiscountCategoryModel>? IDiscountModel.Categories { get => Categories?.ToList<IDiscountCategoryModel>(); set => Categories = value?.Cast<DiscountCategoryModel>().ToList(); }

        /// <inheritdoc cref="IDiscountModel.AccountTypes"/>
        public List<DiscountAccountTypeModel>? AccountTypes { get; set; }

        /// <inheritdoc/>
        List<IDiscountAccountTypeModel>? IDiscountModel.AccountTypes { get => AccountTypes?.ToList<IDiscountAccountTypeModel>(); set => AccountTypes = value?.Cast<DiscountAccountTypeModel>().ToList(); }

        /// <inheritdoc cref="IDiscountModel.ProductTypes"/>
        public List<DiscountProductTypeModel>? ProductTypes { get; set; }

        /// <inheritdoc/>
        List<IDiscountProductTypeModel>? IDiscountModel.ProductTypes { get => ProductTypes?.ToList<IDiscountProductTypeModel>(); set => ProductTypes = value?.Cast<DiscountProductTypeModel>().ToList(); }

        /// <inheritdoc cref="IDiscountModel.ShipCarrierMethods"/>
        public List<DiscountShipCarrierMethodModel>? ShipCarrierMethods { get; set; }

        /// <inheritdoc/>
        List<IDiscountShipCarrierMethodModel>? IDiscountModel.ShipCarrierMethods { get => ShipCarrierMethods?.ToList<IDiscountShipCarrierMethodModel>(); set => ShipCarrierMethods = value?.Cast<DiscountShipCarrierMethodModel>().ToList(); }
        #endregion

        #region Convenience Properties
        /// <inheritdoc/>
        public decimal DiscountTotal { get; set; }
        #endregion
    }
}
