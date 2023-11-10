// <copyright file="AppliedSalesOrderItemDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sales order item discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSalesOrderItemDiscount
        : IAppliedDiscountBase<SalesOrderItem, AppliedSalesOrderItemDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SalesOrderItemDiscounts")]
    public class AppliedSalesOrderItemDiscount
        : AppliedDiscountBase<SalesOrderItem, AppliedSalesOrderItemDiscount>, IAppliedSalesOrderItemDiscount
    {
    }
}
