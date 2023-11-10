// <copyright file="IAmFilterableByFranchise.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByFranchise interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by franchise.</summary>
    /// <typeparam name="TEntity">Type of the franchise link entity.</typeparam>
    public interface IAmFilterableByFranchise<TEntity> : IBase
    {
        /// <summary>Gets or sets the franchises.</summary>
        /// <value>The franchises.</value>
        ICollection<TEntity>? Franchises { get; set; }
    }

    /// <summary>Interface for am filterable by franchise id.</summary>
    public interface IAmFilterableByFranchise : IBase
    {
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        int FranchiseID { get; set; }

        /// <summary>Gets or sets the franchise.</summary>
        /// <value>The franchise.</value>
        Franchise? Franchise { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) franchise id.</summary>
    public interface IAmFilterableByNullableFranchise : IBase
    {
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        int? FranchiseID { get; set; }

        /// <summary>Gets or sets the franchise.</summary>
        /// <value>The franchise.</value>
        Franchise? Franchise { get; set; }
    }
}
