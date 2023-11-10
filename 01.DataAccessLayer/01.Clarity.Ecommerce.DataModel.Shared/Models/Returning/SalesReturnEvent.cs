// <copyright file="SalesReturnEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesReturnEvent : ISalesEventBase<SalesReturn, SalesReturnEventType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Returning", "SalesReturnEvent")]
    public class SalesReturnEvent
        : SalesEventBase<SalesReturn, SalesReturnEventType>,
            ISalesReturnEvent
    {
    }
}
