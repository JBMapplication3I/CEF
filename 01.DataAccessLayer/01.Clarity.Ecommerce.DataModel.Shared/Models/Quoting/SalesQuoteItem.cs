// <copyright file="SalesQuoteItem.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote item class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesQuoteItem
        : ISalesItemBase<SalesQuoteItem, AppliedSalesQuoteItemDiscount, SalesQuoteItemTarget>
    {
        #region SalesQuoteItem Properties
        /// <summary>Gets or sets the unit sold price modifier.</summary>
        /// <value>The unit sold price modifier.</value>
        decimal? UnitSoldPriceModifier { get; set; }

        /// <summary>Gets or sets the unit sold price modifier mode.</summary>
        /// <value>The unit sold price modifier mode.</value>
        int? UnitSoldPriceModifierMode { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using Interfaces.DataModel;

    [SqlSchema("Quoting", "SalesQuoteItem")]
    public class SalesQuoteItem
        : SalesItemBase<SalesQuote, SalesQuoteItem, AppliedSalesQuoteItemDiscount, SalesQuoteItemTarget>,
            ISalesQuoteItem
    {
        #region SalesQuoteItem Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? UnitSoldPriceModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? UnitSoldPriceModifierMode { get; set; }
        #endregion
    }
}
