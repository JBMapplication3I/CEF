// <copyright file="IAmFilterableByCategory.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByCategory interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by category.</summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IAmFilterableByCategory<TEntity> : IBase
    {
        /// <summary>Gets or sets the categories.</summary>
        /// <value>The categories.</value>
        ICollection<TEntity>? Categories { get; set; }
    }

    /// <summary>Interface for am filterable by category id.</summary>
    public interface IAmFilterableByCategory : IBase
    {
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        int CategoryID { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        Category? Category { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) category id.</summary>
    public interface IAmFilterableByNullableCategory : IBase
    {
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        int? CategoryID { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        Category? Category { get; set; }
    }
}
