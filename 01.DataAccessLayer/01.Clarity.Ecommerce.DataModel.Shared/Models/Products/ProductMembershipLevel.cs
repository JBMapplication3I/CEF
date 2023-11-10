// <copyright file="ProductMembershipLevel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product Membership level class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IProductMembershipLevel
        : IAmAProductRelationshipTableWhereProductIsTheMaster<MembershipLevel>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the membership repeat type.</summary>
        /// <value>The identifier of the membership repeat type.</value>
        int MembershipRepeatTypeID { get; set; }

        /// <summary>Gets or sets the type of the membership repeat.</summary>
        /// <value>The type of the membership repeat.</value>
        MembershipRepeatType? MembershipRepeatType { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the subscriptions.</summary>
        /// <value>The subscriptions.</value>
        ICollection<Subscription>? Subscriptions { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Products", "ProductMembershipLevel")]
    public class ProductMembershipLevel : Base, IProductMembershipLevel
    {
        private ICollection<Subscription>? subscriptions;

        public ProductMembershipLevel()
        {
            subscriptions = new HashSet<Subscription>();
        }

        #region IAmARelationshipTable<Product, MembershipLevel>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(0)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByProduct.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Product? IAmFilterableByProduct.Product { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAProductRelationshipTableWhereProductIsTheMaster<MembershipLevel>.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheMaster<MembershipLevel>.Product { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual MembershipLevel? Slave { get; set; }
        #endregion

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(MembershipRepeatType)), DefaultValue(0)]
        public int MembershipRepeatTypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual MembershipRepeatType? MembershipRepeatType { get; set; }

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Subscription>? Subscriptions { get => subscriptions; set => subscriptions = value; }
        #endregion
    }
}
