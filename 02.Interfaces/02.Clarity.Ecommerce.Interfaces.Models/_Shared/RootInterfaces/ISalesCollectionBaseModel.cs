// <copyright file="ISalesCollectionBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesCollectionBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for sales collection base model.</summary>
    /// <typeparam name="TITypeModel">        Type of the type model's interface.</typeparam>
    /// <typeparam name="TIStoredFileModel">  Type of the stored file model's interface.</typeparam>
    /// <typeparam name="TIContactModel">     Type of the contact model's interface.</typeparam>
    /// <typeparam name="TISalesEventModel">  Type of the sales event model's interface.</typeparam>
    /// <typeparam name="TIDiscountModel">    Type of the discount model's interface.</typeparam>
    /// <typeparam name="TIItemDiscountModel">Type of the item discount model's interface.</typeparam>
    /// <seealso cref="ISalesCollectionBaseModel"/>
    /// <seealso cref="IHaveATypeBaseModel{TITypeModel}"/>
    /// <seealso cref="IHaveStoredFilesBaseModel{TIStoredFileModel}"/>
    public interface ISalesCollectionBaseModel<TITypeModel, TIStoredFileModel, TIContactModel, TISalesEventModel, TIDiscountModel, TIItemDiscountModel>
        : ISalesCollectionBaseModel,
            IHaveATypeBaseModel<TITypeModel>,
            IHaveStoredFilesBaseModel<TIStoredFileModel>,
            IHaveAppliedDiscountsBaseModel<TIDiscountModel>,
            IHaveContactsBaseModel<TIContactModel>,
            IHaveSalesEventsBaseModel<TISalesEventModel>
        where TITypeModel : ITypableBaseModel
        where TIStoredFileModel : IAmAStoredFileRelationshipTableModel
        where TIDiscountModel : IAppliedDiscountBaseModel
        where TIItemDiscountModel : IAppliedDiscountBaseModel
        where TIContactModel : IAmAContactRelationshipTableModel
        where TISalesEventModel : ISalesEventBaseModel
    {
        /// <summary>Gets or sets the sales items.</summary>
        /// <value>The sales items.</value>
        List<ISalesItemBaseModel<TIItemDiscountModel>>? SalesItems { get; set; }
    }

    /// <summary>Interface for sales collection base model.</summary>
    /// <seealso cref="IHaveAStatusBaseModel{IStatusModel}"/>
    /// <seealso cref="IHaveAStateBaseModel{IStateModel}"/>
    /// <seealso cref="IAmFilterableByNullableAccountModel"/>
    /// <seealso cref="IAmFilterableByNullableBrandModel"/>
    /// <seealso cref="IAmFilterableByNullableStoreModel"/>
    /// <seealso cref="IAmFilterableByNullableUserModel"/>
    public interface ISalesCollectionBaseModel
        : IHaveAStatusBaseModel<IStatusModel>,
            IHaveAStateBaseModel<IStateModel>,
            IAmFilterableByNullableAccountModel,
            IAmFilterableByNullableBrandModel,
            IAmFilterableByNullableFranchiseModel,
            IAmFilterableByNullableStoreModel,
            IAmFilterableByNullableUserModel
    {
        #region Sales Collection Base Properties
        /// <summary>Gets or sets the totals.</summary>
        /// <value>The totals.</value>
        ICartTotals Totals { get; set; }

        /// <summary>Gets or sets the original date.</summary>
        /// <value>The original date.</value>
        DateTime? OriginalDate { get; set; }

        /// <summary>Gets or sets the due date.</summary>
        /// <value>The due date.</value>
        DateTime? DueDate { get; set; }

        /// <summary>Gets or sets the balance due.</summary>
        /// <value>The balance due.</value>
        decimal? BalanceDue { get; set; }

        /// <summary>Gets or sets the shipping same as billing.</summary>
        /// <value>The shipping same as billing.</value>
        bool? ShippingSameAsBilling { get; set; }

        /// <summary>Gets or sets the item quantity.</summary>
        /// <value>The item quantity.</value>
        decimal ItemQuantity { get; set; }

        /// <summary>Gets or sets the identifier of the session.</summary>
        /// <value>The identifier of the session.</value>
        Guid? SessionID { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the shipping detail.</summary>
        /// <value>The shipping detail.</value>
        ISalesOrderShippingModel? ShippingDetail { get; set; }

        /// <summary>Gets or sets the identifier of the shipment.</summary>
        /// <value>The identifier of the shipment.</value>
        int? ShipmentID { get; set; }

        /// <summary>Gets or sets the shipment key.</summary>
        /// <value>The shipment key.</value>
        string? ShipmentKey { get; set; }

        /// <summary>Gets or sets the shipment.</summary>
        /// <value>The shipment.</value>
        IShipmentModel? Shipment { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserUserName { get; set; }

        /// <summary>Gets or sets the user contact email.</summary>
        /// <value>The user contact email.</value>
        string? UserContactEmail { get; set; }

        /// <summary>Gets or sets the name of the user contact first.</summary>
        /// <value>The name of the user contact first.</value>
        string? UserContactFirstName { get; set; }

        /// <summary>Gets or sets the name of the user contact last.</summary>
        /// <value>The name of the user contact last.</value>
        string? UserContactLastName { get; set; }

        /// <summary>Gets or sets the identifier of the billing contact.</summary>
        /// <value>The identifier of the billing contact.</value>
        int? BillingContactID { get; set; }

        /// <summary>Gets or sets the billing contact key.</summary>
        /// <value>The billing contact key.</value>
        string? BillingContactKey { get; set; }

        /// <summary>Gets or sets the billing contact.</summary>
        /// <value>The billing contact.</value>
        IContactModel? BillingContact { get; set; }

        /// <summary>Gets or sets the identifier of the shipping contact.</summary>
        /// <value>The identifier of the shipping contact.</value>
        int? ShippingContactID { get; set; }

        /// <summary>Gets or sets the shipping contact key.</summary>
        /// <value>The shipping contact key.</value>
        string? ShippingContactKey { get; set; }

        /// <summary>Gets or sets the shipping contact.</summary>
        /// <value>The shipping contact.</value>
        IContactModel? ShippingContact { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the rate quotes.</summary>
        /// <value>The rate quotes.</value>
        List<IRateQuoteModel>? RateQuotes { get; set; }
        #endregion
    }
}
