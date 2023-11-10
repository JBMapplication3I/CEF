// <copyright file="StatusableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the statusable base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    /// <summary>A statusable base.</summary>
    /// <seealso cref="DisplayableBase"/>
    /// <seealso cref="IStatusableBase"/>
    public abstract class StatusableBase : DisplayableBase, IStatusableBase
    {
    }
}
