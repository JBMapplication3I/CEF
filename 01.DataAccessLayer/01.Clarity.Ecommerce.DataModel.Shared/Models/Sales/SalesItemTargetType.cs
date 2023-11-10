// <copyright file="SalesItemTargetType.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales item target type class</summary>
// ReSharper disable MissingBlankLines
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesItemTargetType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Sales", "SalesItemTargetType")]
    public class SalesItemTargetType : TypableBase, ISalesItemTargetType
    {
    }
}
