// <copyright file="PriceRounding.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rounding class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IPriceRounding : IBase
    {
        #region PriceRounding Properties
        /// <summary>Gets or sets the price point key.</summary>
        /// <value>The price point key.</value>
        string? PricePointKey { get; set; }

        /// <summary>Gets or sets the product key.</summary>
        /// <value>The product key.</value>
        string? ProductKey { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the round how.</summary>
        /// <value>The round how.</value>
        int RoundHow { get; set; }

        /// <summary>Gets or sets the round to.</summary>
        /// <value>The round to.</value>
        int RoundTo { get; set; }

        /// <summary>Gets or sets the rounding amount.</summary>
        /// <value>The rounding amount.</value>
        int RoundingAmount { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;

    [SqlSchema("Pricing", "PriceRounding")]
    public class PriceRounding : Base, IPriceRounding
    {
        #region PriceRounding Properties
        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? PricePointKey { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? ProductKey { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int RoundHow { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int RoundTo { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int RoundingAmount { get; set; }
        #endregion
    }
}
