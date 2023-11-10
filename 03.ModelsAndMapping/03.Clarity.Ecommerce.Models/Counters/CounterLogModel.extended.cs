// <copyright file="CounterLogModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CounterLogModel class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the counter log.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="ICounterLogModel"/>
    public partial class CounterLogModel
    {
        /// <inheritdoc/>
        public decimal? Value { get; set; }

        #region Associated Objects
        /// <inheritdoc/>
        public int CounterID { get; set; }

        /// <inheritdoc/>
        public string? CounterKey { get; set; }

        /// <inheritdoc cref="ICounterLogModel.Counter"/>
        public CounterModel? Counter { get; set; }

        /// <inheritdoc/>
        ICounterModel? ICounterLogModel.Counter { get => Counter; set => Counter = (CounterModel?)value; }
        #endregion
    }
}
