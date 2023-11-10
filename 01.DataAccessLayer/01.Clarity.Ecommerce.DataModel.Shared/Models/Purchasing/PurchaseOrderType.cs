// <copyright file="PurchaseOrderType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IPurchaseOrderType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Purchasing", "PurchaseOrderType")]
    public class PurchaseOrderType : TypableBase, IPurchaseOrderType
    {
    }
}
