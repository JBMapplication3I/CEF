// <copyright file="SalesReturnItemTarget.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SalesReturn item target class</summary>
// ReSharper disable MissingBlankLines
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesReturnItemTarget : ISalesItemTargetBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Returning", "SalesReturnItemTarget")]
    public class SalesReturnItemTarget
        : SalesItemTargetBase<SalesReturnItem>,
            ISalesReturnItemTarget
    {
    }
}
