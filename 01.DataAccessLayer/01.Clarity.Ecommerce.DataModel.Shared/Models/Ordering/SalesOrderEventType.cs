// <copyright file="SalesOrderEventType.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order event type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesOrderEventType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Ordering", "SalesOrderEventType")]
    public class SalesOrderEventType : TypableBase, ISalesOrderEventType
    {
    }
}
