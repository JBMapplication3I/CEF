// <copyright file="SalesCollectionBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales collection base model class</summary>
// ReSharper disable InheritdocInvalidUsage
// ReSharper disable MissingXmlDoc
// ReSharper disable UnusedTypeParameter
// ReSharper disable UnusedMember.Global
#pragma warning disable SA1117 // Parameters should be on same line or separate lines
#pragma warning disable SA1600 // Elements should be documented
namespace Clarity.Ecommerce.MVC.Api.Models
{
    using System;
    using System.Collections.Generic;
    using ServiceStack;

    /// <summary>A data Model for the sales collection base.</summary>
    /// <typeparam name="TITypeModel">        Type of the ti type model.</typeparam>
    /// <typeparam name="TTypeModel">         Type of the type model.</typeparam>
    /// <typeparam name="TIStoredFileModel">  Type of the ti stored file model.</typeparam>
    /// <typeparam name="TStoredFileModel">   Type of the stored file model.</typeparam>
    /// <typeparam name="TIContactModel">     Type of the ti contact model.</typeparam>
    /// <typeparam name="TContactModel">      Type of the contact model.</typeparam>
    /// <typeparam name="TIEventModel">       Type of the ti event model.</typeparam>
    /// <typeparam name="TEventModel">        Type of the event model.</typeparam>
    /// <typeparam name="TIDiscountModel">    Type of the ti discount model.</typeparam>
    /// <typeparam name="TDiscountModel">     Type of the discount model.</typeparam>
    /// <typeparam name="TIItemDiscountModel">Type of the ti item discount model.</typeparam>
    /// <typeparam name="TItemDiscountModel"> Type of the item discount model.</typeparam>
    /// <seealso cref="BaseModel"/>
    public abstract class SalesCollectionBaseModel<TITypeModel,
            TTypeModel,
            TIStoredFileModel,
            TStoredFileModel,
            TIContactModel,
            TContactModel,
            TIEventModel,
            TEventModel,
            TIDiscountModel,
            TDiscountModel,
            TIItemDiscountModel,
            TItemDiscountModel>
        : SalesCollectionBaseModel
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [ApiMember(Name = nameof(Type), DataType = "TypeModel", ParameterType = "body", IsRequired = true,
            Description = "Model for Type of this object, optional")]
        public TTypeModel? Type { get; set; }

        #region Associated Objects
        /// <summary>Gets or sets the stored files.</summary>
        /// <value>The stored files.</value>
        [ApiMember(Name = nameof(StoredFiles), DataType = "List<TStoredFileModel>", ParameterType = "body", IsRequired = true,
            Description = "Stored Files for this sales collection, optional")]
        public List<TStoredFileModel>? StoredFiles { get; set; }

        /// <summary>Gets or sets the contacts.</summary>
        /// <value>The contacts.</value>
        [ApiMember(Name = nameof(Contacts), DataType = "List<TContactModel>", ParameterType = "body", IsRequired = true,
            Description = "Contacts for this sales collection, optional")]
        public List<TContactModel>? Contacts { get; set; }

        /// <summary>Gets or sets the discounts.</summary>
        /// <value>The discounts.</value>
        [ApiMember(Name = nameof(Discounts), DataType = "List<TDiscountModel>", ParameterType = "body", IsRequired = false)]
        public List<TDiscountModel>? Discounts { get; set; }

