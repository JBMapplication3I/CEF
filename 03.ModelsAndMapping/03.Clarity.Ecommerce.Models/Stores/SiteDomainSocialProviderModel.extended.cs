// <copyright file="SiteDomainSocialProviderModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the site domain social provider model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the brand site domain.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.ISiteDomainSocialProviderModel"/>
    public partial class SiteDomainSocialProviderModel
    {
        /// <inheritdoc/>
        public string? Script { get; set; }

        /// <inheritdoc/>
        public string? UrlValues { get; set; }
    }
}
