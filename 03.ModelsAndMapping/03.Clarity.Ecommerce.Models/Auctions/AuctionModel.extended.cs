// <copyright file="AuctionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the auction model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <content>Interface for auction model.</content>
    public partial class AuctionModel
    {
        #region Properties
        /// <inheritdoc/>
        public DateTime? OpensAt { get; set; }

        /// <inheritdoc/>
        public DateTime? ClosesAt { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        List<ILotModel>? IAuctionModel.Lots { get => Lots?.ToList<ILotModel>(); set => Lots = value?.Cast<LotModel>().ToList(); }

        /// <inheritdoc cref="IAuctionModel.Lots"/>
        public List<LotModel>? Lots { get; set; }
        #endregion
    }
}
