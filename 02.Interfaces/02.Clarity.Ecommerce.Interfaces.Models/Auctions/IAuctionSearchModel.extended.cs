// <copyright file="IAuctionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the Auction interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for auction model.</summary>
    public partial interface IAuctionSearchModel
    {
        /// <summary>Gets or sets the ID of the user to filter auctions by.</summary>
        /// <value>The ID of the user to fiter auctions by.</value>
        int? UserID { get; set; }
    }
}
