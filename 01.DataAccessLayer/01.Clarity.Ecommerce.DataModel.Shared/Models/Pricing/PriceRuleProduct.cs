// <copyright file="PriceRuleProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule product class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IPriceRuleProduct
        : IAmAProductRelationshipTableWhereProductIsTheSlave<PriceRule>
    {
        #region PriceRuleProduct Properties
        /// <summary>Gets or sets a value indicating whether the override price.</summary>
        /// <value>True if override price, false if not.</value>
        bool OverridePrice { get; set; }

        /// <summary>Gets or sets the override base price.</summary>
        /// <value>The override base price.</value>
        decimal? OverrideBasePrice { get; set; }

        /// <summary>Gets or sets the override sale price.</summary>
        /// <value>The override sale price.</value>
        decimal? OverrideSalePrice { get; set; }
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

    [SqlSchema("Pricing", "PriceRuleProduct")]
    public class PriceRuleProduct : Base, IPriceRuleProduct
    {
        #region IAmARelationshipTable<PriceRule, Product>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master)), DefaultValue(null)]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual PriceRule? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(null)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Product? Slave { get; set; }
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
        int IAmAProductRelationshipTableWhereProductIsTheSlave<PriceRule>.ProductID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Product? IAmAProductRelationshipTableWhereProductIsTheSlave<PriceRule>.Product { get => Slave; set => Slave = value; }
        #endregion

        #region PriceRuleProduct Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool OverridePrice { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? OverrideBasePrice { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? OverrideSalePrice { get; set; }
        #endregion
    }
}
