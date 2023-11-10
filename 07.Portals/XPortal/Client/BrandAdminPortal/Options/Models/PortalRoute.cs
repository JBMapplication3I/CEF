// <copyright file="PortalRoute.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the portal route class</summary>
namespace Clarity.Ecommerce.MVC.Api.Options
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A portal route.</summary>
    public class PortalRoute
    {
        /// <summary>Initializes a new instance of the <see cref="PortalRoute"/> class.</summary>
        public PortalRoute()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PortalRoute"/> class.</summary>
        /// <param name="path">              Full pathname of the file.</param>
        /// <param name="title">             The title.</param>
        /// <param name="headerText">        The header text.</param>
        /// <param name="navGroup">          Group the navigation belongs to.</param>
        /// <param name="requiredRole">      The required role.</param>
        /// <param name="requiredPermission">The required permission.</param>
        /// <param name="icon">              The font awesome icon.</param>
        /// <param name="hide">              True to hide, false to show in menu.</param>
        /// <param name="sort">              The sort order.</param>
        public PortalRoute(
            string path,
            string? title = null,
            string? headerText = null,
            string? navGroup = null,
            string? requiredRole = null,
            string? requiredPermission = null,
            string? icon = null,
            bool hide = false,
            int? sort = null)
        {
            Path = path;
            Hide = hide;
            if (Contract.CheckValidKey(navGroup))
            {
                NavGroup = navGroup;
            }
            else if (path.ToCharArray().Count(x => x == '/') > 1)
            {
                // If there's no nav group provided but the path has multiple segments, use the first segment as the nav group
                NavGroup = path.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];
            }
            if (Contract.CheckValidKey(title))
            {
                Title = title;
                if (!Contract.CheckValidKey(headerText))
                {
                    // If there's no header text but there is a valid title, use the title instead
                    HeaderText = title;
                }
            }
            else if (Contract.CheckValidKey(headerText))
            {
                HeaderText = headerText;
            }
            if (Contract.CheckValidKey(icon))
            {
                Icon = icon;
            }
            if (Contract.CheckValidKey(requiredRole))
            {
                RequiredRole = requiredRole;
            }
            if (Contract.CheckValidKey(requiredPermission))
            {
                RequiredPermission = requiredPermission;
            }
            Sort = sort;
        }

        /// <summary>Gets or sets the full pathname of the file.</summary>
        /// <value>The full pathname of the file.</value>
        public string? Path { get; set; }

        /// <summary>Gets or sets the full pathname of the override file.</summary>
        /// <value>The full pathname of the override file.</value>
        public string? OverridePath { get; set; }

        /// <summary>Gets or sets the group the navigation belongs to.</summary>
        /// <value>The navigation group.</value>
        [DefaultValue(null)]
        public string? NavGroup { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [DefaultValue(null)]
        public string? Title { get; set; }

        /// <summary>Gets or sets the header text.</summary>
        /// <value>The header text.</value>
        [DefaultValue(null)]
        public string? HeaderText { get; set; }

        /// <summary>Gets or sets the font awesome icon.</summary>
        /// <value>The font awesome icon.</value>
        public string? Icon { get; set; }

        /// <summary>Gets or sets the required role.</summary>
        /// <value>The required role.</value>
        [DefaultValue(null)]
        public string? RequiredRole { get; set; }

        /// <summary>Gets or sets the required permission.</summary>
        /// <value>The required permission.</value>
        [DefaultValue(null)]
        public string? RequiredPermission { get; set; }

        /// <summary>Gets or sets a value indicating whether from menu is hidden.</summary>
        /// <value>True if hide from menu, false if not.</value>
        [DefaultValue(false)]
        public bool? Hide { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        [DefaultValue(null)]
        public int? Sort { get; set; }

        #region Flags
        /// <summary>Gets a value indicating whether this PortalRoute has path parameters.</summary>
        /// <value>True if this PortalRoute has path parameters, false if not.</value>
        [JsonIgnore]
        public bool HasPathParams => Path?.Contains('{') == true;

        /// <summary>Gets a value indicating whether this PortalRoute has title parameters.</summary>
        /// <value>True if this PortalRoute has title parameters, false if not.</value>
        [JsonIgnore]
        public bool HasTitleParams => Title?.Contains("{{") == true;

        /// <summary>Gets a value indicating whether this PortalRoute has header text parameters.</summary>
        /// <value>True if this PortalRoute has header text parameters, false if not.</value>
        [JsonIgnore]
        public bool HasHeaderTextParams => HeaderText?.Contains("{{") == true;

        /// <summary>Gets or sets the original route path for this route (only set when overridden).</summary>
        /// <value>The original route path.</value>
        [JsonIgnore]
        public string? OriginalPath { get; set; }
        #endregion
    }
}
