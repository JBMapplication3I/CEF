// <copyright file="SalesReturnReason.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the setting type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesReturnReason : ITypableBase
    {
        #region SalesReturnReason Properties
        /// <summary>Gets or sets a value indicating whether this ISalesReturnReason is restocking fee applicable.</summary>
        /// <value>True if this ISalesReturnReason is restocking fee applicable, false if not.</value>
        bool IsRestockingFeeApplicable { get; set; }

        /// <summary>Gets or sets the restocking fee percent.</summary>
        /// <value>The restocking fee percent.</value>
        decimal? RestockingFeePercent { get; set; }

        /// <summary>Gets or sets the restocking fee amount.</summary>
        /// <value>The restocking fee amount.</value>
        decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the restocking fee amount currency.</summary>
        /// <value>The identifier of the restocking fee amount currency.</value>
        int? RestockingFeeAmountCurrencyID { get; set; }

        /// <summary>Gets or sets the restocking fee amount currency.</summary>
        /// <value>The restocking fee amount currency.</value>
        Currency? RestockingFeeAmountCurrency { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Returning", "SalesReturnReason")]
    public class SalesReturnReason : TypableBase, ISalesReturnReason
    {
        #region SalesReturnReason Properties
        [DefaultValue(false)]
        public bool IsRestockingFeeApplicable { get; set; }

        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? RestockingFeePercent { get; set; }

        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Related Objects
        [InverseProperty(nameof(ID)), ForeignKey(nameof(RestockingFeeAmountCurrency)), DefaultValue(null)]
        public int? RestockingFeeAmountCurrencyID { get; set; }

        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Currency? RestockingFeeAmountCurrency { get; set; }
        #endregion
    }
}
