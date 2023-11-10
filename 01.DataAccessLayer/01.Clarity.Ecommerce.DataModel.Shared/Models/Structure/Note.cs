// <copyright file="Note.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the note class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface INote : IHaveATypeBase<NoteType>
    {
        #region Note Properties
        /// <summary>Gets or sets the note.</summary>
        /// <remarks>The 1 is required as you can't have a property with the same name as the class.</remarks>
        /// <value>The note.</value>
        string? Note1 { get; set; }
        #endregion

        #region Related Objects
        #region User
        /// <summary>Gets or sets the identifier of the created by user.</summary>
        /// <value>The identifier of the created by user.</value>
        int? CreatedByUserID { get; set; }

        /// <summary>Gets or sets the created by user.</summary>
        /// <value>The created by user.</value>
        User? CreatedByUser { get; set; }

        /// <summary>Gets or sets the identifier of the updated by user.</summary>
        /// <value>The identifier of the updated by user.</value>
        int? UpdatedByUserID { get; set; }

        /// <summary>Gets or sets the updated by user.</summary>
        /// <value>The updated by user.</value>
        User? UpdatedByUser { get; set; }
        #endregion

        #region The Object the note belongs to (only 1 should be set at a time)
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int? ManufacturerID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        int? BrandID { get; set; }

        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        int? FranchiseID { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the sales group.</summary>
        /// <value>The identifier of the sales group.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the identifier of the purchase order.</summary>
        /// <value>The identifier of the purchase order.</value>
        int? PurchaseOrderID { get; set; }

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the identifier of the sales return.</summary>
        /// <value>The identifier of the sales return.</value>
        int? SalesReturnID { get; set; }

        /// <summary>Gets or sets the identifier of the sales invoice.</summary>
        /// <value>The identifier of the sales invoice.</value>
        int? SalesInvoiceID { get; set; }

        /// <summary>Gets or sets the identifier of the sales quote.</summary>
        /// <value>The identifier of the sales quote.</value>
        int? SalesQuoteID { get; set; }

        /// <summary>Gets or sets the identifier of the sample request.</summary>
        /// <value>The identifier of the sample request.</value>
        int? SampleRequestID { get; set; }

        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        int? CartID { get; set; }

        /// <summary>Gets or sets the identifier of the purchase order item.</summary>
        /// <value>The identifier of the purchase order item.</value>
        int? PurchaseOrderItemID { get; set; }

        /// <summary>Gets or sets the identifier of the sales order item.</summary>
        /// <value>The identifier of the sales order item.</value>
        int? SalesOrderItemID { get; set; }

        /// <summary>Gets or sets the identifier of the sales return item.</summary>
        /// <value>The identifier of the sales return item.</value>
        int? SalesReturnItemID { get; set; }

        /// <summary>Gets or sets the identifier of the sales invoice item.</summary>
        /// <value>The identifier of the sales invoice item.</value>
        int? SalesInvoiceItemID { get; set; }

        /// <summary>Gets or sets the identifier of the sales quote item.</summary>
        /// <value>The identifier of the sales quote item.</value>
        int? SalesQuoteItemID { get; set; }

        /// <summary>Gets or sets the identifier of the sample request item.</summary>
        /// <value>The identifier of the sample request item.</value>
        int? SampleRequestItemID { get; set; }

        /// <summary>Gets or sets the identifier of the cart item.</summary>
        /// <value>The identifier of the cart item.</value>
        int? CartItemID { get; set; }
        #endregion
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("System", "Note")]
    public class Note : Base, INote
    {
        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual NoteType? Type { get; set; }
        #endregion

        #region Note Properties
        [Required, Column("Note"), StringLength(8000), StringIsUnicode(false)]
        public string? Note1 { get; set; }
        #endregion

        #region Related Objects
        #region User
        /// <inheritdoc/>
        // Foreign Key Handled in modelBuilder for cascading
        [DefaultValue(null)]
        public int? CreatedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? CreatedByUser { get; set; }

        /// <inheritdoc/>
        // Foreign Key Handled in modelBuilder for cascading
        [DefaultValue(null)]
        public int? UpdatedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? UpdatedByUser { get; set; }
        #endregion

        #region The object this Note belongs to (only 1 should be set at time)
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Account))]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User))]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Vendor))]
        public int? VendorID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Manufacturer))]
        public int? ManufacturerID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand))]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Franchise))]
        public int? FranchiseID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store))]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroup))]
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PurchaseOrder))]
        public int? PurchaseOrderID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesOrder))]
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesInvoice))]
        public int? SalesInvoiceID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesQuote))]
        public int? SalesQuoteID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SampleRequest))]
        public int? SampleRequestID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesReturn))]
        public int? SalesReturnID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Cart))]
        public int? CartID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PurchaseOrderItem))]
        public int? PurchaseOrderItemID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesOrderItem))]
        public int? SalesOrderItemID { get; set; }

        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesInvoiceItem))]
        public int? SalesInvoiceItemID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesQuoteItem))]
        public int? SalesQuoteItemID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SampleRequestItem))]
        public int? SampleRequestItemID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesReturnItem))]
        public int? SalesReturnItemID { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(CartItem))]
        public int? CartItemID { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Account? Account { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Vendor? Vendor { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Manufacturer? Manufacturer { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Franchise? Franchise { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroup { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual PurchaseOrder? PurchaseOrder { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesOrder? SalesOrder { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesInvoice? SalesInvoice { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesQuote? SalesQuote { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SampleRequest? SampleRequest { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesReturn? SalesReturn { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Cart? Cart { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual PurchaseOrderItem? PurchaseOrderItem { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesOrderItem? SalesOrderItem { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesInvoiceItem? SalesInvoiceItem { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesQuoteItem? SalesQuoteItem { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SampleRequestItem? SampleRequestItem { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SalesReturnItem? SalesReturnItem { get; set; }

        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual CartItem? CartItem { get; set; }
        #endregion
        #endregion
    }
}
