﻿// <copyright file="BrandProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand product class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IBrandProduct
        : IAmABrandRelationshipTableWhereBrandIsTheMaster<Product>,
            IAmAProductRelationshipTableWhereProductIsTheSlave<Brand>
    {
        #region BrandProduct Properties
        /// <summary>Gets or sets a value indicating whether this record is visible in the owner.</summary>
        /// <value>True if this record is visible in the owner, false if not.</value>
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

    [SqlSchema("Brands", "BrandProduct")]
    public class BrandProduct : Base, IBrandProduct
    {
        #region IAmARelationshipTable<Brand, Product>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Brand? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Product? Slave { get; set; }

        #region IAmFilterableByBrand
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByBrand.BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Brand? IAmFilterableByBrand.Brand { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByProduct.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmFilterableByProduct.Product { get => Slave; set => Slave = value; }
        #endregion

        #region IAmABrandRelationshipTableWhereBrandIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmABrandRelationshipTableWhereBrandIsTheMaster<Product>.BrandID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Brand? IAmABrandRelationshipTableWhereBrandIsTheMaster<Product>.Brand { get => Master; set => Master = value; }
        #endregion

        #region IAmAProductRelationshipTableWhereProductIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAProductRelationshipTableWhereProductIsTheSlave<Brand>.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheSlave<Brand>.Product { get => Slave; set => Slave = value; }
        #endregion
        #endregion

        #region BrandProduct Properties
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
            return new System.Text.StringBuilder(base.ToHashableString())
                // Contact
                .Append("VS: ").AppendLine(IsVisibleIn.ToString())
                .Append("PM: ").AppendLine(PriceMsrp?.ToString("c5") ?? string.Empty)
                .Append("PR: ").AppendLine(PriceReduction?.ToString("c5") ?? string.Empty)
                .Append("PB: ").AppendLine(PriceBase?.ToString("c5") ?? string.Empty)
                .Append("PS: ").AppendLine(PriceSale?.ToString("c5") ?? string.Empty)
                // Related Objects
                .Append("M: ").AppendLine(Master?.ToHashableString() ?? $"No Master={MasterID}")
                .Append("S: ").AppendLine(Slave?.ToHashableString() ?? $"No Slave={SlaveID}")
                .ToString();
        }
        #endregion
    }
}
