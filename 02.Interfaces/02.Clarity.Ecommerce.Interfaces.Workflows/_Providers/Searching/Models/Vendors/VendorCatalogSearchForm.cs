// <copyright file="VendorCatalogSearchForm.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Vendor Catalog Search Form</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>Form for viewing the vendor catalog search.</summary>
    /// <seealso cref="SearchFormBase"/>
    public class VendorCatalogSearchForm : SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="VendorCatalogSearchForm"/> class.</summary>
        public VendorCatalogSearchForm()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VendorCatalogSearchForm"/> class.</summary>
        /// <param name="other">The other.</param>
        public VendorCatalogSearchForm(SearchFormBase other)
            : base(other)
        {
        }
    }
}
