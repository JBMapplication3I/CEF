// <copyright file="MapMiddleware.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map middleware class</summary>
namespace Microsoft.Owin.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Used to create path based branches in your application pipeline. The owin.RequestPathBase is not
    /// included in the evaluation, only owin.RequestPath. Matching paths have the matching piece removed from
    /// owin.RequestPath and added to the owin.RequestPathBase.</summary>
    public class MapMiddleware
    {
        /// <summary>The next.</summary>
        private readonly Func<IDictionary<string, object>, Task> _next;

        /// <summary>Options for controlling the operation.</summary>
        private readonly MapOptions _options;

        /// <summary>Initializes a new instance of the <see cref="MapMiddleware" /> class.</summary>
        /// <param name="next">   The normal pipeline taken for a negative match.</param>
        /// <param name="options">.</param>
        public MapMiddleware(Func<IDictionary<string, object>, Task> next, MapOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            var pathMatch = options.PathMatch;
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options;
        }

        /// <summary>Process an individual request.</summary>
        /// <param name="environment">.</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task Invoke(IDictionary<string, object> environment)
#pragma warning restore IDE1006 // Naming Styles
        {
            IOwinContext owinContext = new OwinContext(environment);
            var path = owinContext.Request.Path;
            if (!path.StartsWithSegments(_options.PathMatch, out var pathString))
            {
                await _next(environment);
            }
            else
            {
                var pathBase = owinContext.Request.PathBase;
                owinContext.Request.PathBase = pathBase + _options.PathMatch;
                owinContext.Request.Path = pathString;
                await _options.Branch(environment);
                owinContext.Request.PathBase = pathBase;
                owinContext.Request.Path = path;
                pathBase = new PathString();
            }
        }
    }
}
