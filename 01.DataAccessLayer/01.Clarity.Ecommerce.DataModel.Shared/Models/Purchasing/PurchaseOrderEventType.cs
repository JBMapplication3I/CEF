// <copyright file="PurchaseOrderEventType.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order event type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IPurchaseOrderEventType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Purchasing", "PurchaseOrderEventType")]
    public class PurchaseOrderEventType : TypableBase, IPurchaseOrderEventType
    {
    }
}
