// <copyright file="ManufacturerCatalogSearchForm.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer catalog search Windows Form</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>Form for viewing the manufacturer catalog search.</summary>
    /// <seealso cref="SearchFormBase"/>
    public class ManufacturerCatalogSearchForm : SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="ManufacturerCatalogSearchForm"/> class.</summary>
        public ManufacturerCatalogSearchForm()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ManufacturerCatalogSearchForm"/> class.</summary>
        /// <param name="other">The other.</param>
        public ManufacturerCatalogSearchForm(SearchFormBase other)
            : base(other)
        {
        }
    }
}
