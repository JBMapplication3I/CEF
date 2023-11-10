// <copyright file="MapWhenExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map when extensions class</summary>
namespace Owin
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Owin;
    using Microsoft.Owin.Mapping;

    /// <summary>Extension methods for the MapWhenMiddleware.</summary>
    public static class MapWhenExtensions
    {
        /// <summary>Branches the request pipeline based on the result of the given predicate.</summary>
        /// <param name="app">          .</param>
        /// <param name="predicate">    Invoked with the request environment to determine if the branch should be taken.</param>
        /// <param name="configuration">Configures a branch to take.</param>
        /// <returns>An IAppBuilder.</returns>
        public static IAppBuilder MapWhen(
            this IAppBuilder app,
            Func<IOwinContext, bool> predicate,
            Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            var mapWhenOption = new MapWhenOptions { Predicate = predicate };
            var appBuilder = app.Use<MapWhenMiddleware>(mapWhenOption);
            var appBuilder1 = app.New();
            configuration(appBuilder1);
            mapWhenOption.Branch =
                (Func<IDictionary<string, object>, Task>)appBuilder1.Build(
                    typeof(Func<IDictionary<string, object>, Task>));
            return appBuilder;
        }

        /// <summary>Branches the request pipeline based on the async result of the given predicate.</summary>
        /// <param name="app">          .</param>
        /// <param name="predicate">    Invoked asynchronously with the request environment to determine if the branch
        ///                             should be taken.</param>
        /// <param name="configuration">Configures a branch to take.</param>
        /// <returns>An IAppBuilder.</returns>
        public static IAppBuilder MapWhenAsync(
            this IAppBuilder app,
            Func<IOwinContext, Task<bool>> predicate,
            Action<IAppBuilder> configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            var mapWhenOption = new MapWhenOptions { PredicateAsync = predicate };
            var appBuilder = app.Use<MapWhenMiddleware>(mapWhenOption);
            var appBuilder1 = app.New();
            configuration(appBuilder1);
            mapWhenOption.Branch =
                (Func<IDictionary<string, object>, Task>)appBuilder1.Build(
                    typeof(Func<IDictionary<string, object>, Task>));
            return appBuilder;
        }
    }
}
