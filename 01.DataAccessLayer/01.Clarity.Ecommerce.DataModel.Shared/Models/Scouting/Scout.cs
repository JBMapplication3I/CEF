// <copyright file="Scout.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the scout class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IScout : IAmFilterableByCategory<ScoutCategory>
    {
        #region Scout Properties
        /// <summary>Gets or sets the Date/Time of the year minimum.</summary>
        /// <value>The year minimum.</value>
        DateTime? YearMin { get; set; }

        /// <summary>Gets or sets the Date/Time of the year maximum.</summary>
        /// <value>The year maximum.</value>
        DateTime? YearMax { get; set; }

        /// <summary>Gets or sets the price minimum.</summary>
        /// <value>The price minimum.</value>
        decimal? PriceMin { get; set; }

        /// <summary>Gets or sets the price maximum.</summary>
        /// <value>The price maximum.</value>
        decimal? PriceMax { get; set; }

        /// <summary>Gets or sets the hours used minimum.</summary>
        /// <value>The hours used minimum.</value>
        decimal? HoursUsedMin { get; set; }

        /// <summary>Gets or sets the hours used maximum.</summary>
        /// <value>The hours used maximum.</value>
        decimal? HoursUsedMax { get; set; }

        /// <summary>Gets or sets the distance used minimum.</summary>
        /// <value>The distance used minimum.</value>
        decimal? DistanceUsedMin { get; set; }

        /// <summary>Gets or sets the distance used maximum.</summary>
        /// <value>The distance used maximum.</value>
        decimal? DistanceUsedMax { get; set; }

        /// <summary>Gets or sets the distance unit of measure.</summary>
        /// <value>The distance unit of measure.</value>
        string? DistanceUnitOfMeasure { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the created by user.</summary>
        /// <value>The identifier of the created by user.</value>
        int CreatedByUserID { get; set; }

        /// <summary>Gets or sets the created by user.</summary>
        /// <value>The created by user.</value>
        User? CreatedByUser { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Scouting", "Scout")]
    public class Scout : Base, IScout
    {
        private ICollection<ScoutCategory>? categories;

        public Scout()
        {
            // IAmFilterableByCategory<ScoutCategory>
            categories = new HashSet<ScoutCategory>();
        }

        #region Scout Properties
        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? YearMin { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? YearMax { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceMin { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceMax { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? HoursUsedMin { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? HoursUsedMax { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? DistanceUsedMin { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? DistanceUsedMax { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? DistanceUnitOfMeasure { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(CreatedByUser)), DefaultValue(null)]
        public int CreatedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? CreatedByUser { get; set; }
        #endregion

        #region IAmFilterableByCategory<ScoutCategory>
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<ScoutCategory>? Categories { get => categories; set => categories = value; }
        #endregion
    }
}
