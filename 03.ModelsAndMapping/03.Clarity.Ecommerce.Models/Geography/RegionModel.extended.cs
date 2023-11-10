// <copyright file="RegionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the region.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IRegionModel"/>
    public partial class RegionModel
    {
        #region Properties
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Region Code")]
        public string? Code { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO31661), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Region Code ISO 3166-1")]
        public string? ISO31661 { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO31662), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Region Code ISO 3166-2")]
        public string? ISO31662 { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ISO3166Alpha2), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Region Code ISO 3166 Alpha 2")]
        public string? ISO3166Alpha2 { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CountryID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Country ID")]
        public int CountryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CountryKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Country Key")]
        public string? CountryKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CountryName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Country Name")]
        public string? CountryName { get; set; }

        /// <inheritdoc cref="IRegionModel.Country"/>
        [ApiMember(Name = nameof(Country), DataType = "CountryModel", ParameterType = "body", IsRequired = false,
            Description = "Country")]
        public CountryModel? Country { get; set; }

        /// <inheritdoc/>
        ICountryModel? IRegionModel.Country { get => Country; set => Country = (CountryModel?)value; }
        #endregion

        #region IClonable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Address
            builder.Append("Cd: ").AppendLine(Code ?? string.Empty);
            builder.Append("31661: ").AppendLine(ISO31661 ?? string.Empty);
            builder.Append("31662: ").AppendLine(ISO31662 ?? string.Empty);
            builder.Append("3166A2: ").AppendLine(ISO3166Alpha2 ?? string.Empty);
            // Related Objects
            builder.Append("Co: ").AppendLine(Country?.ToHashableString() ?? $"No Country={CountryID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
