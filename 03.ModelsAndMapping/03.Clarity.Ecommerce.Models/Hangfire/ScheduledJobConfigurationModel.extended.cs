// <copyright file="ScheduledJobConfigurationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the scheduled job configuration model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the scheduled job configuration.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IScheduledJobConfigurationModel"/>
    public partial class ScheduledJobConfigurationModel
    {
        #region Related Objects
        /// <inheritdoc/>
        public int? NotificationTemplateID { get; set; }

        /// <inheritdoc/>
        public string? NotificationTemplateKey { get; set; }

        /// <inheritdoc/>
        public string? NotificationTemplateName { get; set; }

        /// <inheritdoc cref="IScheduledJobConfigurationModel.NotificationTemplate"/>
        public EmailTemplateModel? NotificationTemplate { get; set; }

        /// <inheritdoc/>
        IEmailTemplateModel? IScheduledJobConfigurationModel.NotificationTemplate { get => NotificationTemplate; set => NotificationTemplate = (EmailTemplateModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IScheduledJobConfigurationModel.ScheduledJobConfigurationSettings"/>
        public List<ScheduledJobConfigurationSettingModel>? ScheduledJobConfigurationSettings { get; set; }

        /// <inheritdoc/>
        List<IScheduledJobConfigurationSettingModel>? IScheduledJobConfigurationModel.ScheduledJobConfigurationSettings { get => ScheduledJobConfigurationSettings?.ToList<IScheduledJobConfigurationSettingModel>(); set => ScheduledJobConfigurationSettings = value?.Cast<ScheduledJobConfigurationSettingModel>().ToList(); }
        #endregion
    }
}
