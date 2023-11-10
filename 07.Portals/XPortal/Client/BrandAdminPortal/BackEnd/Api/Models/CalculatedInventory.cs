// <copyright file="CalculatedInventory.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calculated inventory class</summary>
// ReSharper disable MissingXmlDoc
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class CalculatedInventory
    {
        public int ProductID { get; set; }

        public bool IsDiscontinued { get; set; }

        public bool IsUnlimitedStock { get; set; }

        public bool IsOutOfStock { get; set; }

        public decimal? QuantityPresent { get; set; }

        public decimal? QuantityAllocated { get; set; }

        public decimal? QuantityOnHand { get; set; }

        public decimal? MaximumPurchaseQuantity { get; set; }

        public decimal? MaximumPurchaseQuantityIfPastPurchased { get; set; }

        public bool AllowBackOrder { get; set; }

        public decimal? MaximumBackOrderPurchaseQuantity { get; set; }

        public decimal? MaximumBackOrderPurchaseQuantityIfPastPurchased { get; set; }

        public decimal? MaximumBackOrderPurchaseQuantityGlobal { get; set; }

        public bool AllowPreSale { get; set; }

        public DateTime? PreSellEndDate { get; set; }

        public decimal? QuantityPreSellable { get; set; }

        public decimal? QuantityPreSold { get; set; }

        public decimal? MaximumPrePurchaseQuantity { get; set; }

        public decimal? MaximumPrePurchaseQuantityIfPastPurchased { get; set; }

        public decimal? MaximumPrePurchaseQuantityGlobal { get; set; }

        public List<ProductInventoryLocationSectionModel>? RelevantLocations { get; set; }
    }
}
