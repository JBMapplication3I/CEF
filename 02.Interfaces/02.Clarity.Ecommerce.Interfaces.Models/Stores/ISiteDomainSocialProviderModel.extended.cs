// <copyright file="ISiteDomainSocialProviderModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISiteDomainSocialProviderModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for brand site domain model.</summary>
    public partial interface ISiteDomainSocialProviderModel
    {
        /// <summary>Gets or sets the script.</summary>
        /// <value>The script.</value>
        string? Script { get; set; }

        /// <summary>Gets or sets the URL values.</summary>
        /// <value>The URL values.</value>
        string? UrlValues { get; set; }
    }
}
