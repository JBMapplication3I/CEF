// <copyright file="CartsFeatureSet.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class CartsFeatureSet
    {
        public bool enabled { get; set; }

        public string? cartUrlFragment { get; set; }

        public SimpleEnablableFeature? compare { get; set; }

        public SimpleEnablableFeature? favoritesList { get; set; }

        public SimpleEnablableFeature? notifyMeWhenInStock { get; set; }

        public SimpleEnablableFeature? shoppingLists { get; set; }

        public SimpleEnablableFeature? wishList { get; set; }

        public SimpleEnablableFeature? serviceDebug { get; set; }
    }
}
