// <copyright file="StoreAccountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store account model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the store account.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IStoreAccountModel"/>
    public partial class StoreAccountModel
    {
        #region StoreAccount Properties
        /// <inheritdoc/>
        public bool HasAccessToStore { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? PricePointID { get; set; }

        /// <inheritdoc/>
        public string? PricePointKey { get; set; }

        /// <inheritdoc/>
        public string? PricePointName { get; set; }

        /// <inheritdoc cref="IStoreAccountModel.PricePoint"/>
        public PricePointModel? PricePoint { get; set; }

        /// <inheritdoc/>
        IPricePointModel? IStoreAccountModel.PricePoint { get => PricePoint; set => PricePoint = (PricePointModel?)value; }
        #endregion
    }
}
