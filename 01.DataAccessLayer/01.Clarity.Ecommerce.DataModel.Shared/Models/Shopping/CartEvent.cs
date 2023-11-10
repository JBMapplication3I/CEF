// <copyright file="CartEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ICartEvent : ISalesEventBase<Cart, CartEventType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Shopping", "CartEvent")]
    public class CartEvent
        : SalesEventBase<Cart, CartEventType>,
            ICartEvent
    {
    }
}
