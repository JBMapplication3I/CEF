// <copyright file="Hubs.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SignalRCore Hubs class</summary>
namespace Clarity.Ecommerce.SignalRCore
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using Models;

    /// <summary>A hubs.</summary>
    /// <seealso cref="Hub"/>
    public class Hubs : Hub
    {
        private readonly IService service;

        /// <summary>Initializes a new instance of the <see cref="Hubs"/> class.</summary>
        /// <param name="service">The service.</param>
        public Hubs(IService service)
        {
            this.service = service;
        }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public string? Message { get; set; }

        /// <summary>Gets or sets the current bid.</summary>
        /// <value>The current bid.</value>
        public decimal CurrentBid { get; set; }

        /// <summary>Gets or sets the returned bid after placing a bid.</summary>
        /// <value>The returned bid.</value>
        public StandardBid? ReturnedBid { get; set; }

        /// <summary>Sends a quick bid.</summary>
        /// <param name="standardBid">The standard bid.</param>
        /// <returns>A Task.</returns>
        public async Task SendQuickBidAsync(StandardBid standardBid)
        {
            await ProcessQuickBidAsync(standardBid).ConfigureAwait(false);
            await Clients.All.SendAsync("ReceiveQuickBid" + standardBid.LotID, ReturnedBid, Message).ConfigureAwait(false);
        }

        /// <summary>Process the quick bid described by standardBid.</summary>
        /// <param name="standardBid">The standard bid.</param>
        /// <returns>A Task.</returns>
        public async Task ProcessQuickBidAsync(StandardBid standardBid)
        {
            try
            {
                var bid = await service.GetAsync<BidModel>(
                    "Auctions/Bid/SignalRQuickBid",
                    new()
                    {
                        ["MaxBid"] = standardBid.MaxBid,
                        ["BidIncrement"] = standardBid.BidIncrement,
                        ["UserID"] = standardBid.UserID,
                        ["LotID"] = standardBid.LotID,
                    });
                CurrentBid = bid?.CurrentBid ?? 0.00m;
                ReturnedBid = new()
                {
                    CurrentBid = CurrentBid,
                    UserID = standardBid.UserID,
                    LotID = standardBid.LotID,
                    MaxBid = bid?.MaxBid ?? 00.00m,
                };
            }
            catch (Exception e)
            {
                Message = $"Error: {e.Message}, Inner Exception: {e.InnerException?.Message}";
            }
        }

        /// <summary>Sends a maximum automatic bid.</summary>
        /// <param name="standardBid">The standard bid.</param>
        /// <returns>A Task.</returns>
        public async Task SendMaxAutoBidAsync(StandardBid standardBid)
        {
            await ProcessMaxAutoBidAsync(standardBid).ConfigureAwait(false);
            await Clients.All.SendAsync($"ReceiveMaxAutoBid{standardBid.LotID}", ReturnedBid, Message).ConfigureAwait(false);
        }

        /// <summary>Process the maximum automatic bid described by standardBid.</summary>
        /// <param name="standardBid">The standard bid.</param>
        /// <returns>A Task.</returns>
        public async Task ProcessMaxAutoBidAsync(StandardBid standardBid)
        {
            try
            {
                var highestBid = await GetHighestCurrentBidAsync(standardBid.LotID).ConfigureAwait(false);
                standardBid.CurrentBid = highestBid ?? standardBid.BidIncrement;
                var bid = await CreateBidAsync(standardBid).ConfigureAwait(false);
                await FindNextBidAsync(
                        userID: standardBid.UserID,
                        currentBid: bid?.CurrentBid ?? 0,
                        lotID: standardBid.LotID,
                        bidIncrement: standardBid.BidIncrement/*, maxBid: standardBid.MaxBid*/)
                    .ConfigureAwait(false);
                var currentBid = await GetHighestCurrentBidAsync(standardBid.LotID).ConfigureAwait(false);
                CurrentBid = currentBid ?? 0.00m;
                ReturnedBid = new()
                {
                    CurrentBid = CurrentBid,
                    UserID = standardBid.UserID,
                    LotID = standardBid.LotID,
                    MaxBid = bid?.MaxBid ?? 00.00m,
                };
            }
            catch (Exception e)
            {
                Message = $"Error: {e.Message}, Inner Exception: {e.InnerException?.Message}";
            }
        }

        /// <summary>Gets highest current bid.</summary>
        /// <param name="lotID">Identifier for the product.</param>
        /// <returns>The highest current bid.</returns>
        public Task<decimal?> GetHighestCurrentBidAsync(int lotID)
        {
            return service.GetAsync<decimal?>(
                "Auctions/Bid/GetSignalRHighestCurrentBid",
                new() { ["LotID"] = lotID, });
        }

        /// <summary>Searches for the next bid.</summary>
        /// <param name="userID">      Identifier for the user.</param>
        /// <param name="currentBid">  The current bid.</param>
        /// <param name="lotID">   Identifier for the product.</param>
        /// <param name="bidIncrement">The bid increment.</param>
        /// <returns>The found bid.</returns>
        private async Task FindNextBidAsync(int userID, decimal currentBid, int lotID, decimal bidIncrement/*, decimal? maxBid*/)
        {
            while (true)
            {
                var bid = await service.GetAsync<BidModel>("Auctions/Bid/GetSignalRBid", new() { ["UserID"] = userID })
                    .ConfigureAwait(false);
                if (bid?.CurrentBid > currentBid || currentBid >= bid?.MaxBid)
                {
                    return;
                }
                bid = await CreateBidAsync(new()
                {
                    CurrentBid = currentBid + bidIncrement,
                    UserID = bid!.UserID,
                    MaxBid = bid.MaxBid!.Value,
                    LotID = lotID,
                }).ConfigureAwait(false);
                userID = bid!.UserID;
                currentBid = bid.CurrentBid!.Value;
            }
        }

        /// <summary>Creates bid.</summary>
        /// <param name="standardBid">The standard bid.</param>
        /// <returns>The new bid.</returns>
        private Task<BidModel?> CreateBidAsync(StandardBid standardBid)
        {
            return service.PostAsync(
                "Auctions/Bid/CreateSignalRBid",
                new BidModel
                {
                    UserID = standardBid.UserID,
                    CurrentBid = standardBid.CurrentBid,
                    LotID = standardBid.LotID,
                    MaxBid = standardBid.MaxBid,
                });
        }
    }
}
