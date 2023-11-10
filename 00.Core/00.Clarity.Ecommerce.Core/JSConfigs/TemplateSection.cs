// <copyright file="TemplateSection.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the TemplateSection interface</summary>
// ReSharper disable StyleCop.SA1300 // Conforming with JS output
// ReSharper disable StyleCop.SA1623 // Conforming with JS output
// ReSharper disable InconsistentNaming // Conforming with JS output
#pragma warning disable IDE1006 // Naming Styles, because this is used in storefront, Conforming with JS output
#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace Clarity.Ecommerce
{
    /// <summary>Interface for template section.</summary>
    public class TemplateSection
    {
        /// <summary>Gets or sets a value indicating whether the active.</summary>
        /// <value>True if active, false if not.</value>
        public bool active { get; set; }

        /// <summary>Gets or sets a value indicating whether the complete.</summary>
        /// <value>True if complete, false if not.</value>
        public bool complete { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? name { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        public string? title { get; set; }

        /// <summary>Gets or sets a value indicating whether this ITemplateSection is shown.</summary>
        /// <value>True if show, false if not.</value>
        public bool show { get; set; }

        /// <summary>Gets or sets URL of the heading details.</summary>
        /// <value>The heading details URL.</value>
        public string? headingDetailsURL { get; set; }

        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        public int position { get; set; }

        /// <summary>Gets or sets URL of the template.</summary>
        /// <value>The template URL.</value>
        public string? templateURL { get; set; }

        /// <summary>Gets or sets the continue text.</summary>
        /// <value>The continue text.</value>
        public string? continueText { get; set; }

        /// <summary>Gets or sets a value indicating whether the button is shown.</summary>
        /// <value>True if show button, false if not.</value>
        public bool showButton { get; set; }

        /// <summary>Gets or sets the children.</summary>
        /// <value>The children.</value>
        public TemplateSection[]? children { get; set; }
    }
}
