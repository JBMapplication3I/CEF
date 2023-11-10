// <copyright file="ZoneModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zone model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class ZoneModel
    {
        #region Zone Properties
        /// <inheritdoc/>
        public int Height { get; set; }

        /// <inheritdoc/>
        public int Width { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IZoneModel.AdZones"/>
        public List<AdZoneModel>? AdZones { get; set; }

        /// <inheritdoc/>
        List<IAdZoneModel>? IZoneModel.AdZones { get => AdZones?.ToList<IAdZoneModel>(); set => AdZones = value?.Cast<AdZoneModel>().ToList(); }
        #endregion
    }
}
