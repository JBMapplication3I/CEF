// <copyright file="SampleRequestItemTarget.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SampleRequest item target class</summary>
// ReSharper disable MissingBlankLines
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISampleRequestItemTarget : ISalesItemTargetBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Sampling", "SampleRequestItemTarget")]
    public class SampleRequestItemTarget
        : SalesItemTargetBase<SampleRequestItem>,
            ISampleRequestItemTarget
    {
    }
}
