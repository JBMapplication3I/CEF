// <copyright file="IBrandAccountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IBrandAccountModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for brand account model.</summary>
    public partial interface IBrandAccountModel
    {
        /// <summary>Gets or sets a value indicating whether this IBrandAccountModel has access to brand.</summary>
        /// <value>True if this IBrandAccountModel has access to brand, false if not.</value>
        public bool IsVisibleIn { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the price point.</summary>
        /// <value>The identifier of the price point.</value>
        int? PricePointID { get; set; }

        /// <summary>Gets or sets the price point.</summary>
        /// <value>The price point.</value>
        IPricePointModel? PricePoint { get; set; }

        /// <summary>Gets or sets the price point key.</summary>
        /// <value>The price point key.</value>
        string? PricePointKey { get; set; }

        /// <summary>Gets or sets the name of the price point.</summary>
        /// <value>The name of the price point.</value>
        string? PricePointName { get; set; }
        #endregion
    }
}