        /// <summary>Gets or sets the sales items.</summary>
        /// <value>The sales items.</value>
        [ApiMember(Name = nameof(SalesItems), DataType = "List<SalesItemBaseModel<TIItemDiscountModel, TItemDiscountModel>>", ParameterType = "body", IsRequired = false)]
        public List<SalesItemBaseModel<TIItemDiscountModel, TItemDiscountModel>>? SalesItems { get; set; }
        #endregion
    }

    public partial class SalesCollectionBaseModel : BaseModel
    {
        #region IHaveATypeBaseModel<ITypeModel>
        /// <inheritdoc cref="IHaveATypeBaseModel.TypeID"/>
        [ApiMember(Name = nameof(TypeID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the Type of this Account, required if no TypeModel present")]
        public int TypeID { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel.TypeKey"/>
        [ApiMember(Name = nameof(TypeKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Key for the Type of this Account, read-only")]
        public string? TypeKey { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel.TypeName"/>
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name for the Type of this Account, read-only")]
        public string? TypeName { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel.TypeDisplayName"/>
        [ApiMember(Name = nameof(TypeDisplayName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Display Name for the Type of this object, read-only")]
        public string? TypeDisplayName { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel.TypeTranslationKey"/>
        [ApiMember(Name = nameof(TypeTranslationKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Translation Key for the Type of this object, read-only")]
        public string? TypeTranslationKey { get; set; }

        /// <inheritdoc cref="IHaveATypeBaseModel.TypeSortOrder"/>
        [ApiMember(Name = nameof(TypeSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Sort Order for the Type of this object, read-only")]
        public int? TypeSortOrder { get; set; }
        #endregion

        #region IHaveAStatusBaseModel<IStatusModel>
        /// <summary>Gets or sets the identifier of the status.</summary>
        /// <value>The identifier of the status.</value>
        [ApiMember(Name = nameof(StatusID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the Status of this object, required if no StatusModel present")]
        public int StatusID { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        [ApiMember(Name = nameof(Status), DataType = "StatusModel", ParameterType = "body", IsRequired = true,
            Description = "Model for Status of this object, required if no StatusID present")]
        public StatusModel? Status { get; set; }

        /// <summary>Gets or sets the status key.</summary>
        /// <value>The status key.</value>
        [ApiMember(Name = nameof(StatusKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Key for the Status of this object, read-only")]
        public string? StatusKey { get; set; }

        /// <summary>Gets or sets the name of the status.</summary>
        /// <value>The name of the status.</value>
        [ApiMember(Name = nameof(StatusName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name for the Status of this object, read-only")]
        public string? StatusName { get; set; }

        /// <summary>Gets or sets the name of the status display.</summary>
        /// <value>The name of the status display.</value>
        [ApiMember(Name = nameof(StatusDisplayName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Display Name for the Status of this object, read-only")]
        public string? StatusDisplayName { get; set; }

        /// <summary>Gets or sets the status translation key.</summary>
        /// <value>The status translation key.</value>
        [ApiMember(Name = nameof(StatusTranslationKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Translation Key for the Status of this object, read-only")]
        public string? StatusTranslationKey { get; set; }

        /// <summary>Gets or sets the status sort order.</summary>
        /// <value>The status sort order.</value>
        [ApiMember(Name = nameof(StatusSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Sort Order for the Status of this object, read-only")]
        public int? StatusSortOrder { get; set; }
        #endregion

        #region IHaveAStateBaseModel<IStateModel>
        /// <summary>Gets or sets the identifier of the state.</summary>
        /// <value>The identifier of the state.</value>
        [ApiMember(Name = nameof(StateID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the State of this object, required if no StateModel present")]
        public int StateID { get; set; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        [ApiMember(Name = nameof(State), DataType = "StateModel", ParameterType = "body", IsRequired = true,
            Description = "Model for State of this object, required if no StateID present")]
        public StateModel? State { get; set; }

        /// <summary>Gets or sets the state key.</summary>
        /// <value>The state key.</value>
        [ApiMember(Name = nameof(StateKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Key for the State of this object, read-only")]
        public string? StateKey { get; set; }

        /// <summary>Gets or sets the name of the state.</summary>
        /// <value>The name of the state.</value>
        [ApiMember(Name = nameof(StateName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name for the State of this object, read-only")]
        public string? StateName { get; set; }

        /// <summary>Gets or sets the name of the state display.</summary>
        /// <value>The name of the state display.</value>
        [ApiMember(Name = nameof(StateDisplayName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Display Name for the State of this object, read-only")]
        public string? StateDisplayName { get; set; }

        /// <summary>Gets or sets the state translation key.</summary>
        /// <value>The state translation key.</value>
        [ApiMember(Name = nameof(StateTranslationKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Translation Key for the State of this object, read-only")]
        public string? StateTranslationKey { get; set; }

        /// <summary>Gets or sets the state sort order.</summary>
        /// <value>The state sort order.</value>
        [ApiMember(Name = nameof(StateSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Sort Order for the State of this object, read-only")]
        public int? StateSortOrder { get; set; }
        #endregion

        #region IAmFilterableByBrandModel Properties
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        public int? BrandID { get; set; }

        /// <summary>Gets or sets the brand.</summary>
        /// <value>The brand.</value>
        public BrandModel? Brand { get; set; }

        /// <summary>Gets or sets the brand key.</summary>
        /// <value>The brand key.</value>
        public string? BrandKey { get; set; }

        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        public string? BrandName { get; set; }
        #endregion

        #region IAmFilterableByBrandModel Properties
        /// <summary>Gets or sets the identifier of the franchise.</summary>
        /// <value>The identifier of the franchise.</value>
        public int? FranchiseID { get; set; }

        /// <summary>Gets or sets the franchise.</summary>
        /// <value>The franchise.</value>
        public FranchiseModel? Franchise { get; set; }

        /// <summary>Gets or sets the franchise key.</summary>
        /// <value>The franchise key.</value>
        public string? FranchiseKey { get; set; }

        /// <summary>Gets or sets the name of the franchise.</summary>
        /// <value>The name of the franchise.</value>
        public string? FranchiseName { get; set; }
        #endregion

        #region IAmFilterableByStoreModel Properties
        /// <summary>Gets or sets the identifier of the store.</summary>
        /// <value>The identifier of the store.</value>
        public int? StoreID { get; set; }

        /// <summary>Gets or sets the store.</summary>
        /// <value>The store.</value>
        public StoreModel? Store { get; set; }

        /// <summary>Gets or sets the store key.</summary>
        /// <value>The store key.</value>
        public string? StoreKey { get; set; }

        /// <summary>Gets or sets the name of the store.</summary>
        /// <value>The name of the store.</value>
        public string? StoreName { get; set; }

        /// <summary>Gets or sets URL of the store seo.</summary>
        /// <value>The store seo URL.</value>
        public string? StoreSeoUrl { get; set; }
        #endregion

        /// <summary>Gets or sets the identifier of the session.</summary>
        /// <value>The identifier of the session.</value>
        public Guid? SessionID { get; set; }

        /// <summary>Gets or sets the totals.</summary>
        /// <value>The totals.</value>
        public CartTotals? Totals { get; set; }

        /// <summary>Gets or sets the original date.</summary>
        /// <value>The original date.</value>
        public DateTime? OriginalDate { get; set; }

        /// <summary>Gets or sets the due date.</summary>
        /// <value>The due date.</value>
        public DateTime? DueDate { get; set; }

        /// <summary>Gets or sets the shipping same as billing.</summary>
        /// <value>The shipping same as billing.</value>
        public bool? ShippingSameAsBilling { get; set; }

        /// <summary>Gets or sets the balance due.</summary>
        /// <value>The balance due.</value>
        public decimal? BalanceDue { get; set; }

        /// <summary>Gets or sets the item quantity.</summary>
        /// <value>The item quantity.</value>
        public decimal ItemQuantity { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the shipping detail.</summary>
        /// <value>The shipping detail.</value>
        public SalesOrderShippingModel? ShippingDetail { get; set; }

        /// <summary>Gets or sets the identifier of the shipment.</summary>
        /// <value>The identifier of the shipment.</value>
        public int? ShipmentID { get; set; }

        /// <summary>Gets or sets the shipment key.</summary>
        /// <value>The shipment key.</value>
        public string? ShipmentKey { get; set; }

        /// <summary>Gets or sets the name of the shipment.</summary>
        /// <value>The name of the shipment.</value>
        public string? ShipmentName { get; set; }

        /// <summary>Gets or sets the shipment.</summary>
        /// <value>The shipment.</value>
        public ShipmentModel? Shipment { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        public int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public UserModel? User { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        public string? UserKey { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string? UserUserName { get; set; }

        /// <summary>Gets or sets the user contact email.</summary>
        /// <value>The user contact email.</value>
        public string? UserContactEmail { get; set; }

        /// <summary>Gets or sets the name of the user contact first.</summary>
        /// <value>The name of the user contact first.</value>
        public string? UserContactFirstName { get; set; }

        /// <summary>Gets or sets the name of the user contact last.</summary>
        /// <value>The name of the user contact last.</value>
        public string? UserContactLastName { get; set; }

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        public int? AccountID { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        public AccountModel? Account { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        public string? AccountKey { get; set; }

        /// <summary>Gets or sets the name of the account.</summary>
        /// <value>The name of the account.</value>
        public string? AccountName { get; set; }

        /// <summary>Gets or sets the identifier of the billing contact.</summary>
        /// <value>The identifier of the billing contact.</value>
        public int? BillingContactID { get; set; }

        /// <summary>Gets or sets the billing contact key.</summary>
        /// <value>The billing contact key.</value>
        public string? BillingContactKey { get; set; }

        /// <summary>Gets or sets the billing contact.</summary>
        /// <value>The billing contact.</value>
        public ContactModel? BillingContact { get; set; }

        /// <summary>Gets or sets the identifier of the shipping contact.</summary>
        /// <value>The identifier of the shipping contact.</value>
        public int? ShippingContactID { get; set; }

        /// <summary>Gets or sets the shipping contact key.</summary>
        /// <value>The shipping contact key.</value>
        public string? ShippingContactKey { get; set; }

        /// <summary>Gets or sets the shipping contact.</summary>
        /// <value>The shipping contact.</value>
        public ContactModel? ShippingContact { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the rate quotes.</summary>
        /// <value>The rate quotes.</value>
        [ApiMember(Name = nameof(RateQuotes), DataType = "List<RateQuoteModel>", ParameterType = "body", IsRequired = false)]
        public List<RateQuoteModel>? RateQuotes { get; set; }
        #endregion
    }
}
