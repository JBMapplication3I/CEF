// <copyright file="ProductShipCarrierMethod.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product price point class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IProductShipCarrierMethod
        : IAmFilterableByNullableStore,
            IAmFilterableByNullableBrand,
            IAmAProductRelationshipTableWhereProductIsTheMaster<ShipCarrierMethod>
    {
        #region ProductShipCarrierMethod Properties
        /// <summary>Gets or sets the minimum quantity.</summary>
        /// <value>The minimum quantity.</value>
        decimal? MinQuantity { get; set; }

        /// <summary>Gets or sets the maximum quantity.</summary>
        /// <value>The maximum quantity.</value>
        decimal? MaxQuantity { get; set; }

        /// <summary>Gets or sets the Date/Time of from.</summary>
        /// <value>from date.</value>
        DateTime? From { get; set; }

        /// <summary>Gets or sets the Date/Time of to.</summary>
        /// <value>To date.</value>
        DateTime? To { get; set; }

        /// <summary>Gets or sets the unit of measure.</summary>
        /// <value>The unit of measure.</value>
        string? UnitOfMeasure { get; set; }

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        decimal Price { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        Currency? Currency { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Products", "ProductShipCarrierMethod")]
    public class ProductShipCarrierMethod : Base, IProductShipCarrierMethod
    {
        #region IAmARelationshipTable<Product, ShipCarrierMethod>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
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
        int IAmAProductRelationshipTableWhereProductIsTheMaster<ShipCarrierMethod>.ProductID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheMaster<ShipCarrierMethod>.Product { get => Master; set => Master = value; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual ShipCarrierMethod? Slave { get; set; }
        #endregion

        #region IAmFilterableByNullableStore Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(0)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(0)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        #region ProductShipCarrierMethod Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MinQuantity { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? MaxQuantity { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? From { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? To { get; set; }

        /// <inheritdoc/>
        [StringLength(10), DefaultValue(null)]
        public string? UnitOfMeasure { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal Price { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Currency)), DefaultValue(null)]
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual Currency? Currency { get; set; }
        #endregion
    }
}
