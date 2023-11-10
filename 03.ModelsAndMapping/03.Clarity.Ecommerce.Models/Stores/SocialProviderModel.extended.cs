// <copyright file="SocialProviderModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SocialProvider model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the SocialProvider.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ISocialProviderModel"/>
    public partial class SocialProviderModel
    {
        #region SocialProvider Properties
        /// <inheritdoc/>
        public string? Url { get; set; }

        /// <inheritdoc/>
        public string? UrlFormat { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ISocialProviderModel.SiteDomainSocialProviders"/>
        public List<SiteDomainSocialProviderModel>? SiteDomainSocialProviders { get; set; }

        /// <inheritdoc/>
        List<ISiteDomainSocialProviderModel>? ISocialProviderModel.SiteDomainSocialProviders { get => SiteDomainSocialProviders?.Cast<ISiteDomainSocialProviderModel>().ToList(); set => SiteDomainSocialProviders = value?.Cast<SiteDomainSocialProviderModel>().ToList(); }
        #endregion
    }
}
