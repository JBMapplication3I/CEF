// <copyright file="IInventoryLocationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IInventoryLocationModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for inventory location model.</summary>
    public partial interface IInventoryLocationModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the sections.</summary>
        /// <value>The sections.</value>
        List<IInventoryLocationSectionModel>? Sections { get; set; }

        /// <summary>Gets or sets the regions.</summary>
        /// <value>The regions.</value>
        List<IInventoryLocationRegionModel>? Regions { get; set; }

        /// <summary>Gets or sets the users.</summary>
        /// <value>The users.</value>
        List<IInventoryLocationUserModel>? Users { get; set; }
        #endregion
    }
}
