// <copyright file="StateableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stateable base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    /// <summary>A stateable base.</summary>
    /// <seealso cref="DisplayableBase"/>
    /// <seealso cref="IStateableBase"/>
    public abstract class StateableBase : DisplayableBase, IStateableBase
    {
    }
}
