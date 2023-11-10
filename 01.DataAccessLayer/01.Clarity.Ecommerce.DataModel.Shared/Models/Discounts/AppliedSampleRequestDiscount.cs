// <copyright file="AppliedSampleRequestDiscount.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the applied samples request discount class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IAppliedSampleRequestDiscount
        : IAppliedDiscountBase<SampleRequest, AppliedSampleRequestDiscount>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Discounts", "SampleRequestDiscounts")]
    public class AppliedSampleRequestDiscount
        : AppliedDiscountBase<SampleRequest, AppliedSampleRequestDiscount>, IAppliedSampleRequestDiscount
    {
    }
}
