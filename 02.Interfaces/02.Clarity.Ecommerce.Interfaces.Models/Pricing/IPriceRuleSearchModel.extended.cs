// <copyright file="IPriceRuleSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPriceRuleSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for price rule search model.</summary>
    public partial interface IPriceRuleSearchModel
    {
        /// <summary>Gets or sets the priority.</summary>
        /// <value>The priority.</value>
        int? Priority { get; set; }

        /// <summary>Gets or sets the minimum quantity minimum.</summary>
        /// <value>The minimum quantity minimum.</value>
        decimal? MinQuantityMin { get; set; }

        /// <summary>Gets or sets the minimum quantity maximum.</summary>
        /// <value>The minimum quantity maximum.</value>
        decimal? MinQuantityMax { get; set; }

        /// <summary>Gets or sets the maximum quantity minimum.</summary>
        /// <value>The maximum quantity minimum.</value>
        decimal? MaxQuantityMin { get; set; }

        /// <summary>Gets or sets the maximum quantity maximum.</summary>
        /// <value>The maximum quantity maximum.</value>
        decimal? MaxQuantityMax { get; set; }

        /// <summary>Gets or sets the Date/Time of the start date minimum.</summary>
        /// <value>The start date minimum.</value>
        DateTime? StartDateMin { get; set; }

        /// <summary>Gets or sets the Date/Time of the start date maximum.</summary>
        /// <value>The start date maximum.</value>
        DateTime? StartDateMax { get; set; }

        /// <summary>Gets or sets the Date/Time of the end date minimum.</summary>
        /// <value>The end date minimum.</value>
        DateTime? EndDateMin { get; set; }

        /// <summary>Gets or sets the Date/Time of the end date maximum.</summary>
        /// <value>The end date maximum.</value>
        DateTime? EndDateMax { get; set; }
    }
}
