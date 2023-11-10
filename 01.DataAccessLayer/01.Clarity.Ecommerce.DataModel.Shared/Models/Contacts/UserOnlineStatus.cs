// <copyright file="UserOnlineStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user online status class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IUserOnlineStatus : IStatusableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Contacts", "UserOnlineStatus")]
    public class UserOnlineStatus : StatusableBase, IUserOnlineStatus
    {
    }
}
