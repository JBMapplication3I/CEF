// <copyright file="SalesQuoteModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the sales quote.</summary>
    /// <seealso cref="SalesCollectionBaseModel{ITypeModel, TypeModel, ISalesQuoteFileModel, SalesQuoteFileModel, ISalesQuoteContactModel, SalesQuoteContactModel, ISalesQuoteEventModel, SalesQuoteEventModel, IAppliedSalesQuoteDiscountModel, AppliedSalesQuoteDiscountModel, IAppliedSalesQuoteItemDiscountModel, AppliedSalesQuoteItemDiscountModel}"/>
    /// <seealso cref="ISalesQuoteModel"/>
    public partial class SalesQuoteModel
    {
        #region SalesQuote Properties
        /// <inheritdoc/>
        public DateTime? RequestedShipDate { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalDiscountsModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalDiscountsModifierMode { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalFeesModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalFeesModifierMode { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalHandlingModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalHandlingModifierMode { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalShippingModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalShippingModifierMode { get; set; }

        /// <inheritdoc/>
        public decimal? SubtotalTaxesModifier { get; set; }

        /// <inheritdoc/>
        public int? SubtotalTaxesModifierMode { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? RateQuoteID { get; set; }

        /// <inheritdoc/>
        public string? RateQuoteKey { get; set; }

        /// <inheritdoc/>
        public string? RateQuoteName { get; set; }

        /// <inheritdoc cref="ISalesQuoteModel.RateQuote"/>
        public RateQuoteModel? RateQuote { get; set; }

        /// <inheritdoc/>
        IRateQuoteModel? ISalesQuoteModel.RateQuote { get => RateQuote; set => RateQuote = (RateQuoteModel?)value; }

        /// <inheritdoc/>
        public int? SalesGroupAsRequestMasterID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupAsRequestMasterKey { get; set; }

        /// <inheritdoc cref="ISalesQuoteModel.SalesGroupAsRequestMaster"/>
        public SalesGroupModel? SalesGroupAsRequestMaster { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISalesQuoteModel.SalesGroupAsRequestMaster { get => SalesGroupAsRequestMaster; set => SalesGroupAsRequestMaster = (SalesGroupModel?)value; }

        /// <inheritdoc/>
        public int? SalesGroupAsRequestSubID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupAsRequestSubKey { get; set; }

        /// <inheritdoc cref="ISalesQuoteModel.SalesGroupAsRequestSub"/>
        public SalesGroupModel? SalesGroupAsRequestSub { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISalesQuoteModel.SalesGroupAsRequestSub { get => SalesGroupAsRequestSub; set => SalesGroupAsRequestSub = (SalesGroupModel?)value; }

        /// <inheritdoc/>
        public int? SalesGroupAsResponseMasterID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupAsResponseMasterKey { get; set; }

        /// <inheritdoc cref="ISalesQuoteModel.SalesGroupAsResponseMaster"/>
        public SalesGroupModel? SalesGroupAsResponseMaster { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISalesQuoteModel.SalesGroupAsResponseMaster { get => SalesGroupAsResponseMaster; set => SalesGroupAsResponseMaster = (SalesGroupModel?)value; }

        /// <inheritdoc/>
        public int? SalesGroupAsResponseSubID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupAsResponseSubKey { get; set; }

        /// <inheritdoc cref="ISalesQuoteModel.SalesGroupAsResponseSub"/>
        public SalesGroupModel? SalesGroupAsResponseSub { get; set; }

        /// <inheritdoc/>
        ISalesGroupModel? ISalesQuoteModel.SalesGroupAsResponseSub { get => SalesGroupAsResponseSub; set => SalesGroupAsResponseSub = (SalesGroupModel?)value; }

        /// <inheritdoc/>
        public int? ResponseAsStoreID { get; set; }

        /// <inheritdoc/>
        public string? ResponseAsStoreKey { get; set; }

        /// <inheritdoc/>
        public string? ResponseAsStoreName { get; set; }

        /// <inheritdoc cref="ISalesQuoteModel.ResponseAsStore"/>
        public StoreModel? ResponseAsStore { get; set; }

        /// <inheritdoc/>
        IStoreModel? ISalesQuoteModel.ResponseAsStore { get => ResponseAsStore; set => ResponseAsStore = (StoreModel?)value; }

        /// <inheritdoc/>
        public int? ResponseAsVendorID { get; set; }

        /// <inheritdoc/>
        public string? ResponseAsVendorKey { get; set; }

        /// <inheritdoc/>
        public string? ResponseAsVendorName { get; set; }

        /// <inheritdoc cref="ISalesQuoteModel.ResponseAsVendor"/>
        public VendorModel? ResponseAsVendor { get; set; }

        /// <inheritdoc/>
        IVendorModel? ISalesQuoteModel.ResponseAsVendor { get => ResponseAsVendor; set => ResponseAsVendor = (VendorModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ISalesQuoteModel.SalesQuoteCategories"/>
        public List<SalesQuoteCategoryModel>? SalesQuoteCategories { get; set; }

        /// <inheritdoc/>
        List<ISalesQuoteCategoryModel>? ISalesQuoteModel.SalesQuoteCategories { get => SalesQuoteCategories?.ToList<ISalesQuoteCategoryModel>(); set => SalesQuoteCategories = value?.Cast<SalesQuoteCategoryModel>().ToList(); }

        /// <inheritdoc cref="ISalesQuoteModel.AssociatedSalesOrders"/>
        public List<SalesQuoteSalesOrderModel>? AssociatedSalesOrders { get; set; }

        /// <inheritdoc/>
        List<ISalesQuoteSalesOrderModel>? ISalesQuoteModel.AssociatedSalesOrders { get => AssociatedSalesOrders?.ToList<ISalesQuoteSalesOrderModel>(); set => AssociatedSalesOrders = value?.Cast<SalesQuoteSalesOrderModel>().ToList(); }
        #endregion
    }
}
