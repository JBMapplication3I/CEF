// <copyright file="SalesReturnState.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return state class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesReturnState : IStateableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Returning", "SalesReturnState")]
    public class SalesReturnState : StateableBase, ISalesReturnState
    {
    }
}
