// <copyright file="XPortalConfiguration.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the portal configuration class</summary>
namespace Clarity.Ecommerce.MVC.Api.Options
{
    /// <summary>A portal configuration.</summary>
    public class XPortalConfiguration
    {
        /// <summary>Gets or sets options for controlling the portal.</summary>
        /// <value>Options that control the portal.</value>
        public PortalOptions? PortalOptions { get; set; }

        /// <summary>Gets or sets options for controlling the API.</summary>
        /// <value>Options that control the API.</value>
        public APIOptions? APIOptions { get; set; }

        /// <summary>Gets or sets options for controlling the routing.</summary>
        /// <value>Options that control the routing.</value>
        public RoutingOptions? RoutingOptions { get; set; }

        /// <summary>Gets or sets options for controlling the product editor.</summary>
        /// <value>Options that control the product editor.</value>
        public ProductEditorOptions? ProductEditorOptions { get; set; }
    }
}
