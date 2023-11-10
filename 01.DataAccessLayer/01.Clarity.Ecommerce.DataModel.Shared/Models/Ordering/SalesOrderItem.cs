// <copyright file="SalesOrderItem.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order item class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesOrderItem
        : ISalesItemBase<SalesOrderItem, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Ordering", "SalesOrderItem")]
    public class SalesOrderItem
        : SalesItemBase<SalesOrder, SalesOrderItem, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>,
            ISalesOrderItem
    {
    }
}
