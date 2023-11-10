// <copyright file="SalesQuoteEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesQuoteEvent : ISalesEventBase<SalesQuote, SalesQuoteEventType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Quoting", "SalesQuoteEvent")]
    public class SalesQuoteEvent
        : SalesEventBase<SalesQuote, SalesQuoteEventType>,
            ISalesQuoteEvent
    {
    }
}
