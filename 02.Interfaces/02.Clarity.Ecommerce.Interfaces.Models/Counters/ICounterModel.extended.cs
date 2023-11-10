// <copyright file="ICounterModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICounterModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for counter model.</summary>
    public partial interface ICounterModel
    {
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        decimal? Value { get; set; }

        #region Associated Objects
        /// <summary>Gets or sets the counter logs.</summary>
        /// <value>The counter logs.</value>
        List<ICounterLogModel>? CounterLogs { get; set; }
        #endregion
    }
}
