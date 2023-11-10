// <copyright file="UserForPricingModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user for pricing model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using Interfaces.Models;

    /// <summary>A data Model for the user for pricing.</summary>
    /// <seealso cref="IUserForPricingModel"/>
    public class UserForPricingModel : IUserForPricingModel
    {
        /// <inheritdoc/>
        public int ID { get; set; }

        /// <inheritdoc/>
        public string? CustomKey { get; set; }

        /// <inheritdoc/>
        public string? UserName { get; set; }

        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        public int? PreferredStoreID { get; set; }

        /// <inheritdoc/>
        public int? CountryID { get; set; }

        /// <inheritdoc/>
        public ICollection<string>? Roles { get; set; }

        /// <inheritdoc/>
        public SerializableAttributesDictionary? SerializableAttributes { get; set; }
    }
}
