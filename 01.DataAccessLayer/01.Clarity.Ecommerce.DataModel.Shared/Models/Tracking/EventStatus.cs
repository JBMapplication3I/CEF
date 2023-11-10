// <copyright file="EventStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the event status class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IEventStatus : IStatusableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Tracking", "EventStatus")]
    public class EventStatus : StatusableBase, IEventStatus
    {
    }
}
