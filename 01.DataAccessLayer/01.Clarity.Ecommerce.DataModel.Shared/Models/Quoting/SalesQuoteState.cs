// <copyright file="SalesQuoteState.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote state class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesQuoteState : IStateableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Quoting", "SalesQuoteState")]
    public class SalesQuoteState : StateableBase, ISalesQuoteState
    {
    }
}
