// <copyright file="IStoreAccountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoreAccountModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for store discount model.</summary>
    public partial interface IStoreAccountModel
    {
        /// <summary>Gets or sets a value indicating whether this IStoreAccountModel has access to store.</summary>
        /// <value>True if this IStoreAccountModel has access to store, false if not.</value>
        bool HasAccessToStore { get; set; }

        #region Related Objects
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
        #endregion
    }
}
