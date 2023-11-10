// <copyright file="SalesItemDiscountBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item discount base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the sales item discount base.</summary>
    /// <typeparam name="TSalesItemModel">Type of the sales item model.</typeparam>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ISalesItemDiscountBaseModel{TSalesItemModel}"/>
    public abstract class SalesItemDiscountBaseModel<TSalesItemModel>
        : NameableBaseModel, ISalesItemDiscountBaseModel<TSalesItemModel>
    {
        /// <inheritdoc/>
        public decimal Quantity { get; set; }

        /// <inheritdoc/>
        public decimal? QuantityBackOrdered { get; set; }

        /// <inheritdoc/>
        public decimal UnitCorePrice { get; set; }

        /// <inheritdoc/>
        public decimal UnitSoldPrice { get; set; }

        /// <inheritdoc/>
        public decimal ExtendedPrice { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int SalesItemID { get; set; }

        /// <inheritdoc/>
        public abstract TSalesItemModel? SalesItem { get; set; }

        /// <inheritdoc/>
        public int ProductID { get; set; }

        /// <inheritdoc cref="ISalesItemDiscountBaseModel{TSalesItemModel}.Product"/>
        public ProductModel? Product { get; set; }

        /// <inheritdoc/>
        IProductModel? ISalesItemDiscountBaseModel<TSalesItemModel>.Product { get => Product; set => Product = (ProductModel?)value; }

        /// <inheritdoc/>
        public int DiscountID { get; set; }

        /// <inheritdoc cref="ISalesItemDiscountBaseModel{TSalesItemModel}.Discount"/>
        public virtual DiscountModel? Discount { get; set; }

        /// <inheritdoc/>
        IDiscountModel? ISalesItemDiscountBaseModel<TSalesItemModel>.Discount { get => Discount; set => Discount = (DiscountModel?)value; }
        #endregion
    }
}
