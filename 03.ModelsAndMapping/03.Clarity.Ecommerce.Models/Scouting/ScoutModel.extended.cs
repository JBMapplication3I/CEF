// <copyright file="ScoutModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the scout model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    public partial class ScoutModel
    {
        #region Scout Properties
        /// <inheritdoc/>
        public DateTime? YearMin { get; set; }

        /// <inheritdoc/>
        public DateTime? YearMax { get; set; }

        /// <inheritdoc/>
        public decimal? PriceMin { get; set; }

        /// <inheritdoc/>
        public decimal? PriceMax { get; set; }

        /// <inheritdoc/>
        public decimal? HoursUsedMin { get; set; }

        /// <inheritdoc/>
        public decimal? HoursUsedMax { get; set; }

        /// <inheritdoc/>
        public decimal? DistanceUsedMin { get; set; }

        /// <inheritdoc/>
        public decimal? DistanceUsedMax { get; set; }

        /// <inheritdoc/>
        public string? DistanceUnitOfMeasure { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int CreatedByUserID { get; set; }

        /// <inheritdoc cref="IScoutModel.CreatedByUser"/>
        public UserModel? CreatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IScoutModel.CreatedByUser { get => CreatedByUser; set => CreatedByUser = (UserModel?)value; }

        /// <inheritdoc/>
        public string? CreatedByUserKey { get; set; }
        #endregion
    }
}
