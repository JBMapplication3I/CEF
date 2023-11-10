// <copyright file="SalesInvoice.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISalesInvoice
        : ISalesCollectionBase<SalesInvoice,
                SalesInvoiceStatus,
                SalesInvoiceType,
                SalesInvoiceItem,
                AppliedSalesInvoiceDiscount,
                SalesInvoiceState,
                SalesInvoiceFile,
                SalesInvoiceContact,
                SalesInvoiceEvent,
                SalesInvoiceEventType>,
            IHaveNotesBase
    {
        #region Sales Invoice Properties
        /// <summary>Gets or sets the balance due.</summary>
        /// <value>The balance due.</value>
        decimal? BalanceDue { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales group.</summary>
        /// <value>The identifier of the sales group.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the group the sales belongs to.</summary>
        /// <value>The sales group.</value>
        SalesGroup? SalesGroup { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the associated sales orders.</summary>
        /// <value>The associated sales orders.</value>
        ICollection<SalesOrderSalesInvoice>? AssociatedSalesOrders { get; set; }

        /// <summary>Gets or sets the sales invoice payments.</summary>
        /// <value>The sales invoice payments.</value>
        ICollection<SalesInvoicePayment>? SalesInvoicePayments { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Invoicing", "SalesInvoice")]
    public class SalesInvoice
        : SalesCollectionBase<SalesInvoice,
              SalesInvoiceStatus,
              SalesInvoiceType,
              SalesInvoiceItem,
              AppliedSalesInvoiceDiscount,
              SalesInvoiceState,
              SalesInvoiceFile,
              SalesInvoiceContact,
              SalesInvoiceEvent,
              SalesInvoiceEventType>,
          ISalesInvoice
    {
        private ICollection<Note>? notes;
        private ICollection<SalesOrderSalesInvoice>? associatedSalesOrders;
        private ICollection<SalesInvoicePayment>? salesInvoicePayments;

        public SalesInvoice()
        {
            // HaveNotesBase
            notes = new HashSet<Note>();
            // SalesInvoice
            salesInvoicePayments = new HashSet<SalesInvoicePayment>();
            associatedSalesOrders = new HashSet<SalesOrderSalesInvoice>();
        }

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region Sales Invoice Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4)]
        public decimal? BalanceDue { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroup))]
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroup { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrderSalesInvoice>? AssociatedSalesOrders { get => associatedSalesOrders; set => associatedSalesOrders = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesInvoicePayment>? SalesInvoicePayments { get => salesInvoicePayments; set => salesInvoicePayments = value; }
        #endregion
    }
}
