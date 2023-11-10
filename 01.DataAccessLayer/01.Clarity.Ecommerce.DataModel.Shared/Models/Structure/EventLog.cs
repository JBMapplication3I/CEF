// <copyright file="EventLog.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the event log class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IEventLog
        : INameableBase,
            IAmFilterableByNullableStore,
            IAmFilterableByNullableBrand
    {
        /// <summary>Gets or sets the identifier of the data.</summary>
        /// <value>The identifier of the data.</value>
        int? DataID { get; set; }

        /// <summary>Gets or sets the log level.</summary>
        /// <value>The log level.</value>
        int? LogLevel { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("System", "SystemLog")]
    public class EventLog : NameableBase, IEventLog
    {
        #region IAmFilterableByNullableStore Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? DataID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? LogLevel { get; set; }
    }
}
