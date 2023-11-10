// <copyright file="Review.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the review class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IReview : INameableBase, IHaveATypeBase<ReviewType>
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
        DateTime? ApprovedDate { get; set; }

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

        /// <summary>Gets or sets the submitted by user.</summary>
        /// <value>The submitted by user.</value>
        User? SubmittedByUser { get; set; }

        /// <summary>Gets or sets the identifier of the approved by user.</summary>
        /// <value>The identifier of the approved by user.</value>
        int? ApprovedByUserID { get; set; }

        /// <summary>Gets or sets the approved by user.</summary>
        /// <value>The approved by user.</value>
        User? ApprovedByUser { get; set; }
        #endregion

        #region Object that was reviewed (Should only be one of these at at time)
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        int? CategoryID { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        Category? Category { get; set; }

        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int? ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        Manufacturer? Manufacturer { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int? ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        Product? Product { get; set; }

        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        int? StoreID { get; set; }

        /// <summary>Gets or sets the store.</summary>
        /// <value>The store.</value>
        Store? Store { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }

        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        Vendor? Vendor { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Reviews", "Review")]
    public class Review : NameableBase, IReview
    {
        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual ReviewType? Type { get; set; }
        #endregion

        #region Review Properties
        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public decimal Value { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Comment { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool Approved { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ApprovedDate { get; set; }

        /// <inheritdoc/>
        [StringLength(255), StringIsUnicode(false), DefaultValue(null)]
        public string? Title { get; set; }

        /// <inheritdoc/>
        [StringLength(255), StringIsUnicode(false), DefaultValue(null)]
        public string? Location { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SubmittedByUser)), DefaultValue(null)]
        public int SubmittedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? SubmittedByUser { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ApprovedByUser)), DefaultValue(null)]
        public int? ApprovedByUserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? ApprovedByUser { get; set; }
        #endregion

        #region Object that was reviewed (Should only be one of these at at time)
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Category)), DefaultValue(null)]
        public int? CategoryID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Category? Category { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Manufacturer)), DefaultValue(null)]
        public int? ManufacturerID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Manufacturer? Manufacturer { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Product)), DefaultValue(null)]
        public int? ProductID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Product? Product { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(null)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Vendor)), DefaultValue(null)]
        public int? VendorID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Vendor? Vendor { get; set; }
        #endregion
    }
}
