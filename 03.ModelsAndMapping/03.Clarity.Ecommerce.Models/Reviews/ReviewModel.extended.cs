// <copyright file="ReviewModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the review model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the review.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IReviewModel"/>
    public partial class ReviewModel
    {
        /// <inheritdoc/>
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        public decimal Value { get; set; }

        /// <inheritdoc/>
        public string? Comment { get; set; }

        /// <inheritdoc/>
        public bool Approved { get; set; }

        /// <inheritdoc/>
        public DateTime? ApprovedDate { get; set; }

        /// <inheritdoc/>
        public string? Title { get; set; }

        /// <inheritdoc/>
        public string? Location { get; set; }

        /// <inheritdoc/>
        public int SubmittedByUserID { get; set; }

        /// <inheritdoc/>
        public string? SubmittedByUserKey { get; set; }

        /// <inheritdoc cref="IReviewModel.SubmittedByUser"/>
        public UserModel? SubmittedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IReviewModel.SubmittedByUser { get => SubmittedByUser; set => SubmittedByUser = (UserModel?)value; }

        /// <inheritdoc/>
        public string? SubmittedByUserUserName { get; set; }

        /// <inheritdoc/>
        public int? ApprovedByUserID { get; set; }

        /// <inheritdoc/>
        public string? ApprovedByUserKey { get; set; }

        /// <inheritdoc cref="IReviewModel.ApprovedByUser"/>
        public UserModel? ApprovedByUser { get; set; }

        /// <inheritdoc/>
        IUserModel? IReviewModel.ApprovedByUser { get => ApprovedByUser; set => ApprovedByUser = (UserModel?)value; }

        /// <inheritdoc/>
        public string? ApprovedByUserUserName { get; set; }

        /// <inheritdoc/>
        public int? CategoryID { get; set; }

        /// <inheritdoc cref="IReviewModel.Category"/>
        public CategoryModel? Category { get; set; }

        /// <inheritdoc/>
        ICategoryModel? IReviewModel.Category { get => Category; set => Category = (CategoryModel?)value; }

        /// <inheritdoc/>
        public string? CategoryKey { get; set; }

        /// <inheritdoc/>
        public string? CategoryName { get; set; }

        /// <inheritdoc/>
        public int? ManufacturerID { get; set; }

        /// <inheritdoc cref="IReviewModel.Manufacturer"/>
        public ManufacturerModel? Manufacturer { get; set; }

        /// <inheritdoc/>
        IManufacturerModel? IReviewModel.Manufacturer { get => Manufacturer; set => Manufacturer = (ManufacturerModel?)value; }

        /// <inheritdoc/>
        public string? ManufacturerKey { get; set; }

        /// <inheritdoc/>
        public string? ManufacturerName { get; set; }

        /// <inheritdoc/>
        public int? ProductID { get; set; }

        /// <inheritdoc cref="IReviewModel.Product"/>
        public ProductModel? Product { get; set; }

        /// <inheritdoc/>
        IProductModel? IReviewModel.Product { get => Product; set => Product = (ProductModel?)value; }

        /// <inheritdoc/>
        public string? ProductKey { get; set; }

        /// <inheritdoc/>
        public string? ProductName { get; set; }

        /// <inheritdoc/>
        public int? StoreID { get; set; }

        /// <inheritdoc cref="IReviewModel.Store"/>
        public StoreModel? Store { get; set; }

        /// <inheritdoc/>
        IStoreModel? IReviewModel.Store { get => Store; set => Store = (StoreModel?)value; }

        /// <inheritdoc/>
        public string? StoreKey { get; set; }

        /// <inheritdoc/>
        public string? StoreName { get; set; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc cref="IReviewModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IReviewModel.User { get => User; set => User = (UserModel?)value; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        public string? UserUserName { get; set; }

        /// <inheritdoc/>
        public int? VendorID { get; set; }

        /// <inheritdoc cref="IReviewModel.Vendor"/>
        public VendorModel? Vendor { get; set; }

        /// <inheritdoc/>
        IVendorModel? IReviewModel.Vendor { get => Vendor; set => Vendor = (VendorModel?)value; }

        /// <inheritdoc/>
        public string? VendorKey { get; set; }

        /// <inheritdoc/>
        public string? VendorName { get; set; }
    }
}
