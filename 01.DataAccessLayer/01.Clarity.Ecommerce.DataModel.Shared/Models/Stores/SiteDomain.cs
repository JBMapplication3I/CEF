// <copyright file="SiteDomain.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the site domain class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISiteDomain
        : INameableBase,
          IAmFilterableByBrand<BrandSiteDomain>
    {
        #region SiteDomain Properties
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
        #endregion

        #region Associated Objects
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Stores", "SiteDomain")]
    public class SiteDomain : NameableBase, ISiteDomain
    {
        private ICollection<BrandSiteDomain>? brands;

        public SiteDomain()
        {
            // IAmFilterableByBrand
            brands = new HashSet<BrandSiteDomain>();
        }

        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandSiteDomain>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise Properties
        #endregion

        #region SiteDomain Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), DefaultValue(null)]
        public string? HeaderContent { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), DefaultValue(null)]
        public string? FooterContent { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), DefaultValue(null)]
        public string? SideBarContent { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), DefaultValue(null)]
        public string? CatalogContent { get; set; }

        /// <inheritdoc/>
        [Required, StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? Url { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? AlternateUrl1 { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? AlternateUrl2 { get; set; }

        /// <inheritdoc/>
        [StringLength(512), StringIsUnicode(false), DefaultValue(null)]
        public string? AlternateUrl3 { get; set; }
        #endregion

        #region Associated Objects
        #endregion
    }
}
