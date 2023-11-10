// <copyright file="IAmFilterableByBrandModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByBrandModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by brand model.</summary>
    /// <typeparam name="TModel">Type of the brand relationship model.</typeparam>
    public interface IAmFilterableByBrandModel<TModel>
    {
        /// <summary>Gets or sets the brands.</summary>
        /// <value>The brands.</value>
        List<TModel>? Brands { get; set; }
    }

    /// <summary>Interface for am filterable by brand model.</summary>
    public interface IAmFilterableByBrandModel
    {
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        int BrandID { get; set; }

        /// <summary>Gets or sets the brand.</summary>
        /// <value>The brand.</value>
        IBrandModel? Brand { get; set; }

        /// <summary>Gets or sets the brand key.</summary>
        /// <value>The brand key.</value>
        string? BrandKey { get; set; }

        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        string? BrandName { get; set; }
    }
}
