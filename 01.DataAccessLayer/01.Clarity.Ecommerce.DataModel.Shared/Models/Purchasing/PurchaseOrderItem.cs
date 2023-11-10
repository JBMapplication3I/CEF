// <copyright file="PurchaseOrderItem.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order item class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IPurchaseOrderItem
        : ISalesItemBase<PurchaseOrderItem, AppliedPurchaseOrderItemDiscount, PurchaseOrderItemTarget>
    {
        #region PurchaseOrderItem Properties
        /// <summary>Gets or sets the Date/Time of the date received.</summary>
        /// <value>The date received.</value>
        DateTime? DateReceived { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using Interfaces.DataModel;

    [SqlSchema("Purchasing", "PurchaseOrderItem")]
    public class PurchaseOrderItem
        : SalesItemBase<PurchaseOrder, PurchaseOrderItem, AppliedPurchaseOrderItemDiscount, PurchaseOrderItemTarget>,
            IPurchaseOrderItem
    {
        #region PurchaseOrderItem Properties
        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? DateReceived { get; set; }
        #endregion
    }
}
