// <copyright file="QuickOrderFormProductsModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the quick order form products model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the quick order form products.</summary>
    /// <seealso cref="IQuickOrderFormProductsModel"/>
    public class QuickOrderFormProductsModel : IQuickOrderFormProductsModel
    {
        /// <inheritdoc/>
        public List<string>? Headers { get; set; }

        /// <inheritdoc cref="IQuickOrderFormProductsModel.ProductsByCategory"/>
        public List<ProductByCategoryModel>? ProductsByCategory { get; set; }

        /// <inheritdoc/>
        List<IProductByCategoryModel>? IQuickOrderFormProductsModel.ProductsByCategory { get => ProductsByCategory?.ToList<IProductByCategoryModel>(); set => ProductsByCategory = value?.Cast<ProductByCategoryModel>().ToList(); }
    }
}
