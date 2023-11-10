// <copyright file="IAmFilterableByNullableFranchiseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByNullableFranchiseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by nullable franchise model.</summary>
    public interface IAmFilterableByNullableFranchiseModel
    {
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        int? FranchiseID { get; set; }

        /// <summary>Gets or sets the franchise.</summary>
        /// <value>The franchise.</value>
        IFranchiseModel? Franchise { get; set; }

        /// <summary>Gets or sets the franchise key.</summary>
        /// <value>The franchise key.</value>
        string? FranchiseKey { get; set; }

        /// <summary>Gets or sets the name of the franchise.</summary>
        /// <value>The name of the franchise.</value>
        string? FranchiseName { get; set; }
    }
}
