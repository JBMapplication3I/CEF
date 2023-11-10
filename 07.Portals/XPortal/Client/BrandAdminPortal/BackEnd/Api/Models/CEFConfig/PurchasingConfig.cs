// <copyright file="PurchasingConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class PurchasingConfig
    {
        public SimpleEnablableFeature? availabilityDates { get; set; }

        public SimpleEnablableFeature? documentRequired { get; set; }

        public SimpleEnablableFeature? documentRequired_override { get; set; }

        public SimpleEnablableFeature? minMax { get; set; }

        public SimpleEnablableFeature? minMax_after { get; set; }

        public SimpleEnablableFeature? minOrder { get; set; }

        public SimpleEnablableFeature? roleRequiredToPurchase { get; set; }

        public SimpleEnablableFeature? roleRequiredToSee { get; set; }

        public SimpleEnablableFeature? mustPurchaseInMultiplesOf { get; set; }

        public SimpleEnablableFeature? mustPurchaseInMultiplesOf_override { get; set; }

    }
}
