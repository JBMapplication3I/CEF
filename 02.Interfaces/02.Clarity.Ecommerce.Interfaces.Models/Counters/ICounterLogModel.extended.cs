// <copyright file="ICounterLogModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICounterLogModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for counter log model.</summary>
    public partial interface ICounterLogModel
    {
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        decimal? Value { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the counter.</summary>
        /// <value>The identifier of the counter.</value>
        int CounterID { get; set; }

        /// <summary>Gets or sets the counter key.</summary>
        /// <value>The counter key.</value>
        string? CounterKey { get; set; }

        /// <summary>Gets or sets the counter.</summary>
        /// <value>The counter.</value>
        ICounterModel? Counter { get; set; }
        #endregion
    }
}
