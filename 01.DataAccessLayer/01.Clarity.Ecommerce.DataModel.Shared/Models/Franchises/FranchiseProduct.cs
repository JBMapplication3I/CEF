// <copyright file="FranchiseProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the franchise product class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IFranchiseProduct
        : IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Product>,
            IAmAProductRelationshipTableWhereProductIsTheSlave<Franchise>
    {
        #region FranchiseProduct Properties
        /// <summary>Gets or sets a value indicating whether this IFranchiseProduct is visible in franchise.</summary>
        /// <value>True if this IFranchiseProduct is visible in franchise, false if not.</value>
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

    [SqlSchema("Franchises", "FranchiseProduct")]
    public class FranchiseProduct : Base, IFranchiseProduct
    {
        #region IAmARelationshipTable<Franchise, Product>
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
        public virtual Product? Slave { get; set; }

        #region IAmFilterableByFranchise
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmFilterableByFranchise.FranchiseID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Franchise? IAmFilterableByFranchise.Franchise { get => Master; set => Master = value; }
        #endregion

        #region IAmFilterableByProduct
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByProduct.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmFilterableByProduct.Product { get => Slave; set => Slave = value; }
        #endregion

        #region IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use MasterID instead.", true)]
        int IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Product>.FranchiseID { get => MasterID; set => MasterID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Master instead.", true)]
        Franchise? IAmAFranchiseRelationshipTableWhereFranchiseIsTheMaster<Product>.Franchise { get => Master; set => Master = value; }
        #endregion

        #region IAmAProductRelationshipTableWhereProductIsTheSlave
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmAProductRelationshipTableWhereProductIsTheSlave<Franchise>.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheSlave<Franchise>.Product { get => Slave; set => Slave = value; }
        #endregion
        #endregion

        #region FranchiseProduct Properties
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
            builder.Append("VF: ").AppendLine(IsVisibleIn.ToString());
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
