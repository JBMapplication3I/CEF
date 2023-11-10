// <copyright file="IUserForPricingModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUserForPricingModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for user for pricing model.</summary>
    public interface IUserForPricingModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int ID { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        string? CustomKey { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserName { get; set; }

        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the identifier of the preferred store.</summary>
        /// <value>The identifier of the preferred store.</value>
        int? PreferredStoreID { get; set; }

        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int? CountryID { get; set; }

        /// <summary>Gets or sets the roles.</summary>
        /// <value>The roles.</value>
        ICollection<string>? Roles { get; set; }

        /// <summary>Gets or sets the serializable attributes.</summary>
        /// <value>The serializable attributes.</value>
        SerializableAttributesDictionary? SerializableAttributes { get; set; }
    }
}
