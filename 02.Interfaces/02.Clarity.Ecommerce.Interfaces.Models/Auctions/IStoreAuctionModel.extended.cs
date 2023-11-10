// <copyright file="IStoreAuctionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoreAuctionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IStoreAuctionModel
    {
        /// <summary>Gets or sets a value indicating whether this record has access to the store.</summary>
        /// <value>True if this record has access to the store, false if not.</value>
        bool IsVisibleIn { get; set; }
    }
}
