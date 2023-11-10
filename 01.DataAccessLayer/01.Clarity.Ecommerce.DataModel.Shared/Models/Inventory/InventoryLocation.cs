// <copyright file="InventoryLocation.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IInventoryLocation
        : INameableBase,
            IHaveANullableContactBase,
            IAmFilterableByBrand<BrandInventoryLocation>,
            IAmFilterableByFranchise<FranchiseInventoryLocation>,
            IAmFilterableByStore<StoreInventoryLocation>
    {
        #region Associated Objects
        /// <summary>Gets or sets the sections.</summary>
        /// <value>The sections.</value>
        ICollection<InventoryLocationSection>? Sections { get; set; }

        /// <summary>Gets or sets the regions.</summary>
        /// <value>The regions.</value>
        ICollection<InventoryLocationRegion>? Regions { get; set; }

        /// <summary>Gets or sets the users.</summary>
        /// <value>The users.</value>
        ICollection<InventoryLocationUser>? Users { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Inventory", "InventoryLocation")]
    public class InventoryLocation : NameableBase, IInventoryLocation
    {
        private ICollection<BrandInventoryLocation>? brands;
        private ICollection<FranchiseInventoryLocation>? franchises;
        private ICollection<StoreInventoryLocation>? stores;
        private ICollection<InventoryLocationSection>? sections;
        private ICollection<InventoryLocationRegion>? regions;
        private ICollection<InventoryLocationUser>? users;

        public InventoryLocation()
        {
            // IAmFilterableByBrand
            brands = new HashSet<BrandInventoryLocation>();
            // IAmFilterableByFranchise
            franchises = new HashSet<FranchiseInventoryLocation>();
            // IAmFilterableByStore
            stores = new HashSet<StoreInventoryLocation>();
            // InventoryLocation Properties
            sections = new HashSet<InventoryLocationSection>();
            // InventoryLocation Regions
            regions = new HashSet<InventoryLocationRegion>();
            // InventoryLocation Users
            users = new HashSet<InventoryLocationUser>();
        }

        #region IHaveANullableContactBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Contact)), DefaultValue(null)]
        public int? ContactID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Contact? Contact { get; set; }
        #endregion

        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandInventoryLocation>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<FranchiseInventoryLocation>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByStore Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<StoreInventoryLocation>? Stores { get => stores; set => stores = value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<InventoryLocationSection>? Sections { get => sections; set => sections = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<InventoryLocationRegion>? Regions { get => regions; set => regions = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<InventoryLocationUser>? Users { get => users; set => users = value; }
        #endregion
    }
}
