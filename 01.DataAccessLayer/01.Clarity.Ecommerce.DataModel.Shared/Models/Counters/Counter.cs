// <copyright file="Counter.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Counter class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ICounter : IHaveATypeBase<CounterType>
    {
        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        decimal? Value { get; set; }

        #region Associated Objects
        /// <summary>Gets or sets the counter logs.</summary>
        /// <value>The counter logs.</value>
        ICollection<CounterLog>? CounterLogs { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Counters", "Counter")]
    public class Counter : Base, ICounter
    {
        private ICollection<CounterLog>? counterLogs;

        public Counter()
        {
            counterLogs = new HashSet<CounterLog>();
        }

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual CounterType? Type { get; set; }
        #endregion

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? Value { get; set; }

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CounterLog>? CounterLogs { get => counterLogs; set => counterLogs = value; }
        #endregion
    }
}
