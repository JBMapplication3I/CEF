// <copyright file="SampleRequestState.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the samples request state class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISampleRequestState : IStateableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Sampling", "SampleRequestState")]
    public class SampleRequestState : StateableBase, ISampleRequestState
    {
    }
}
