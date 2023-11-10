﻿// <copyright file="HistoricalCurrencyRate.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the historical currency rate class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IHistoricalCurrencyRate : IBase
    {
        #region HistoricalCurrencyRate Properties
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        /// <summary>Gets or sets the on date.</summary>
        /// <value>The on date.</value>
        DateTime OnDate { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the starting currency.</summary>
        /// <value>The identifier of the starting currency.</value>
        int StartingCurrencyID { get; set; }

        /// <summary>Gets or sets the starting currency.</summary>
        /// <value>The starting currency.</value>
        Currency? StartingCurrency { get; set; }

        /// <summary>Gets or sets the identifier of the ending currency.</summary>
        /// <value>The identifier of the ending currency.</value>
        int EndingCurrencyID { get; set; }

        /// <summary>Gets or sets the ending currency.</summary>
        /// <value>The ending currency.</value>
        Currency? EndingCurrency { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Currencies", "HistoricalCurrencyRate")]
    public class HistoricalCurrencyRate : Base, IHistoricalCurrencyRate
    {
        #region HistoricalCurrencyRate Properties
        /// <inheritdoc/>
        [DecimalPrecision(24, 20)]
        public decimal Rate { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime OnDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(StartingCurrency)), Index, DefaultValue(0)]
        public int StartingCurrencyID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Currency? StartingCurrency { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(EndingCurrency)), Index, DefaultValue(0)]
        public int EndingCurrencyID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Currency? EndingCurrency { get; set; }
        #endregion
    }
}
