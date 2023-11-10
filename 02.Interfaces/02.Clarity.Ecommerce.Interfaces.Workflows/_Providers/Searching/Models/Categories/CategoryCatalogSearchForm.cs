// <copyright file="CategoryCatalogSearchForm.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Category Catalog Search Form</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>Form for viewing the product catalog search.</summary>
    /// <seealso cref="SearchFormBase"/>
    public class CategoryCatalogSearchForm : SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="CategoryCatalogSearchForm"/> class.</summary>
        public CategoryCatalogSearchForm()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CategoryCatalogSearchForm"/> class.</summary>
        /// <param name="other">The other.</param>
        public CategoryCatalogSearchForm(SearchFormBase other)
            : base(other)
        {
        }
    }
}
