// <copyright file="ProductCategoryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product category model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the product category.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IProductCategoryModel"/>
    public partial class ProductCategoryModel
    {
        /// <inheritdoc/>
        public int? SortOrder { get; set; }
    }
}
