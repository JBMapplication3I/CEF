// <copyright file="AppointmentType.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the AppointmentType class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IAppointmentType
        : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Scheduling", "AppointmentType")]
    public class AppointmentType : TypableBase, IAppointmentType
    {
    }
}
