// <copyright file="CountryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the country model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the country.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="ICountryModel"/>
    public partial class CountryModel
    {
        #region Country Properties
        /// <inheritdoc/>
        public string? Code { get; set; }

        /// <inheritdoc/>
        public string? ISO3166Alpha2 { get; set; }

        /// <inheritdoc/>
        public string? ISO3166Alpha3 { get; set; }

        /// <inheritdoc/>
        public int? ISO3166Numeric { get; set; }

        /// <inheritdoc/>
        public string? PhoneRegEx { get; set; }

        /// <inheritdoc/>
        public string? PhonePrefix { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ICountryModel.Regions"/>
        public List<RegionModel>? Regions { get; set; }

        /// <inheritdoc/>
        List<IRegionModel>? ICountryModel.Regions { get => Regions?.ToList<IRegionModel>(); set => Regions = value?.Cast<RegionModel>().ToList(); }
        #endregion

        #region IClonable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Address
            builder.Append("Cd: ").AppendLine(Code ?? string.Empty);
            builder.Append("Rx: ").AppendLine(PhoneRegEx ?? string.Empty);
            builder.Append("Px: ").AppendLine(PhonePrefix ?? string.Empty);
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
