// <copyright file="ImportResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the import response class</summary>
namespace Clarity.Ecommerce.Providers.Importer
{
    using System.Collections.Generic;

    /// <summary>An import response.</summary>
    public class ImportResponse
    {
        /// <summary>Gets the information messages.</summary>
        /// <value>The information messages.</value>
        public List<string> InfoMessages { get; } = new();

        /// <summary>Gets the error messages.</summary>
        /// <value>The error messages.</value>
        public List<string> ErrorMessages { get; } = new();

        /// <summary>Gets the debug messages.</summary>
        /// <value>The debug messages.</value>
        public List<string> DebugMessages { get; } = new();
    }
}
