// <copyright file="AppointmentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Appointment Model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class AppointmentModel
    {
        #region Appointment Properties
        /// <inheritdoc/>
        public DateTime? AppointmentStart { get; set; }

        /// <inheritdoc/>
        public DateTime? AppointmentEnd { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        public string? SalesOrderKey { get; set; }

        /// <inheritdoc cref="IAppointmentModel.SalesOrder"/>
        public SalesOrderModel? SalesOrder { get; set; }

        /// <inheritdoc/>
        ISalesOrderModel? IAppointmentModel.SalesOrder { get => SalesOrder; set => SalesOrder = (SalesOrderModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IAppointmentModel.Calendars"/>
        public List<CalendarAppointmentModel>? Calendars { get; set; }

        /// <inheritdoc/>
        List<ICalendarAppointmentModel>? IAppointmentModel.Calendars { get => Calendars?.ToList<ICalendarAppointmentModel>(); set => Calendars = value?.Cast<CalendarAppointmentModel>().ToList(); }
        #endregion
    }
}
