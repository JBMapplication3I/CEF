// <copyright file="SalesInvoiceEvent.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice event class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISalesInvoiceEvent : ISalesEventBase<SalesInvoice, SalesInvoiceEventType>
    {
        #region SalesInvoiceEvent Properties
        /// <summary>Gets or sets the old balance due.</summary>
        /// <value>The old balance due.</value>
        decimal? OldBalanceDue { get; set; }

        /// <summary>Gets or sets the new balance due.</summary>
        /// <value>The new balance due.</value>
        decimal? NewBalanceDue { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using Interfaces.DataModel;

    [SqlSchema("Invoicing", "SalesInvoiceEvent")]
    public class SalesInvoiceEvent
        : SalesEventBase<SalesInvoice, SalesInvoiceEventType>,
            ISalesInvoiceEvent
    {
        #region SalesInvoiceEvent Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? OldBalanceDue { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? NewBalanceDue { get; set; }
        #endregion
    }
}
