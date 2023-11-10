// <copyright file="AppointmentSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Appointment Search Model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using ServiceStack;

    public partial class AppointmentSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(FilterStart), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "Start of date filter")]
        public DateTime? FilterStart { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(FilterEnd), DataType = "DateTime?", ParameterType = "query", IsRequired = false,
            Description = "End of date filter")]
        public DateTime? FilterEnd { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CalendarIDs), DataType = "int[]", ParameterType = "query", IsRequired = false,
            Description = "IDs of the Schedules that own the events")]
        public int[]? CalendarIDs { get; set; }
    }
}
