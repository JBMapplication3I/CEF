// <copyright file="IBrandAuctionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IBrandAuctionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IBrandAuctionModel
    {
        /// <summary>Gets or sets a value indicating whether this record has access to the brand.</summary>
        /// <value>True if this record has access to the brand, false if not.</value>
        bool IsVisibleIn { get; set; }
    }
}
