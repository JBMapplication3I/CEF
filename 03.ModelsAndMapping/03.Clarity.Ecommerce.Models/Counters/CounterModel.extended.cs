// <copyright file="CounterModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CounterModel class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class CounterModel
    {
        /// <inheritdoc/>
        public decimal? Value { get; set; }

        #region Associated Objects
        /// <inheritdoc cref="ICounterModel.CounterLogs"/>
        public List<CounterLogModel>? CounterLogs { get; set; }

        /// <inheritdoc/>
        List<ICounterLogModel>? ICounterModel.CounterLogs { get => CounterLogs?.ToList<ICounterLogModel>(); set => CounterLogs = value?.Cast<CounterLogModel>().ToList(); }
        #endregion
    }
}
