// <copyright file="AppliedSalesOrderDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sales order discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSalesOrderDiscount
        : IAppliedDiscountBase<SalesOrder, AppliedSalesOrderDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SalesOrderDiscounts")]
    public class AppliedSalesOrderDiscount
        : AppliedDiscountBase<SalesOrder, AppliedSalesOrderDiscount>, IAppliedSalesOrderDiscount
    {
    }
}
