// <copyright file="UrlConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the UrlConfig class</summary>
namespace Clarity.Ecommerce
{
    /// <summary>Interface for URL configuration.</summary>
    public class UrlConfig
    {
        /// <summary>Gets or sets the domain where the area is hosted. This can be provided by the API via Brand or Store
        /// lookup by Site Domain (except the API area itself). Leave null to use the current domain.
        /// @warning Do not end with an '/'.</summary>
        /// <value>The host URL.</value>
        public string? HostUrl { get; set; }

        /// <summary>Gets or sets the relative path to the area. This can be provided by the API via Brand or Store
        /// lookup by Site Domain (except the API area itself). Leave null to use the root or look up via Brand or
        /// Store. This should be relative to the host or return value of the Brand or Store lookup by Site Domain if
        /// set.
        /// @warning Do not end with an '/'.</summary>
        /// <value>The full pathname of the relative file.</value>
        public string? RelativePath { get; set; }

        /// <summary>Gets or sets when set to "Brand", the UI Host Domain will be provided by the first active site
        /// domain on the Brand. When set to "Store", the UI Host Domain will be provided by the first active site
        /// domain on the Store. When set to false, the UI Host Domain wil be provided by however host is set.
        /// @warning This setting overrides ui.host when set to "Brand" or "Store"
        /// @example
        /// // Not provided by lookup false // Provided by Store Lookup using the SiteDomain.Url
        /// { type: "Store", whichUrl: "primary" }
        /// // Provided by Store Lookup using SiteDomain.AlternateUrl1
        /// { type: "Store", whichUrl: "alternate-1" }
        /// // Provided by Store Lookup using SiteDomain.AlternateUrl2
        /// { type: "Store", whichUrl: "alternate-2" }
        /// // Provided by Store Lookup using SiteDomain.AlternateUrl3
        /// { type: "Store", whichUrl: "alternate-3" }
        /// // Provided by Brand Lookup using SiteDomain.Url
        /// { type: "Brand", whichUrl: "primary" }
        /// // Provided by Brand Lookup using SiteDomain.AlternateUrl1
        /// { type: "Brand", whichUrl: "alternate-1" }
        /// // Provided by Brand Lookup using SiteDomain.AlternateUrl2
        /// { type: "Brand", whichUrl: "alternate-2" }
        /// // Provided by Brand Lookup using SiteDomain.AlternateUrl3
        /// { type: "Brand", whichUrl: "alternate-3" }.</summary>
        /// <value>The host lookup method.</value>
        public Enums.HostLookupMethod HostLookupMethod { get; set; }

        /// <summary>Gets or sets URL of the host lookup which.</summary>
        /// <value>The host lookup which URL.</value>
        public Enums.HostLookupWhichUrl? HostLookupWhichUrl { get; set; }
    }
}
