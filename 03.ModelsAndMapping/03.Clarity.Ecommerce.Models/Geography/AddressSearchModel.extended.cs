// <copyright file="AddressSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the address search model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the address search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IAddressSearchModel"/>
    public partial class AddressSearchModel
    {
        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(CountryKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Filter by the Related Country CustomKey (Equals, Case-Insensitive)")]
        public string? CountryKey { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(CountryName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Filter by the Related Country Name (Contains, Case-Insensitive)")]
        public string? CountryName { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(CountryDescription), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Filter by the Related Country Description (Contains, Case-Insensitive)")]
        public string? CountryDescription { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(RegionKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Filter by the Related Region CustomKey (Equals, Case-Insensitive)")]
        public string? RegionKey { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(RegionName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Filter by the Related Region Name (Contains, Case-Insensitive)")]
        public string? RegionName { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(RegionDescription), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Filter by the Related Region Description (Contains, Case-Insensitive)")]
        public string? RegionDescription { get; set; }

        #region LocateableSearch
        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(ZipCode), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Origin ZipCode from which you extending by radius to find entities")]
        public string? ZipCode { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(Latitude), DataType = "double?", ParameterType = "query", IsRequired = false,
            Description = "Origin Latitude from which you extending by radius to find entities")]
        public double? Latitude { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(Longitude), DataType = "double?", ParameterType = "query", IsRequired = false,
            Description = "Origin Longitude from which you extending by radius to find entities")]
        public double? Longitude { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(Radius), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The number of units to extend in a radius from the center of the Origin ZipCode")]
        public int? Radius { get; set; }

        /// <inheritdoc/>
        [ServiceStack.ApiMember(Name = nameof(Units), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The unit of measure for the radius. Must be either mi (miles) or km (kilometers). Default is mi.")]
        public Enums.LocatorUnits? Units { get; set; }
        #endregion
    }
}
