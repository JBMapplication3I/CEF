// <copyright file="IHaveSeoBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveSeoBaseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have SEO base search model.</summary>
    /// <seealso cref="IBaseSearchModel"/>
    public interface IHaveSeoBaseSearchModel : IBaseSearchModel
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

        /// <summary>Gets or sets the seo URL strict.</summary>
        /// <value>The seo URL strict.</value>
        bool? SeoUrlStrict { get; set; }

        /// <summary>Gets or sets the seo URL include null.</summary>
        /// <value>The seo URL include null.</value>
        bool? SeoUrlIncludeNull { get; set; }

        /// <summary>Gets or sets the seo keywords strict.</summary>
        /// <value>The seo keywords strict.</value>
        bool? SeoKeywordsStrict { get; set; }

        /// <summary>Gets or sets the seo keywords include null.</summary>
        /// <value>The seo keywords include null.</value>
        bool? SeoKeywordsIncludeNull { get; set; }

        /// <summary>Gets or sets the seo meta data strict.</summary>
        /// <value>The seo meta data strict.</value>
        bool? SeoMetaDataStrict { get; set; }

        /// <summary>Gets or sets the seo meta data include null.</summary>
        /// <value>The seo meta data include null.</value>
        bool? SeoMetaDataIncludeNull { get; set; }

        /// <summary>Gets or sets the seo description strict.</summary>
        /// <value>The seo description strict.</value>
        bool? SeoDescriptionStrict { get; set; }

        /// <summary>Gets or sets the seo description include null.</summary>
        /// <value>The seo description include null.</value>
        bool? SeoDescriptionIncludeNull { get; set; }

        /// <summary>Gets or sets the seo page title strict.</summary>
        /// <value>The seo page title strict.</value>
        bool? SeoPageTitleStrict { get; set; }

        /// <summary>Gets or sets the seo page title include null.</summary>
        /// <value>The seo page title include null.</value>
        bool? SeoPageTitleIncludeNull { get; set; }
    }
}
