// <copyright file="DetailLevel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the detail level class</summary>
namespace Clarity.Ecommerce.Providers.Taxes.AvalaraInt.Models
{
    /// <summary>Values that represent detail levels.</summary>
    public enum DetailLevel
    {
        /// <summary>An enum constant representing the tax option.</summary>
        Tax,

        /// <summary>An enum constant representing the document option.</summary>
        Document,

        /// <summary>An enum constant representing the line option.</summary>
        Line,

        /// <summary>An enum constant representing the diagnostic option.</summary>
        Diagnostic,
    }
}
