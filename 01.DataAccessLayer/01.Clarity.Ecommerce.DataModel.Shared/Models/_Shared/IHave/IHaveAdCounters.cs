// <copyright file="IHaveAdCounters.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAdCounters interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for have ad counters.</summary>
    public interface IHaveAdCounters
    {
        /// <summary>Gets or sets the identifier of the impression counter.</summary>
        /// <value>The identifier of the impression counter.</value>
        int? ImpressionCounterID { get; set; }

        /// <summary>Gets or sets the impression counter.</summary>
        /// <value>The impression counter.</value>
        Counter? ImpressionCounter { get; set; }

        /// <summary>Gets or sets the identifier of the click counter.</summary>
        /// <value>The identifier of the click counter.</value>
        int? ClickCounterID { get; set; }

        /// <summary>Gets or sets the click counter.</summary>
        /// <value>The click counter.</value>
        Counter? ClickCounter { get; set; }
    }
}
