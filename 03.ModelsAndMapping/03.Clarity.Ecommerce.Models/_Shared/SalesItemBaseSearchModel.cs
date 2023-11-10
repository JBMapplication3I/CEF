// <copyright file="SalesItemBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the sales item base search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="ISalesItemBaseSearchModel"/>
    public class SalesItemBaseSearchModel : NameableBaseSearchModel, ISalesItemBaseSearchModel
    {
        #region IAmFilterableByProductSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ProductID), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Product ID For Search, Note: This will be overridden on data calls automatically")]
        public int? ProductID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ProductKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Product Key for objects")]
        public string? ProductKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ProductName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Product Name for objects")]
        public string? ProductName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ProductSeoUrl), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The Product SEO URL for objects")]
        public string? ProductSeoUrl { get; set; }
        #endregion

        #region IAmFilterableByUserSearchModel
        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "User ID For Search, Note: This will be overridden on data calls automatically")]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserIDIncludeNull), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "User ID For Search, Note: This will be overridden on data calls automatically")]
        public bool? UserIDIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The User Key for objects")]
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserUsername), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The User Name for objects")]
        public string? UserUsername { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(UserIDOrCustomKeyOrUserName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? UserIDOrCustomKeyOrUserName { get; set; }
        #endregion

        /// <inheritdoc/>
        public string? Sku { get; set; }

        /// <inheritdoc/>
        public int? OriginalCurrencyID { get; set; }

        /// <inheritdoc/>
        public int? SellingCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? UserExternalID { get; set; }

        /// <inheritdoc/>
        public int? MasterID { get; set; }

        /// <inheritdoc/>
        public string? MasterKey { get; set; }

        /// <inheritdoc/>
        public string? MasterTypeName { get; set; }

        /// <inheritdoc/>
        public Guid? CartSessionID { get; set; }

        /// <inheritdoc/>
        public string? TrackingNumber { get; set; }

        /// <inheritdoc/>
        public int? CartUserID { get; set; }

        /// <inheritdoc/>
        public string? ForceUniqueLineItemKey { get; set; }

        /// <inheritdoc/>
        public bool? ForceUniqueLineItemKeyMatchNulls { get; set; }

        /// <inheritdoc/>
        public decimal? MinQuantity { get; set; }

        /// <inheritdoc/>
        public decimal? MaxQuantity { get; set; }

        /// <inheritdoc/>
        public decimal? MatchQuantity { get; set; }

        /// <inheritdoc/>
        public decimal? MinUnitCorePrice { get; set; }

        /// <inheritdoc/>
        public decimal? MaxUnitCorePrice { get; set; }

        /// <inheritdoc/>
        public decimal? MatchUnitCorePrice { get; set; }

        /// <inheritdoc/>
        public bool? OriginalCurrencyIDIncludeNull { get; set; }

        /// <inheritdoc/>
        public bool? ProductIDIncludeNull { get; set; }

        /// <inheritdoc/>
        public decimal? MinQuantityBackOrdered { get; set; }

        /// <inheritdoc/>
        public decimal? MaxQuantityBackOrdered { get; set; }

        /// <inheritdoc/>
        public decimal? MatchQuantityBackOrdered { get; set; }

        /// <inheritdoc/>
        public bool? MatchQuantityBackOrderedIncludeNull { get; set; }

        /// <inheritdoc/>
        public decimal? MinQuantityPreSold { get; set; }

        /// <inheritdoc/>
        public decimal? MaxQuantityPreSold { get; set; }

        /// <inheritdoc/>
        public decimal? MatchQuantityPreSold { get; set; }

        /// <inheritdoc/>
        public bool? MatchQuantityPreSoldIncludeNull { get; set; }

        /// <inheritdoc/>
        public bool? SellingCurrencyIDIncludeNull { get; set; }

        /// <inheritdoc/>
        public decimal? MinUnitCorePriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        public decimal? MaxUnitCorePriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        public decimal? MatchUnitCorePriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        public bool? MatchUnitCorePriceInSellingCurrencyIncludeNull { get; set; }

        /// <inheritdoc/>
        public decimal? MinUnitSoldPrice { get; set; }

        /// <inheritdoc/>
        public decimal? MaxUnitSoldPrice { get; set; }

        /// <inheritdoc/>
        public decimal? MatchUnitSoldPrice { get; set; }

        /// <inheritdoc/>
        public bool? MatchUnitSoldPriceIncludeNull { get; set; }

        /// <inheritdoc/>
        public decimal? MinUnitSoldPriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        public decimal? MaxUnitSoldPriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        public decimal? MatchUnitSoldPriceInSellingCurrency { get; set; }

        /// <inheritdoc/>
        public bool? MatchUnitSoldPriceInSellingCurrencyIncludeNull { get; set; }

        /// <inheritdoc/>
        public bool? ForceUniqueLineItemKeyStrict { get; set; }

        /// <inheritdoc/>
        public bool? ForceUniqueLineItemKeyIncludeNull { get; set; }

        /// <inheritdoc/>
        public bool? SkuStrict { get; set; }

        /// <inheritdoc/>
        public bool? SkuIncludeNull { get; set; }

        /// <inheritdoc/>
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public bool? UnitOfMeasureStrict { get; set; }

        /// <inheritdoc/>
        public bool? UnitOfMeasureIncludeNull { get; set; }

        #region Cart Items Only
        /// <inheritdoc/>
        public decimal? MinUnitSoldPriceModifier { get; set; }

        /// <inheritdoc/>
        public decimal? MaxUnitSoldPriceModifier { get; set; }

        /// <inheritdoc/>
        public decimal? MatchUnitSoldPriceModifier { get; set; }

        /// <inheritdoc/>
        public bool? MatchUnitSoldPriceModifierIncludeNull { get; set; }

        /// <inheritdoc/>
        public int? MinUnitSoldPriceModifierMode { get; set; }

        /// <inheritdoc/>
        public int? MaxUnitSoldPriceModifierMode { get; set; }

        /// <inheritdoc/>
        public int? MatchUnitSoldPriceModifierMode { get; set; }

        /// <inheritdoc/>
        public bool? MatchUnitSoldPriceModifierModeIncludeNull { get; set; }
        #endregion

        #region Purchase Order Items Only
        /// <inheritdoc/>
        public DateTime? MinDateReceived { get; set; }

        /// <inheritdoc/>
        public DateTime? MaxDateReceived { get; set; }

        /// <inheritdoc/>
        public DateTime? MatchDateReceived { get; set; }

        /// <inheritdoc/>
        public bool? MatchDateReceivedIncludeNull { get; set; }
        #endregion

        #region Sales Return Items Only
        /// <inheritdoc/>
        public decimal? MinRestockingFeeAmount { get; set; }

        /// <inheritdoc/>
        public decimal? MaxRestockingFeeAmount { get; set; }

        /// <inheritdoc/>
        public decimal? MatchRestockingFeeAmount { get; set; }

        /// <inheritdoc/>
        public bool? MatchRestockingFeeAmountIncludeNull { get; set; }

        /// <inheritdoc/>
        public int? SalesReturnReasonID { get; set; }

        /// <inheritdoc/>
        public bool? SalesReturnReasonIDIncludeNull { get; set; }
        #endregion
    }
}
