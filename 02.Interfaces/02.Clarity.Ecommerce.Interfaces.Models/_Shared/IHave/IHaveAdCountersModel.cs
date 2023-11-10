// <copyright file="IHaveAdCountersModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAdCountersModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have ad counters model.</summary>
    public interface IHaveAdCountersModel
    {
        /// <summary>Gets or sets the identifier of the impression counter.</summary>
        /// <value>The identifier of the impression counter.</value>
        int? ImpressionCounterID { get; set; }

        /// <summary>Gets or sets the impression counter key.</summary>
        /// <value>The impression counter key.</value>
        string? ImpressionCounterKey { get; set; }

        /// <summary>Gets or sets the impression counter.</summary>
        /// <value>The impression counter.</value>
        ICounterModel? ImpressionCounter { get; set; }

        /// <summary>Gets or sets the identifier of the click counter.</summary>
        /// <value>The identifier of the click counter.</value>
        int? ClickCounterID { get; set; }

        /// <summary>Gets or sets the click counter key.</summary>
        /// <value>The click counter key.</value>
        string? ClickCounterKey { get; set; }

        /// <summary>Gets or sets the click counter.</summary>
        /// <value>The click counter.</value>
        ICounterModel? ClickCounter { get; set; }
    }
}
