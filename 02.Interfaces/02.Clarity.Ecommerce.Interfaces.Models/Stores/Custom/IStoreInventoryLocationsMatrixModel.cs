// <copyright file="IStoreInventoryLocationsMatrixModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoreInventoryLocationsMatrixModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for store inventory locations matrix model.</summary>
    public interface IStoreInventoryLocationsMatrixModel
    {
        /// <summary>Gets or sets the identifier of the distribution center inventory location.</summary>
        /// <value>The identifier of the distribution center inventory location.</value>
        int DistributionCenterInventoryLocationID { get; set; }

        /// <summary>Gets or sets the distribution center inventory location key.</summary>
        /// <value>The distribution center inventory location key.</value>
        string? DistributionCenterInventoryLocationKey { get; set; }

        /// <summary>Gets or sets the name of the distribution center inventory location.</summary>
        /// <value>The name of the distribution center inventory location.</value>
        string? DistributionCenterInventoryLocationName { get; set; }

        /// <summary>Gets or sets the identifier of the internal inventory location.</summary>
        /// <value>The identifier of the internal inventory location.</value>
        int InternalInventoryLocationID { get; set; }

        /// <summary>Gets or sets the internal inventory location key.</summary>
        /// <value>The internal inventory location key.</value>
        string? InternalInventoryLocationKey { get; set; }

        /// <summary>Gets or sets the name of the internal inventory location.</summary>
        /// <value>The name of the internal inventory location.</value>
        string? InternalInventoryLocationName { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int StoreID { get; set; }

        /// <summary>Gets or sets the store key.</summary>
        /// <value>The store key.</value>
        string? StoreKey { get; set; }

        /// <summary>Gets or sets the name of the store.</summary>
        /// <value>The name of the store.</value>
        string? StoreName { get; set; }
    }
}
