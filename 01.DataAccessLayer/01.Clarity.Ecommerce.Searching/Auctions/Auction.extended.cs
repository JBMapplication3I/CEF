// <copyright file="Auction.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the auction SQL search extensions class</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ecommerce.DataModel;
    using Utilities;

    public static partial class AuctionSQLSearchExtensions
    {
        public static IQueryable<Auction> FilterAuctionsByUserWithBids(
            this IQueryable<Auction> query,
            int? userID)
        {
            if (!Contract.CheckValidID(userID))
            {
                return query;
            }
            return query.Where(x => x.Lots!.Any(x => x.Bids!.Any(x => x.UserID == userID!.Value)));
        }
    }
}
