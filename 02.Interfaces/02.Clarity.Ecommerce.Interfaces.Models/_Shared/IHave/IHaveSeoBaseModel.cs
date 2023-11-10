// <copyright file="IHaveSeoBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveSeoBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have SEO base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveSeoBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the SEO keywords.</summary>
        /// <value>The SEO keywords.</value>
        string? SeoKeywords { get; set; }

        /// <summary>Gets or sets URL of the SEO.</summary>
        /// <value>The SEO URL.</value>
        string? SeoUrl { get; set; }

        /// <summary>Gets or sets information describing the SEO meta.</summary>
        /// <value>Information describing the SEO meta.</value>
        string? SeoMetaData { get; set; }

        /// <summary>Gets or sets information describing the SEO.</summary>
        /// <value>Information describing the SEO.</value>
        string? SeoDescription { get; set; }

        /// <summary>Gets or sets the SEO page title.</summary>
        /// <value>The SEO page title.</value>
        string? SeoPageTitle { get; set; }
    }
}
