// <copyright file="UserEventAttendanceType.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user event attendance type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IUserEventAttendanceType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("CalendarEvents", "UserEventAttendanceType")]
    public class UserEventAttendanceType : TypableBase, IUserEventAttendanceType
    {
    }
}
