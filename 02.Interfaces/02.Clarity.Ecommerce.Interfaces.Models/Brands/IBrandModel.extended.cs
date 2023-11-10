// <copyright file="IBrandModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IBrandModel interface</summary>
// ReSharper disable InconsistentNaming
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for brand model.</summary>
    public partial interface IBrandModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the brand currencies.</summary>
        /// <value>The brand currencies.</value>
        List<IBrandCurrencyModel>? BrandCurrencies { get; set; }

        /// <summary>Gets or sets the brand languages.</summary>
        /// <value>The brand languages.</value>
        List<IBrandLanguageModel>? BrandLanguages { get; set; }

        /// <summary>Gets or sets the brand site domains.</summary>
        /// <value>The brand site domains.</value>
        List<IBrandSiteDomainModel>? BrandSiteDomains { get; set; }
        #endregion
    }
}
