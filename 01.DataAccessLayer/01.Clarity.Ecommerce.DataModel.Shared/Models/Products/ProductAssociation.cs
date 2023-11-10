// <copyright file="ProductAssociation.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product association class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IProductAssociation
        : IHaveATypeBase<ProductAssociationType>,
            IAmARelationshipTable<Product, Product>,
            IAmFilterableByNullableStore,
            IAmFilterableByNullableBrand
    {
        #region ProductAssociation Properties
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        decimal? Quantity { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Products", "ProductAssociation")]
    public class ProductAssociation : Base, IProductAssociation
    {
        #region IAmARelationshipTable<Product, Product>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Product? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Product? Slave { get; set; }
        #endregion

        #region ProductAssociation Properties
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? Quantity { get; set; }

        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? UnitOfMeasure { get; set; }

        [DefaultValue(null)]
        public int? SortOrder { get; set; }
        #endregion

        #region IAmFilterableByNullableStore Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ProductAssociationType? Type { get; set; }
        #endregion
    }
}
