// <copyright file="SalesQuote.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISalesQuote
        : ISalesCollectionBase<SalesQuote,
                SalesQuoteStatus,
                SalesQuoteType,
                SalesQuoteItem,
                AppliedSalesQuoteDiscount,
                SalesQuoteState,
                SalesQuoteFile,
                SalesQuoteContact,
                SalesQuoteEvent,
                SalesQuoteEventType>,
            IHaveNotesBase
    {
        #region SalesQuote Properties
        /// <summary>Gets or sets the requested ship date.</summary>
        /// <value>The requested ship date.</value>
        DateTime? RequestedShipDate { get; set; }

        /// <summary>Gets or sets the subtotal shipping modifier.</summary>
        /// <value>The subtotal shipping modifier.</value>
        decimal? SubtotalShippingModifier { get; set; }

        /// <summary>Gets or sets the subtotal shipping modifier mode.</summary>
        /// <value>The subtotal shipping modifier mode.</value>
        int? SubtotalShippingModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal taxes modifier.</summary>
        /// <value>The subtotal taxes modifier.</value>
        decimal? SubtotalTaxesModifier { get; set; }

        /// <summary>Gets or sets the subtotal taxes modifier mode.</summary>
        /// <value>The subtotal taxes modifier mode.</value>
        int? SubtotalTaxesModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal fees modifier.</summary>
        /// <value>The subtotal fees modifier.</value>
        decimal? SubtotalFeesModifier { get; set; }

        /// <summary>Gets or sets the subtotal fees modifier mode.</summary>
        /// <value>The subtotal fees modifier mode.</value>
        int? SubtotalFeesModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal handling modifier.</summary>
        /// <value>The subtotal handling modifier.</value>
        decimal? SubtotalHandlingModifier { get; set; }

        /// <summary>Gets or sets the subtotal handling modifier mode.</summary>
        /// <value>The subtotal handling modifier mode.</value>
        int? SubtotalHandlingModifierMode { get; set; }

        /// <summary>Gets or sets the subtotal discounts modifier.</summary>
        /// <value>The subtotal discounts modifier.</value>
        decimal? SubtotalDiscountsModifier { get; set; }

        /// <summary>Gets or sets the subtotal discounts modifier mode.</summary>
        /// <value>The subtotal discounts modifier mode.</value>
        int? SubtotalDiscountsModifierMode { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales group as master.</summary>
        /// <value>The identifier of the sales group as master.</value>
        int? SalesGroupAsRequestMasterID { get; set; }

        /// <summary>Gets or sets the sales group as master.</summary>
        /// <value>The sales group as master.</value>
        SalesGroup? SalesGroupAsRequestMaster { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as sub.</summary>
        /// <value>The identifier of the sales group as sub.</value>
        int? SalesGroupAsRequestSubID { get; set; }

        /// <summary>Gets or sets the sales group as sub.</summary>
        /// <value>The sales group as sub.</value>
        SalesGroup? SalesGroupAsRequestSub { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as response.</summary>
        /// <value>The identifier of the sales group as response.</value>
        int? SalesGroupAsResponseMasterID { get; set; }

        /// <summary>Gets or sets the sales group as response.</summary>
        /// <value>The sales group as response.</value>
        SalesGroup? SalesGroupAsResponseMaster { get; set; }

        /// <summary>Gets or sets the identifier of the sales group as response sub.</summary>
        /// <value>The identifier of the sales group as response sub.</value>
        int? SalesGroupAsResponseSubID { get; set; }

        /// <summary>Gets or sets the sales group as response sub.</summary>
        /// <value>The sales group as response sub.</value>
        SalesGroup? SalesGroupAsResponseSub { get; set; }

        /// <summary>Gets or sets the identifier of the response as vendor.</summary>
        /// <value>The identifier of the response as vendor.</value>
        int? ResponseAsVendorID { get; set; }

        /// <summary>Gets or sets the response as vendor.</summary>
        /// <value>The response as vendor.</value>
        Vendor? ResponseAsVendor { get; set; }

        /// <summary>Gets or sets the identifier of the response as store.</summary>
        /// <value>The identifier of the response as store.</value>
        int? ResponseAsStoreID { get; set; }

        /// <summary>Gets or sets the response as store.</summary>
        /// <value>The response as store.</value>
        Store? ResponseAsStore { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the associated sales orders.</summary>
        /// <value>The associated sales orders.</value>
        ICollection<SalesQuoteSalesOrder>? AssociatedSalesOrders { get; set; }

        /// <summary>Gets or sets the categories the sales quote belongs to.</summary>
        /// <value>The sales quote categories.</value>
        ICollection<SalesQuoteCategory>? SalesQuoteCategories { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Quoting", "SalesQuote")]
    public class SalesQuote
        : SalesCollectionBase<SalesQuote,
              SalesQuoteStatus,
              SalesQuoteType,
              SalesQuoteItem,
              AppliedSalesQuoteDiscount,
              SalesQuoteState,
              SalesQuoteFile,
              SalesQuoteContact,
              SalesQuoteEvent,
              SalesQuoteEventType>,
            ISalesQuote
    {
        private ICollection<Note>? notes;
        private ICollection<SalesQuoteSalesOrder>? associatedSalesOrders;
        private ICollection<SalesQuoteCategory>? salesQuoteCategories;

        public SalesQuote()
        {
            // HaveNotesBase
            notes = new HashSet<Note>();
            // SalesQuote
            associatedSalesOrders = new HashSet<SalesQuoteSalesOrder>();
            salesQuoteCategories = new HashSet<SalesQuoteCategory>();
        }

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region SalesQuote Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalShippingModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalShippingModifierMode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalTaxesModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalTaxesModifierMode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalFeesModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalFeesModifierMode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalHandlingModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalHandlingModifierMode { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? SubtotalDiscountsModifier { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SubtotalDiscountsModifierMode { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        [DefaultValue(null)]
        public DateTime? RequestedShipDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ResponseAsVendor)), DefaultValue(null)]
        public int? ResponseAsVendorID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Vendor? ResponseAsVendor { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ResponseAsStore)), DefaultValue(null)]
        public int? ResponseAsStoreID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Store? ResponseAsStore { get; set; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        ////[InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroupAsMaster))]
        [DefaultValue(null)]
        public int? SalesGroupAsRequestMasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroupAsRequestMaster { get; set; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        ////[InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroupAsResponse))]
        [DefaultValue(null)]
        public int? SalesGroupAsRequestSubID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroupAsRequestSub { get; set; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        ////[InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroupAsResponse))]
        [DefaultValue(null)]
        public int? SalesGroupAsResponseMasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroupAsResponseMaster { get; set; }

        /// <inheritdoc/>
        // NOTE: Relationship handled in ModelBuilder
        ////[InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroupAsResponse))]
        [DefaultValue(null)]
        public int? SalesGroupAsResponseSubID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroupAsResponseSub { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuoteSalesOrder>? AssociatedSalesOrders { get => associatedSalesOrders; set => associatedSalesOrders = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<SalesQuoteCategory>? SalesQuoteCategories { get => salesQuoteCategories; set => salesQuoteCategories = value; }
        #endregion
    }
}
