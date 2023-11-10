// <copyright file="SampleRequestEventType.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample request event type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISampleRequestEventType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Sampling", "SampleRequestEventType")]
    public class SampleRequestEventType : TypableBase, ISampleRequestEventType
    {
    }
}
