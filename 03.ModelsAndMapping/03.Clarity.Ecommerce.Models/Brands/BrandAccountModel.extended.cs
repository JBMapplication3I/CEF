// <copyright file="BrandAccountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand account model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    public partial class BrandAccountModel
    {
        /// <inheritdoc/>
        public bool IsVisibleIn { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int? PricePointID { get; set; }

        /// <inheritdoc/>
        public string? PricePointKey { get; set; }

        /// <inheritdoc/>
        public string? PricePointName { get; set; }

        /// <inheritdoc cref="IBrandAccountModel.PricePoint"/>
        public PricePointModel? PricePoint { get; set; }

        /// <inheritdoc/>
        IPricePointModel? IBrandAccountModel.PricePoint { get => PricePoint; set => PricePoint = (PricePointModel?)value; }
        #endregion
    }
}
