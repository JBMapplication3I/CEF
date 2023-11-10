// <copyright file="CalendarEventImageType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event image type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ICalendarEventImageType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("CalendarEvents", "CalendarEventImageType")]
    public class CalendarEventImageType : TypableBase, ICalendarEventImageType
    {
    }
}
