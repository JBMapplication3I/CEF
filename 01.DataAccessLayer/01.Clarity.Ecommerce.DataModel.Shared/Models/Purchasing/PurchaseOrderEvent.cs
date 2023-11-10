// <copyright file="PurchaseOrderEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IPurchaseOrderEvent : ISalesEventBase<PurchaseOrder, PurchaseOrderEventType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Purchasing", "PurchaseOrderEvent")]
    public class PurchaseOrderEvent
        : SalesEventBase<PurchaseOrder, PurchaseOrderEventType>,
            IPurchaseOrderEvent
    {
    }
}
