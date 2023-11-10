// <copyright file="ICartValidatorItemModificationResult.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartValidatorItemModificationResult interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for cart validator item modification result.</summary>
    public interface ICartValidatorItemModificationResult
    {
        /// <summary>Gets the messages.</summary>
        /// <value>The messages.</value>
        List<string> Messages { get; }

        /// <summary>Gets or sets the old quantity.</summary>
        /// <value>The old quantity.</value>
        decimal OldQuantity { get; set; }

        /// <summary>Gets or sets the new quantity.</summary>
        /// <value>The new quantity.</value>
        decimal NewQuantity { get; set; }

        /// <summary>Gets or sets the old quantity back ordered.</summary>
        /// <value>The old quantity back ordered.</value>
        decimal OldQuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the new quantity back ordered.</summary>
        /// <value>The new quantity back ordered.</value>
        decimal NewQuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the old quantity pre sold.</summary>
        /// <value>The old quantity pre sold.</value>
        decimal OldQuantityPreSold { get; set; }

        /// <summary>Gets or sets the new quantity pre sold.</summary>
        /// <value>The new quantity pre sold.</value>
        decimal NewQuantityPreSold { get; set; }

        /// <summary>Gets or sets the floor allowed.</summary>
        /// <value>The floor allowed.</value>
        decimal? FloorAllowed { get; set; }

        /// <summary>Gets or sets the ceiling allowed.</summary>
        /// <value>The ceiling allowed.</value>
        decimal? CeilingAllowed { get; set; }

        /// <summary>Gets or sets the ceiling allowed back ordered.</summary>
        /// <value>The ceiling allowed back ordered.</value>
        decimal? CeilingAllowedBackOrdered { get; set; }

        /// <summary>Gets or sets the ceiling allowed pre sold.</summary>
        /// <value>The ceiling allowed pre sold.</value>
        decimal? CeilingAllowedPreSold { get; set; }

        /// <summary>Gets or sets a value indicating whether the need to modify.</summary>
        /// <value>True if need to modify, false if not.</value>
        bool NeedToModify { get; set; }
    }
}
