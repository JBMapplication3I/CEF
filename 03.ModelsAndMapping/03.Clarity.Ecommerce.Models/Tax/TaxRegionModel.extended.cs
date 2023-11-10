// <copyright file="TaxRegionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax region model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the tax region.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="ITaxRegionModel"/>
    public partial class TaxRegionModel
    {
        #region TaxRegion Properties
        /// <inheritdoc/>
        public decimal Rate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int RegionID { get; set; }

        /// <inheritdoc/>
        public string? RegionKey { get; set; }

        /// <inheritdoc/>
        public string? RegionName { get; set; }

        /// <inheritdoc cref="ITaxRegionModel.Region"/>
        public RegionModel? Region { get; set; }

        /// <inheritdoc/>
        IRegionModel? ITaxRegionModel.Region { get => Region; set => Region = (RegionModel?)value; }
        #endregion
    }
}
