// <copyright file="LotCatalogSearchForm.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Lot Catalog Search Form</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>Form for viewing the Lot catalog search.</summary>
    /// <seealso cref="SearchFormBase"/>
    public class LotCatalogSearchForm : SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="LotCatalogSearchForm"/> class.</summary>
        public LotCatalogSearchForm()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="LotCatalogSearchForm"/> class.</summary>
        /// <param name="other">The other.</param>
        public LotCatalogSearchForm(SearchFormBase other)
            : base(other)
        {
        }
    }
}
