// <copyright file="SalesCollectionBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales collection base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    /// <summary>A sales collection base.</summary>
    /// <typeparam name="TSalesCollection">Type of this.</typeparam>
    /// <typeparam name="TStatus">         Type of the status.</typeparam>
    /// <typeparam name="TType">           Type of the type.</typeparam>
    /// <typeparam name="TSalesItem">      Type of the sales item.</typeparam>
    /// <typeparam name="TDiscount">       Type of the discount.</typeparam>
    /// <typeparam name="TState">          Type of the state.</typeparam>
    /// <typeparam name="TStoredFile">     Type of the stored file.</typeparam>
    /// <typeparam name="TContact">        Type of the contact.</typeparam>
    /// <typeparam name="TSalesEvent">     Type of the sales event.</typeparam>
    /// <typeparam name="TSalesEventType"> Type of the sales event type.</typeparam>
    /// <seealso cref="Base"/>
    /// <seealso cref="INameableBase"/>
    public abstract class SalesCollectionBase<TSalesCollection, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
        : Base, ISalesCollectionBase<TSalesCollection, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
        where TSalesCollection
            : IHaveAppliedDiscountsBase<TSalesCollection, TDiscount>,
                IHaveContactsBase<TSalesCollection, TContact>,
                IHaveSalesEventsBase<TSalesCollection, TSalesEvent, TSalesEventType>
        where TStatus : IStatusableBase
        where TType : ITypableBase
        where TSalesItem : IBase
        where TDiscount : IAppliedDiscountBase<TSalesCollection, TDiscount>
        where TState : IStateableBase
        where TStoredFile : IAmAStoredFileRelationshipTable<TSalesCollection>
        where TContact : IAmAContactRelationshipTable<TSalesCollection, TContact>
        where TSalesEvent : ISalesEventBase<TSalesCollection, TSalesEventType>
        where TSalesEventType : ITypableBase
    {
        private ICollection<TSalesItem>? salesItems;
        private ICollection<TDiscount>? discounts;
        private ICollection<TStoredFile>? storedFiles;
        private ICollection<TContact>? contacts;
        private ICollection<TSalesEvent>? salesEvents;
        private ICollection<RateQuote>? rateQuotes;

        /// <summary>Initializes a new instance of the <see cref="SalesCollectionBase{TSalesCollection, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType}"/> class.</summary>
        protected SalesCollectionBase()
        {
            // SalesCollectionBase
            salesItems = new HashSet<TSalesItem>();
            discounts = new HashSet<TDiscount>();
            contacts = new HashSet<TContact>();
            storedFiles = new HashSet<TStoredFile>();
            salesEvents = new HashSet<TSalesEvent>();
            rateQuotes = new HashSet<RateQuote>();
        }

        #region IAmFilterableByNullableAccount Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Account)), DefaultValue(null)]
        public virtual int? AccountID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Account? Account { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public virtual int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        #region IAmFilterableByNullableFranchise Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Franchise)), DefaultValue(null)]
        public virtual int? FranchiseID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Franchise? Franchise { get; set; }
        #endregion

        #region IAmFilterableByNullableStore Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store)), DefaultValue(null)]
        public virtual int? StoreID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableUser Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), Index, DefaultValue(null)]
        public virtual int? UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }
        #endregion

        #region IHaveAStatusBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual TStatus? Status { get; set; }
        #endregion

        #region IHaveAStateBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(State)), DefaultValue(0)]
        public int StateID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual TState? State { get; set; }
        #endregion

        #region IHaveATypeBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), Index, DefaultValue(0)]
        public virtual int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null), JsonIgnore]
        public virtual TType? Type { get; set; }
        #endregion

        #region SalesCollectionBase Properties
        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? DueDate { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal SubtotalItems { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal SubtotalShipping { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal SubtotalTaxes { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal SubtotalFees { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal SubtotalHandling { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal SubtotalDiscounts { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Total { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? ShippingSameAsBilling { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        // Foreign Key handled in modelBuilder for Cascading
        [DefaultValue(null)]
        public int? BillingContactID { get; set; }

        [DefaultValue(null), JsonIgnore]
        public virtual Contact? BillingContact { get; set; }

        /// <inheritdoc/>
        // Foreign Key handled in modelBuilder for Cascading
        [DefaultValue(null)]
        public int? ShippingContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Contact? ShippingContact { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<TSalesItem>? SalesItems { get => salesItems; set => salesItems = value; }

        /// <inheritdoc/>
        [DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<TDiscount>? Discounts { get => discounts; set => discounts = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<TStoredFile>? StoredFiles { get => storedFiles; set => storedFiles = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<TContact>? Contacts { get => contacts; set => contacts = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<TSalesEvent>? SalesEvents { get => salesEvents; set => salesEvents = value; }

        /// <inheritdoc/>
        [DefaultValue(null), ForceMapOutWithListing, JsonIgnore]
        public virtual ICollection<RateQuote>? RateQuotes { get => rateQuotes; set => rateQuotes = value; }
        #endregion
    }
}
