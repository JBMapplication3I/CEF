// <copyright file="AttributeGroup.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the attribute group class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IAttributeGroup : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Attributes", "AttributeGroup")]
    public class AttributeGroup : TypableBase, IAttributeGroup
    {
    }
}
