// <copyright file="ITaxRegionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITaxRegionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for tax region model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface ITaxRegionModel
    {
        /// <summary>Gets or sets the rate.</summary>
        /// <value>The rate.</value>
        decimal Rate { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the region.</summary>
        /// <value>The identifier of the region.</value>
        int RegionID { get; set; }

        /// <summary>Gets or sets the region key.</summary>
        /// <value>The region key.</value>
        string? RegionKey { get; set; }

        /// <summary>Gets or sets the name of the region.</summary>
        /// <value>The name of the region.</value>
        string? RegionName { get; set; }

        /// <summary>Gets or sets the region.</summary>
        /// <value>The region.</value>
        IRegionModel? Region { get; set; }
        #endregion
    }
}
