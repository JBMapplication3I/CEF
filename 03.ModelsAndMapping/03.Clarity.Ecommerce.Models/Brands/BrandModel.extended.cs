// <copyright file="BrandModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Brand model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the Brand.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IBrandModel"/>
    public partial class BrandModel
    {
        /// <inheritdoc cref="IBrandModel.BrandCurrencies"/>
        public List<BrandCurrencyModel>? BrandCurrencies { get; set; }

        /// <inheritdoc/>
        List<IBrandCurrencyModel>? IBrandModel.BrandCurrencies { get => BrandCurrencies?.Cast<IBrandCurrencyModel>().ToList(); set => BrandCurrencies = value?.Cast<BrandCurrencyModel>().ToList(); }

        /// <inheritdoc cref="IBrandModel.BrandLanguages"/>
        public List<BrandLanguageModel>? BrandLanguages { get; set; }

        /// <inheritdoc/>
        List<IBrandLanguageModel>? IBrandModel.BrandLanguages { get => BrandLanguages?.Cast<IBrandLanguageModel>().ToList(); set => BrandLanguages = value?.Cast<BrandLanguageModel>().ToList(); }

        /// <inheritdoc cref="IBrandModel.BrandSiteDomains"/>
        public List<BrandSiteDomainModel>? BrandSiteDomains { get; set; }

        /// <inheritdoc/>
        List<IBrandSiteDomainModel>? IBrandModel.BrandSiteDomains { get => BrandSiteDomains?.Cast<IBrandSiteDomainModel>().ToList(); set => BrandSiteDomains = value?.Cast<BrandSiteDomainModel>().ToList(); }
    }
}
