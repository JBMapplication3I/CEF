// <copyright file="AuctionService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the auction service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    [PublicAPI,
        Route("/Auctions/Auction/GetActiveAuctions", "GET",
            Summary = "Endpoint for The Your Active Auctions Section.")]
    public partial class GetActiveAuctions : IReturn<List<AuctionModel>>
    {
    }

    [PublicAPI,
        Route("/Auctions/Auction/GetMoreItemsFromYourAuctions", "GET",
            Summary = "Endpoint for More Items From Your Auctions Section.")]
    public partial class GetMoreItemsFromYourAuctions : IReturn<List<LotModel>>
    {
    }

    [PublicAPI,
        Route("/Auctions/Bid/CreateSignalRBid", "POST")]
    public partial class CreateSignalRBid : BidModel, IReturn<BidModel>
    {
    }

    [PublicAPI,
        Route("/Auctions/Bid/GetSignalRBid", "GET")]
    public partial class GetSignalRBid : IReturn<BidModel>
    {
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int UserID { get; set; }
    }

    [PublicAPI,
        Route("/Auctions/Bid/SignalRQuickBid", "GET")]
    public partial class SignalRQuickBid : IReturn<BidModel>
    {
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int UserID { get; set; }

        [ApiMember(Name = nameof(LotID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int LotID { get; set; }

        [ApiMember(Name = nameof(BidIncrement), DataType = "decimal", ParameterType = "query", IsRequired = true)]
        public decimal BidIncrement { get; set; }

        [ApiMember(Name = nameof(MaxBid), DataType = "decimal", ParameterType = "query", IsRequired = true)]
        public decimal MaxBid { get; set; }
    }

    [PublicAPI,
        Route("/Auctions/Bid/GetSignalRHighestCurrentBid", "GET")]
    public partial class GetSignalRHighestCurrentBid : IReturn<decimal?>
    {
        [ApiMember(Name = nameof(LotID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int LotID { get; set; }
    }

    [PublicAPI,
        Route("/Auctions/Bid/GetSortedProductIDsByHighestBidCount", "GET")]
    public partial class GetSortedProductIDsByHighestBidCount : IReturn<BidModel>
    {
        [ApiMember(Name = nameof(IDs), DataType = "List<int>", ParameterType = "query", IsRequired = true)]
        // ReSharper disable once InconsistentNaming
        public List<int> IDs { get; set; } = null!;
    }

    [PublicAPI,
        Route("/Auctions/Bid/LargeBidNotifications", "GET")]
    public partial class LargeBidNotifications : IReturn<bool>
    {
        [ApiMember(Name = nameof(Bid), DataType = "decimal", ParameterType = "query", IsRequired = true)]
        public decimal Bid { get; set; }
    }

    [PublicAPI,
        Route("/Auctions/Bid/ProcessPaymentLotWon", "POST")]
    public partial class ProcessPaymentLotWon : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int UserID { get; set; }

        [ApiMember(Name = nameof(LotID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int LotID { get; set; }
    }

    [PublicAPI,
        Route("/Auctions/Auction/GetAuctionsByPostalCodeRadius", "GET")]
    public partial class GetAuctionsByZipCodeRadius : IReturn<List<AuctionModel>>
    {
        [ApiMember(Name = nameof(PostalCode), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string? PostalCode { get; set; }

        [ApiMember(Name = nameof(Radius), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int Radius { get; set; }
    }

    public partial class AuctionService
    {
        public async Task<object?> Post(BidOnGroupedLots request)
        {
            return await Workflows.Auctions.BidOnGroupedLots(
                    request.GroupID,
                    CurrentUserIDOrThrow401,
                    request.Amount,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetAuctionsByZipCodeRadius request)
        {
            return await Workflows.Auctions.GetAuctionsByPostalCodeRadius(
                    request.PostalCode,
                    request.Radius,
                    Enums.LocatorUnits.Miles,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetSignalRHighestCurrentBid request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            return await context.Bids
                .AsNoTracking()
                .Where(x => x.LotID == request.LotID)
                .OrderByDescending(x => x.CurrentBid)
                .Select(x => x.CurrentBid)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(SignalRQuickBid request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var bid = await context.Bids
                .AsNoTracking()
                .Where(x => x.LotID == request.LotID)
                .OrderByDescending(x => x.CurrentBid)
                .FirstOrDefaultAsync();
            if (request.UserID == bid.UserID)
            {
                return bid;
            }
            var currentBid = bid?.CurrentBid ?? 0;
            currentBid = currentBid > request.MaxBid
                ? currentBid
                : currentBid + request.BidIncrement;
            var userBid = new Bid
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                CurrentBid = currentBid,
                UserID = request.UserID,
                Won = false,
                MaxBid = request.MaxBid,
                StatusID = 1,
                LotID = request.LotID,
            };
            context.Bids.Add(userBid);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            var topBidsUserID = await context.Bids
                .AsNoTracking()
                .Where(x => x.LotID == request.LotID)
                .OrderByDescending(x => x.CurrentBid)
                .Select(x => x.UserID)
                .FirstOrDefaultAsync();
            if (topBidsUserID == request.UserID)
            {
                userBid.StatusID = 2;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return userBid;
        }

        public async Task<object?> Post(ProcessPaymentLotWon request)
        {
            var result = await Workflows.Auctions.ProcessPaymentLotWon(
                    request.LotID,
                    request.UserID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            return result.ActionSucceeded.BoolToCEFAR(result.Messages.ToArray());
        }

        public async Task<object?> Get(LargeBidNotifications request)
        {
            return await Workflows.Auctions.LargeBidNotifications(request.Bid, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetSortedProductIDsByHighestBidCount request)
        {
            return await Workflows.Auctions.SortProductIDsByHighestBidCountAsync(
                    request.IDs,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetActiveAuctions _)
        {
            return await Workflows.Auctions.GetActiveAuctions(
                    CurrentUserIDOrThrow401,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetMoreItemsFromYourAuctions _)
        {
            return await Workflows.Auctions.GetAuctionsWithBidHistoryAsync(
                    CurrentUserIDOrThrow401,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetSignalRBid request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            return await context.Bids
                .Where(x => x.UserID != request.UserID)
                .OrderByDescending(x => x.CurrentBid)
                .Select(x => new BidModel
                {
                    CurrentBid = x.CurrentBid,
                    UserID = x.UserID,
                    MaxBid = x.MaxBid,
                })
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(CreateSignalRBid request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            Contract.RequiresValidID(request.LotID);
            var bid = context.Bids.Add(new()
            {
                Active = request.Active,
                CreatedDate = DateExtensions.GenDateTime,
                CurrentBid = request.CurrentBid,
                CustomKey = request.CustomKey,
                UserID = CurrentUserIDOrThrow401,
                Won = request.Won,
                MaxBid = request.MaxBid,
                StatusID = 1,
                LotID = request.LotID,
            });
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            request.ID = bid.ID;
            return request;
        }

        public async Task<object?> Get(GetAuctionsByIDs request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Auctions.GetLastModifiedForByIDsResultAsync(
                        auctionIDs: Contract.RequiresNotEmpty(request.IDs).ToList(),
                        brandID: request.BrandID,
                        storeID: request.StoreID,
                        isVendorAdmin: request.IsVendorAdmin ?? false,
                        vendorAdminID: request.VendorAdminID,
                        contextProfileName: null),
                    async () => (await Workflows.Auctions.GetByIDsAsync(
                                auctionIDs: Contract.RequiresNotEmpty(request.IDs).ToList(),
                                brandID: request.BrandID,
                                storeID: request.StoreID,
                                isVendorAdmin: request.IsVendorAdmin ?? false,
                                vendorAdminID: request.VendorAdminID,
                                contextProfileName: null)
                            .ConfigureAwait(false))
                        .Cast<AuctionModel>()
                        .ToList())
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetLotsByIDs request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Lots.GetLastModifiedForByIDsResultAsync(
                        lotIDs: Contract.RequiresNotEmpty(request.IDs).ToList(),
                        brandID: request.BrandID,
                        storeID: request.StoreID,
                        isVendorAdmin: request.IsVendorAdmin ?? false,
                        vendorAdminID: request.VendorAdminID,
                        contextProfileName: null),
                    async () => (await Workflows.Lots.GetByIDsAsync(
                                lotIDs: Contract.RequiresNotEmpty(request.IDs).ToList(),
                                brandID: request.BrandID,
                                storeID: request.StoreID,
                                isVendorAdmin: request.IsVendorAdmin ?? false,
                                vendorAdminID: request.VendorAdminID,
                                contextProfileName: null)
                            .ConfigureAwait(false))
                        .Cast<LotModel>()
                        .ToList())
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(ValidateVinNumber request)
        {
            return await Workflows.Lots.ValidateVinNumber(
                request.ProductID,
                request.VinNumber!,
                null)
            .ConfigureAwait(false);
        }
    }

    [PublicAPI,
        Route("/Auctions/Auction/Admin/Portal/Records", "GET")]
    public partial class AdminGetAuctionsForPortal : AuctionSearchModel, IReturn<AuctionPagedResults>
    {
    }

    [PublicAPI,
        Route("/Auctions/Bids/Admin/Portal/Records", "GET")]
    public partial class AdminGetBidsForPortal : BidSearchModel, IReturn<BidPagedResults>
    {
    }

    [PublicAPI,
        Route("/Auctions/Lots/Admin/Portal/Records", "GET")]
    public partial class AdminGetLotsForPortal : LotSearchModel, IReturn<LotPagedResults>
    {
    }

    public partial class AuctionService
    {
        public async Task<object?> Get(AdminGetAuctionsForPortal request)
        {
            switch (CurrentAPIKind)
            {
                case Enums.APIKind.BrandAdmin:
                {
                    request.BrandID = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.FranchiseAdmin:
                {
                    request.FranchiseID = await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.StoreAdmin:
                {
                    request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.ManufacturerAdmin:
                case Enums.APIKind.VendorAdmin:
                default:
                {
                    throw HttpError.Forbidden("Invalid operation");
                }
            }
            request.Active = true;
            request.AsListing = true;
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Auctions.GetLastModifiedForResultSetAsync(request, ServiceContextProfileName),
                    async () =>
                    {
                        var (results, totalPages, totalCount) = await Workflows.Auctions.SearchAsync(
                                request,
                                request.AsListing,
                                ServiceContextProfileName)
                            .ConfigureAwait(false);
                        return new AuctionPagedResults
                        {
                            Results = results.Cast<AuctionModel>().ToList(),
                            CurrentCount = request.Paging?.Size ?? totalCount,
                            CurrentPage = request.Paging?.StartIndex ?? 1,
                            TotalPages = totalPages,
                            TotalCount = totalCount,
                            Sorts = request.Sorts,
                            Groupings = request.Groupings,
                        };
                    })
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(AdminGetLotsForPortal request)
        {
            // NOTE: To keep data management & security feasible, require an auction identifier for this call
            _ = Contract.RequiresValidID<InvalidOperationException>(request.AuctionID);
            switch (CurrentAPIKind)
            {
                case Enums.APIKind.BrandAdmin:
                {
                    // request.BrandID = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.FranchiseAdmin:
                {
                    // request.FranchiseID = await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.StoreAdmin:
                {
                    // request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.ManufacturerAdmin:
                case Enums.APIKind.VendorAdmin:
                default:
                {
                    throw HttpError.Forbidden("Invalid operation");
                }
            }
            request.Active = true;
            request.AsListing = true;
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Lots.GetLastModifiedForResultSetAsync(request, ServiceContextProfileName),
                    async () =>
                    {
                        var (results, totalPages, totalCount) = await Workflows.Lots.SearchAsync(
                                request,
                                request.AsListing,
                                ServiceContextProfileName)
                            .ConfigureAwait(false);
                        return new LotPagedResults
                        {
                            Results = results.Cast<LotModel>().ToList(),
                            CurrentCount = request.Paging?.Size ?? totalCount,
                            CurrentPage = request.Paging?.StartIndex ?? 1,
                            TotalPages = totalPages,
                            TotalCount = totalCount,
                            Sorts = request.Sorts,
                            Groupings = request.Groupings,
                        };
                    })
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(AdminGetBidsForPortal request)
        {
            // NOTE: To keep data management & security feasible, require either a listing or lot identifier for this call
            _ = Contract.RequiresValidID<InvalidOperationException>(request.LotID);
            switch (CurrentAPIKind)
            {
                case Enums.APIKind.BrandAdmin:
                {
                    // request.BrandID = await CurrentBrandForBrandAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.FranchiseAdmin:
                {
                    // request.FranchiseID = await CurrentFranchiseForFranchiseAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.StoreAdmin:
                {
                    // request.StoreID = await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false);
                    break;
                }
                case Enums.APIKind.ManufacturerAdmin:
                case Enums.APIKind.VendorAdmin:
                default:
                {
                    throw HttpError.Forbidden("Invalid operation");
                }
            }
            request.Active = true;
            request.AsListing = true;
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Bids.GetLastModifiedForResultSetAsync(request, ServiceContextProfileName),
                    async () =>
                    {
                        var (results, totalPages, totalCount) = await Workflows.Bids.SearchAsync(
                                request,
                                request.AsListing,
                                ServiceContextProfileName)
                            .ConfigureAwait(false);
                        return new BidPagedResults
                        {
                            Results = results.Cast<BidModel>().ToList(),
                            CurrentCount = request.Paging?.Size ?? totalCount,
                            CurrentPage = request.Paging?.StartIndex ?? 1,
                            TotalPages = totalPages,
                            TotalCount = totalCount,
                            Sorts = request.Sorts,
                            Groupings = request.Groupings,
                        };
                    })
                .ConfigureAwait(false);
        }
    }
}
