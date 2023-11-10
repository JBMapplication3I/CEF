// <copyright file="TaxCountryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax country model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the tax country.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ITaxCountryModel"/>
    public partial class TaxCountryModel
    {
        #region TaxCountry Properties
        /// <inheritdoc/>
        public decimal Rate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int CountryID { get; set; }

        /// <inheritdoc/>
        public string? CountryKey { get; set; }

        /// <inheritdoc/>
        public string? CountryName { get; set; }

        /// <inheritdoc cref="ITaxCountryModel.Country"/>
        public CountryModel? Country { get; set; }

        /// <inheritdoc/>
        ICountryModel? ITaxCountryModel.Country { get => Country; set => Country = (CountryModel?)value; }
        #endregion
    }
}
