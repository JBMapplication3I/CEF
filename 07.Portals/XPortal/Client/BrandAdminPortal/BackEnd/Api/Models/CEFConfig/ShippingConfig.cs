// <copyright file="ShippingConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class ShippingConfig
    {
        public bool enabled { get; set; }

        public SimpleEnablableFeature? carrierAccounts { get; set; }

        public SimpleEnablableFeature? events { get; set; }

        public SimpleEnablableFeature? packages { get; set; }

        public SimpleEnablableFeature? masterPacks { get; set; }

        public SimpleEnablableFeature? pallets { get; set; }

        public SimpleEnablableFeature? shipToStore { get; set; }

        public SimpleEnablableFeature? inStorePickup { get; set; }

        public ShippingRatesConfig? rates { get; set; }

        public SimpleEnablableFeature? restrictions { get; set; }

        public SplitShippingConfig? splitShipping { get; set; }

        public SimpleEnablableFeature? leadTimes { get; set; }

        public SimpleEnablableFeature? handlingFees { get; set; }

    }

    [PublicAPI]
    public class ShippingRatesConfig
    {
        public bool enabled { get; set; }

        public SimpleEnablableFeature? estimator { get; set; }

        public SimpleEnablableFeature? flat { get; set; }

        public bool productsCanBeFreeShipping { get; set; }

    }

    [PublicAPI]
    public class SplitShippingConfig
    {
        public bool enabled { get; set; }

        public bool onlyAllowOneDestination { get; set; }

    }
}
