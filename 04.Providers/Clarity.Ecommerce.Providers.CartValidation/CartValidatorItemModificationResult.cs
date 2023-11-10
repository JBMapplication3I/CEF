// <copyright file="CartValidatorItemModificationResult.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart validator item modification result class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using Interfaces.Models;

    /// <summary>Encapsulates the result of a cart validator item modification.</summary>
    /// <seealso cref="ICartValidatorItemModificationResult"/>
    public class CartValidatorItemModificationResult : ICartValidatorItemModificationResult
    {
        /// <inheritdoc/>
        public List<string> Messages { get; set; } = new List<string>();

        /// <inheritdoc/>
        public decimal OldQuantity { get; set; }

        /// <inheritdoc/>
        public decimal NewQuantity { get; set; }

        /// <inheritdoc/>
        public decimal OldQuantityBackOrdered { get; set; }

        /// <inheritdoc/>
        public decimal NewQuantityBackOrdered { get; set; }

        /// <inheritdoc/>
        public decimal OldQuantityPreSold { get; set; }

        /// <inheritdoc/>
        public decimal NewQuantityPreSold { get; set; }

        /// <inheritdoc/>
        public decimal? FloorAllowed { get; set; }

        /// <inheritdoc/>
        public decimal? CeilingAllowed { get; set; }

        /// <inheritdoc/>
        public decimal? CeilingAllowedBackOrdered { get; set; }

        /// <inheritdoc/>
        public decimal? CeilingAllowedPreSold { get; set; }

        /// <inheritdoc/>
        public bool NeedToModify { get; set; }
    }
}
