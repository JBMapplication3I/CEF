// <copyright file="APIOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the API Options class</summary>
namespace Clarity.Ecommerce.MVC.Api.Options
{
    using System.Linq;
    using Microsoft.AspNetCore.Components;
    using Newtonsoft.Json.Bson;
    using Utilities;

    /// <summary>A routing options.</summary>
    public class RoutingOptions
    {
        /// <summary>The final routes.</summary>
        private static PortalRoute[]? finalRoutes;

        /// <summary>Groups the final route belongs to.</summary>
        private static PortalRouteGroup[]? finalRouteGroups;

        /// <summary>Gets or sets the page title suffix.</summary>
        /// <value>The page title suffix.</value>
        public string? PageTitleSuffix { get; set; }

        /// <summary>Gets or sets the default route.</summary>
        /// <value>The default route.</value>
        public string? DefaultRoute { get; set; }

        /// <summary>Gets or sets the disabled routes.</summary>
        /// <value>The disabled routes.</value>
        public string[]? DisabledRoutes { get; set; }

        /// <summary>Gets or sets the enabled routes.</summary>
        /// <value>The enabled routes.</value>
        public string[]? EnabledRoutes { get; set; }

        /// <summary>Gets or sets the route overrides.</summary>
        /// <value>The route overrides.</value>
        public PortalRoute[]? RouteOverrides { get; set; }

        /// <summary>Gets or sets the route group overrides.</summary>
        /// <value>The route group overrides.</value>
        public PortalRouteGroup[]? RouteGroupOverrides { get; set; }

        /// <summary>Gets route by path.</summary>
        /// <param name="path">Full pathname of the file.</param>
        /// <returns>The route by path.</returns>
        public PortalRoute GetRouteByPath(string path)
        {
            if (Contract.CheckEmpty(finalRoutes))
            {
                BuildFinalRoutes();
            }
            return finalRoutes!.Single(x => x.Path == path || x.OriginalPath == path);
        }

        /// <summary>Builds final route groups.</summary>
        /// <returns>A PortalRouteGroup[].</returns>
        public PortalRouteGroup[] BuildFinalRouteGroups()
        {
            if (Contract.CheckNotEmpty(finalRouteGroups))
            {
                return finalRouteGroups!;
            }
            var routeGroups = BuildDefaultRouteGroups().ToList();
            if (Contract.CheckEmpty(RouteGroupOverrides))
            {
                // No changes to default route groups
                return finalRouteGroups = routeGroups.ToArray();
            }
            // Override any route that remains with values per route as specified
            // Assume null value means not to override original, use "<none>" to
            // intentionally override to null.
            foreach (var overrideRouteGroup in RouteGroupOverrides!)
            {
                var routeGroup = routeGroups.SingleOrDefault(x => x.Name == overrideRouteGroup.Name);
                if (routeGroup is null)
                {
                    continue;
                }
                if (Contract.CheckValidKey(overrideRouteGroup.Title))
                {
                    routeGroup.Title = NullIfNoneTagElseValue(overrideRouteGroup.Title);
                }
                if (Contract.CheckValidKey(overrideRouteGroup.Icon))
                {
                    routeGroup.Icon = NullIfNoneTagElseValue(overrideRouteGroup.Icon);
                }
                if (overrideRouteGroup.Sort.HasValue)
                {
                    routeGroup.Sort = overrideRouteGroup.Sort;
                }
            }
            return finalRouteGroups = routeGroups.ToArray();
        }

