// <copyright file="StoreInventoryLocationType.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store inventory location type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IStoreInventoryLocationType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Stores", "StoreInventoryLocationType")]
    public class StoreInventoryLocationType : TypableBase, IStoreInventoryLocationType
    {
    }
}
