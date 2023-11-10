// <copyright file="IAppointmentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Appointment Model interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    public partial interface IAppointmentModel
    {
        #region ScheduledEvent Properties
        /// <summary>Gets or sets the date and time the appointment starts.</summary>
        /// <value>The date and time the appointment starts.</value>
        DateTime? AppointmentStart { get; set; }

        /// <summary>Gets or sets the date and time the appointment ends.</summary>
        /// <value>The date and time the appointment ends.</value>
        DateTime? AppointmentEnd { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the sales order key.</summary>
        /// <value>The sales order key.</value>
        string? SalesOrderKey { get; set; }

        /// <summary>Gets or sets the sales order.</summary>
        /// <value>The sales order.</value>
        ISalesOrderModel? SalesOrder { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets a list of calendars on which this appointment appears.</summary>
        /// <value>A list of calendars on which this appointment appears.</value>
        List<ICalendarAppointmentModel>? Calendars { get; set; }
        #endregion
    }
}
