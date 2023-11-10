// <copyright file="IAuctionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuctionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <content>Interface for auction model.</content>
    public partial interface IAuctionModel
    {
        #region Properties
        /// <summary>Gets or sets the Date/Time of the opens at.</summary>
        /// <value>The opens at.</value>
        DateTime? OpensAt { get; set; }

        /// <summary>Gets or sets the Date/Time of the closes at.</summary>
        /// <value>The closes at.</value>
        DateTime? ClosesAt { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the lots.</summary>
        /// <value>The lots.</value>
        List<ILotModel>? Lots { get; set; }
        #endregion
    }
}
