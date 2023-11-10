// <copyright file="IZoneModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IZoneModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for zone model.</summary>
    public partial interface IZoneModel
    {
        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        int Height { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        int Width { get; set; }

        #region Associated Objects
        /// <summary>Gets or sets the ad zones.</summary>
        /// <value>The ad zones.</value>
        List<IAdZoneModel>? AdZones { get; set; }
        #endregion
    }
}
