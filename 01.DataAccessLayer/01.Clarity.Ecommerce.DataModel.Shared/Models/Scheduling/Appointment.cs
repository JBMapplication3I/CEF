// <copyright file="Appointment.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Appointment class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Clarity.Ecommerce.DataModel;

    public interface IAppointment
        : INameableBase,
            IHaveATypeBase<AppointmentType>,
            IHaveAStatusBase<AppointmentStatus>
    {
        #region Appointment Properties
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

        /// <summary>Gets or sets the sales order.</summary>
        /// <value>The sales order.</value>
        SalesOrder? SalesOrder { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets a list of calendars on which this appointment appears.</summary>
        /// <value>A list of calendars on which this appointment appears.</value>
        ICollection<CalendarAppointment>? Calendars { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Clarity.Ecommerce.Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Scheduling", "Appointment")]
    public class Appointment : NameableBase, IAppointment
    {
        private ICollection<CalendarAppointment>? calendars;

        public Appointment()
        {
            calendars = new HashSet<CalendarAppointment>();
        }

        #region IHaveATypeBase<AppointmentType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AppointmentType? Type { get; set; }
        #endregion

        #region IHaveAStatusBase<AppointmentStatus>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual AppointmentStatus? Status { get; set; }
        #endregion

        #region Appointment Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? AppointmentStart { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public DateTime? AppointmentEnd { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesOrder)), DefaultValue(0)]
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        [JsonIgnore, DefaultValue(null)]
        public virtual SalesOrder? SalesOrder { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<CalendarAppointment>? Calendars { get => calendars; set => calendars = value; }
        #endregion
    }
}
