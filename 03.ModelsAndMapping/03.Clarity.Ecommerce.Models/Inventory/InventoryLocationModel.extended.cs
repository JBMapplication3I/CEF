// <copyright file="InventoryLocationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class InventoryLocationModel
    {
        /// <inheritdoc cref="IInventoryLocationModel.Sections"/>
        public List<InventoryLocationSectionModel>? Sections { get; set; }

        /// <inheritdoc/>
        List<IInventoryLocationSectionModel>? IInventoryLocationModel.Sections { get => Sections?.ToList<IInventoryLocationSectionModel>(); set => Sections = value?.Cast<InventoryLocationSectionModel>().ToList(); }

        /// <inheritdoc cref="IInventoryLocationModel.Regions"/>
        public List<InventoryLocationRegionModel>? Regions { get; set; }

        /// <inheritdoc/>
        List<IInventoryLocationRegionModel>? IInventoryLocationModel.Regions { get => Regions?.ToList<IInventoryLocationRegionModel>(); set => Regions = value?.Cast<InventoryLocationRegionModel>().ToList(); }

        /// <inheritdoc cref="IInventoryLocationModel.Users"/>
        public List<InventoryLocationUserModel>? Users { get; set; }

        /// <inheritdoc/>
        List<IInventoryLocationUserModel>? IInventoryLocationModel.Users { get => Users?.ToList<IInventoryLocationUserModel>(); set => Users = value?.Cast<InventoryLocationUserModel>().ToList(); }
    }
}
