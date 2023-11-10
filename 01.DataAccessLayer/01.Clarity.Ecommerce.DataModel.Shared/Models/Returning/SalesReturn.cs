// <copyright file="SalesReturn.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISalesReturn
        : ISalesCollectionBase<SalesReturn,
                SalesReturnStatus,
                SalesReturnType,
                SalesReturnItem,
                AppliedSalesReturnDiscount,
                SalesReturnState,
                SalesReturnFile,
                SalesReturnContact,
                SalesReturnEvent,
                SalesReturnEventType>,
            IHaveNotesBase
    {
        #region Sales Return Properties
        /// <summary>Gets or sets the purchase order number.</summary>
        /// <value>The purchase order number.</value>
        string? PurchaseOrderNumber { get; set; }

        /// <summary>Gets or sets the balance due.</summary>
        /// <value>The balance due.</value>
        decimal? BalanceDue { get; set; }

        /// <summary>Gets or sets the tracking number.</summary>
        /// <value>The tracking number.</value>
        string? TrackingNumber { get; set; }

        /// <summary>Gets or sets the identifier of the refund transaction.</summary>
        /// <value>The identifier of the refund transaction.</value>
        string? RefundTransactionID { get; set; }

        /// <summary>Gets or sets the identifier of the tax transaction.</summary>
        /// <value>The identifier of the tax transaction.</value>
        string? TaxTransactionID { get; set; }

        /// <summary>Gets or sets the return approved date.</summary>
        /// <value>The return approved date.</value>
        DateTime? ReturnApprovedDate { get; set; }

        /// <summary>Gets or sets the return commitment date.</summary>
        /// <value>The return commitment date.</value>
        DateTime? ReturnCommitmentDate { get; set; }

        /// <summary>Gets or sets the required ship date.</summary>
        /// <value>The required ship date.</value>
        DateTime? RequiredShipDate { get; set; }

        /// <summary>Gets or sets the requested ship date.</summary>
        /// <value>The requested ship date.</value>
        DateTime? RequestedShipDate { get; set; }

        /// <summary>Gets or sets the actual ship date.</summary>
        /// <value>The actual ship date.</value>
        DateTime? ActualShipDate { get; set; }

        /// <summary>Gets or sets the refund amount.</summary>
        /// <value>The refund amount.</value>
        decimal? RefundAmount { get; set; }
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
        /// <summary>Gets or sets the sales return payments.</summary>
        /// <value>The sales return payments.</value>
        ICollection<SalesReturnPayment>? SalesReturnPayments { get; set; }

        /// <summary>Gets or sets the associated sales orders.</summary>
        /// <value>The associated sales orders.</value>
        ICollection<SalesReturnSalesOrder>? AssociatedSalesOrders { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Returning", "SalesReturn")]
    public class SalesReturn
        : SalesCollectionBase<SalesReturn,
            SalesReturnStatus,
            SalesReturnType,
            SalesReturnItem,
            AppliedSalesReturnDiscount,
            SalesReturnState,
            SalesReturnFile,
            SalesReturnContact,
            SalesReturnEvent,
            SalesReturnEventType>,
          ISalesReturn
    {
        private ICollection<Note>? notes;
        private ICollection<SalesReturnSalesOrder>? associatedSalesOrders;
        private ICollection<SalesReturnPayment>? salesReturnPayments;

        public SalesReturn()
        {
            // HaveNotesBase
            notes = new HashSet<Note>();
            // SalesReturn
            salesReturnPayments = new HashSet<SalesReturnPayment>();
            associatedSalesOrders = new HashSet<SalesReturnSalesOrder>();
        }

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region Sales Return Properties
        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? PurchaseOrderNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DefaultValue(null)]
        public string? TrackingNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DefaultValue(null)]
        public string? RefundTransactionID { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DefaultValue(null)]
        public string? TaxTransactionID { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? BalanceDue { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? RefundAmount { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ReturnApprovedDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ReturnCommitmentDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? RequiredShipDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? RequestedShipDate { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ActualShipDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroup)), DefaultValue(null)]
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroup { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesReturnPayment>? SalesReturnPayments { get => salesReturnPayments; set => salesReturnPayments = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesReturnSalesOrder>? AssociatedSalesOrders { get => associatedSalesOrders; set => associatedSalesOrders = value; }
        #endregion
    }
}
