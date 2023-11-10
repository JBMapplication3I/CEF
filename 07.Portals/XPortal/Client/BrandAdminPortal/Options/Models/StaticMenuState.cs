// <copyright file="StaticMenuState.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the static menu state class</summary>
namespace Clarity.Ecommerce.MVC.Api.Options
{
    using System.Collections.Generic;
    using Blazorise;

    /// <summary>A static menu state.</summary>
    public class StaticMenuState
    {
        /// <summary>Gets or sets the groups.</summary>
        /// <value>The groups.</value>
        public List<(string name, string title, string? icon)?> Groups { get; set; } = new();

        /// <summary>Gets or sets the pages.</summary>
        /// <value>The pages.</value>
        public List<(string group, string title, string href, string icon)> Pages { get; set; } = new();

        /// <summary>Gets or sets a value indicating whether the collapse navigation menu.</summary>
        /// <value>True if collapse navigation menu, false if not.</value>
        public bool CollapseNavMenu { get; set; } = false;

        /// <summary>Gets or sets the current bar mode.</summary>
        /// <value>The current bar mode.</value>
        public BarMode CurrentBarMode { get; set; } = BarMode.VerticalSmall;

        /// <summary>Gets or sets the side bar menu visible.</summary>
        /// <value>The side bar menu visible.</value>
        public Dictionary<string, bool> SideBarMenuVisible { get; set; } = new();
    }
}
