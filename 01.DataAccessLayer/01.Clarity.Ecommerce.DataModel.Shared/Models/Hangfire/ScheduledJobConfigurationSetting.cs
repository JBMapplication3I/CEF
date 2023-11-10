// <copyright file="ScheduledJobConfigurationSetting.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the scheduled job configuration setting class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IScheduledJobConfigurationSetting : IAmARelationshipTable<ScheduledJobConfiguration, Setting>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Hangfire", "ScheduledJobConfigurationSetting")]
    public class ScheduledJobConfigurationSetting : Base, IScheduledJobConfigurationSetting
    {
        #region IAmARelationshipTable<ScheduledJobConfiguration, Setting>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ScheduledJobConfiguration? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual Setting? Slave { get; set; }
        #endregion
    }
}
