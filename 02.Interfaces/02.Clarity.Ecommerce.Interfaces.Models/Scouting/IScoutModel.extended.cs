// <copyright file="IScoutModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IScoutModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    public partial interface IScoutModel
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
        IUserModel? CreatedByUser { get; set; }

        /// <summary>Gets or sets the created by user key.</summary>
        /// <value>The created by user key.</value>
        string? CreatedByUserKey { get; set; }
        #endregion
    }
}
