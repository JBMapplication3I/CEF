// <copyright file="CategoryIndexableModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category indexable model class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>A data Model for the category indexable.</summary>
    /// <seealso cref="IndexableModelBase"/>
    public class CategoryIndexableModel : IndexableModelBase
    {
        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        public string? Category { get; set; }
    }
}
