// <copyright file="FavoriteCategorySearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite category search model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the favorite category search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IFavoriteCategorySearchModel"/>
    public partial class FavoriteCategorySearchModel
    {
        /// <inheritdoc/>
        public int? CategoryID { get; set; }

        /// <inheritdoc/>
        public string? CategoryKey { get; set; }

        /// <inheritdoc/>
        public string? CategoryName { get; set; }
    }
}
