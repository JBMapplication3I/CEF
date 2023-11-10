// <copyright file="BrandInventoryLocationType.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand inventory location type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IBrandInventoryLocationType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Brands", "BrandInventoryLocationType")]
    public class BrandInventoryLocationType : TypableBase, IBrandInventoryLocationType
    {
    }
}
