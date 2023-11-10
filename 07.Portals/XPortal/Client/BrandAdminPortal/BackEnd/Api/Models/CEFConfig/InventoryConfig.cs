// <copyright file="InventoryConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory config class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class InventoryConfig
    {
        public bool enabled { get; set; }

        public InventoryBackOrderConfig? backOrder { get; set; }

        public InventoryPreSaleConfig? preSale { get; set; }

        public InventoryAdvancedConfig? advanced { get; set; }

    }

    [PublicAPI]
    public class InventoryPreSaleMaxPerProductAccountConfig
    {
        public bool enabled { get; set; }

        public SimpleEnablableFeature? after { get; set; }

    }

    [PublicAPI]
    public class InventoryPreSaleConfig
    {
        public bool enabled { get; set; }

        public SimpleEnablableFeature? maxPerProductGlobal { get; set; }

        public InventoryPreSaleMaxPerProductAccountConfig? maxPerProductPerAccount { get; set; }

    }

    [PublicAPI]
    public class InventoryBackOrderMaxPerProductAccountConfig
    {
        public bool enabled { get; set; }

        public SimpleEnablableFeature? after { get; set; }

    }

    [PublicAPI]
    public class InventoryBackOrderConfig
    {
        public bool enabled { get; set; }

        public SimpleEnablableFeature? maxPerProductGlobal { get; set; }

        public InventoryBackOrderMaxPerProductAccountConfig? maxPerProductPerAccount { get; set; }

    }

    [PublicAPI]
    public class InventoryAdvancedConfig
    {
        public bool enabled { get; set; }

        public bool countOnlyThisStoresWarehouseStock { get; set; }

    }
}
