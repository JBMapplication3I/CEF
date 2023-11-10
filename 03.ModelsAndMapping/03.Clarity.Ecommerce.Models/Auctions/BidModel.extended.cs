// <copyright file="BidModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the bid model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>Interface for bid model.</summary>
    public partial class BidModel
    {
        #region Properties
        /// <inheritdoc/>
        public decimal? CurrentBid { get; set; }

        /// <inheritdoc/>
        public decimal? MaxBid { get; set; }

        /// <inheritdoc/>
        public bool Won { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? LotID { get; set; }

        /// <inheritdoc/>
        public string? LotKey { get; set; }

        /// <inheritdoc/>
        public string? LotName { get; set; }

        /// <inheritdoc cref="IBidModel.Lot" />
        public LotModel? Lot { get; set; }

        /// <inheritdoc/>
        ILotModel? IBidModel.Lot { get => Lot; set => Lot = (LotModel?)value; }
        #endregion
    }
}
