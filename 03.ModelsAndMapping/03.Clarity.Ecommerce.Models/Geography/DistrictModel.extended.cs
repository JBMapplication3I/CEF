// <copyright file="DistrictModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the region.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IDistrictModel"/>
    public partial class DistrictModel
    {
        #region Properties
        /// <inheritdoc/>
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "District Code")]
        public string? Code { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [ApiMember(Name = nameof(RegionID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Region ID")]
        public int? RegionID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RegionKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Region Key")]
        public string? RegionKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RegionName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Region Name")]
        public string? RegionName { get; set; }

        /// <inheritdoc cref="IDistrictModel.Region"/>
        [ApiMember(Name = nameof(Region), DataType = "RegionModel", ParameterType = "body", IsRequired = false,
            Description = "Region")]
        public RegionModel? Region { get; set; }

        /// <inheritdoc/>
        IRegionModel? IDistrictModel.Region { get => Region; set => Region = (RegionModel?)value; }

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

        /// <inheritdoc cref="IDistrictModel.Country"/>
        [ApiMember(Name = nameof(Country), DataType = "CountryModel", ParameterType = "body", IsRequired = false,
            Description = "Country")]
        public CountryModel? Country { get; set; }

        /// <inheritdoc/>
        ICountryModel? IDistrictModel.Country { get => Country; set => Country = (CountryModel?)value; }
        #endregion

        #region IClonable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Address
            builder.Append("Cd: ").AppendLine(Code ?? string.Empty);
            // Related Objects
            builder.Append("Co: ").AppendLine(Country?.ToHashableString() ?? $"No Country={CountryID}");
            builder.Append("Re: ").AppendLine(Region?.ToHashableString() ?? $"No Region={RegionID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
