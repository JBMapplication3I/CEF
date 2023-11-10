// <copyright file="IBidModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the bid interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for bid model.</summary>
    public partial interface IBidModel
    {
        #region Properties
        /// <summary>Gets or sets the current bid.</summary>
        /// <value>The current bid.</value>
        decimal? CurrentBid { get; set; }

        /// <summary>Gets or sets the maximum bid.</summary>
        /// <value>The maximum bid.</value>
        decimal? MaxBid { get; set; }

        /// <summary>Gets or sets a value indicating whether the won the listing/lot.</summary>
        /// <value>True if won the listing/lot, false if not.</value>
        bool Won { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the lot.</summary>
        /// <value>The identifier of the lot.</value>
        int? LotID { get; set; }

        /// <summary>Gets or sets the lot key.</summary>
        /// <value>The lot key.</value>
        string? LotKey { get; set; }

        /// <summary>Gets or sets the name of the lot.</summary>
        /// <value>The name of the lot.</value>
        string? LotName { get; set; }

        /// <summary>Gets or sets the lot.</summary>
        /// <value>The lot.</value>
        ILotModel? Lot { get; set; }
        #endregion
    }
}
