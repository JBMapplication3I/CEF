// <copyright file="AuctionCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account association workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using JSConfigs;
    using Mapper;
    using Models;

    public partial class AuctionWorkflow
    {
        /// <summary>Endpoint for The Your Active Auctions Section (assume sorted by closest/recent end date).</summary>
        /// <param name="userID">            The user ID.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>Your Active Auctions.</returns>
        public async Task<List<IAuctionModel>> GetActiveAuctions(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Auctions
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.Lots!.Any(y => y.Active && y.Bids!.Any(z => z.Active && z.UserID == userID)))
                .OrderBy(x => x.UpdatedDate)
                .SelectListAuctionAndMapToAuctionModel(contextProfileName)
                .ToList();
        }

        /// <inheritdoc />
        public async Task<List<IAuctionModel>> GetAuctionsWithBidHistoryAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var auctionAndLotIDs = context.Auctions
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.Lots!.Any(y => y.Bids!.Any(z => z.UserID == userID)) && !x.Lots!.All(y => y.Bids!.Any(z => z.UserID == userID)))
                .Select(x => new
                {
                    x.ID,
                    LotIDs = x.Lots!.Where(y => !y.Bids!.Any(z => z.UserID == userID)).OrderBy(y => y.Bids!.Count).Select(y => y.ID).Take(3),
                })
                .Where(x => x.LotIDs.Any())
                .ToDictionary(x => x.ID, x => x.LotIDs);
            if (!auctionAndLotIDs.Any())
            {
                return new List<IAuctionModel>();
            }
            return context.Auctions
                .AsNoTracking()
                .FilterByIDs(auctionAndLotIDs.Keys)
                .SelectListAuctionAndMapToAuctionModel(contextProfileName)
                .Select(x =>
                {
                    x.Lots = context.Lots
                        .AsNoTracking()
                        .FilterByIDs(auctionAndLotIDs[x.ID])
                        .SelectListLotAndMapToLotModel(contextProfileName)
                        .ToList();
                    return x;
                })
                .ToList();
        }

        /// <summary>Endpoint for Sort Products By Highest Bid Count.</summary>
        /// <param name="productIDs">        The product IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>Products sorted By Highest Bid Count.</returns>
        public async Task<List<int>> SortProductIDsByHighestBidCountAsync(List<int> productIDs, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var sortedProductIDs = await context.Lots
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.Auction!.Status!.CustomKey != "Closed" && x.Product != null)
                .OrderByDescending(x => x.Bids!.Count)
                .Select(x => x.Product!)
                .FilterByActive(true)
                .FilterByIDs(productIDs)
                .Take(20)
                .Select(x => x.ID)
                .ToListAsync();
            sortedProductIDs.AddRange(productIDs.Except(sortedProductIDs));
            return sortedProductIDs;
        }

        /// <summary>A notification for when my bid is "x" percentage over the current bid that I know that the bid I am
        /// placing greatly exceeds the current highest bid value.</summary>
        /// <param name="bid">               The bid.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if the bid is over x percentage of the current highest bid.</returns>
        public async Task<bool> LargeBidNotifications(decimal bid, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var highestBid = await context.Bids
                .AsNoTracking()
                .FilterByActive(true)
                .OrderByDescending(x => x.CurrentBid)
                .Select(x => x.CurrentBid)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            var percentageOfHighestBid = CEFConfigDictionary.PercentageOfGreatestBid;
            return percentageOfHighestBid is not (null or <= 0)
                && bid > highestBid + highestBid / 100m * percentageOfHighestBid;
        }

        /// <summary>Logic that uses the Buyer's Credit Card to process payment when a lot has been won.</summary>
        /// <param name="lotID">             The lotID.</param>
        /// <param name="userID">            The userID.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Payment Response.</returns>
        public async Task<CEFActionResponse<IPaymentResponse?>> ProcessPaymentLotWon(
            int lotID,
            int userID,
            string? contextProfileName)
        {
            var bid = (await Workflows.Bids.SearchAsync(
                        new BidSearchModel { Active = true, UserID = userID, LotID = lotID, Won = true },
                        false,
                        contextProfileName)
                    .ConfigureAwait(false))
                .results
                .FirstOrDefault();
            if (bid == null)
            {
                return CEFAR.FailingCEFAR<IPaymentResponse?>(
                    $"No bids were found with user ID {userID}, lot ID {lotID}, and won the lot.");
            }
            var wallet = (await Workflows.Wallets.SearchAsync(
                        new WalletSearchModel { Active = true, UserID = userID },
                        false,
                        contextProfileName)
                    .ConfigureAwait(false))
                .results
                .FirstOrDefault();
            if (wallet == null)
            {
                return CEFAR.FailingCEFAR<IPaymentResponse?>(
                    $"No wallets have been setup for user ID {userID}.");
            }
            if (string.IsNullOrEmpty(wallet.Token))
            {
                return CEFAR.FailingCEFAR<IPaymentResponse?>(
                    $"wallet ID {wallet.ID} does not have a token to process the payment.");
            }
            var paymentProvider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            if (paymentProvider is null)
            {
                return CEFAR.FailingCEFAR<IPaymentResponse?>(
                    "ERROR! No payment provider found.");
            }
            var paymentResponse = await paymentProvider.CaptureAsync(
                    authorizationToken: wallet.Token!,
                    payment: new PaymentModel { Amount = bid.CurrentBid },
                    contextProfileName)
                .ConfigureAwait(false);
            if (paymentResponse.Approved)
            {
                return CEFAR.PassingCEFAR(paymentResponse);
            }
            else
            {
                return CEFAR.FailingCEFAR<IPaymentResponse?>(paymentResponse.ResponseCode);
            }
        }

        /// <inheritdoc/>
        public async Task<DateTime?> GetLastModifiedForByIDsResultAsync(
            List<int> auctionIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Auctions
                .AsNoTracking()
                .FilterByIDs(auctionIDs)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .FirstAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<IAuctionModel>> GetByIDsAsync(
            List<int> auctionIDs,
            int? brandID,
            int? storeID,
            bool isVendorAdmin,
            int? vendorAdminID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // await CleanProductsAsync(productIDs.ToArray(), context).ConfigureAwait(false);
            var auctions = new List<IAuctionModel>();
            foreach (var auctionID in auctionIDs)
            {
                var p = await GetAsync(auctionID, contextProfileName).ConfigureAwait(false);
                if (p is null)
                {
                    continue;
                }
                auctions.Add(p);
            }
            return auctions;
        }

        /// <summary>Gets a list of auctions within a specified radius of a specified postal code.</summary>
        /// <param name="postalCode">The postalCode.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="units">The Locator Units.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List of Auctions within a specified radius.</returns>
        public async Task<List<IAuctionModel>> GetAuctionsByPostalCodeRadius(
            string? postalCode,
            int? radius,
            Enums.LocatorUnits units,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var zipCode = await Workflows.ZipCodes.GetByZipCodeValueAsync(
                    postalCode,
                    context.ContextProfileName)
                .ConfigureAwait(false);
            var addressIDs = await context.Addresses
                .AsNoTracking()
                .FilterByActive(true)
                .FilterAddressesByZipCodeRadius(
                    zipCode?.Longitude,
                    zipCode?.Latitude,
                    radius,
                    units)
                .Select(x => x.ID)
                .ToListAsync();
            return context.Auctions
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.Contact != null
                    && x.Contact.Address != null
                    && addressIDs.Any(y => y == x.Contact.Address.ID))
                .SelectListAuctionAndMapToAuctionModel(contextProfileName)
                .ToList();
        }

        /// <inheritdoc />
        public async Task<CEFActionResponse> BidOnGroupedLots(
            int groupID,
            int userID,
            decimal amount,
            string? contextProfileName)
        {
            var lots = (await Workflows.Lots.SearchAsync(
                        new LotSearchModel { Active = true, LotGroupID = groupID },
                        false,
                        contextProfileName)
                    .ConfigureAwait(false))
                .results;
            foreach (var lot in lots)
            {
                var bidStatusID = await Workflows.BidStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: 1,
                        byKey: string.Empty,
                        byName: string.Empty,
                        byDisplayName: string.Empty,
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                BidModel bidModel = new()
                {
                    Active = true,
                    LotID = lot.ID,
                    CreatedDate = DateExtensions.GenDateTime,
                    CurrentBid = amount,
                    StatusID = bidStatusID,
                    UserID = userID,
                };
                var topCurrentBid = (await Workflows.Bids.SearchAsync(
                            new BidSearchModel { Active = true, LotID = lot.ID },
                            false,
                            contextProfileName)
                        .ConfigureAwait(false))
                    .results
                    .OrderByDescending(x => x.CurrentBid)
                    .FirstOrDefault();
                if (topCurrentBid != null)
                {
                    bidModel.CurrentBid += topCurrentBid.CurrentBid;
                }
                var result = await Workflows.Bids.UpsertAsync(bidModel, contextProfileName).ConfigureAwait(false);
                if (!result.ActionSucceeded)
                {
                    return result;
                }
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc />
        protected override async Task<IQueryable<Auction>> FilterQueryByModelCustomAsync(IQueryable<Auction> query, IAuctionSearchModel search, Interfaces.DataModel.IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelCustomAsync(query, search, context).ConfigureAwait(false))
                .FilterAuctionsByUserWithBids(search.UserID);
        }
    }
}
