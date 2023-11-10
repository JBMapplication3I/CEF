// <copyright file="UnhandledMappingModes.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the unhandled mapping modes class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter
{
    /// <summary>Values that represent unhandled mapping modes.</summary>
    public enum UnhandledMappingModes
    {
        /// <summary>Ignore properties that are not mapped.</summary>
        Ignore,

        /// <summary>All unmapped properties will go to JsonAttributes on the entity
        /// kind from the RecordsAre property of the Mapping Config.</summary>
        JsonAttributes,
    }
}