        /// <summary>Builds final routes.</summary>
        /// <returns>A PortalRoute[].</returns>
        // ReSharper disable once CognitiveComplexity
        public PortalRoute[] BuildFinalRoutes()
        {
            if (Contract.CheckNotEmpty(finalRoutes))
            {
                return finalRoutes!;
            }
            var routes = BuildDefaultRoutes().ToList();
            if (Contract.CheckEmpty(RouteOverrides)
                && Contract.CheckEmpty(DisabledRoutes)
                && Contract.CheckEmpty(EnabledRoutes))
            {
                // No changes to default routes
                return finalRoutes = routes.ToArray();
            }
            if (Contract.CheckNotEmpty(DisabledRoutes))
            {
                // Only include routes which aren't explicitly disabled
                routes = routes.Where(x => !DisabledRoutes!.Contains(x.Path)).ToList();
            }
            else if (Contract.CheckNotEmpty(EnabledRoutes))
            {
                // Only include routes which are explicitly enabled
                routes = routes.Where(x => EnabledRoutes!.Contains(x.Path)).ToList();
            }
            if (Contract.CheckEmpty(RouteOverrides))
            {
                return finalRoutes = routes.ToArray();
            }
            // Override any route that remains with values per route as specified
            // Assume null value means not to override original, use "<none>" to
            // intentionally override to null.
            foreach (var overrideRoute in RouteOverrides!)
            {
                var route = routes.SingleOrDefault(x => x.Path == overrideRoute.Path);
                if (route is null)
                {
                    continue;
                }
                if (Contract.CheckValidKey(overrideRoute.OverridePath))
                {
                    route.OriginalPath = route.Path;
                    route.Path = overrideRoute.OverridePath;
                }
                if (Contract.CheckValidKey(overrideRoute.Title))
                {
                    route.Title = NullIfNoneTagElseValue(overrideRoute.Title);
                }
                if (Contract.CheckValidKey(overrideRoute.HeaderText))
                {
                    route.HeaderText = NullIfNoneTagElseValue(overrideRoute.HeaderText);
                }
                if (Contract.CheckValidKey(overrideRoute.NavGroup))
                {
                    route.NavGroup = NullIfNoneTagElseValue(overrideRoute.NavGroup);
                }
                if (Contract.CheckValidKey(overrideRoute.RequiredRole))
                {
                    route.RequiredRole = NullIfNoneTagElseValue(overrideRoute.RequiredRole);
                }
                if (Contract.CheckValidKey(overrideRoute.RequiredPermission))
                {
                    route.RequiredPermission = NullIfNoneTagElseValue(overrideRoute.RequiredPermission);
                }
                if (overrideRoute.Hide.HasValue)
                {
                    route.Hide = overrideRoute.Hide;
                }
                if (overrideRoute.Sort.HasValue)
                {
                    route.Sort = overrideRoute.Sort;
                }
            }
            return finalRoutes = routes.ToArray();
        }

        /// <summary>Builds default route groups.</summary>
        /// <returns>A PortalRouteGroup[].</returns>
        private static PortalRouteGroup[] BuildDefaultRouteGroups() => new PortalRouteGroup[]
        {
            new(sort: 100, name: "sales", title: "Sales", icon: "far fa-analytics"),
            new(sort: 200, name: "catalog", title: "Catalog", icon: "far fa-tags"),
            new(sort: 300, name: "customers", title: "Customers", icon: "far fa-users"),
            new(sort: 400, name: "fulfillment", title: "Fulfillment", icon: "far fa-box-up"),
            new(sort: 500, name: "details", title: "Details", icon: "far fa-binoculars"),
        };

