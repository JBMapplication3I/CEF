// <copyright file="SampleRequestEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sample request event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISampleRequestEvent : ISalesEventBase<SampleRequest, SampleRequestEventType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Sampling", "SampleRequestEvent")]
    public class SampleRequestEvent
        : SalesEventBase<SampleRequest, SampleRequestEventType>,
            ISampleRequestEvent
    {
    }
}
