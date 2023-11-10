// <copyright file="CartTotals.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public partial class CartTotals
    {
        public decimal? SubTotal { get; set; }

        public decimal? Shipping { get; set; }

        public decimal? Handling { get; set; }

        public decimal? Fees { get; set; }

        public decimal? Discounts { get; set; }

        public decimal? Tax { get; set; }

        public decimal Total => 0m
            + (SubTotal ?? 0m)
            + (Shipping ?? 0m)
            + (Handling ?? 0m)
            + (Fees ?? 0m)
            + (Math.Abs(Discounts ?? 0m) * -1)
            + (Tax ?? 0m);
    }
}
