// <copyright file="CounterLog.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CounterLog class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ICounterLog : IHaveATypeBase<CounterLogType>
    {
        #region CounterLog Properties
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        decimal? Value { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the counter.</summary>
        /// <value>The identifier of the counter.</value>
        int CounterID { get; set; }

        /// <summary>Gets or sets the counter.</summary>
        /// <value>The counter.</value>
        Counter? Counter { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Counters", "CounterLog")]
    public class CounterLog : Base, ICounterLog
    {
        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual CounterLogType? Type { get; set; }
        #endregion

        #region CounterLog Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4)]
        public decimal? Value { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Counter))]
        public int CounterID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Counter? Counter { get; set; }
        #endregion
    }
}
