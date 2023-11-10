// <copyright file="ISiteDomainModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISiteDomainModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for site domain model.</summary>
    public partial interface ISiteDomainModel
    {
        /// <summary>Gets or sets the header content.</summary>
        /// <value>The header content.</value>
        string? HeaderContent { get; set; }

        /// <summary>Gets or sets the footer content.</summary>
        /// <value>The footer content.</value>
        string? FooterContent { get; set; }

        /// <summary>Gets or sets the side bar content.</summary>
        /// <value>The side bar content.</value>
        string? SideBarContent { get; set; }

        /// <summary>Gets or sets the catalog content.</summary>
        /// <value>The catalog content.</value>
        string? CatalogContent { get; set; }

        /// <summary>Gets or sets URL of the document.</summary>
        /// <value>The URL.</value>
        string? Url { get; set; }

        /// <summary>Gets or sets the alternate URL 1.</summary>
        /// <value>The alternate URL 1.</value>
        string? AlternateUrl1 { get; set; }

        /// <summary>Gets or sets the alternate URL 2.</summary>
        /// <value>The alternate URL 2.</value>
        string? AlternateUrl2 { get; set; }

        /// <summary>Gets or sets the alternate URL 3.</summary>
        /// <value>The alternate URL 3.</value>
        string? AlternateUrl3 { get; set; }

        #region Associated Objects
        /// <summary>Gets or sets the brand site domains.</summary>
        /// <value>The brand site domains.</value>
        List<IBrandSiteDomainModel>? BrandSiteDomains { get; set; }
        #endregion
    }
}
