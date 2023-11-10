// <copyright file="IPriceRoundingModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPriceRoundingModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for price rounding model.</summary>
    /// <seealso cref="IBaseModel"/>
    public partial interface IPriceRoundingModel
    {
        /// <summary>Gets or sets the product key.</summary>
        /// <value>The product key.</value>
        string? ProductKey { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the price point key.</summary>
        /// <value>The price point key.</value>
        string? PricePointKey { get; set; }

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
    }
}
