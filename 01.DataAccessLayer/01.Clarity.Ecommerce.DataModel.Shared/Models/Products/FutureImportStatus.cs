// <copyright file="FutureImportStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the future import status class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IFutureImportStatus : IStatusableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Products", "FutureImportStatus")]
    public class FutureImportStatus : StatusableBase, IFutureImportStatus
    {
    }
}
