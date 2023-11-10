// <copyright file="CalendarModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CalendarModel Model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class CalendarModel
    {
        #region Calendar Properties
        /// <inheritdoc/>
        public decimal? MondayHoursStart { get; set; }

        /// <inheritdoc/>
        public decimal? MondayHoursEnd { get; set; }

        /// <inheritdoc/>
        public decimal? TuesdayHoursStart { get; set; }

        /// <inheritdoc/>
        public decimal? TuesdayHoursEnd { get; set; }

        /// <inheritdoc/>
        public decimal? WednesdayHoursStart { get; set; }

        /// <inheritdoc/>
        public decimal? WednesdayHoursEnd { get; set; }

        /// <inheritdoc/>
        public decimal? ThursdayHoursStart { get; set; }

        /// <inheritdoc/>
        public decimal? ThursdayHoursEnd { get; set; }

        /// <inheritdoc/>
        public decimal? FridayHoursStart { get; set; }

        /// <inheritdoc/>
        public decimal? FridayHoursEnd { get; set; }

        /// <inheritdoc/>
        public decimal? SaturdayHoursStart { get; set; }

        /// <inheritdoc/>
        public decimal? SaturdayHoursEnd { get; set; }

        /// <inheritdoc/>
        public decimal? SundayHoursStart { get; set; }

        /// <inheritdoc/>
        public decimal? SundayHoursEnd { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int AccountID { get; set; }

        /// <inheritdoc/>
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        public string? AccountName { get; set; }

        /// <inheritdoc cref="ICalendarModel.Account"/>
        public AccountModel? Account { get; set; }

        /// <inheritdoc/>
        IAccountModel? ICalendarModel.Account { get => Account; set => Account = (AccountModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ICalendarModel.Appointments"/>
        public List<CalendarAppointmentModel>? Appointments { get; set; }

        /// <inheritdoc/>
        List<ICalendarAppointmentModel>? ICalendarModel.Appointments { get => Appointments?.ToList<ICalendarAppointmentModel>(); set => Appointments = value?.Cast<CalendarAppointmentModel>().ToList(); }
        #endregion
    }
}
