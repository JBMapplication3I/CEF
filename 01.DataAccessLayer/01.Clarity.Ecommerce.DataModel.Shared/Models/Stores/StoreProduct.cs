// <copyright file="StoreProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store product class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IStoreProduct
        : IAmAStoreRelationshipTableWhereStoreIsTheMaster<Product>,
            IAmAProductRelationshipTableWhereProductIsTheSlave<Store>
    {
        #region StoreProduct Properties
        /// <summary>Gets or sets a value indicating whether this IStoreProduct is visible in store.</summary>
        /// <value>True if this IStoreProduct is visible in store, false if not.</value>
        bool IsVisibleIn { get; set; }

        /// <summary>Gets or sets the price base.</summary>
        /// <value>The price base.</value>
        decimal? PriceBase { get; set; }

        /// <summary>Gets or sets the price msrp.</summary>
        /// <value>The price msrp.</value>
        decimal? PriceMsrp { get; set; }

        /// <summary>Gets or sets the price reduction.</summary>
        /// <value>The price reduction.</value>
        decimal? PriceReduction { get; set; }

        /// <summary>Gets or sets the price sale.</summary>
        /// <value>The price sale.</value>
        decimal? PriceSale { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Stores", "StoreProduct")]
    public class StoreProduct : Base, IStoreProduct
    {
        #region IAmARelationshipTable<Store, Product>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Store? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Product? Slave { get; set; }
        #endregion

        #region IAmFilterableByStore
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByStore.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmFilterableByStore.Store { get => Master; set => Master = value; }
        #endregion

        #region IAmAStoreRelationshipTableWhereStoreIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAStoreRelationshipTableWhereStoreIsTheMaster<Product>.StoreID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Store? IAmAStoreRelationshipTableWhereStoreIsTheMaster<Product>.Store { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByProduct.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmFilterableByProduct.Product { get => Slave; set => Slave = value; }
        #endregion

        #region IAmAProductRelationshipTableWhereProductIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAProductRelationshipTableWhereProductIsTheSlave<Store>.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheSlave<Store>.Product { get => Slave; set => Slave = value; }
        #endregion

        #region StoreProduct Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsVisibleIn { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceBase { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceMsrp { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceReduction { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? PriceSale { get; set; }
        #endregion

        #region ICloneable Functions
        /// <inheritdoc/>
        public override string ToHashableString()
        {
            var builder = new System.Text.StringBuilder(base.ToHashableString());
            // Contact
            builder.Append("VS: ").AppendLine(IsVisibleIn.ToString());
            builder.Append("PM: ").AppendLine(PriceMsrp?.ToString("c5") ?? string.Empty);
            builder.Append("PR: ").AppendLine(PriceReduction?.ToString("c5") ?? string.Empty);
            builder.Append("PB: ").AppendLine(PriceBase?.ToString("c5") ?? string.Empty);
            builder.Append("PS: ").AppendLine(PriceSale?.ToString("c5") ?? string.Empty);
            // Related Objects
            builder.Append("M: ").AppendLine(Master?.ToHashableString() ?? $"No Master={MasterID}");
            builder.Append("S: ").AppendLine(Slave?.ToHashableString() ?? $"No Slave={SlaveID}");
            // Return
            return builder.ToString();
        }
        #endregion
    }
}
