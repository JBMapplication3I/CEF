// <copyright file="PurchaseOrderStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order status class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IPurchaseOrderStatus : IStatusableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Purchasing", "PurchaseOrderStatus")]
    public class PurchaseOrderStatus : StatusableBase, IPurchaseOrderStatus
    {
    }
}
