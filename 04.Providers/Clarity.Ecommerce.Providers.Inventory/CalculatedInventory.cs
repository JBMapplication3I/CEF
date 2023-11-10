// <copyright file="CalculatedInventory.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calculated inventory class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A calculated inventory.</summary>
    public class CalculatedInventory : ICalculatedInventory
    {
        private bool allowPreSale;

        private bool allowBackOrder;

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int ProductID { get; set; }

        /// <inheritdoc/>
        public string? ProductUOM { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsDiscontinued { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsUnlimitedStock { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsOutOfStock { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantityPresent { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantityAllocated { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantityOnHand { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? MaximumPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? MaximumPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool AllowBackOrder
        {
            get
            {
                if (!JSConfigs.CEFConfigDictionary.InventoryBackOrderEnabled)
                {
                    return false;
                }
                return allowBackOrder;
            }

            set
            {
                if (!JSConfigs.CEFConfigDictionary.InventoryBackOrderEnabled)
                {
                    allowBackOrder = false;
                    return;
                }
                allowBackOrder = value;
            }
        }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? MaximumBackOrderPurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? MaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? MaximumBackOrderPurchaseQuantityGlobal { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool AllowPreSale
        {
            get
            {
                if (!JSConfigs.CEFConfigDictionary.InventoryPreSaleEnabled)
                {
                    return false;
                }
                return allowPreSale;
            }

            set
            {
                if (!JSConfigs.CEFConfigDictionary.InventoryPreSaleEnabled)
                {
                    allowPreSale = false;
                    return;
                }
                allowPreSale = value;
            }
        }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? PreSellEndDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantityPreSellable { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? QuantityPreSold { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? MaximumPrePurchaseQuantity { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? MaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public decimal? MaximumPrePurchaseQuantityGlobal { get; set; }

        /// <inheritdoc cref="ICalculatedInventory.RelevantLocations" />
        [DefaultValue(null)]
        public List<ProductInventoryLocationSectionModel>? RelevantLocations { get; set; }

        /// <inheritdoc/>
        List<IProductInventoryLocationSectionModel>? ICalculatedInventory.RelevantLocations { get => RelevantLocations?.ToList<IProductInventoryLocationSectionModel>(); set => RelevantLocations = value?.Cast<ProductInventoryLocationSectionModel>().ToList(); }
    }
}
