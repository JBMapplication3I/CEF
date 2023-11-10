// <copyright file="MapOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the map options class</summary>
namespace Microsoft.Owin.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Options for the Map middleware.</summary>
    public class MapOptions
    {
        /// <summary>The branch taken for a positive match.</summary>
        /// <value>The branch.</value>
        public Func<IDictionary<string, object>, Task> Branch
        {
            get;
            set;
        }

        /// <summary>The path to match.</summary>
        /// <value>The path match.</value>
        public PathString PathMatch
        {
            get;
            set;
        }
    }
}
