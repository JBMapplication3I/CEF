// <copyright file="AuctionCatalogSearchForm.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Auction Catalog Search Form</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>Form for viewing the product catalog search.</summary>
    /// <seealso cref="SearchFormBase"/>
    public class AuctionCatalogSearchForm : SearchFormBase
    {
        /// <summary>Initializes a new instance of the <see cref="AuctionCatalogSearchForm"/> class.</summary>
        public AuctionCatalogSearchForm()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="AuctionCatalogSearchForm"/> class.</summary>
        /// <param name="other">The other.</param>
        public AuctionCatalogSearchForm(SearchFormBase other)
            : base(other)
        {
        }
    }
}
