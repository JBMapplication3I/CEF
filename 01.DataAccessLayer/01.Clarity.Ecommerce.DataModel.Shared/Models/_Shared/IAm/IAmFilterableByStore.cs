// <copyright file="IAmFilterableByStore.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByStore interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by store.</summary>
    /// <typeparam name="TEntity">Type of the store link entity.</typeparam>
    public interface IAmFilterableByStore<TEntity> : IBase
    {
        /// <summary>Gets or sets the stores.</summary>
        /// <value>The stores.</value>
        ICollection<TEntity>? Stores { get; set; }
    }

    /// <summary>Interface for am filterable by store id.</summary>
    public interface IAmFilterableByStore : IBase
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int StoreID { get; set; }

        /// <summary>Gets or sets the store.</summary>
        /// <value>The store.</value>
        Store? Store { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) store id.</summary>
    public interface IAmFilterableByNullableStore : IBase
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets the store.</summary>
        /// <value>The store.</value>
        Store? Store { get; set; }
    }
}
