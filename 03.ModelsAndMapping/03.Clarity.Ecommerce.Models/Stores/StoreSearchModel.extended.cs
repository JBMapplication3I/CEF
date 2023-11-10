// <copyright file="StoreSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the store search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IStoreSearchModel"/>
    public partial class StoreSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(HostUrl), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Host URL for search")]
        public string? HostUrl { get; set; }

        #region LocateableSearch
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ZipCode), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Origin ZipCode from which you extending by radius to find entities")]
        public string? ZipCode { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Latitude), DataType = "double?", ParameterType = "query", IsRequired = false,
            Description = "Origin Latitude from which you extending by radius to find entities")]
        public double? Latitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Longitude), DataType = "double?", ParameterType = "query", IsRequired = false,
            Description = "Origin Longitude from which you extending by radius to find entities")]
        public double? Longitude { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Radius), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "The number of units to extend in a radius from the center of the Origin ZipCode")]
        public int? Radius { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Units), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The unit of measure for the radius. Must be either mi (miles) or km (kilometers). Default is mi.")]
        public Enums.LocatorUnits? Units { get; set; }
        #endregion

        /// <inheritdoc/>
        [ApiMember(Name = nameof(RegionID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "This is the Region where the store is located.")]
        public int? RegionID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreContactRegionID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "This filters if the store services this region.")]
        public int? StoreContactRegionID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CountryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "This is the Country where the store is located.")]
        public int? CountryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreContactCountryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "This filters if the store services this country.")]
        public int? StoreContactCountryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(City), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "This is the city where the store is located.")]
        public string? City { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreContactCity), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "This filters if the store services this city.")]
        public string? StoreContactCity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SortByMembershipLevel), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Enable sorting by membership level")]
        public bool? SortByMembershipLevel { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(DistrictID), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "This filters if the store services this district.")]
        public int? DistrictID { get; set; }
    }
}
