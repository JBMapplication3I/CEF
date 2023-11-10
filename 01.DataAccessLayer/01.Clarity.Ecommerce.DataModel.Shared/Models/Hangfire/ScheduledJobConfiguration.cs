// <copyright file="ScheduledJobConfiguration.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IScheduledJobConfiguration interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IScheduledJobConfiguration : INameableBase
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the notification template.</summary>
        /// <value>The identifier of the notification template.</value>
        int? NotificationTemplateID { get; set; }

        /// <summary>Gets or sets the notification template.</summary>
        /// <value>The notification template.</value>
        EmailTemplate? NotificationTemplate { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the scheduled job configuration settings.</summary>
        /// <value>The scheduled job configuration settings.</value>
        ICollection<ScheduledJobConfigurationSetting>? ScheduledJobConfigurationSettings { get; set; }
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

    [SqlSchema("Hangfire", "ScheduledJobConfiguration")]
    public class ScheduledJobConfiguration : NameableBase, IScheduledJobConfiguration
    {
        private ICollection<ScheduledJobConfigurationSetting>? scheduledJobConfigurationSettings;

        public ScheduledJobConfiguration()
        {
            scheduledJobConfigurationSettings = new HashSet<ScheduledJobConfigurationSetting>();
        }

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(NotificationTemplate)), DefaultValue(null)]
        public int? NotificationTemplateID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual EmailTemplate? NotificationTemplate { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ScheduledJobConfigurationSetting>? ScheduledJobConfigurationSettings { get => scheduledJobConfigurationSettings; set => scheduledJobConfigurationSettings = value; }
        #endregion
    }
}
