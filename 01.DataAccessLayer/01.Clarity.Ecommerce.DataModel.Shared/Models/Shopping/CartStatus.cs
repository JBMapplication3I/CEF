﻿// <copyright file="CartStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart status class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ICartStatus : IStatusableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Shopping", "CartStatus")]
    public class CartStatus : StatusableBase, ICartStatus
    {
    }
}
