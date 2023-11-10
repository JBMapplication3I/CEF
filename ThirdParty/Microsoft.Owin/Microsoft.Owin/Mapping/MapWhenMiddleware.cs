// <copyright file="MapWhenMiddleware.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map when middleware class</summary>
namespace Microsoft.Owin.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Determines if the request should take a specific branch of the pipeline by passing the environment
    /// to a user defined callback.</summary>
    public class MapWhenMiddleware
    {
        /// <summary>The next.</summary>
        private readonly Func<IDictionary<string, object>, Task> _next;

        /// <summary>Options for controlling the operation.</summary>
        private readonly MapWhenOptions _options;

        /// <summary>Initializes a new instance of the <see cref="MapWhenMiddleware" /> class.</summary>
        /// <param name="next">   The normal application pipeline.</param>
        /// <param name="options">.</param>
        public MapWhenMiddleware(Func<IDictionary<string, object>, Task> next, MapWhenOptions options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>Process an individual request.</summary>
        /// <param name="environment">.</param>
        /// <returns>A Task.</returns>
#pragma warning disable IDE1006 // Naming Styles
        public async Task Invoke(IDictionary<string, object> environment)
#pragma warning restore IDE1006 // Naming Styles
        {
            IOwinContext owinContext = new OwinContext(environment);
            if (_options.Predicate != null)
            {
                if (!_options.Predicate(owinContext))
                {
                    await _next(environment);
                }
                else
                {
                    await _options.Branch(environment);
                }
            }
            else if (!await _options.PredicateAsync(owinContext))
            {
                await _next(environment);
            }
            else
            {
                await _options.Branch(environment);
            }
        }
    }
}
