// <copyright file="IUserEventAttendanceSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUserEventAttendanceSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for user event attendance search model.</summary>
    public partial interface IUserEventAttendanceSearchModel
    {
        /// <summary>Gets or sets the identifier of the calendar event.</summary>
        /// <value>The identifier of the calendar event.</value>
        int? CalendarEventID { get; set; }

        /// <summary>Gets or sets the calendar event key.</summary>
        /// <value>The calendar event key.</value>
        string? CalendarEventKey { get; set; }

        /// <summary>Gets or sets the is paid in full.</summary>
        /// <value>The is paid in full.</value>
        bool? IsPaidInFull { get; set; }

        /// <summary>Gets or sets the has valid passport.</summary>
        /// <value>The has valid passport.</value>
        bool? HasValidPassport { get; set; }

        /// <summary>Gets or sets the days until departure.</summary>
        /// <value>The days until departure.</value>
        int? DaysUntilDeparture { get; set; }

        /// <summary>Gets or sets the name of the contact first.</summary>
        /// <value>The name of the contact first.</value>
        string? ContactFirstName { get; set; }

        /// <summary>Gets or sets the name of the contact last.</summary>
        /// <value>The name of the contact last.</value>
        string? ContactLastName { get; set; }
    }
}
