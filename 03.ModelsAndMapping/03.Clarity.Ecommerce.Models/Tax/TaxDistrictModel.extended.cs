// <copyright file="TaxDistrictModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax district model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the tax district.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="ITaxDistrictModel"/>
    public partial class TaxDistrictModel
    {
        #region TaxDistrict Properties
        /// <inheritdoc/>
        public decimal Rate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int DistrictID { get; set; }

        /// <inheritdoc/>
        public string? DistrictKey { get; set; }

        /// <inheritdoc/>
        public string? DistrictName { get; set; }

        /// <inheritdoc cref="ITaxDistrictModel.District"/>
        public DistrictModel? District { get; set; }

        /// <inheritdoc/>
        IDistrictModel? ITaxDistrictModel.District { get => District; set => District = (DistrictModel?)value; }
        #endregion
    }
}
