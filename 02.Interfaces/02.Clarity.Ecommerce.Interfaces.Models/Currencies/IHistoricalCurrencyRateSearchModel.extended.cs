// <copyright file="IHistoricalCurrencyRateSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHistoricalCurrencyRateSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for historical currency rate search model.</summary>
    public partial interface IHistoricalCurrencyRateSearchModel
    {
        /// <summary>Gets or sets the starting currency key.</summary>
        /// <value>The starting currency key.</value>
        string? StartingCurrencyKey { get; set; }

        /// <summary>Gets or sets the ending currency key.</summary>
        /// <value>The ending currency key.</value>
        string? EndingCurrencyKey { get; set; }
    }
}
