// <copyright file="StandardBid.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard bid class</summary>
namespace Clarity.Ecommerce.SignalRCore
{
    /// <summary>A standard bid.</summary>
    public class StandardBid
    {
        /// <summary>Gets or sets the current bid.</summary>
        /// <value>The current bid.</value>
        public decimal CurrentBid { get; set; }

        /// <summary>Gets or sets the bid increment.</summary>
        /// <value>The bid increment.</value>
        public decimal BidIncrement { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        public int UserID { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        public int LotID { get; set; }

        /// <summary>Gets or sets the maximum bid.</summary>
        /// <value>The maximum bid.</value>
        public decimal MaxBid { get; set; }
    }
}
