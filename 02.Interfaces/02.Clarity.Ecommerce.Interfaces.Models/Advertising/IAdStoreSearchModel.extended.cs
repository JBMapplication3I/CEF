// <copyright file="IAdStoreSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAdStoreSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for ad store search model.</summary>
    public partial interface IAdStoreSearchModel
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the identifier of the zone.</summary>
        /// <value>The identifier of the zone.</value>
        int? ZoneID { get; set; }
    }
}
