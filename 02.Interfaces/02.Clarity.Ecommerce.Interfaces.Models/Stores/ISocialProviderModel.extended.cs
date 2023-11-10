// <copyright file="ISocialProviderModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISocialProviderModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for social provider model.</summary>
    public partial interface ISocialProviderModel
    {
        #region Social Provider Properties
        /// <summary>Gets or sets URL of the document.</summary>
        /// <value>The URL.</value>
        string? Url { get; set; }

        /// <summary>Gets or sets the URL format.</summary>
        /// <value>The URL format.</value>
        string? UrlFormat { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the site domain social providers.</summary>
        /// <value>The site domain social providers.</value>
        List<ISiteDomainSocialProviderModel>? SiteDomainSocialProviders { get; set; }
        #endregion
    }
}
