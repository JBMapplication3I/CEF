// <copyright file="DashboardPageConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDashboardPageConfig interface</summary>
// ReSharper disable StyleCop.SA1300 // Conforming with JS output
// ReSharper disable StyleCop.SA1623 // Conforming with JS output
// ReSharper disable InconsistentNaming // Conforming with JS output
#pragma warning disable IDE1006 // Naming Styles, because this is used in storefront, Conforming with JS output
#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace Clarity.Ecommerce
{
    /// <summary>Interface for dashboard page configuration.</summary>
    public class DashboardPageConfig
    {
        /// <summary>Gets or sets the name to be injected to ID's and Name's of elements (no spaces).</summary>
        /// <value>The name.</value>
        public string? name { get; set; }

        /// <summary>Gets or sets the title's current translated value (will be set by the UI, don't put in cefConfig).</summary>
        /// <value>The title.</value>
        public string? title { get; set; }

        /// <summary>Gets or sets the title's translation key.</summary>
        /// <value>The title key.</value>
        public string? titleKey { get; set; }

        /// <summary>Gets or sets the name of the UI Router state for this section.</summary>
        /// <value>The SREF.</value>
        public string? sref { get; set; }

        /// <summary>Gets or sets a value indicating whether the section will show. When disabled, the section will not
        /// be generated into the menus.</summary>
        /// <value>True if enabled, false if not.</value>
        public bool enabled { get; set; }

        /// <summary>Gets or sets the SVG image content for the icon.</summary>
        /// <value>The icon.</value>
        public string? icon { get; set; }

        /// <summary>Gets or sets the array of child sections (if any).</summary>
        /// <value>The children.</value>
        public DashboardPageConfig[]? children { get; set; }

        /// <summary>Gets or sets the sort order for display.</summary>
        /// <value>The order.</value>
        public int order { get; set; }

        /// <summary>Gets or sets requires the user to have at least one of these roles to be displayed.</summary>
        /// <value>The request any roles.</value>
        public string[]? reqAnyRoles { get; set; }

        /// <summary>Gets or sets requires the user to have at least one of these permissions to be displayed.</summary>
        /// <value>The request any permissions.</value>
        public string[]? reqAnyPerms { get; set; }
    }
}
