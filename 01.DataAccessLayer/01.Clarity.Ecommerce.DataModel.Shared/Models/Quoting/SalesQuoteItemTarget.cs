// <copyright file="SalesQuoteItemTarget.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SalesQuote item target class</summary>
// ReSharper disable MissingBlankLines
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesQuoteItemTarget : ISalesItemTargetBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Quoting", "SalesQuoteItemTarget")]
    public class SalesQuoteItemTarget
        : SalesItemTargetBase<SalesQuoteItem>,
            ISalesQuoteItemTarget
    {
    }
}
