// <copyright file="ICalendarModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Calendar Model interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface ICalendarModel
    {
        /// <summary>Gets or sets the start time for Monday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The start time for Monday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? MondayHoursStart { get; set; }

        /// <summary>Gets or sets the end time for Monday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The end time for Monday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? MondayHoursEnd { get; set; }

        /// <summary>Gets or sets the start time for Tuesday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The start time for Tuesday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? TuesdayHoursStart { get; set; }

        /// <summary>Gets or sets the end time for Tuesday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The end time for Tuesday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? TuesdayHoursEnd { get; set; }

        /// <summary>Gets or sets the start time for Wednesday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The start time for Wednesday availability. If either start or, end are null, it is treated as
        /// "closed".</value>
        decimal? WednesdayHoursStart { get; set; }

        /// <summary>Gets or sets the end time for Wednesday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The end time for Wednesday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? WednesdayHoursEnd { get; set; }

        /// <summary>Gets or sets the start time for Thursday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The start time for Thursday availability. If either start or, end are null, it is treated as
        /// "closed".</value>
        decimal? ThursdayHoursStart { get; set; }

        /// <summary>Gets or sets the end time for Thursday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The end time for Thursday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? ThursdayHoursEnd { get; set; }

        /// <summary>Gets or sets the start time for Friday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The start time for Friday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? FridayHoursStart { get; set; }

        /// <summary>Gets or sets the end time for Friday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The end time for Friday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? FridayHoursEnd { get; set; }

        /// <summary>Gets or sets the start time for Saturday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The start time for Saturday availability. If either start or, end are null, it is treated as
        /// "closed".</value>
        decimal? SaturdayHoursStart { get; set; }

        /// <summary>Gets or sets the end time for Saturday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The end time for Saturday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? SaturdayHoursEnd { get; set; }

        /// <summary>Gets or sets the start time for Sunday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The start time for Sunday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? SundayHoursStart { get; set; }

        /// <summary>Gets or sets the end time for Sunday availability.</summary>
        /// <remarks>Valid values are null (closed), or 0 (midnight) to 24 (midnight end of day).</remarks>
        /// <value>The end time for Sunday availability. If either start or, end are null, it is treated as "closed".</value>
        decimal? SundayHoursEnd { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the ID of the account that owns this calendar.</summary>
        /// <value>The ID of the account that owns this calendar.</value>
        int AccountID { get; set; }

        /// <summary>Gets or sets the custom key of the account that owns this calendar.</summary>
        /// <value>The custom key of the account that owns this calendar.</value>
        string? AccountKey { get; set; }

        /// <summary>Gets or sets the name of the account that owns this calendar.</summary>
        /// <value>The name of the account that owns this calendar.</value>
        string? AccountName { get; set; }

        /// <summary>Gets or sets the account that owns this calendar.</summary>
        /// <value>The account that owns this calendar.</value>
        IAccountModel? Account { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets a list of appointments on this calendar.</summary>
        /// <value>A list of appointments on this calendar.</value>
        List<ICalendarAppointmentModel>? Appointments { get; set; }
        #endregion
    }
}
