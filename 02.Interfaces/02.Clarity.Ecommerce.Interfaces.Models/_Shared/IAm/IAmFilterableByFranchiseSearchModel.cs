// <copyright file="IAmFilterableByFranchiseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByFranchiseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by franchise search model.</summary>
    public interface IAmFilterableByFranchiseSearchModel
    {
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        int? FranchiseID { get; set; }

        /// <summary>Gets or sets the franchise identifier include null.</summary>
        /// <value>The franchise identifier include null.</value>
        bool? FranchiseIDIncludeNull { get; set; }

        /// <summary>Gets or sets the franchise key.</summary>
        /// <value>The franchise key.</value>
        string? FranchiseKey { get; set; }

        /// <summary>Gets or sets the name of the franchise.</summary>
        /// <value>The name of the franchise.</value>
        string? FranchiseName { get; set; }

        /// <summary>Gets or sets the franchise name strict.</summary>
        /// <value>The franchise name strict.</value>
        bool? FranchiseNameStrict { get; set; }

        /// <summary>Gets or sets the franchise name include null.</summary>
        /// <value>The franchise name include null.</value>
        bool? FranchiseNameIncludeNull { get; set; }

        /// <summary>Gets or sets the identifier of the franchise category.</summary>
        /// <value>The identifier of the franchise category.</value>
        int? FranchiseCategoryID { get; set; }
    }
}
