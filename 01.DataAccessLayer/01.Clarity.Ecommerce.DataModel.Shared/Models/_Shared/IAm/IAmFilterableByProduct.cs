// <copyright file="IAmFilterableByProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByProduct interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by product.</summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IAmFilterableByProduct<TEntity> : IBase
    {
        /// <summary>Gets or sets the products.</summary>
        /// <value>The products.</value>
        ICollection<TEntity>? Products { get; set; }
    }

    /// <summary>Interface for am filterable by product id.</summary>
    public interface IAmFilterableByProduct : IBase
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        Product? Product { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) product id.</summary>
    public interface IAmFilterableByNullableProduct : IBase
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int? ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        Product? Product { get; set; }
    }
}
