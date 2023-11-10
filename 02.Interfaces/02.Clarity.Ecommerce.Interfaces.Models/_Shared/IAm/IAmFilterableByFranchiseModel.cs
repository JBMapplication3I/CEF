// <copyright file="IAmFilterableByFranchiseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByFranchiseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by franchise model.</summary>
    /// <typeparam name="TModel">Type of the franchise relationship model.</typeparam>
    public interface IAmFilterableByFranchiseModel<TModel>
    {
        /// <summary>Gets or sets the franchises.</summary>
        /// <value>The franchises.</value>
        List<TModel>? Franchises { get; set; }
    }

    /// <summary>Interface for am filterable by franchise model.</summary>
    public interface IAmFilterableByFranchiseModel
    {
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        int FranchiseID { get; set; }

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
