// <copyright file="MapExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map extensions class</summary>
namespace Owin
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Owin;
    using Microsoft.Owin.Mapping;

    /// <summary>Extension methods for the MapMiddleware.</summary>
    public static class MapExtensions
    {
        /// <summary>If the request path starts with the given pathMatch, execute the app configured via configuration
        /// parameter instead of continuing to the next component in the pipeline.</summary>
        /// <param name="app">          .</param>
        /// <param name="pathMatch">    The path to match.</param>
        /// <param name="configuration">The branch to take for positive path matches.</param>
        /// <returns>An IAppBuilder.</returns>
        public static IAppBuilder Map(this IAppBuilder app, string pathMatch, Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (pathMatch == null)
            {
                throw new ArgumentNullException(nameof(pathMatch));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (!string.IsNullOrEmpty(pathMatch) && pathMatch.EndsWith("/", StringComparison.Ordinal))
            {
                throw new ArgumentException(Resources.Exception_PathMustNotEndWithSlash, nameof(pathMatch));
            }
            return app.Map(new PathString(pathMatch), configuration);
        }

        /// <summary>If the request path starts with the given pathMatch, execute the app configured via configuration
        /// parameter instead of continuing to the next component in the pipeline.</summary>
        /// <param name="app">          .</param>
        /// <param name="pathMatch">    The path to match.</param>
        /// <param name="configuration">The branch to take for positive path matches.</param>
        /// <returns>An IAppBuilder.</returns>
        public static IAppBuilder Map(this IAppBuilder app, PathString pathMatch, Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            if (pathMatch.HasValue && pathMatch.Value.EndsWith("/", StringComparison.Ordinal))
            {
                throw new ArgumentException(Resources.Exception_PathMustNotEndWithSlash, nameof(pathMatch));
            }
            var mapOption = new MapOptions { PathMatch = pathMatch };
            var appBuilder = app.Use<MapMiddleware>(mapOption);
            var appBuilder1 = app.New();
            configuration(appBuilder1);
            mapOption.Branch =
                (Func<IDictionary<string, object>, Task>)appBuilder1.Build(
                    typeof(Func<IDictionary<string, object>, Task>));
            return appBuilder;
        }
    }
}
