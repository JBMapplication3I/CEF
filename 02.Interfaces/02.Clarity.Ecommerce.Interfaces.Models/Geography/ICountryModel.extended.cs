// <copyright file="ICountryModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICountryModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for country model.</summary>
    public partial interface ICountryModel
    {
        #region Country Properties
        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        string? Code { get; set; }

        /// <summary>Gets or sets the ISO 3166 alpha 2 code.</summary>
        /// <value>The ISO 3166 alpha 2 code.</value>
        string? ISO3166Alpha2 { get; set; }

        /// <summary>Gets or sets the ISO 3166 alpha 3 code.</summary>
        /// <value>The ISO 3166 alpha 3 code.</value>
        string? ISO3166Alpha3 { get; set; }

        /// <summary>Gets or sets the ISO 3166 numeric code.</summary>
        /// <value>The ISO 3166 numeric code.</value>
        int? ISO3166Numeric { get; set; }

        /// <summary>Gets or sets the phone RegEx.</summary>
        /// <value>The phone RegEx.</value>
        string? PhoneRegEx { get; set; }

        /// <summary>Gets or sets the phone prefix.</summary>
        /// <value>The phone prefix.</value>
        string? PhonePrefix { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the regions.</summary>
        /// <value>The regions.</value>
        List<IRegionModel>? Regions { get; set; }
        #endregion
    }
}
