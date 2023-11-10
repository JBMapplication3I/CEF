// <copyright file="StoreInventoryLocationsMatrixModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store inventory locations matrix model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the store inventory locations matrix.</summary>
    public class StoreInventoryLocationsMatrixModel : IStoreInventoryLocationsMatrixModel
    {
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        [ApiMember(Name = nameof(StoreID), IsRequired = true, AllowMultiple = false, DataType = "int", ParameterType = "body", Description = "The identifier of the store.")]
        public int StoreID { get; set; }

        /// <summary>Gets or sets the store key.</summary>
        /// <value>The store key.</value>
        [ApiMember(Name = nameof(StoreKey), IsRequired = true, AllowMultiple = false, DataType = "string", ParameterType = "body", Description = "The store key.")]
        public string? StoreKey { get; set; }

        /// <summary>Gets or sets the name of the store.</summary>
        /// <value>The name of the store.</value>
        [ApiMember(Name = nameof(StoreName), IsRequired = true, AllowMultiple = false, DataType = "string", ParameterType = "body", Description = "The name of the store.")]
        public string? StoreName { get; set; }

        /// <summary>Gets or sets the identifier of the internal inventory location.</summary>
        /// <value>The identifier of the internal inventory location.</value>
        [ApiMember(Name = nameof(InternalInventoryLocationID), IsRequired = true, AllowMultiple = false, DataType = "int", ParameterType = "body", Description = "The identifier of the internal inventory location.")]
        public int InternalInventoryLocationID { get; set; }

        /// <summary>Gets or sets the internal inventory location key.</summary>
        /// <value>The internal inventory location key.</value>
        [ApiMember(Name = nameof(InternalInventoryLocationKey), IsRequired = true, AllowMultiple = false, DataType = "string", ParameterType = "body", Description = "The internal inventory location key.")]
        public string? InternalInventoryLocationKey { get; set; }

        /// <summary>Gets or sets the name of the internal inventory location.</summary>
        /// <value>The name of the internal inventory location.</value>
        [ApiMember(Name = nameof(InternalInventoryLocationName), IsRequired = true, AllowMultiple = false, DataType = "string", ParameterType = "body", Description = "The name of the internal inventory location.")]
        public string? InternalInventoryLocationName { get; set; }

        /// <summary>Gets or sets the identifier of the distribution center inventory location.</summary>
        /// <value>The identifier of the distribution center inventory location.</value>
        [ApiMember(Name = nameof(DistributionCenterInventoryLocationID), IsRequired = true, AllowMultiple = false, DataType = "int", ParameterType = "body", Description = "The identifier of the distribution center inventory location.")]
        public int DistributionCenterInventoryLocationID { get; set; }

        /// <summary>Gets or sets the distribution center inventory location key.</summary>
        /// <value>The distribution center inventory location key.</value>
        [ApiMember(Name = nameof(DistributionCenterInventoryLocationKey), IsRequired = true, AllowMultiple = false, DataType = "string", ParameterType = "body", Description = "The distribution center inventory location key.")]
        public string? DistributionCenterInventoryLocationKey { get; set; }

        /// <summary>Gets or sets the name of the distribution center inventory location.</summary>
        /// <value>The name of the distribution center inventory location.</value>
        [ApiMember(Name = nameof(DistributionCenterInventoryLocationName), IsRequired = true, AllowMultiple = false, DataType = "string", ParameterType = "body", Description = "The name of the distribution center inventory location.")]
        public string? DistributionCenterInventoryLocationName { get; set; }
    }
}
