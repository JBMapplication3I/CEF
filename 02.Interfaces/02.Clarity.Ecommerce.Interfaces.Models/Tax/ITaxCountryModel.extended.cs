// <copyright file="ITaxCountryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITaxCountryModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for tax country model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface ITaxCountryModel
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int CountryID { get; set; }

        /// <summary>Gets or sets the country key.</summary>
        /// <value>The country key.</value>
        string? CountryKey { get; set; }

        /// <summary>Gets or sets the name of the country.</summary>
        /// <value>The name of the country.</value>
        string? CountryName { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        ICountryModel? Country { get; set; }
        #endregion
    }
}
