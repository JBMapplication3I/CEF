// <copyright file="SalesOrderItemTarget.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SalesOrder item target class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesOrderItemTarget : ISalesItemTargetBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Ordering", "SalesOrderItemTarget")]
    public class SalesOrderItemTarget
        : SalesItemTargetBase<SalesOrderItem>,
            ISalesOrderItemTarget
    {
    }
}
