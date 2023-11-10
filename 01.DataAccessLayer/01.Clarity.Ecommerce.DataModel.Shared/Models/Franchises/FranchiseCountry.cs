﻿// <copyright file="FranchiseCountry.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise country class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IFranchiseCountry
        : IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Country>
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

    [SqlSchema("Franchises", "FranchiseCountry")]
    public class FranchiseCountry : Base, IFranchiseCountry
    {
        #region IAmARelationshipTable<Franchise, Country>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Franchise? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Country? Slave { get; set; }
        #endregion

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByFranchise.FranchiseID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Franchise? IAmFilterableByFranchise.Franchise { get => Master; set => Master = value; }
        #endregion

        #region IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Country>.FranchiseID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Franchise? IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Country>.Franchise { get => Master; set => Master = value; }
        #endregion
    }
}
