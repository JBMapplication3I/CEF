// <copyright file="IScheduledJobConfigurationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IScheduledJobConfigurationModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for scheduled job configuration model.</summary>
    public partial interface IScheduledJobConfigurationModel
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the notification template.</summary>
        /// <value>The identifier of the notification template.</value>
        int? NotificationTemplateID { get; set; }

        /// <summary>Gets or sets the notification template key.</summary>
        /// <value>The notification template key.</value>
        string? NotificationTemplateKey { get; set; }

        /// <summary>Gets or sets the name of the notification template.</summary>
        /// <value>The name of the notification template.</value>
        string? NotificationTemplateName { get; set; }

        /// <summary>Gets or sets the notification template.</summary>
        /// <value>The notification template.</value>
        IEmailTemplateModel? NotificationTemplate { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the scheduled job configuration settings.</summary>
        /// <value>The scheduled job configuration settings.</value>
        List<IScheduledJobConfigurationSettingModel>? ScheduledJobConfigurationSettings { get; set; }
        #endregion
    }
}
