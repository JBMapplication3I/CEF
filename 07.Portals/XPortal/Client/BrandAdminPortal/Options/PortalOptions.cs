// <copyright file="PortalOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the portal options class</summary>
namespace Clarity.Ecommerce.MVC.Api.Options
{
    using Blazorise;

    /// <summary>A portal options.</summary>
    public class PortalOptions
    {
        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string? Title { get; set; }

        /// <summary>Gets or sets the full pathname of the brand logo image file.</summary>
        /// <value>The full pathname of the brand logo image file.</value>
        public string? BrandLogoImagePath { get; set; }

        /// <summary>Gets or sets the full pathname of the brand logo image as icon file.</summary>
        /// <value>The full pathname of the brand logo image as icon file.</value>
        public string? BrandLogoImageAsIconPath { get; set; }

        /// <summary>Gets or sets the theme.</summary>
        /// <value>The theme.</value>
        public Theme? Theme { get; set; }

        /// <summary>Gets or sets the bar theme contrast.</summary>
        /// <value>The bar theme contrast.</value>
        public ThemeContrast? BarThemeContrast { get; set; }
    }
}
