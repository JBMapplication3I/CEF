// <copyright file="SearchSort.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the search sort class</summary>
namespace Clarity.Ecommerce.Enums
{
    /// <summary>Values that represent search sorts.</summary>
    public enum SearchSort
    {
        /// <summary>An enum constant representing the relevance option (the function score value).</summary>
        Relevance,

        /// <summary>An enum constant representing the popular option (the x.TotalPurchasedQuantity value).</summary>
        Popular,

        /// <summary>An enum constant representing the recent option (the (x.UpdatedDate ?? x.CreatedDate) value).</summary>
        Recent,

        /// <summary>An enum constant representing the name ascending option.</summary>
        NameAscending,

        /// <summary>An enum constant representing the name descending option.</summary>
        NameDescending,

        /// <summary>An enum constant representing the price ascending option (functions with FlatPricing only).</summary>
        PricingAscending,

        /// <summary>An enum constant representing the price descending option (functions with FlatPricing only).</summary>
        PricingDescending,

        /// <summary>An enum constant representing the rating ascending option.</summary>
        RatingAscending,

        /// <summary>An enum constant representing the rating descending option.</summary>
        RatingDescending,

        /// <summary>An enum constant representing the defined option (the x.SortOrder value).</summary>
        Defined,

        /// <summary>An enum constant representing the closing soon option.</summary>
        ClosingSoon,
    }
}
