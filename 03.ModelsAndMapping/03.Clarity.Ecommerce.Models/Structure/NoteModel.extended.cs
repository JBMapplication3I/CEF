// <copyright file="NoteModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the note model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the note.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="INoteModel"/>
    public partial class NoteModel
    {
        /// <inheritdoc/>
        public string? Note1 { get; set; }

        /// <inheritdoc/>
        public int? CreatedByUserID { get; set; }

        /// <inheritdoc/>
        public string? CreatedByUserKey { get; set; }

        /// <inheritdoc/>
        public string? CreatedByUserUsername { get; set; }

        /// <inheritdoc/>
        public string? CreatedByUserContactFirstName { get; set; }

        /// <inheritdoc/>
        public string? CreatedByUserContactLastName { get; set; }

        /// <inheritdoc cref="INoteModel.CreatedByUser"/>
        public UserModel? CreatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? INoteModel.CreatedByUser { get => CreatedByUser; set => CreatedByUser = (UserModel?)value; }

        /// <inheritdoc/>
        public int? UpdatedByUserID { get; set; }

        /// <inheritdoc/>
        public string? UpdatedByUserKey { get; set; }

        /// <inheritdoc cref="INoteModel.UpdatedByUser"/>
        public UserModel? UpdatedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? INoteModel.UpdatedByUser { get => UpdatedByUser; set => UpdatedByUser = (UserModel?)value; }

        /// <inheritdoc/>
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc/>
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        public int? FranchiseID { get; set; }

        /// <inheritdoc/>
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        public int? VendorID { get; set; }

        /// <inheritdoc/>
        public int? ManufacturerID { get; set; }

        /// <inheritdoc/>
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        public int? PurchaseOrderID { get; set; }

        /// <inheritdoc/>
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        public int? SalesInvoiceID { get; set; }

        /// <inheritdoc/>
        public int? SalesQuoteID { get; set; }

        /// <inheritdoc/>
        public int? SampleRequestID { get; set; }

        /// <inheritdoc/>
        public int? SalesReturnID { get; set; }

        /// <inheritdoc/>
        public int? CartID { get; set; }

        /// <inheritdoc/>
        public int? PurchaseOrderItemID { get; set; }

        /// <inheritdoc/>
        public int? SalesOrderItemID { get; set; }

        /// <inheritdoc/>
        public int? SalesInvoiceItemID { get; set; }

        /// <inheritdoc/>
        public int? SalesQuoteItemID { get; set; }

        /// <inheritdoc/>
        public int? SampleRequestItemID { get; set; }

        /// <inheritdoc/>
        public int? SalesReturnItemID { get; set; }

        /// <inheritdoc/>
        public int? CartItemID { get; set; }
    }
}
