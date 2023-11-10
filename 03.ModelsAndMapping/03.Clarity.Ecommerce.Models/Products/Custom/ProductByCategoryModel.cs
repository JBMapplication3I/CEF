// <copyright file="ProductByCategoryModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product by category model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the product by category.</summary>
    /// <seealso cref="IProductByCategoryModel"/>
    public class ProductByCategoryModel : IProductByCategoryModel
    {
        /// <inheritdoc/>
        public int? CategoryID { get; set; }

        /// <inheritdoc/>
        public string? CategoryName { get; set; }

        /// <inheritdoc/>
        public int? SortOrder { get; set; }

        /// <inheritdoc cref="IProductByCategoryModel.Products"/>
        public List<ProductModel>? Products { get; set; }

        /// <inheritdoc/>
        List<IProductModel>? IProductByCategoryModel.Products { get => Products?.ToList<IProductModel>(); set => Products = value?.Cast<ProductModel>().ToList(); }
    }
}
