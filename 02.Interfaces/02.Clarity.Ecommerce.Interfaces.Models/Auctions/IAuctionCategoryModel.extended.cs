// <copyright file="IAuctionCategoryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuctionCategoryModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IAuctionCategoryModel
    {
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
    }
}
