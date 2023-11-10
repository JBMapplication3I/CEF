// <copyright file="ShipCostCalculatorDataContract.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ShipCostCalculatorDataContract class</summary>
namespace Clarity.Ecommerce.Providers.Packaging.DynamicsAx.Models
{
    /// <summary>A package dimensions calculator</summary>
    public class ShipCostCalculatorDataContract
    {
        /// <summary>Gets or sets the error message.</summary>
        /// <value>The error message</value>
        public string? ErrorMessage { get; set; }

        /// <summary>Gets or sets the package depth</summary>
        /// <value>The package depth</value>
        public decimal? PackageDepth { get; set; }

        /// <summary>Gets or sets the package hazard class</summary>
        /// <value>The package hazard class</value>
        public string? PackageHazardClass { get; set; }

        /// <summary>Gets or sets the package height</summary>
        /// <value>The package height</value>
        public decimal? PackageHeight { get; set; }

        /// <summary>Gets or sets the identifier</summary>
        /// <value>The identifier</value>
        public string? PackageItemId { get; set; }

        /// <summary>Gets or sets the package weight</summary>
        /// <value>The package weight</value>
        public decimal? PackageWeight { get; set; }

        /// <summary>Gets or sets the package width</summary>
        /// <value>The package width</value>
        public decimal? PackageWidth { get; set; }

        /// <summary>Gets or sets the package success flag</summary>
        /// <value>The package success flag</value>
        public bool? Success { get; set; }
    }
}
