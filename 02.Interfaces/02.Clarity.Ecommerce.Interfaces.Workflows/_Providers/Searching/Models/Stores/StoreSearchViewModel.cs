// <copyright file="StoreSearchViewModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product search view model class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    using System.Collections.Generic;

    /// <summary>A ViewModel for the product search.</summary>
    /// <seealso cref="SearchViewModelBase{StoreCatalogSearchForm, StoreIndexableModel}"/>
    public class StoreSearchViewModel
        : SearchViewModelBase<StoreCatalogSearchForm, StoreIndexableModel>
    {
        /// <summary>Gets or sets the badge IDs.</summary>
        /// <value>The badge IDs.</value>
        public Dictionary<string, long?>? BadgeIDs { get; set; } = new();
    }
}
