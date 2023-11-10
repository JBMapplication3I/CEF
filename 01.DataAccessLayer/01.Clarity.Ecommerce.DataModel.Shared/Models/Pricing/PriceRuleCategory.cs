// <copyright file="PriceRuleCategory.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rule category class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for price rule category.</summary>
    public interface IPriceRuleCategory
        : IAmACategoryRelationshipTableWhereCategoryIsTheSlave<PriceRule>
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

    [SqlSchema("Pricing", "PriceRuleCategory")]
    public class PriceRuleCategory : Base, IPriceRuleCategory
    {
        #region IAmARelationshipTable<PriceRule, Category>
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
        public virtual Category? Slave { get; set; }
        #endregion

        #region IAmFilterableByCategory
        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmFilterableByCategory.CategoryID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Category? IAmFilterableByCategory.Category { get => Slave; set => Slave = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        int IAmACategoryRelationshipTableWhereCategoryIsTheSlave<PriceRule>.CategoryID { get => SlaveID; set => SlaveID = value; }

        /// <inheritdoc/>
        [NotMapped, JsonIgnore, Obsolete("Cannot use in queries, use Slave instead.", true)]
        Category? IAmACategoryRelationshipTableWhereCategoryIsTheSlave<PriceRule>.Category { get => Slave; set => Slave = value; }
        #endregion
    }
}
