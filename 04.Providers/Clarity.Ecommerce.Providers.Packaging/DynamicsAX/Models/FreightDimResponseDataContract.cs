// <copyright file="FreightDimResponseDataContract.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FreightDimResponseDataContract class</summary>
namespace Clarity.Ecommerce.Providers.Packaging.DynamicsAx.Models
{
    using System.Collections.Generic;

    /// <summary>A package dimensions calculator collection.</summary>
    public class FreightDimResponseDataContract
    {
        /// <summary>Gets or sets the error message.</summary>
        /// <value>The error message.</value>
        public string? ErrorMessage { get; set; }

        /// <summary>Gets or sets the package collection.</summary>
        /// <value>The package collection.</value>
        public List<ShipCostCalculatorDataContract>? Packages { get; set; }
    }
}
