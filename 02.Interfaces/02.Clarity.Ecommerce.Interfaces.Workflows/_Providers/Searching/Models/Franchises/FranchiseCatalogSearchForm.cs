// <copyright file="FranchiseCatalogSearchForm.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise catalog search Windows Form</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>Form for viewing the franchise catalog search.</summary>
    /// <seealso cref="SearchFormBase"/>
    public class FranchiseCatalogSearchForm : SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="FranchiseCatalogSearchForm"/> class.</summary>
        public FranchiseCatalogSearchForm()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FranchiseCatalogSearchForm"/> class.</summary>
        /// <param name="other">The other.</param>
        public FranchiseCatalogSearchForm(SearchFormBase other)
            : base(other)
        {
        }
    }
}
