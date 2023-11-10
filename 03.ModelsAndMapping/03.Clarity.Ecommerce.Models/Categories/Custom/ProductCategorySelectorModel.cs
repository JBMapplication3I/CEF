// <copyright file="ProductCategorySelectorModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product category selector model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the product category selector.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IProductCategorySelectorModel"/>
    public class ProductCategorySelectorModel : NameableBaseModel, IProductCategorySelectorModel
    {
        /// <inheritdoc/>
        public string? SeoUrl { get; set; }

        /// <inheritdoc/>
        public int? ParentID { get; set; }

        /// <inheritdoc/>
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        public bool? HasChildren { get; set; }

        /// <inheritdoc/>
        public bool IsSelfSelected { get; set; }

        /// <inheritdoc/>
        public bool IsChildSelected { get; set; }

        /// <inheritdoc cref="IProductCategorySelectorModel.Children"/>
        public List<ProductCategorySelectorModel>? Children { get; set; }

        /// <inheritdoc/>
        List<IProductCategorySelectorModel>? IProductCategorySelectorModel.Children { get => Children?.ToList<IProductCategorySelectorModel>(); set => Children = value?.Cast<ProductCategorySelectorModel>().ToList(); }
    }
}
