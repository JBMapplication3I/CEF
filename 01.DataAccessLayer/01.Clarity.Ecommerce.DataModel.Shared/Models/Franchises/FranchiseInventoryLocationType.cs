// <copyright file="FranchiseInventoryLocationType.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise inventory location type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IFranchiseInventoryLocationType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Franchises", "FranchiseInventoryLocationType")]
    public class FranchiseInventoryLocationType : TypableBase, IFranchiseInventoryLocationType
    {
    }
}
