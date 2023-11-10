// <copyright file="IAmFilterableByNullableStoreModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByNullableStoreModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by nullable store model.</summary>
    public interface IAmFilterableByNullableStoreModel
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int? StoreID { get; set; }

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
