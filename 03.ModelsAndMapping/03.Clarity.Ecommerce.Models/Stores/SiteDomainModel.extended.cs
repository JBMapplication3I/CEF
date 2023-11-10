// <copyright file="SiteDomainModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the site domain model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the Site Domain.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ISiteDomainModel"/>
    public partial class SiteDomainModel
    {
        /// <inheritdoc/>
        public string? HeaderContent { get; set; }

        /// <inheritdoc/>
        public string? FooterContent { get; set; }

        /// <inheritdoc/>
        public string? SideBarContent { get; set; }

        /// <inheritdoc/>
        public string? CatalogContent { get; set; }

        /// <inheritdoc/>
        public string? Url { get; set; }

        /// <inheritdoc/>
        public string? AlternateUrl1 { get; set; }

        /// <inheritdoc/>
        public string? AlternateUrl2 { get; set; }

        /// <inheritdoc/>
        public string? AlternateUrl3 { get; set; }

        #region Associated Objects
        /// <inheritdoc cref="ISiteDomainModel.BrandSiteDomains"/>
        public List<BrandSiteDomainModel>? BrandSiteDomains { get; set; }

        /// <inheritdoc/>
        List<IBrandSiteDomainModel>? ISiteDomainModel.BrandSiteDomains { get => BrandSiteDomains?.Cast<IBrandSiteDomainModel>().ToList(); set => BrandSiteDomains = value?.Cast<BrandSiteDomainModel>().ToList(); }
        #endregion
    }
}
