// <copyright file="IReviewModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IReviewModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for review model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface IReviewModel
    {
        #region Review Properties
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        decimal Value { get; set; }

        /// <summary>Gets or sets the comment.</summary>
        /// <value>The comment.</value>
        string? Comment { get; set; }

        /// <summary>Gets or sets a value indicating whether the approved.</summary>
        /// <value>True if approved, false if not.</value>
        bool Approved { get; set; }

        /// <summary>Gets or sets the approved date.</summary>
        /// <value>The approved date.</value>
        System.DateTime? ApprovedDate { get; set; }

        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        string? Title { get; set; }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        string? Location { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the submitted by user.</summary>
        /// <value>The identifier of the submitted by user.</value>
        int SubmittedByUserID { get; set; }

        /// <summary>Gets or sets the submitted by user key.</summary>
        /// <value>The submitted by user key.</value>
        string? SubmittedByUserKey { get; set; }

        /// <summary>Gets or sets the submitted by user.</summary>
        /// <value>The submitted by user.</value>
        IUserModel? SubmittedByUser { get; set; }

        /// <summary>Gets or sets the name of the submitted by user.</summary>
        /// <value>The name of the submitted by user.</value>
        string? SubmittedByUserUserName { get; set; }

        /// <summary>Gets or sets the identifier of the approved by user.</summary>
        /// <value>The identifier of the approved by user.</value>
        int? ApprovedByUserID { get; set; }

        /// <summary>Gets or sets the approved by user key.</summary>
        /// <value>The approved by user key.</value>
        string? ApprovedByUserKey { get; set; }

        /// <summary>Gets or sets the approved by user.</summary>
        /// <value>The approved by user.</value>
        IUserModel? ApprovedByUser { get; set; }

        /// <summary>Gets or sets the name of the approved by user.</summary>
        /// <value>The name of the approved by user.</value>
        string? ApprovedByUserUserName { get; set; }
        #endregion

        #region Object that was reviewed (Should only be one of these at at time)
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        int? CategoryID { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        ICategoryModel? Category { get; set; }

        /// <summary>Gets or sets the category key.</summary>
        /// <value>The category key.</value>
        string? CategoryKey { get; set; }

        /// <summary>Gets or sets the name of the category.</summary>
        /// <value>The name of the category.</value>
        string? CategoryName { get; set; }

        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int? ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        IManufacturerModel? Manufacturer { get; set; }

        /// <summary>Gets or sets the manufacturer key.</summary>
        /// <value>The manufacturer key.</value>
        string? ManufacturerKey { get; set; }

        /// <summary>Gets or sets the name of the manufacturer.</summary>
        /// <value>The name of the manufacturer.</value>
        string? ManufacturerName { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int? ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        IProductModel? Product { get; set; }

        /// <summary>Gets or sets the product key.</summary>
        /// <value>The product key.</value>
        string? ProductKey { get; set; }

        /// <summary>Gets or sets the name of the product.</summary>
        /// <value>The name of the product.</value>
        string? ProductName { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets the store.</summary>
        /// <value>The store.</value>
        IStoreModel? Store { get; set; }

        /// <summary>Gets or sets the store key.</summary>
        /// <value>The store key.</value>
        string? StoreKey { get; set; }

        /// <summary>Gets or sets the name of the store.</summary>
        /// <value>The name of the store.</value>
        string? StoreName { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserUserName { get; set; }

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        IVendorModel? Vendor { get; set; }

        /// <summary>Gets or sets the vendor key.</summary>
        /// <value>The vendor key.</value>
        string? VendorKey { get; set; }

        /// <summary>Gets or sets the name of the vendor.</summary>
        /// <value>The name of the vendor.</value>
        string? VendorName { get; set; }
        #endregion
    }
}
