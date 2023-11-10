// <copyright file="UserEventAttendanceSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user event attendance search model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the user event attendance search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IUserEventAttendanceSearchModel"/>
    public partial class UserEventAttendanceSearchModel
    {
        /// <inheritdoc/>
        public int? CalendarEventID { get; set; }

        /// <inheritdoc/>
        public string? CalendarEventKey { get; set; }

        /// <inheritdoc/>
        public bool? IsPaidInFull { get; set; }

        /// <inheritdoc/>
        public bool? HasValidPassport { get; set; }

        /// <inheritdoc/>
        public int? DaysUntilDeparture { get; set; }

        /// <inheritdoc/>
        public string? ContactFirstName { get; set; }

        /// <inheritdoc/>
        public string? ContactLastName { get; set; }
    }
}
