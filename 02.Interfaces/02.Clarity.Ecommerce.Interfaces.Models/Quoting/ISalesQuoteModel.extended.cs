// <copyright file="ISalesQuoteModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesQuoteModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for sales quote model.</summary>
    public partial interface ISalesQuoteModel
    {
        /// <summary>Gets or sets the requested ship date.</summary>
        /// <value>The requested ship date.</value>
        DateTime? RequestedShipDate { get; set; }

        /// <summary>Gets or sets the subtotal discounts modifier.</summary>
        /// <value>The subtotal discounts modifier.</value>
        decimal? SubtotalDiscountsModifier { get; set; }

        /// <summary>Gets or sets the subtotal discounts modifier mode.</summary>
        /// <value>The subtotal discounts modifier mode.</value>
        int? SubtotalDiscountsModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal fees modifier.</summary>
        /// <value>The subtotal fees modifier.</value>
        decimal? SubtotalFeesModifier { get; set; }

        /// <summary>Gets or sets the subtotal fees modifier mode.</summary>
        /// <value>The subtotal fees modifier mode.</value>
        int? SubtotalFeesModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal handling modifier.</summary>
        /// <value>The subtotal handling modifier.</value>
        decimal? SubtotalHandlingModifier { get; set; }

        /// <summary>Gets or sets the subtotal handling modifier mode.</summary>
        /// <value>The subtotal handling modifier mode.</value>
        int? SubtotalHandlingModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal shipping modifier.</summary>
        /// <value>The subtotal shipping modifier.</value>
        decimal? SubtotalShippingModifier { get; set; }

        /// <summary>Gets or sets the subtotal shipping modifier mode.</summary>
        /// <value>The subtotal shipping modifier mode.</value>
        int? SubtotalShippingModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal taxes modifier.</summary>
        /// <value>The subtotal taxes modifier.</value>
        decimal? SubtotalTaxesModifier { get; set; }

        /// <summary>Gets or sets the subtotal taxes modifier mode.</summary>
        /// <value>The subtotal taxes modifier mode.</value>
        int? SubtotalTaxesModifierMode { get; set; }

        /// <summary>Gets or sets the identifier of the rate quote.</summary>
        /// <value>The identifier of the rate quote.</value>
        int? RateQuoteID { get; set; }

        /// <summary>Gets or sets the rate quote key.</summary>
        /// <value>The rate quote key.</value>
        string? RateQuoteKey { get; set; }

        /// <summary>Gets or sets the name of the rate quote.</summary>
        /// <value>The name of the rate quote.</value>
        string? RateQuoteName { get; set; }

        /// <summary>Gets or sets the rate quote.</summary>
        /// <value>The rate quote.</value>
        IRateQuoteModel? RateQuote { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as request master.</summary>
        /// <value>The identifier of the sales group as request master.</value>
        int? SalesGroupAsRequestMasterID { get; set; }

        /// <summary>Gets or sets the sales group as request master key.</summary>
        /// <value>The sales group as request master key.</value>
        string? SalesGroupAsRequestMasterKey { get; set; }

        /// <summary>Gets or sets the sales group as request master.</summary>
        /// <value>The sales group as request master.</value>
        ISalesGroupModel? SalesGroupAsRequestMaster { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as request sub.</summary>
        /// <value>The identifier of the sales group as request sub.</value>
        int? SalesGroupAsRequestSubID { get; set; }

        /// <summary>Gets or sets the sales group as request sub key.</summary>
        /// <value>The sales group as request sub key.</value>
        string? SalesGroupAsRequestSubKey { get; set; }

        /// <summary>Gets or sets the sales group as request sub.</summary>
        /// <value>The sales group as request sub.</value>
        ISalesGroupModel? SalesGroupAsRequestSub { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as response master.</summary>
        /// <value>The identifier of the sales group as response master.</value>
        int? SalesGroupAsResponseMasterID { get; set; }

        /// <summary>Gets or sets the sales group as response master key.</summary>
        /// <value>The sales group as response master key.</value>
        string? SalesGroupAsResponseMasterKey { get; set; }

        /// <summary>Gets or sets the sales group as response master.</summary>
        /// <value>The sales group as response master.</value>
        ISalesGroupModel? SalesGroupAsResponseMaster { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as response sub.</summary>
        /// <value>The identifier of the sales group as response sub.</value>
        int? SalesGroupAsResponseSubID { get; set; }

        /// <summary>Gets or sets the sales group as response sub key.</summary>
        /// <value>The sales group as response sub key.</value>
        string? SalesGroupAsResponseSubKey { get; set; }

        /// <summary>Gets or sets the sales group as response sub.</summary>
        /// <value>The sales group as response sub.</value>
        ISalesGroupModel? SalesGroupAsResponseSub { get; set; }

        /// <summary>Gets or sets the identifier of the response as store.</summary>
        /// <value>The identifier of the response as store.</value>
        int? ResponseAsStoreID { get; set; }

        /// <summary>Gets or sets the response as store key.</summary>
        /// <value>The response as store key.</value>
        string? ResponseAsStoreKey { get; set; }

        /// <summary>Gets or sets the name of the response as store.</summary>
        /// <value>The name of the response as store.</value>
        string? ResponseAsStoreName { get; set; }

        /// <summary>Gets or sets the response as store.</summary>
        /// <value>The response as store.</value>
        IStoreModel? ResponseAsStore { get; set; }

        /// <summary>Gets or sets the identifier of the response as vendor.</summary>
        /// <value>The identifier of the response as vendor.</value>
        int? ResponseAsVendorID { get; set; }

        /// <summary>Gets or sets the response as vendor key.</summary>
        /// <value>The response as vendor key.</value>
        string? ResponseAsVendorKey { get; set; }

        /// <summary>Gets or sets the name of the response as vendor.</summary>
        /// <value>The name of the response as vendor.</value>
        string? ResponseAsVendorName { get; set; }

        /// <summary>Gets or sets the response as vendor.</summary>
        /// <value>The response as vendor.</value>
        IVendorModel? ResponseAsVendor { get; set; }

        #region Associated Objects
        /// <summary>Gets or sets the associated sales orders.</summary>
        /// <value>The associated sales orders.</value>
        List<ISalesQuoteSalesOrderModel>? AssociatedSalesOrders { get; set; }

        /// <summary>Gets or sets the categories the sales quote belongs to.</summary>
        /// <value>The sales quote categories.</value>
        List<ISalesQuoteCategoryModel>? SalesQuoteCategories { get; set; }
        #endregion
    }
}
