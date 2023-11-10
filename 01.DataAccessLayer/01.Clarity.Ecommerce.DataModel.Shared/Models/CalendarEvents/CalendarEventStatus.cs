// <copyright file="CalendarEventStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event status class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ICalendarEventStatus : IStatusableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("CalendarEvents", "CalendarEventStatus")]
    public class CalendarEventStatus : StatusableBase, ICalendarEventStatus
    {
    }
}
