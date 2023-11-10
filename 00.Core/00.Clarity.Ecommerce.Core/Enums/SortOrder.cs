// <copyright file="SortOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sort order class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent sort orders.</summary>
    public enum SortOrder
    {
        /// <summary>An enum constant representing the by default option.</summary>
        ByDefault = 0,

        /// <summary>An enum constant representing the by position option.</summary>
        ByPosition = 1,

        /// <summary>An enum constant representing the by name option.</summary>
        ByName = 2,

        /// <summary>An enum constant representing the by price option.</summary>
        ByPrice = 3,

        /// <summary>An enum constant representing the by relevance option.</summary>
        ByRelevance = 4, // Default
    }
}
