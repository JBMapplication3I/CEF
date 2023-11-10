// <copyright file="SocialProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the social provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISocialProvider : INameableBase
    {
        #region SocialProvider Properties
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
        ICollection<SiteDomainSocialProvider>? SiteDomainSocialProviders { get; set; }
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

    [SqlSchema("Stores", "SocialProvider")]
    public class SocialProvider : NameableBase, ISocialProvider
    {
        private ICollection<SiteDomainSocialProvider>? siteDomainSocialProviders;

        public SocialProvider()
        {
            siteDomainSocialProviders = new HashSet<SiteDomainSocialProvider>();
        }

        #region SocialProvider Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(1024), DefaultValue(null)]
        public string? Url { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(1024), DefaultValue(null)]
        public string? UrlFormat { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<SiteDomainSocialProvider>? SiteDomainSocialProviders { get => siteDomainSocialProviders; set => siteDomainSocialProviders = value; }
        #endregion
    }
}
