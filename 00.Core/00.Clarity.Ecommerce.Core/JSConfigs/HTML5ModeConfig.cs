// <copyright file="HTML5ModeConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the HTML5 mode configuration class</summary>
// ReSharper disable StyleCop.SA1300 // Conforming with JS output
// ReSharper disable StyleCop.SA1623 // Conforming with JS output
// ReSharper disable InconsistentNaming // Conforming with JS output
#pragma warning disable IDE1006 // Naming Styles, because this is used in storefront, Conforming with JS output
#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace Clarity.Ecommerce
{
    /// <summary>Interface for HTML5 mode configuration.</summary>
    public class HTML5ModeConfig
    {
        /// <summary>Gets or sets a value indicating whether this IHTML5ModeConfig is enabled.</summary>
        /// <value>True if enabled, false if not.</value>
        public bool enabled { get; set; }

        /// <summary>Gets or sets a value indicating whether the require base.</summary>
        /// <value>True if require base, false if not.</value>
        public bool requireBase { get; set; }
    }
}
