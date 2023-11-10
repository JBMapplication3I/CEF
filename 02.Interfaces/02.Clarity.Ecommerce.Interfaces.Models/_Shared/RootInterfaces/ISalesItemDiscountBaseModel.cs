// <copyright file="ISalesItemDiscountBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesItemDiscountBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for sales item discount base model.</summary>
    /// <typeparam name="TSalesItemModel">Type of the sales item model.</typeparam>
    /// <seealso cref="INameableBaseModel"/>
    public interface ISalesItemDiscountBaseModel<TSalesItemModel>
        : INameableBaseModel
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal Quantity { get; set; }

        /// <summary>Gets or sets the quantity back ordered.</summary>
        /// <value>The quantity back ordered.</value>
        decimal? QuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the unit core price.</summary>
        /// <value>The unit core price.</value>
        decimal UnitCorePrice { get; set; }

        /// <summary>Gets or sets the unit sold price.</summary>
        /// <value>The unit sold price.</value>
        decimal UnitSoldPrice { get; set; }

        /// <summary>Gets or sets the extended price.</summary>
        /// <value>The extended price.</value>
        decimal ExtendedPrice { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales item.</summary>
        /// <value>The identifier of the sales item.</value>
        int SalesItemID { get; set; }

        /// <summary>Gets or sets the sales item.</summary>
        /// <value>The sales item.</value>
        TSalesItemModel? SalesItem { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        IProductModel? Product { get; set; }

        /// <summary>Gets or sets the identifier of the discount.</summary>
        /// <value>The identifier of the discount.</value>
        int DiscountID { get; set; }

        /// <summary>Gets or sets the discount.</summary>
        /// <value>The discount.</value>
        IDiscountModel? Discount { get; set; }
        #endregion
    }
}
