// <copyright file="ISalesCollectionBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesCollectionBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for sales collection base.</summary>
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
    public interface ISalesCollectionBase<TSalesCollection, TStatus, TType, TSalesItem, TDiscount, TState, TStoredFile, TContact, TSalesEvent, TSalesEventType>
        : IHaveATypeBase<TType>,
            IHaveAStatusBase<TStatus>,
            IHaveAStateBase<TState>,
            ISalesCollectionBase,
            IHaveStoredFilesBase<TSalesCollection, TStoredFile>,
            IHaveAppliedDiscountsBase<TSalesCollection, TDiscount>,
            IHaveContactsBase<TSalesCollection, TContact>,
            IHaveSalesEventsBase<TSalesCollection, TSalesEvent, TSalesEventType>
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
        /// <summary>Gets or sets the sales items.</summary>
        /// <value>The sales items.</value>
        ICollection<TSalesItem>? SalesItems { get; set; }
    }

    /// <summary>Interface for sales collection base.</summary>
    public interface ISalesCollectionBase
        : IHaveATypeBase,
            IHaveAStatusBase,
            IHaveAStateBase,
            IAmFilterableByNullableAccount,
            IAmFilterableByNullableBrand,
            IAmFilterableByNullableFranchise,
            IAmFilterableByNullableStore,
            IAmFilterableByNullableUser
    {
        #region ISalesCollectionBase Properties
        /// <summary>Gets or sets the due date.</summary>
        /// <value>The due date.</value>
        DateTime? DueDate { get; set; }

        /// <summary>Gets or sets the subtotal items.</summary>
        /// <value>The subtotal items.</value>
        decimal SubtotalItems { get; set; }

        /// <summary>Gets or sets the subtotal shipping.</summary>
        /// <value>The subtotal shipping.</value>
        decimal SubtotalShipping { get; set; }

        /// <summary>Gets or sets the subtotal taxes.</summary>
        /// <value>The subtotal taxes.</value>
        decimal SubtotalTaxes { get; set; }

        /// <summary>Gets or sets the subtotal fees.</summary>
        /// <value>The subtotal fees.</value>
        decimal SubtotalFees { get; set; }

        /// <summary>Gets or sets the subtotal handling.</summary>
        /// <value>The subtotal handling.</value>
        decimal SubtotalHandling { get; set; }

        /// <summary>Gets or sets the subtotal discounts.</summary>
        /// <value>The subtotal discounts.</value>
        decimal SubtotalDiscounts { get; set; }

        /// <summary>Gets or sets the number of. </summary>
        /// <value>The total.</value>
        decimal Total { get; set; }

        /// <summary>Gets or sets the shipping same as billing.</summary>
        /// <value>The shipping same as billing.</value>
        bool? ShippingSameAsBilling { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the billing contact.</summary>
        /// <value>The identifier of the billing contact.</value>
        int? BillingContactID { get; set; }

        /// <summary>Gets or sets the billing contact.</summary>
        /// <value>The billing contact.</value>
        Contact? BillingContact { get; set; }

        /// <summary>Gets or sets the identifier of the shipping contact.</summary>
        /// <value>The identifier of the shipping contact.</value>
        int? ShippingContactID { get; set; }

        /// <summary>Gets or sets the shipping contact.</summary>
        /// <value>The shipping contact.</value>
        Contact? ShippingContact { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the rate quotes.</summary>
        /// <value>The rate quotes.</value>
        ICollection<RateQuote>? RateQuotes { get; set; }
        #endregion
    }
}
