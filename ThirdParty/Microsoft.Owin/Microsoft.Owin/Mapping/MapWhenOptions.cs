// <copyright file="MapWhenOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map when options class</summary>
namespace Microsoft.Owin.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Options for the MapWhen middleware.</summary>
    public class MapWhenOptions
    {
        /// <summary>The branch taken for a positive match.</summary>
        /// <value>The branch.</value>
        public Func<IDictionary<string, object>, Task> Branch
        {
            get;
            set;
        }

        /// <summary>The user callback that determines if the branch should be taken.</summary>
        /// <value>The predicate.</value>
        public Func<IOwinContext, bool> Predicate
        {
            get;
            set;
        }

        /// <summary>The async user callback that determines if the branch should be taken.</summary>
        /// <value>The predicate asynchronous.</value>
        public Func<IOwinContext, Task<bool>> PredicateAsync
        {
            get;
            set;
        }
    }
}
