// <copyright file="SalesReturnItem.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return item class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesReturnItem
        : ISalesItemBase<SalesReturnItem, AppliedSalesReturnItemDiscount, SalesReturnItemTarget>
    {
        #region SalesReturnItem Properties
        /// <summary>Gets or sets the restocking fee amount.</summary>
        /// <value>The restocking fee amount.</value>
        decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales return reason.</summary>
        /// <value>The identifier of the sales return reason.</value>
        int? SalesReturnReasonID { get; set; }

        /// <summary>Gets or sets the sales return reason.</summary>
        /// <value>The sales return reason.</value>
        SalesReturnReason? SalesReturnReason { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Returning", "SalesReturnItem")]
    public class SalesReturnItem
        : SalesItemBase<SalesReturn, SalesReturnItem, AppliedSalesReturnItemDiscount, SalesReturnItemTarget>,
            ISalesReturnItem
    {
        #region SalesReturnItem Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? RestockingFeeAmount { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesReturnReason)), DefaultValue(null)]
        public int? SalesReturnReasonID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapInEver, DontMapOutEver]
        public virtual SalesReturnReason? SalesReturnReason { get; set; }
        #endregion
    }
}
