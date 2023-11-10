// <copyright file="IQuickOrderFormProductsModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IQuickOrderFormProductsModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for quick order form products model.</summary>
    public interface IQuickOrderFormProductsModel
    {
        /// <summary>Gets or sets the headers.</summary>
        /// <value>The headers.</value>
        List<string>? Headers { get; set; }

        /// <summary>Gets or sets the category the products by belongs to.</summary>
        /// <value>The products by category.</value>
        List<IProductByCategoryModel>? ProductsByCategory { get; set; }
    }
}
