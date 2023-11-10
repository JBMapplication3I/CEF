// <copyright file="PortalRouteGroup.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the portal route group class</summary>
namespace Clarity.Ecommerce.MVC.Api.Options
{
    using System.ComponentModel;

    /// <summary>A portal route group.</summary>
    public class PortalRouteGroup
    {
        /// <summary>Initializes a new instance of the <see cref="PortalRouteGroup"/> class.</summary>
        public PortalRouteGroup()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PortalRouteGroup"/> class.</summary>
        /// <param name="sort"> The sort.</param>
        /// <param name="name"> The name.</param>
        /// <param name="title">The title.</param>
        /// <param name="icon"> The icon.</param>
        public PortalRouteGroup(int? sort, string? name, string? title, string? icon)
        {
            Sort = sort;
            Name = name;
            Title = title;
            Icon = icon;
        }

        /// <summary>Gets or sets the sort.</summary>
        /// <value>The sort.</value>
        [DefaultValue(0)]
        public int? Sort { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [DefaultValue(null)]
        public string? Name { get; set; } = "";

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [DefaultValue(null)]
        public string? Title { get; set; } = "";

        /// <summary>Gets or sets the icon.</summary>
        /// <value>The icon.</value>
        [DefaultValue(null)]
        public string? Icon { get; set; } = "";
    }
}
