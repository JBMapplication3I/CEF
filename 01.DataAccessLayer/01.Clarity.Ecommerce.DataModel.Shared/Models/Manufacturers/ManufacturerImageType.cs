// <copyright file="ManufacturerImageType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer image type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IManufacturerImageType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Manufacturers", "ManufacturerImageType")]
    public class ManufacturerImageType : TypableBase, IManufacturerImageType
    {
    }
}
