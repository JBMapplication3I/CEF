// <copyright file="ProductSubscriptionType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product SubscriptionType class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IProductSubscriptionType
        : IAmAProductRelationshipTableWhereProductIsTheMaster<SubscriptionType>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the subscription type repeat type.</summary>
        /// <value>The identifier of the subscription type repeat type.</value>
        int SubscriptionTypeRepeatTypeID { get; set; }

        /// <summary>Gets or sets the type of the subscription type repeat.</summary>
        /// <value>The type of the subscription type repeat.</value>
        SubscriptionTypeRepeatType? SubscriptionTypeRepeatType { get; set; }
        #endregion

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Products", "ProductSubscriptionType")]
    public class ProductSubscriptionType : Base, IProductSubscriptionType
    {
        #region IAmARelationshipTable<Product, SubscriptionType>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? Master { get; set; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot be used in queries, use MasterID instead.", true)]
        int IAmFilterableByProduct.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot be used in queries, use Master instead.", true)]
        Product? IAmFilterableByProduct.Product { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot be used in queries, use MasterID instead.", true)]
        int IAmAProductRelationshipTableWhereProductIsTheMaster<SubscriptionType>.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot be used in queries, use Master instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheMaster<SubscriptionType>.Product { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SubscriptionType? Slave { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the subscription type repeat type.</summary>
        /// <value>The identifier of the subscription type repeat type.</value>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SubscriptionTypeRepeatType)), DefaultValue(0)]
        public int SubscriptionTypeRepeatTypeID { get; set; }

        /// <summary>Gets or sets the type of the subscription type repeat.</summary>
        /// <value>The type of the subscription type repeat.</value>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SubscriptionTypeRepeatType? SubscriptionTypeRepeatType { get; set; }
        #endregion

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }
    }
}
