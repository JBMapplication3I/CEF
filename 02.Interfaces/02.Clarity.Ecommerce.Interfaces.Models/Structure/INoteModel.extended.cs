// <copyright file="INoteModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the INoteModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for note model.</summary>
    public partial interface INoteModel
    {
        #region Note Properties
        /// <summary>Gets or sets the note 1.</summary>
        /// <value>The note 1.</value>
        string? Note1 { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the created by user.</summary>
        /// <value>The identifier of the created by user.</value>
        int? CreatedByUserID { get; set; }

        /// <summary>Gets or sets the created by user key.</summary>
        /// <value>The created by user key.</value>
        string? CreatedByUserKey { get; set; }

        /// <summary>Gets or sets the created by user username.</summary>
        /// <value>The created by user username.</value>
        string? CreatedByUserUsername { get; set; }

        /// <summary>Gets or sets the name of the created by user contact first.</summary>
        /// <value>The name of the created by user contact first.</value>
        string? CreatedByUserContactFirstName { get; set; }

        /// <summary>Gets or sets the name of the created by user contact last.</summary>
        /// <value>The name of the created by user contact last.</value>
        string? CreatedByUserContactLastName { get; set; }

        /// <summary>Gets or sets the created by user.</summary>
        /// <value>The created by user.</value>
        IUserModel? CreatedByUser { get; set; }

        /// <summary>Gets or sets the identifier of the updated by user.</summary>
        /// <value>The identifier of the updated by user.</value>
        int? UpdatedByUserID { get; set; }

        /// <summary>Gets or sets the updated by user key.</summary>
        /// <value>The updated by user key.</value>
        string? UpdatedByUserKey { get; set; }

        /// <summary>Gets or sets the updated by user.</summary>
        /// <value>The updated by user.</value>
        IUserModel? UpdatedByUser { get; set; }
        #endregion

        #region The various objects that have Note Lists
        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        int? BrandID { get; set; }

        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        int? FranchiseID { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int? ManufacturerID { get; set; }

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
    }
}
