// <copyright file="IAmFilterableByStoreModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByStoreModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by store model.</summary>
    /// <typeparam name="TModel">Type of the store relationship model.</typeparam>
    public interface IAmFilterableByStoreModel<TModel>
    {
        /// <summary>Gets or sets the stores.</summary>
        /// <value>The stores.</value>
        List<TModel>? Stores { get; set; }
    }

    /// <summary>Interface for am filterable by store model.</summary>
    public interface IAmFilterableByStoreModel
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int StoreID { get; set; }

        /// <summary>Gets or sets the store.</summary>
        /// <value>The store.</value>
        IStoreModel? Store { get; set; }

        /// <summary>Gets or sets the store key.</summary>
        /// <value>The store key.</value>
        string? StoreKey { get; set; }

        /// <summary>Gets or sets the name of the store.</summary>
        /// <value>The name of the store.</value>
        string? StoreName { get; set; }

        /// <summary>Gets or sets the SEO URL of the store.</summary>
        /// <value>The SEO URL of the store.</value>
        string? StoreSeoUrl { get; set; }
    }
}
