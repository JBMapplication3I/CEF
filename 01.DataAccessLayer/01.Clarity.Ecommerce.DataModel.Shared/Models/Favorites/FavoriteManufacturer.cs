// <copyright file="FavoriteManufacturer.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite manufacturer class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IFavoriteManufacturer
        : IAmAFavoriteRelationshipTable<Manufacturer>,
            IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<User>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Favorites", "FavoriteManufacturer")]
    public class FavoriteManufacturer : Base, IFavoriteManufacturer
    {
        #region IAmAFavoriteRelationshipTable<Manufacturer>
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
        public virtual Manufacturer? Slave { get; set; }
        #endregion

        #region IAmFilterableByManufacturer
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByManufacturer.ManufacturerID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Manufacturer? IAmFilterableByManufacturer.Manufacturer { get => Slave; set => Slave = value; }
        #endregion

        #region IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<User>.ManufacturerID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Manufacturer? IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<User>.Manufacturer { get => Slave; set => Slave = value; }
        #endregion
    }
}
