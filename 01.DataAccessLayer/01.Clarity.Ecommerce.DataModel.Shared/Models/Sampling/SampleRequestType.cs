// <copyright file="SampleRequestType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the samples request type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISampleRequestType : ITypableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Sampling", "SampleRequestType")]
    public class SampleRequestType : TypableBase, ISampleRequestType
    {
    }
}
