// <copyright file="IFranchiseAccountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFranchiseAccountModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IFranchiseAccountModel
    {
        /// <summary>Gets or sets a value indicating whether this account has access to the franchise.</summary>
        /// <value>True if this account has access to the franchise, false if not.</value>
        bool HasAccessToFranchise { get; set; }

        /// <summary>Gets or sets the identifier of the price point.</summary>
        /// <value>The identifier of the price point.</value>
        int? PricePointID { get; set; }

        /// <summary>Gets or sets the price point key.</summary>
        /// <value>The price point key.</value>
        string? PricePointKey { get; set; }

        /// <summary>Gets or sets the name of the price point.</summary>
        /// <value>The name of the price point.</value>
        string? PricePointName { get; set; }

        /// <summary>Gets or sets the price point.</summary>
        /// <value>The price point.</value>
        IPricePointModel? PricePoint { get; set; }
    }
}
