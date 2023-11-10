// <copyright file="PurchaseOrderItemTarget.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PurchaseOrder item target class</summary>
// ReSharper disable MissingBlankLines
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IPurchaseOrderItemTarget : ISalesItemTargetBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Purchasing", "PurchaseOrderItemTarget")]
    public class PurchaseOrderItemTarget
        : SalesItemTargetBase<PurchaseOrderItem>,
          IPurchaseOrderItemTarget
    {
    }
}