        /// <summary>Builds default routes.</summary>
        /// <returns>A PortalRoute[].</returns>
        // TODO@JTG: Append roles/permissions requirements to the default data
        private static PortalRoute[] BuildDefaultRoutes() => new PortalRoute[]
        {
            // Sales
            new(sort: 100, path: "/sales/dashboard", title: "Dashboard", icon: "far fa-tachometer-alt-fast"),
            new(sort: 110, path: "/sales/quotes", title: "Quotes", icon: "far fa-quote-right"),
            new(sort: 111, path: "/sales/quotes/editor/{IDStr}", title: "Edit Quote #{{ID}}", hide: true, icon: "far fa-quote-right"),
            new(sort: 120, path: "/sales/orders", title: "Orders", icon: "far fa-receipt"),
            new(sort: 121, path: "/sales/orders/editor/{IDStr}", title: "Edit Order #{{ID}}", hide: true, icon: "far fa-receipt"),
            new(sort: 130, path: "/sales/invoices", title: "Invoices", icon: "far fa-file-invoice-dollar"),
            new(sort: 131, path: "/sales/invoices/editor/{IDStr}", title: "Edit Invoice #{{ID}}", hide: true, icon: "far fa-file-invoice-dollar"),
            new(sort: 140, path: "/sales/returns", title: "Returns", icon: "far fa-fragile"),
            new(sort: 141, path: "/sales/returns/editor/{IDStr}", title: "Edit Return #{{ID}}", hide: true, icon: "far fa-fragile"),
            new(sort: 150, path: "/sales/bids", title: "Bids", icon: "far fa-comment-alt-dollar"),
            new(sort: 151, path: "/sales/bids/editor/{IDStr}", title: "Edit Bid #{{ID}}", hide: true, icon: "far fa-comment-alt-dollar"),
            // Catalog
            new(sort: 200, path: "/catalog/products", title: "Products", icon: "far fa-box-full"),
            new(sort: 201, path: "/catalog/products/creator", title: "Create Product", hide: true, icon: "far fa-box-full"),
            new(sort: 202, path: "/catalog/products/edit/{IDStr}", title: "Edit Product #{{ID}}", hide: true, icon: "far fa-box-full"),
            new(sort: 203, path: "/catalog/products/importer", title: "Product Importer", hide: true, icon: "far fa-box-full"),
            new(sort: 210, path: "/catalog/categories", title: "Categories", icon: "far fa-tag"),
            new(sort: 211, path: "/catalog/categories/creator", title: "Create Category", hide: true, icon: "far fa-tag"),
            new(sort: 212, path: "/catalog/categories/editor/{IDStr}", title: "Edit Category #{{ID}}", hide: true, icon: "far fa-tag"),
            new(sort: 220, path: "/catalog/warehouses", title: "Warehouses", icon: "far fa-warehouse-alt"),
            new(sort: 221, path: "/catalog/warehouses/creator", title: "Create Warehouse", hide: true, icon: "far fa-warehouse-alt"),
            new(sort: 222, path: "/catalog/warehouses/editor/{IDStr}", title: "Edit Warehouse #{{ID}}", hide: true, icon: "far fa-warehouse-alt"),
            new(sort: 230, path: "/catalog/attributes", title: "Attributes", icon: "far fa-table"),
            new(sort: 231, path: "/catalog/attributes/creator", title: "Create Attribute", hide: true, icon: "far fa-table"),
            new(sort: 232, path: "/catalog/attributes/editor/{IDStr}", title: "Edit Attribute #{{ID}}", hide: true, icon: "far fa-table"),
            new(sort: 240, path: "/catalog/auctions", title: "Auctions", icon: "far fa-gavel"),
            new(sort: 241, path: "/catalog/auctions/creator", title: "Create Auction", hide: true, icon: "far fa-gavel"),
            new(sort: 242, path: "/catalog/auctions/editor/{IDStr}", title: "Edit Auction #{{ID}}", hide: true, icon: "far fa-gavel"),
            new(sort: 250, path: "/catalog/lots", hide: true, title: "Lots", icon: "far fa-ball-pile"),
            new(sort: 251, path: "/catalog/lots/for-auction/{IDStr}", hide: true, title: "Lots for Auction #{{ID}}", icon: "far fa-ball-pile"),
            new(sort: 252, path: "/catalog/lots/creator/for-auction/{IDStr}", title: "Create Lot", hide: true, icon: "far fa-ball-pile"),
            new(sort: 253, path: "/catalog/lots/editor/{IDStr}", title: "Edit Lot #{{ID}}", hide: true, icon: "far fa-ball-pile"),
            // Customers
            new(sort: 300, path: "/customers/accounts", title: "Accounts", icon: "far fa-users"),
            new(sort: 301, path: "/customers/accounts/creator", title: "Create Account", hide: true, icon: "far fa-users"),
            new(sort: 302, path: "/customers/accounts/editor/{IDStr}", title: "Edit Account #{{ID}}", hide: true, icon: "far fa-users"),
            new(sort: 310, path: "/customers/users", title: "Users", icon: "far fa-user"),
            new(sort: 311, path: "/customers/users/creator", title: "Create User", hide: true, icon: "far fa-user"),
            new(sort: 312, path: "/customers/users/editor/{IDStr}", title: "Edit User #{{ID}}", hide: true, icon: "far fa-user"),
            // Fulfillment
            new(sort: 400, path: "/fulfillment/purchase-orders", title: "Purchase Orders", icon: "far fa-receipt"),
            // Details
            new(sort: 500, path: "/details/profile", title: "Profile", icon: "far fa-id-badge"),
            new(sort: 510, path: "/details/reports", title: "Reports", icon: "far fa-analytics"),
            new(sort: 520, path: "/details/notifications", title: "Notifications", icon: "far fa-bell"),
            new(sort: 530, path: "/details/currencies", title: "Currencies", icon: "far fa-money-bill"),
            new(sort: 531, path: "/details/currencies/creator", title: "Create Currency", hide: true, icon: "far fa-money-bill"),
            new(sort: 532, path: "/details/currencies/editor/{IDStr}", title: "Edit Currency #{{ID}}", hide: true, icon: "far fa-money-bill"),
            new(sort: 540, path: "/details/languages", title: "Languages", icon: "far fa-language"),
            new(sort: 541, path: "/details/languages/creator", title: "Create Language", hide: true, icon: "far fa-language"),
            new(sort: 542, path: "/details/languages/editor/{IDStr}", title: "Edit Language #{{ID}}", hide: true, icon: "far fa-language"),
            new(sort: 550, path: "/details/settings", title: "Settings", icon: "far fa-user-cog"),
            new(sort: 560, path: "/details/stores", title: "Stores", icon: "far fa-store"),
            new(sort: 561, path: "/details/stores/creator", title: "Create Store", hide: true, icon: "far fa-store"),
            new(sort: 562, path: "/details/stores/editor/{IDStr}", title: "Edit Store #{{ID}}", hide: true, icon: "far fa-store"),
            new(sort: 570, path: "/details/vendors", title: "Vendors", icon: "far fa-conveyor-belt"),
            new(sort: 571, path: "/details/vendors/creator", title: "Create Vendor", hide: true, icon: "far fa-conveyor-belt"),
            new(sort: 572, path: "/details/vendors/editor/{IDStr}", title: "Edit Vendor #{{ID}}", hide: true, icon: "far fa-conveyor-belt"),
            // Misc.
            new(sort: 600, path: "/sales/nation", title: "Nation", icon: "far fa-arrow-circle-down"),
        };

        /// <summary>Null if none tag.</summary>
        /// <param name="source">Source for the.</param>
        /// <returns>A string?</returns>
        private static string? NullIfNoneTagElseValue(string? source) => source == "<none>" ? null : source;
    }

    /// <summary>A navigation manager portal route extensions.</summary>
    public static class NavigationManagerPortalRouteExtensions
    {
        /// <summary>A NavigationManager extension method that navigate to portal route for.</summary>
        /// <param name="navigationManager">The navigationManager to act on.</param>
        /// <param name="routingOptions">   Options for controlling the routing.</param>
        /// <param name="originalPath">     Full pathname of the original file.</param>
        public static void NavigateToPortalRouteFor(
            this NavigationManager navigationManager,
            RoutingOptions routingOptions,
            string originalPath)
        {
            var route = routingOptions.GetRouteByPath(originalPath);
            navigationManager.NavigateTo(route.Path!);
        }
    }
}
