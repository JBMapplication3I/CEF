// <copyright file="IHaveSeoBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveSeoBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for have seo base.</summary>
    /// <seealso cref="IBase"/>
    public interface IHaveSeoBase : IBase
    {
        /// <summary>Gets or sets URL of the seo.</summary>
        /// <value>The seo URL.</value>
        string? SeoUrl { get; set; }

        /// <summary>Gets or sets the seo keywords.</summary>
        /// <value>The seo keywords.</value>
        string? SeoKeywords { get; set; }

        /// <summary>Gets or sets information describing the seo meta.</summary>
        /// <value>Information describing the seo meta.</value>
        string? SeoMetaData { get; set; }

        /// <summary>Gets or sets information describing the seo.</summary>
        /// <value>Information describing the seo.</value>
        string? SeoDescription { get; set; }

        /// <summary>Gets or sets the seo page title.</summary>
        /// <value>The seo page title.</value>
        string? SeoPageTitle { get; set; }
    }
}
