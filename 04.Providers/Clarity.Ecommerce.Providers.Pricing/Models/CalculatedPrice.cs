// <copyright file="CalculatedPrice.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Calculated Price class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A Calculated Price.</summary>
    /// <seealso cref="ICalculatedPrice"/>
    public class CalculatedPrice : ICalculatedPrice
    {
        /// <summary>Initializes a new instance of the <see cref="CalculatedPrice"/> class.</summary>
        /// <param name="pricingProvider">The pricing provider.</param>
        /// <param name="priceBase">      The price base.</param>
        /// <param name="priceSale">      The price sale.</param>
        /// <param name="priceMsrp">      The price msrp.</param>
        /// <param name="priceReduction"> The price reduction.</param>
        public CalculatedPrice(
            string pricingProvider,
            double priceBase,
            double? priceSale = null,
            double? priceMsrp = null,
            double? priceReduction = null)
        {
            BasePrice = (decimal)priceBase;
            SalePrice = (decimal?)priceSale;
            MsrpPrice = (decimal?)priceMsrp;
            ReductionPrice = (decimal?)priceReduction;
            PricingProvider = pricingProvider;
        }

        /// <inheritdoc/>
        public decimal BasePrice { get; set; }

        /// <inheritdoc/>
        public decimal? SalePrice { get; set; }

        /// <inheritdoc/>
        public decimal? MsrpPrice { get; set; }

        /// <inheritdoc/>
        public decimal? ReductionPrice { get; set; }

        /// <inheritdoc/>
        public string? PricingProvider { get; set; }

        /// <inheritdoc/>
        public string? PriceKey { get; set; }

        /// <inheritdoc/>
        public string? PriceKeyAlt { get; set; }

        /// <inheritdoc cref="ICalculatedPrice.RelevantRules"/>
        public List<PriceRuleModel>? RelevantRules { get; set; }

        /// <inheritdoc/>
        List<IPriceRuleModel>? ICalculatedPrice.RelevantRules { get => RelevantRules?.ToList<IPriceRuleModel>(); set => RelevantRules = value?.Cast<PriceRuleModel>().ToList(); }

        /// <inheritdoc cref="ICalculatedPrice.UsedRules"/>
        public List<PriceRuleModel>? UsedRules { get; set; }

        /// <inheritdoc/>
        List<IPriceRuleModel>? ICalculatedPrice.UsedRules { get => UsedRules?.ToList<IPriceRuleModel>(); set => UsedRules = value?.Cast<PriceRuleModel>().ToList(); }

        /// <inheritdoc/>
        public bool IsValid => BasePrice >= 0m;

        public List<MultiUOMCalculatedPrice>? MultiUOMPrices { get; set; }

        List<IMultiUOMCalculatedPrice>? ICalculatedPrice.MultiUOMPrices { get => MultiUOMPrices?.ToList<IMultiUOMCalculatedPrice>(); set => MultiUOMPrices = value?.Cast<MultiUOMCalculatedPrice>().ToList(); }
    }
}
