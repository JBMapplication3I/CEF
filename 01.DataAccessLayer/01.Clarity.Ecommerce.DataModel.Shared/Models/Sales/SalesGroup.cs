// <copyright file="SalesGroup.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales group class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISalesGroup
        : IHaveNotesBase,
            IAmFilterableByNullableAccount,
            IAmFilterableByNullableBrand
    {
        // NOTE: These properties are listed in order of the expected standard workflow

        /// <summary>Gets or sets the billing contact for this group.</summary>
        /// <value>The identifier of the billing contact.</value>
        int? BillingContactID { get; set; }

        /// <summary>Gets or sets the billing contact for this group.</summary>
        /// <value>The billing contact.</value>
        Contact? BillingContact { get; set; }

        /// <summary>Gets or sets the sales quote masters.</summary>
        /// <remarks>There should only ever be 1!.</remarks>
        /// <value>The sales quote masters.</value>
        ICollection<SalesQuote>? SalesQuoteRequestMasters { get; set; }

        /// <summary>Gets or sets the sub-quotes that would analogous to sub-orders if the customer decided to purchase
        /// the quote.</summary>
        /// <value>The sales quote subs.</value>
        ICollection<SalesQuote>? SalesQuoteRequestSubs { get; set; }

        /// <summary>Gets or sets responses by individual sellers and how they would/could fulfill the quote if made into
        /// one or more orders.</summary>
        /// <value>The sales quote responses.</value>
        ICollection<SalesQuote>? SalesQuoteResponseMasters { get; set; }

        /// <summary>Gets or sets responses by individual sellers and how they would/could fulfill the quote if made into
        /// one or more orders.</summary>
        /// <value>The sales quote responses.</value>
        ICollection<SalesQuote>? SalesQuoteResponseSubs { get; set; }

        /// <summary>Gets or sets the sub orders which go to individual sellers and/or shipping destinations, etc.
        /// according to Split Order rules.</summary>
        /// <value>The sub sales orders.</value>
        ICollection<SalesOrder>? SubSalesOrders { get; set; }

        /// <summary>Gets or sets the sales order masters.</summary>
        /// <remarks>There should only ever be 1!.</remarks>
        /// <value>The sales order masters.</value>
        ICollection<SalesOrder>? SalesOrderMasters { get; set; }

        /// <summary>Gets or sets any Purchase Orders from this company to it's suppliers in order to increase stock to
        /// cover the requested quantities of items ordered. Includes Drop-Ship orders that would be sent directly to
        /// the customer's shipping destination(s).</summary>
        /// <value>The purchase orders.</value>
        ICollection<PurchaseOrder>? PurchaseOrders { get; set; }

        /// <summary>Gets or sets  the initial and any follow-on invoices (such as NET30 monthly payments) that are
        /// issued for this group.</summary>
        /// <value>The sales invoices.</value>
        ICollection<SalesInvoice>? SalesInvoices { get; set; }

        /// <summary>Gets or sets any RMAs requested by the customer for orders issued in this group, according to Split
        /// Return rules.</summary>
        /// <value>The sales returns.</value>
        ICollection<SalesReturn>? SalesReturns { get; set; }

        /// <summary>Gets or sets the sample requests.</summary>
        /// <value>The sample requests.</value>
        ICollection<SampleRequest>? SampleRequests { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Sales", "SalesGroup")]
    public class SalesGroup : Base, ISalesGroup
    {
        private ICollection<Note>? notes;
        private ICollection<SalesQuote>? salesQuoteRequestMasters;
        private ICollection<SalesQuote>? salesQuoteRequestSubs;
        private ICollection<SalesQuote>? salesQuoteResponseMasters;
        private ICollection<SalesQuote>? salesQuoteResponseSubs;
        private ICollection<SalesOrder>? salesOrderMasters;
        private ICollection<SalesOrder>? subSalesOrders;
        private ICollection<PurchaseOrder>? purchaseOrders;
        private ICollection<SalesInvoice>? salesInvoices;
        private ICollection<SalesReturn>? salesReturns;
        private ICollection<SampleRequest>? sampleRequests;

        public SalesGroup()
        {
            // HaveNotesBase
            notes = new HashSet<Note>();
            // SalesGroup
            salesQuoteRequestMasters = new HashSet<SalesQuote>();
            salesQuoteRequestSubs = new HashSet<SalesQuote>();
            salesQuoteResponseMasters = new HashSet<SalesQuote>();
            salesQuoteResponseSubs = new HashSet<SalesQuote>();
            salesOrderMasters = new HashSet<SalesOrder>();
            subSalesOrders = new HashSet<SalesOrder>();
            purchaseOrders = new HashSet<PurchaseOrder>();
            salesInvoices = new HashSet<SalesInvoice>();
            salesReturns = new HashSet<SalesReturn>();
            sampleRequests = new HashSet<SampleRequest>();
        }

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region IAmFilterableByNullableAccount Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Account)), DefaultValue(null)]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, OnlyMapOutFlattenedValuesInsteadOfObjectWithLite, DefaultValue(null), JsonIgnore]
        public virtual Account? Account { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        // NOTE: These properties are listed in order of the expected standard workflow

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(BillingContact))]
        public int? BillingContactID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DontMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual Contact? BillingContact { get; set; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuote>? SalesQuoteRequestMasters { get => salesQuoteRequestMasters; set => salesQuoteRequestMasters = value; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuote>? SalesQuoteRequestSubs { get => salesQuoteRequestSubs; set => salesQuoteRequestSubs = value; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuote>? SalesQuoteResponseMasters { get => salesQuoteResponseMasters; set => salesQuoteResponseMasters = value; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuote>? SalesQuoteResponseSubs { get => salesQuoteResponseSubs; set => salesQuoteResponseSubs = value; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrder>? SubSalesOrders { get => subSalesOrders; set => subSalesOrders = value; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesOrder>? SalesOrderMasters { get => salesOrderMasters; set => salesOrderMasters = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<PurchaseOrder>? PurchaseOrders { get => purchaseOrders; set => purchaseOrders = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesInvoice>? SalesInvoices { get => salesInvoices; set => salesInvoices = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesReturn>? SalesReturns { get => salesReturns; set => salesReturns = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SampleRequest>? SampleRequests { get => sampleRequests; set => sampleRequests = value; }
    }
}
