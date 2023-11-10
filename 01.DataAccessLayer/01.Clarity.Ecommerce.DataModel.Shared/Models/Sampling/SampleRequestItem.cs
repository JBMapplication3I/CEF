// <copyright file="SampleRequestItem.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the samples request item class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISampleRequestItem
        : ISalesItemBase<SampleRequestItem, AppliedSampleRequestItemDiscount, SampleRequestItemTarget>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Sampling", "SampleRequestItem")]
    public class SampleRequestItem
        : SalesItemBase<SampleRequest, SampleRequestItem, AppliedSampleRequestItemDiscount, SampleRequestItemTarget>,
        ISampleRequestItem
    {
    }
}
