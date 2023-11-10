// <copyright file="FavoriteShipCarrier.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite ship carrier class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IFavoriteShipCarrier : IAmAFavoriteRelationshipTable<ShipCarrier>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Favorites", "FavoriteShipCarrier")]
    public class FavoriteShipCarrier : Base, IFavoriteShipCarrier
    {
        #region IAmAFavoriteRelationshipTable<ShipCarrier>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual ShipCarrier? Slave { get; set; }
        #endregion
    }
}
