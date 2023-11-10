// <copyright file="PricingFactoryContextModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pricing factory context model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using Interfaces.Models;

    /// <summary>A data Model for the pricing factory context model.</summary>
    /// <seealso cref="IPricingFactoryContextModel"/>
    /// <remarks>This class cannot be inherited (it is sealed).</remarks>
    public sealed class PricingFactoryContextModel : IPricingFactoryContextModel
    {
        /// <inheritdoc/>
        public decimal Quantity { get; set; } = 1;

        /// <inheritdoc/>
        public string? PricePoint { get; set; }

        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        public int? CountryID { get; set; }

        /// <inheritdoc/>
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        public int? AccountTypeID { get; set; }

        /// <inheritdoc/>
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        public List<string>? UserRoles { get; set; }

        /// <inheritdoc/>
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        public string? StoreKey { get; set; }

        /// <inheritdoc/>
        public int? FranchiseID { get; set; }

        /// <inheritdoc/>
        public string? FranchiseKey { get; set; }

        /// <inheritdoc/>
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        public string? BrandKey { get; set; }

        /// <inheritdoc/>
        public Guid SessionID { get; set; }

        /// <inheritdoc/>
        public object Clone()
        {
            var clone = (PricingFactoryContextModel)this.MemberwiseClone();
            clone.UserRoles = new(UserRoles ?? new());
            return clone;
        }
    }
}
