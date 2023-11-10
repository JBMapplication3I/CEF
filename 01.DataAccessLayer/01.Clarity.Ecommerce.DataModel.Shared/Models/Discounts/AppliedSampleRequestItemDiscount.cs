// <copyright file="AppliedSampleRequestItemDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied sample request item discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSampleRequestItemDiscount
        : IAppliedDiscountBase<SampleRequestItem, AppliedSampleRequestItemDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SampleRequestItemDiscounts")]
    public class AppliedSampleRequestItemDiscount
        : AppliedDiscountBase<SampleRequestItem, AppliedSampleRequestItemDiscount>, IAppliedSampleRequestItemDiscount
    {
    }
}
