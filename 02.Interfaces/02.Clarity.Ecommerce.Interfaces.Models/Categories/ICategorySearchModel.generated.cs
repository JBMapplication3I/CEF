// <autogenerated>
// <copyright file="Category.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ISearchModel Interfaces generated to provide base setups.</summary>
// <remarks>This file was auto-generated by ISearchModels.tt, changes to this
// file will be overwritten automatically when the T4 template is run again.</remarks>
// </autogenerated>
// ReSharper disable BadEmptyBracesLineBreaks, PartialTypeWithSinglePart, RedundantExtendsListEntry, RedundantUsingDirective
#pragma warning disable IDE0005_gen
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for category model.</summary>
    /// <seealso cref="INameableBaseSearchModel"/>
    /// <seealso cref="IAmFilterableByBrandSearchModel"/>
    /// <seealso cref="IAmFilterableByFranchiseSearchModel"/>
    /// <seealso cref="IAmFilterableByProductSearchModel"/>
    /// <seealso cref="IAmFilterableByStoreSearchModel"/>
    /// <seealso cref="IHaveATypeBaseSearchModel"/>
    /// <seealso cref="IHaveAParentBaseSearchModel"/>
    /// <seealso cref="IHaveSeoBaseSearchModel"/>
    public partial interface ICategorySearchModel
        : INameableBaseSearchModel
            , IAmFilterableByBrandSearchModel
            , IAmFilterableByFranchiseSearchModel
            , IAmFilterableByProductSearchModel
            , IAmFilterableByStoreSearchModel
            , IHaveATypeBaseSearchModel
            , IHaveAParentBaseSearchModel
            , IHaveSeoBaseSearchModel
    {
        /// <summary>Gets or sets the IncludeInMenu.</summary>
        /// <value>The IncludeInMenu.</value>
        bool? IncludeInMenu { get; set; }

        /// <summary>Gets or sets the IsVisible.</summary>
        /// <value>The IsVisible.</value>
        bool? IsVisible { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountOverrideFeeIsPercent.</summary>
        /// <value>The MinimumOrderDollarAmountOverrideFeeIsPercent.</value>
        bool? MinimumOrderDollarAmountOverrideFeeIsPercent { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountOverrideFeeIsPercent.</summary>
        /// <value>The MinimumOrderQuantityAmountOverrideFeeIsPercent.</value>
        bool? MinimumOrderQuantityAmountOverrideFeeIsPercent { get; set; }

        /// <summary>Gets or sets the minimum HandlingCharge.</summary>
        /// <value>The minimum HandlingCharge.</value>
        decimal? MinHandlingCharge { get; set; }

        /// <summary>Gets or sets the maximum HandlingCharge.</summary>
        /// <value>The maximum HandlingCharge.</value>
        decimal? MaxHandlingCharge { get; set; }

        /// <summary>Gets or sets the match HandlingCharge.</summary>
        /// <value>The match HandlingCharge.</value>
        decimal? MatchHandlingCharge { get; set; }

        /// <summary>Gets or sets the HandlingCharge when matching must include nulls.</summary>
        /// <value>The HandlingCharge when matching must include nulls.</value>
        bool? MatchHandlingChargeIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumForFreeShippingDollarAmount.</summary>
        /// <value>The minimum MinimumForFreeShippingDollarAmount.</value>
        decimal? MinMinimumForFreeShippingDollarAmount { get; set; }

        /// <summary>Gets or sets the maximum MinimumForFreeShippingDollarAmount.</summary>
        /// <value>The maximum MinimumForFreeShippingDollarAmount.</value>
        decimal? MaxMinimumForFreeShippingDollarAmount { get; set; }

        /// <summary>Gets or sets the match MinimumForFreeShippingDollarAmount.</summary>
        /// <value>The match MinimumForFreeShippingDollarAmount.</value>
        decimal? MatchMinimumForFreeShippingDollarAmount { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmount when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingDollarAmount when matching must include nulls.</value>
        bool? MatchMinimumForFreeShippingDollarAmountIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumForFreeShippingDollarAmountAfter.</summary>
        /// <value>The minimum MinimumForFreeShippingDollarAmountAfter.</value>
        decimal? MinMinimumForFreeShippingDollarAmountAfter { get; set; }

        /// <summary>Gets or sets the maximum MinimumForFreeShippingDollarAmountAfter.</summary>
        /// <value>The maximum MinimumForFreeShippingDollarAmountAfter.</value>
        decimal? MaxMinimumForFreeShippingDollarAmountAfter { get; set; }

        /// <summary>Gets or sets the match MinimumForFreeShippingDollarAmountAfter.</summary>
        /// <value>The match MinimumForFreeShippingDollarAmountAfter.</value>
        decimal? MatchMinimumForFreeShippingDollarAmountAfter { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountAfter when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountAfter when matching must include nulls.</value>
        bool? MatchMinimumForFreeShippingDollarAmountAfterIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountBufferCategoryID.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountBufferCategoryID.</value>
        int? MinimumForFreeShippingDollarAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountBufferCategoryID when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountBufferCategoryID when matching must include nulls.</value>
        bool? MinimumForFreeShippingDollarAmountBufferCategoryIDIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountBufferProductID.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountBufferProductID.</value>
        int? MinimumForFreeShippingDollarAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountBufferProductID when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountBufferProductID when matching must include nulls.</value>
        bool? MinimumForFreeShippingDollarAmountBufferProductIDIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumForFreeShippingQuantityAmount.</summary>
        /// <value>The minimum MinimumForFreeShippingQuantityAmount.</value>
        decimal? MinMinimumForFreeShippingQuantityAmount { get; set; }

        /// <summary>Gets or sets the maximum MinimumForFreeShippingQuantityAmount.</summary>
        /// <value>The maximum MinimumForFreeShippingQuantityAmount.</value>
        decimal? MaxMinimumForFreeShippingQuantityAmount { get; set; }

        /// <summary>Gets or sets the match MinimumForFreeShippingQuantityAmount.</summary>
        /// <value>The match MinimumForFreeShippingQuantityAmount.</value>
        decimal? MatchMinimumForFreeShippingQuantityAmount { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmount when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmount when matching must include nulls.</value>
        bool? MatchMinimumForFreeShippingQuantityAmountIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumForFreeShippingQuantityAmountAfter.</summary>
        /// <value>The minimum MinimumForFreeShippingQuantityAmountAfter.</value>
        decimal? MinMinimumForFreeShippingQuantityAmountAfter { get; set; }

        /// <summary>Gets or sets the maximum MinimumForFreeShippingQuantityAmountAfter.</summary>
        /// <value>The maximum MinimumForFreeShippingQuantityAmountAfter.</value>
        decimal? MaxMinimumForFreeShippingQuantityAmountAfter { get; set; }

        /// <summary>Gets or sets the match MinimumForFreeShippingQuantityAmountAfter.</summary>
        /// <value>The match MinimumForFreeShippingQuantityAmountAfter.</value>
        decimal? MatchMinimumForFreeShippingQuantityAmountAfter { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountAfter when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountAfter when matching must include nulls.</value>
        bool? MatchMinimumForFreeShippingQuantityAmountAfterIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountBufferCategoryID.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountBufferCategoryID.</value>
        int? MinimumForFreeShippingQuantityAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountBufferCategoryID when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountBufferCategoryID when matching must include nulls.</value>
        bool? MinimumForFreeShippingQuantityAmountBufferCategoryIDIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountBufferProductID.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountBufferProductID.</value>
        int? MinimumForFreeShippingQuantityAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountBufferProductID when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountBufferProductID when matching must include nulls.</value>
        bool? MinimumForFreeShippingQuantityAmountBufferProductIDIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumOrderDollarAmount.</summary>
        /// <value>The minimum MinimumOrderDollarAmount.</value>
        decimal? MinMinimumOrderDollarAmount { get; set; }

        /// <summary>Gets or sets the maximum MinimumOrderDollarAmount.</summary>
        /// <value>The maximum MinimumOrderDollarAmount.</value>
        decimal? MaxMinimumOrderDollarAmount { get; set; }

        /// <summary>Gets or sets the match MinimumOrderDollarAmount.</summary>
        /// <value>The match MinimumOrderDollarAmount.</value>
        decimal? MatchMinimumOrderDollarAmount { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmount when matching must include nulls.</summary>
        /// <value>The MinimumOrderDollarAmount when matching must include nulls.</value>
        bool? MatchMinimumOrderDollarAmountIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumOrderDollarAmountAfter.</summary>
        /// <value>The minimum MinimumOrderDollarAmountAfter.</value>
        decimal? MinMinimumOrderDollarAmountAfter { get; set; }

        /// <summary>Gets or sets the maximum MinimumOrderDollarAmountAfter.</summary>
        /// <value>The maximum MinimumOrderDollarAmountAfter.</value>
        decimal? MaxMinimumOrderDollarAmountAfter { get; set; }

        /// <summary>Gets or sets the match MinimumOrderDollarAmountAfter.</summary>
        /// <value>The match MinimumOrderDollarAmountAfter.</value>
        decimal? MatchMinimumOrderDollarAmountAfter { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountAfter when matching must include nulls.</summary>
        /// <value>The MinimumOrderDollarAmountAfter when matching must include nulls.</value>
        bool? MatchMinimumOrderDollarAmountAfterIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountBufferCategoryID.</summary>
        /// <value>The MinimumOrderDollarAmountBufferCategoryID.</value>
        int? MinimumOrderDollarAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountBufferCategoryID when matching must include nulls.</summary>
        /// <value>The MinimumOrderDollarAmountBufferCategoryID when matching must include nulls.</value>
        bool? MinimumOrderDollarAmountBufferCategoryIDIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountBufferProductID.</summary>
        /// <value>The MinimumOrderDollarAmountBufferProductID.</value>
        int? MinimumOrderDollarAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountBufferProductID when matching must include nulls.</summary>
        /// <value>The MinimumOrderDollarAmountBufferProductID when matching must include nulls.</value>
        bool? MinimumOrderDollarAmountBufferProductIDIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumOrderDollarAmountOverrideFee.</summary>
        /// <value>The minimum MinimumOrderDollarAmountOverrideFee.</value>
        decimal? MinMinimumOrderDollarAmountOverrideFee { get; set; }

        /// <summary>Gets or sets the maximum MinimumOrderDollarAmountOverrideFee.</summary>
        /// <value>The maximum MinimumOrderDollarAmountOverrideFee.</value>
        decimal? MaxMinimumOrderDollarAmountOverrideFee { get; set; }

        /// <summary>Gets or sets the match MinimumOrderDollarAmountOverrideFee.</summary>
        /// <value>The match MinimumOrderDollarAmountOverrideFee.</value>
        decimal? MatchMinimumOrderDollarAmountOverrideFee { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountOverrideFee when matching must include nulls.</summary>
        /// <value>The MinimumOrderDollarAmountOverrideFee when matching must include nulls.</value>
        bool? MatchMinimumOrderDollarAmountOverrideFeeIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumOrderQuantityAmount.</summary>
        /// <value>The minimum MinimumOrderQuantityAmount.</value>
        decimal? MinMinimumOrderQuantityAmount { get; set; }

        /// <summary>Gets or sets the maximum MinimumOrderQuantityAmount.</summary>
        /// <value>The maximum MinimumOrderQuantityAmount.</value>
        decimal? MaxMinimumOrderQuantityAmount { get; set; }

        /// <summary>Gets or sets the match MinimumOrderQuantityAmount.</summary>
        /// <value>The match MinimumOrderQuantityAmount.</value>
        decimal? MatchMinimumOrderQuantityAmount { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmount when matching must include nulls.</summary>
        /// <value>The MinimumOrderQuantityAmount when matching must include nulls.</value>
        bool? MatchMinimumOrderQuantityAmountIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumOrderQuantityAmountAfter.</summary>
        /// <value>The minimum MinimumOrderQuantityAmountAfter.</value>
        decimal? MinMinimumOrderQuantityAmountAfter { get; set; }

        /// <summary>Gets or sets the maximum MinimumOrderQuantityAmountAfter.</summary>
        /// <value>The maximum MinimumOrderQuantityAmountAfter.</value>
        decimal? MaxMinimumOrderQuantityAmountAfter { get; set; }

        /// <summary>Gets or sets the match MinimumOrderQuantityAmountAfter.</summary>
        /// <value>The match MinimumOrderQuantityAmountAfter.</value>
        decimal? MatchMinimumOrderQuantityAmountAfter { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountAfter when matching must include nulls.</summary>
        /// <value>The MinimumOrderQuantityAmountAfter when matching must include nulls.</value>
        bool? MatchMinimumOrderQuantityAmountAfterIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountBufferCategoryID.</summary>
        /// <value>The MinimumOrderQuantityAmountBufferCategoryID.</value>
        int? MinimumOrderQuantityAmountBufferCategoryID { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountBufferCategoryID when matching must include nulls.</summary>
        /// <value>The MinimumOrderQuantityAmountBufferCategoryID when matching must include nulls.</value>
        bool? MinimumOrderQuantityAmountBufferCategoryIDIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountBufferProductID.</summary>
        /// <value>The MinimumOrderQuantityAmountBufferProductID.</value>
        int? MinimumOrderQuantityAmountBufferProductID { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountBufferProductID when matching must include nulls.</summary>
        /// <value>The MinimumOrderQuantityAmountBufferProductID when matching must include nulls.</value>
        bool? MinimumOrderQuantityAmountBufferProductIDIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum MinimumOrderQuantityAmountOverrideFee.</summary>
        /// <value>The minimum MinimumOrderQuantityAmountOverrideFee.</value>
        decimal? MinMinimumOrderQuantityAmountOverrideFee { get; set; }

        /// <summary>Gets or sets the maximum MinimumOrderQuantityAmountOverrideFee.</summary>
        /// <value>The maximum MinimumOrderQuantityAmountOverrideFee.</value>
        decimal? MaxMinimumOrderQuantityAmountOverrideFee { get; set; }

        /// <summary>Gets or sets the match MinimumOrderQuantityAmountOverrideFee.</summary>
        /// <value>The match MinimumOrderQuantityAmountOverrideFee.</value>
        decimal? MatchMinimumOrderQuantityAmountOverrideFee { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountOverrideFee when matching must include nulls.</summary>
        /// <value>The MinimumOrderQuantityAmountOverrideFee when matching must include nulls.</value>
        bool? MatchMinimumOrderQuantityAmountOverrideFeeIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum RestockingFeeAmount.</summary>
        /// <value>The minimum RestockingFeeAmount.</value>
        decimal? MinRestockingFeeAmount { get; set; }

        /// <summary>Gets or sets the maximum RestockingFeeAmount.</summary>
        /// <value>The maximum RestockingFeeAmount.</value>
        decimal? MaxRestockingFeeAmount { get; set; }

        /// <summary>Gets or sets the match RestockingFeeAmount.</summary>
        /// <value>The match RestockingFeeAmount.</value>
        decimal? MatchRestockingFeeAmount { get; set; }

        /// <summary>Gets or sets the RestockingFeeAmount when matching must include nulls.</summary>
        /// <value>The RestockingFeeAmount when matching must include nulls.</value>
        bool? MatchRestockingFeeAmountIncludeNull { get; set; }

        /// <summary>Gets or sets the RestockingFeeAmountCurrencyID.</summary>
        /// <value>The RestockingFeeAmountCurrencyID.</value>
        int? RestockingFeeAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the RestockingFeeAmountCurrencyID when matching must include nulls.</summary>
        /// <value>The RestockingFeeAmountCurrencyID when matching must include nulls.</value>
        bool? RestockingFeeAmountCurrencyIDIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum RestockingFeePercent.</summary>
        /// <value>The minimum RestockingFeePercent.</value>
        decimal? MinRestockingFeePercent { get; set; }

        /// <summary>Gets or sets the maximum RestockingFeePercent.</summary>
        /// <value>The maximum RestockingFeePercent.</value>
        decimal? MaxRestockingFeePercent { get; set; }

        /// <summary>Gets or sets the match RestockingFeePercent.</summary>
        /// <value>The match RestockingFeePercent.</value>
        decimal? MatchRestockingFeePercent { get; set; }

        /// <summary>Gets or sets the RestockingFeePercent when matching must include nulls.</summary>
        /// <value>The RestockingFeePercent when matching must include nulls.</value>
        bool? MatchRestockingFeePercentIncludeNull { get; set; }

        /// <summary>Gets or sets the minimum SortOrder.</summary>
        /// <value>The minimum SortOrder.</value>
        int? MinSortOrder { get; set; }

        /// <summary>Gets or sets the maximum SortOrder.</summary>
        /// <value>The maximum SortOrder.</value>
        int? MaxSortOrder { get; set; }

        /// <summary>Gets or sets the match SortOrder.</summary>
        /// <value>The match SortOrder.</value>
        int? MatchSortOrder { get; set; }

        /// <summary>Gets or sets the SortOrder when matching must include nulls.</summary>
        /// <value>The SortOrder when matching must include nulls.</value>
        bool? MatchSortOrderIncludeNull { get; set; }

        /// <summary>Gets or sets the DisplayName.</summary>
        /// <value>The DisplayName.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the match DisplayName strict requirement.</summary>
        /// <value>The match DisplayName strict requirement.</value>
        bool? DisplayNameStrict { get; set; }

        /// <summary>Gets or sets the DisplayName when matching must include nulls.</summary>
        /// <value>The DisplayName when matching must include nulls.</value>
        bool? DisplayNameIncludeNull { get; set; }

        /// <summary>Gets or sets the FooterContent.</summary>
        /// <value>The FooterContent.</value>
        string? FooterContent { get; set; }

        /// <summary>Gets or sets the match FooterContent strict requirement.</summary>
        /// <value>The match FooterContent strict requirement.</value>
        bool? FooterContentStrict { get; set; }

        /// <summary>Gets or sets the FooterContent when matching must include nulls.</summary>
        /// <value>The FooterContent when matching must include nulls.</value>
        bool? FooterContentIncludeNull { get; set; }

        /// <summary>Gets or sets the HeaderContent.</summary>
        /// <value>The HeaderContent.</value>
        string? HeaderContent { get; set; }

        /// <summary>Gets or sets the match HeaderContent strict requirement.</summary>
        /// <value>The match HeaderContent strict requirement.</value>
        bool? HeaderContentStrict { get; set; }

        /// <summary>Gets or sets the HeaderContent when matching must include nulls.</summary>
        /// <value>The HeaderContent when matching must include nulls.</value>
        bool? HeaderContentIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage.</value>
        string? MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage { get; set; }

        /// <summary>Gets or sets the match MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage strict requirement.</summary>
        /// <value>The match MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage strict requirement.</value>
        bool? MinimumForFreeShippingDollarAmountIgnoredAcceptedMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountIgnoredAcceptedMessage when matching must include nulls.</value>
        bool? MinimumForFreeShippingDollarAmountIgnoredAcceptedMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountWarningMessage.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountWarningMessage.</value>
        string? MinimumForFreeShippingDollarAmountWarningMessage { get; set; }

        /// <summary>Gets or sets the match MinimumForFreeShippingDollarAmountWarningMessage strict requirement.</summary>
        /// <value>The match MinimumForFreeShippingDollarAmountWarningMessage strict requirement.</value>
        bool? MinimumForFreeShippingDollarAmountWarningMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingDollarAmountWarningMessage when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingDollarAmountWarningMessage when matching must include nulls.</value>
        bool? MinimumForFreeShippingDollarAmountWarningMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage.</value>
        string? MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage { get; set; }

        /// <summary>Gets or sets the match MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage strict requirement.</summary>
        /// <value>The match MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage strict requirement.</value>
        bool? MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessage when matching must include nulls.</value>
        bool? MinimumForFreeShippingQuantityAmountIgnoredAcceptedMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountWarningMessage.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountWarningMessage.</value>
        string? MinimumForFreeShippingQuantityAmountWarningMessage { get; set; }

        /// <summary>Gets or sets the match MinimumForFreeShippingQuantityAmountWarningMessage strict requirement.</summary>
        /// <value>The match MinimumForFreeShippingQuantityAmountWarningMessage strict requirement.</value>
        bool? MinimumForFreeShippingQuantityAmountWarningMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumForFreeShippingQuantityAmountWarningMessage when matching must include nulls.</summary>
        /// <value>The MinimumForFreeShippingQuantityAmountWarningMessage when matching must include nulls.</value>
        bool? MinimumForFreeShippingQuantityAmountWarningMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountOverrideFeeAcceptedMessage.</summary>
        /// <value>The MinimumOrderDollarAmountOverrideFeeAcceptedMessage.</value>
        string? MinimumOrderDollarAmountOverrideFeeAcceptedMessage { get; set; }

        /// <summary>Gets or sets the match MinimumOrderDollarAmountOverrideFeeAcceptedMessage strict requirement.</summary>
        /// <value>The match MinimumOrderDollarAmountOverrideFeeAcceptedMessage strict requirement.</value>
        bool? MinimumOrderDollarAmountOverrideFeeAcceptedMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountOverrideFeeAcceptedMessage when matching must include nulls.</summary>
        /// <value>The MinimumOrderDollarAmountOverrideFeeAcceptedMessage when matching must include nulls.</value>
        bool? MinimumOrderDollarAmountOverrideFeeAcceptedMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountOverrideFeeWarningMessage.</summary>
        /// <value>The MinimumOrderDollarAmountOverrideFeeWarningMessage.</value>
        string? MinimumOrderDollarAmountOverrideFeeWarningMessage { get; set; }

        /// <summary>Gets or sets the match MinimumOrderDollarAmountOverrideFeeWarningMessage strict requirement.</summary>
        /// <value>The match MinimumOrderDollarAmountOverrideFeeWarningMessage strict requirement.</value>
        bool? MinimumOrderDollarAmountOverrideFeeWarningMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountOverrideFeeWarningMessage when matching must include nulls.</summary>
        /// <value>The MinimumOrderDollarAmountOverrideFeeWarningMessage when matching must include nulls.</value>
        bool? MinimumOrderDollarAmountOverrideFeeWarningMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountWarningMessage.</summary>
        /// <value>The MinimumOrderDollarAmountWarningMessage.</value>
        string? MinimumOrderDollarAmountWarningMessage { get; set; }

        /// <summary>Gets or sets the match MinimumOrderDollarAmountWarningMessage strict requirement.</summary>
        /// <value>The match MinimumOrderDollarAmountWarningMessage strict requirement.</value>
        bool? MinimumOrderDollarAmountWarningMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumOrderDollarAmountWarningMessage when matching must include nulls.</summary>
        /// <value>The MinimumOrderDollarAmountWarningMessage when matching must include nulls.</value>
        bool? MinimumOrderDollarAmountWarningMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountOverrideFeeAcceptedMessage.</summary>
        /// <value>The MinimumOrderQuantityAmountOverrideFeeAcceptedMessage.</value>
        string? MinimumOrderQuantityAmountOverrideFeeAcceptedMessage { get; set; }

        /// <summary>Gets or sets the match MinimumOrderQuantityAmountOverrideFeeAcceptedMessage strict requirement.</summary>
        /// <value>The match MinimumOrderQuantityAmountOverrideFeeAcceptedMessage strict requirement.</value>
        bool? MinimumOrderQuantityAmountOverrideFeeAcceptedMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountOverrideFeeAcceptedMessage when matching must include nulls.</summary>
        /// <value>The MinimumOrderQuantityAmountOverrideFeeAcceptedMessage when matching must include nulls.</value>
        bool? MinimumOrderQuantityAmountOverrideFeeAcceptedMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountOverrideFeeWarningMessage.</summary>
        /// <value>The MinimumOrderQuantityAmountOverrideFeeWarningMessage.</value>
        string? MinimumOrderQuantityAmountOverrideFeeWarningMessage { get; set; }

        /// <summary>Gets or sets the match MinimumOrderQuantityAmountOverrideFeeWarningMessage strict requirement.</summary>
        /// <value>The match MinimumOrderQuantityAmountOverrideFeeWarningMessage strict requirement.</value>
        bool? MinimumOrderQuantityAmountOverrideFeeWarningMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountOverrideFeeWarningMessage when matching must include nulls.</summary>
        /// <value>The MinimumOrderQuantityAmountOverrideFeeWarningMessage when matching must include nulls.</value>
        bool? MinimumOrderQuantityAmountOverrideFeeWarningMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountWarningMessage.</summary>
        /// <value>The MinimumOrderQuantityAmountWarningMessage.</value>
        string? MinimumOrderQuantityAmountWarningMessage { get; set; }

        /// <summary>Gets or sets the match MinimumOrderQuantityAmountWarningMessage strict requirement.</summary>
        /// <value>The match MinimumOrderQuantityAmountWarningMessage strict requirement.</value>
        bool? MinimumOrderQuantityAmountWarningMessageStrict { get; set; }

        /// <summary>Gets or sets the MinimumOrderQuantityAmountWarningMessage when matching must include nulls.</summary>
        /// <value>The MinimumOrderQuantityAmountWarningMessage when matching must include nulls.</value>
        bool? MinimumOrderQuantityAmountWarningMessageIncludeNull { get; set; }

        /// <summary>Gets or sets the RequiresRoles.</summary>
        /// <value>The RequiresRoles.</value>
        string? RequiresRoles { get; set; }

        /// <summary>Gets or sets the match RequiresRoles strict requirement.</summary>
        /// <value>The match RequiresRoles strict requirement.</value>
        bool? RequiresRolesStrict { get; set; }

        /// <summary>Gets or sets the RequiresRoles when matching must include nulls.</summary>
        /// <value>The RequiresRoles when matching must include nulls.</value>
        bool? RequiresRolesIncludeNull { get; set; }

        /// <summary>Gets or sets the RequiresRolesAlt.</summary>
        /// <value>The RequiresRolesAlt.</value>
        string? RequiresRolesAlt { get; set; }

        /// <summary>Gets or sets the match RequiresRolesAlt strict requirement.</summary>
        /// <value>The match RequiresRolesAlt strict requirement.</value>
        bool? RequiresRolesAltStrict { get; set; }

        /// <summary>Gets or sets the RequiresRolesAlt when matching must include nulls.</summary>
        /// <value>The RequiresRolesAlt when matching must include nulls.</value>
        bool? RequiresRolesAltIncludeNull { get; set; }

        /// <summary>Gets or sets the SidebarContent.</summary>
        /// <value>The SidebarContent.</value>
        string? SidebarContent { get; set; }

        /// <summary>Gets or sets the match SidebarContent strict requirement.</summary>
        /// <value>The match SidebarContent strict requirement.</value>
        bool? SidebarContentStrict { get; set; }

        /// <summary>Gets or sets the SidebarContent when matching must include nulls.</summary>
        /// <value>The SidebarContent when matching must include nulls.</value>
        bool? SidebarContentIncludeNull { get; set; }
    }
}
