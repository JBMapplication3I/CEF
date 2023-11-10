// <copyright file="IPOrganizationStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the IP organization status class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IIPOrganizationStatus : IStatusableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Tracking", "IPOrganizationStatus")]
    public class IPOrganizationStatus : StatusableBase, IIPOrganizationStatus
    {
    }
}
