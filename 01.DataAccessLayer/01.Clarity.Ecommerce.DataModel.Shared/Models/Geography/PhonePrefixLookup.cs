// <copyright file="PhonePrefixLookup.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the phone prefix lookup class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IPhonePrefixLookup : IBase
    {
        #region PhonePrefixLookup Properties
        /// <summary>Gets or sets the prefix.</summary>
        /// <value>The prefix.</value>
        string? Prefix { get; set; }

        /// <summary>Gets or sets the time zone.</summary>
        /// <value>The time zone.</value>
        string? TimeZone { get; set; }

        /// <summary>Gets or sets the name of the city.</summary>
        /// <value>The name of the city.</value>
        string? CityName { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the country.</summary>
        /// <value>The identifier of the country.</value>
        int? CountryID { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        Country? Country { get; set; }

        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        int? RegionID { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        Region? Region { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Geography", "PhonePrefixLookup")]
    public class PhonePrefixLookup : Base, IPhonePrefixLookup
    {
        #region PhonePrefixLookup Properties
        /// <inheritdoc/>
        [Index, StringLength(20), DefaultValue(null)]
        public string? Prefix { get; set; }

        /// <inheritdoc/>
        [StringLength(255), DefaultValue(null)]
        public string? TimeZone { get; set; }

        /// <inheritdoc/>
        [StringLength(255), DefaultValue(null)]
        public string? CityName { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Country)), DefaultValue(null)]
        public int? CountryID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Country? Country { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Region)), DefaultValue(null)]
        public int? RegionID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Region? Region { get; set; }
        #endregion
    }
}
