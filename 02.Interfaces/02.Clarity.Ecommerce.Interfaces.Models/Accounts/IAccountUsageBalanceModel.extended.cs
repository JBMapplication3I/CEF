// <copyright file="IAccountUsageBalanceModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountUsageBalanceModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for account usage balance model.</summary>
    public partial interface IAccountUsageBalanceModel
    {
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        int Quantity { get; set; }
    }
}
