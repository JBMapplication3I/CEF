// <copyright file="FavoriteVendor.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite vendor class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IFavoriteVendor
        : IAmAFavoriteRelationshipTable<Vendor>,
            IAmAVendorRelationshipTableWhereVendorIsTheSlave<User>
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

    [SqlSchema("Favorites", "FavoriteVendor")]
    public class FavoriteVendor : Base, IFavoriteVendor
    {
        #region IAmAFavoriteRelationshipTable<Vendor>
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
        public virtual Vendor? Slave { get; set; }
        #endregion

        #region IAmFilterableByVendor
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByVendor.VendorID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Vendor? IAmFilterableByVendor.Vendor { get => Slave; set => Slave = value; }
        #endregion

        #region IAmAVendorRelationshipTableWhereVendorIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAVendorRelationshipTableWhereVendorIsTheSlave<User>.VendorID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Vendor? IAmAVendorRelationshipTableWhereVendorIsTheSlave<User>.Vendor { get => Slave; set => Slave = value; }
        #endregion
    }
}
