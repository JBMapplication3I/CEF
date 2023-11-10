// <copyright file="TypableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the typable base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    /// <summary>A typable base.</summary>
    /// <seealso cref="DisplayableBase"/>
    /// <seealso cref="ITypableBase"/>
    public abstract class TypableBase : DisplayableBase, ITypableBase
    {
    }
}
