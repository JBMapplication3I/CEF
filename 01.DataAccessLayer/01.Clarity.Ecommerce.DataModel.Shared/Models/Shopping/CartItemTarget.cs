// <copyright file="CartItemTarget.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart item target class</summary>
// ReSharper disable MissingBlankLines
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ICartItemTarget : ISalesItemTargetBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Shopping", "CartItemTarget")]
    public class CartItemTarget
        : SalesItemTargetBase<CartItem>,
            ICartItemTarget
    {
    }
}
