// <copyright file="IAuctionWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuctionWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;
    using Providers.Payments;

    /// <summary>Interface for auction workflow.</summary>
    public partial interface IAuctionWorkflow
    {
        /// <summary>Endpoint for The Your Active Auctions Section
        /// (assume sorted by closest/recent end date).</summary>
        /// <param name="userID"> The user ID.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>Your Active Auctions.</returns>
        Task<List<IAuctionModel>> GetActiveAuctions(int userID, string? contextProfileName);

        /// <summary>Endpoint for the Get More Items For Your Auctions Section.</summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>3 Most Popular Lots which do not have bid from User.</returns>
        Task<List<IAuctionModel>> GetAuctionsWithBidHistoryAsync(int userID, string? contextProfileName);

        /// <summary>Endpoint for Sort Products By Highest Bid Count.</summary>
        /// <param name="productIDs">The product IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>Product IDs sorted By Highest Bid Count.</returns>
        Task<List<int>> SortProductIDsByHighestBidCountAsync(List<int> productIDs, string? contextProfileName);

        /// <summary>A notification for when my bid is "x" percentage over the
        /// current bid that I know that the bid I am placing greatly
        /// exceeds the current highest bid value.</summary>
        /// <param name="bid">The bid.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if the bid is over x percentage of the current highest bid.</returns>
        Task<bool> LargeBidNotifications(decimal bid, string? contextProfileName);

        /// <summary>logic that uses the Buyer's Credit Card to process payment when a lot has been won.</summary>
        /// <param name="lotID">The lotID.</param>
        /// <param name="userID">The userID.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Payment Response.</returns>
        Task<CEFActionResponse<IPaymentResponse?>> ProcessPaymentLotWon(int lotID, int userID, string? contextProfileName);
        /// <summary>Gets the last modified for by IDs result.</summary>
        /// <param name="auctionIDs">        The auction IDs.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="isVendorAdmin">     True if this request is for a vendor admin.</param>
        /// <param name="vendorAdminID">     Identifier for the vendor admin.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for by IDs result.</returns>
        Task<DateTime?> GetLastModifiedForByIDsResultAsync(
            List<int> auctionIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName);

        /// <summary>Gets by IDs.</summary>
        /// <param name="auctionIDs">        The auction IDs.</param>
        /// <param name="brandID">           Identifier for the brand.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="isVendorAdmin">     True if this request is for a vendor admin.</param>
        /// <param name="vendorAdminID">     Identifier for the vendor admin.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by IDs.</returns>
        Task<List<IAuctionModel>> GetByIDsAsync(
            List<int> auctionIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName);
        /// <summary>Gets a list of auctions within a specified radius of a specified postal code.</summary>
        /// <param name="postalCode">The postalCode.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="units">The Locator Units.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List of Auctions within a specified radius.</returns>
        Task<List<IAuctionModel>> GetAuctionsByPostalCodeRadius(string? postalCode, int? radius, Enums.LocatorUnits units, string? contextProfileName);

        /// <summary>Bids On a group of lots.</summary>
        /// <param name="groupID">The groupID.</param>
        /// <param name="userID">The userID.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> BidOnGroupedLots(int groupID, int userID, decimal amount, string? contextProfileName);
    }
}
