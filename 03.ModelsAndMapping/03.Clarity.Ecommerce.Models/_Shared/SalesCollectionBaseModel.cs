// <copyright file="SalesCollectionBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales collection base model class</summary>
// ReSharper disable StyleCop.SA1402
#pragma warning disable SA1402 // File may only contain a single type
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the sales collection base.</summary>
    /// <typeparam name="TITypeModel">        Type of the type model's interface.</typeparam>
    /// <typeparam name="TTypeModel">         Type of the type model.</typeparam>
    /// <typeparam name="TIStoredFileModel">  Type of the stored file model's interface.</typeparam>
    /// <typeparam name="TStoredFileModel">   Type of the stored file model.</typeparam>
    /// <typeparam name="TIContactModel">     Type of the contact model's interface.</typeparam>
    /// <typeparam name="TContactModel">      Type of the contact model.</typeparam>
    /// <typeparam name="TISalesEventModel">  Type of the sales event model's interface.</typeparam>
    /// <typeparam name="TSalesEventModel">   Type of the sales event model.</typeparam>
    /// <typeparam name="TIDiscountModel">    Type of the discount model's interface.</typeparam>
    /// <typeparam name="TDiscountModel">     Type of the discount model.</typeparam>
    /// <typeparam name="TIItemDiscountModel">Type of the item discount model's interface.</typeparam>
    /// <typeparam name="TItemDiscountModel"> Type of the item discount model.</typeparam>
    /// <seealso cref="SalesCollectionBaseModel"/>
    /// <seealso cref="ISalesCollectionBaseModel{TITypeModel, TIStoredFileModel, TIContactModel, TISalesEventModel, TIDiscountModel, TIItemDiscountModel}"/>
    public abstract class SalesCollectionBaseModel<TITypeModel, TTypeModel, TIStoredFileModel, TStoredFileModel, TIContactModel, TContactModel, TISalesEventModel, TSalesEventModel, TIDiscountModel, TDiscountModel, TIItemDiscountModel, TItemDiscountModel>
        : SalesCollectionBaseModel,
            ISalesCollectionBaseModel<TITypeModel, TIStoredFileModel, TIContactModel, TISalesEventModel, TIDiscountModel, TIItemDiscountModel>
        where TITypeModel : ITypableBaseModel
        where TTypeModel : class, TITypeModel
        where TIStoredFileModel : IAmAStoredFileRelationshipTableModel
        where TStoredFileModel : class, TIStoredFileModel
        where TIContactModel : IAmAContactRelationshipTableModel
        where TContactModel : class, TIContactModel
        where TIDiscountModel : IAppliedDiscountBaseModel
        where TDiscountModel : class, TIDiscountModel
        where TIItemDiscountModel : IAppliedDiscountBaseModel
        where TItemDiscountModel : class, TIItemDiscountModel
        where TISalesEventModel : ISalesEventBaseModel
        where TSalesEventModel : class, TISalesEventModel
    {
        /// <inheritdoc cref="IHaveATypeBaseModel{TITypeModel}.Type"/>
        [ApiMember(Name = nameof(Type), DataType = "TypeModel", ParameterType = "body", IsRequired = true,
            Description = "Model for Type of this object, optional")]
        public TTypeModel? Type { get; set; }

        /// <inheritdoc/>
        TITypeModel? IHaveATypeBaseModel<TITypeModel>.Type { get => Type; set => Type = (TTypeModel?)value; }

        #region Associated Objects
        /// <inheritdoc cref="IHaveStoredFilesBaseModel{TIStoredFileModel}.StoredFiles"/>
        [ApiMember(Name = nameof(StoredFiles), DataType = "List<TStoredFileModel>", ParameterType = "body", IsRequired = true,
            Description = "Stored Files for this sales collection, optional")]
        public List<TStoredFileModel>? StoredFiles { get; set; }

        /// <inheritdoc/>
        List<TIStoredFileModel>? IHaveStoredFilesBaseModel<TIStoredFileModel>.StoredFiles { get => StoredFiles?.ToList<TIStoredFileModel>(); set => StoredFiles = value?.Cast<TStoredFileModel>().ToList(); }

        /// <inheritdoc cref="IHaveContactsBaseModel{TIContactModel}.Contacts"/>
        [ApiMember(Name = nameof(Contacts), DataType = "List<TContactModel>", ParameterType = "body", IsRequired = true,
            Description = "Contacts for this sales collection, optional")]
        public List<TContactModel>? Contacts { get; set; }

        /// <inheritdoc/>
        List<TIContactModel>? IHaveContactsBaseModel<TIContactModel>.Contacts { get => Contacts?.ToList<TIContactModel>(); set => Contacts = value?.Cast<TContactModel>().ToList(); }

        /// <inheritdoc cref="IHaveSalesEventsBaseModel{TISalesEventModel}.SalesEvents"/>
        [ApiMember(Name = nameof(SalesEvents), DataType = "List<TSalesEventModel>", ParameterType = "body", IsRequired = true,
            Description = "SalesEvents for this sales collection, optional")]
        public List<TSalesEventModel>? SalesEvents { get; set; }

        /// <inheritdoc/>
        List<TISalesEventModel>? IHaveSalesEventsBaseModel<TISalesEventModel>.SalesEvents { get => SalesEvents?.ToList<TISalesEventModel>(); set => SalesEvents = value?.Cast<TSalesEventModel>().ToList(); }

        /// <inheritdoc cref="IHaveAppliedDiscountsBaseModel{TIDiscountModel}.Discounts"/>
        [ApiMember(Name = nameof(Discounts), DataType = "List<TDiscountModel>", ParameterType = "body", IsRequired = false)]
        public List<TDiscountModel>? Discounts { get; set; }

        /// <inheritdoc/>
        List<TIDiscountModel>? IHaveAppliedDiscountsBaseModel<TIDiscountModel>.Discounts { get => Discounts?.ToList<TIDiscountModel>(); set => Discounts = value?.Cast<TDiscountModel>().ToList(); }

        /// <inheritdoc cref="ISalesCollectionBaseModel{TITypeModel, TIStoredFileModel, TIContactModel, TISalesEventModel, TIDiscountModel, TIItemDiscountModel}.SalesItems"/>
        [ApiMember(Name = nameof(SalesItems), DataType = "List<SalesItemBaseModel<TIItemDiscountModel, TItemDiscountModel>>", ParameterType = "body", IsRequired = false)]
        public List<SalesItemBaseModel<TIItemDiscountModel, TItemDiscountModel>>? SalesItems { get; set; }

        /// <inheritdoc/>
        List<ISalesItemBaseModel<TIItemDiscountModel>>? ISalesCollectionBaseModel<TITypeModel, TIStoredFileModel, TIContactModel, TISalesEventModel, TIDiscountModel, TIItemDiscountModel>.SalesItems { get => SalesItems?.Cast<ISalesItemBaseModel<TIItemDiscountModel>>().ToList(); set => SalesItems = value?.Cast<SalesItemBaseModel<TIItemDiscountModel, TItemDiscountModel>>().ToList(); }
        #endregion
    }

    /// <summary>A data Model for the sales collection base.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="ISalesCollectionBaseModel"/>
    public abstract class SalesCollectionBaseModel : BaseModel, ISalesCollectionBaseModel
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
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the Status of this object, required if no StatusModel present")]
        public int StatusID { get; set; }

        /// <inheritdoc cref="IHaveAStatusBaseModel{IStatusModel}.Status"/>
        [ApiMember(Name = nameof(Status), DataType = "StatusModel", ParameterType = "body", IsRequired = true,
            Description = "Model for Status of this object, required if no StatusID present")]
        public StatusModel? Status { get; set; }

        /// <inheritdoc/>
        IStatusModel? IHaveAStatusBaseModel<IStatusModel>.Status { get => Status; set => Status = (StatusModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Key for the Status of this object, read-only")]
        public string? StatusKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name for the Status of this object, read-only")]
        public string? StatusName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusDisplayName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Display Name for the Status of this object, read-only")]
        public string? StatusDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusTranslationKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Translation Key for the Status of this object, read-only")]
        public string? StatusTranslationKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StatusSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Sort Order for the Status of this object, read-only")]
        public int? StatusSortOrder { get; set; }
        #endregion

        #region IHaveAStateBaseModel<IStateModel>
        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Identifier for the State of this object, required if no StateModel present")]
        public int StateID { get; set; }

        /// <inheritdoc cref="IHaveAStateBaseModel{IStateModel}.State"/>
        [ApiMember(Name = nameof(State), DataType = "StateModel", ParameterType = "body", IsRequired = true,
            Description = "Model for State of this object, required if no StateID present")]
        public StateModel? State { get; set; }

        /// <inheritdoc/>
        IStateModel? IHaveAStateBaseModel<IStateModel>.State { get => State; set => State = (StateModel?)value; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Key for the State of this object, read-only")]
        public string? StateKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Name for the State of this object, read-only")]
        public string? StateName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateDisplayName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Display Name for the State of this object, read-only")]
        public string? StateDisplayName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateTranslationKey), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Translation Key for the State of this object, read-only")]
        public string? StateTranslationKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StateSortOrder), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "Sort Order for the State of this object, read-only")]
        public int? StateSortOrder { get; set; }
        #endregion

        #region IAmFilterableByAccountModel Properties
        /// <inheritdoc/>
        public int? AccountID { get; set; }

        /// <inheritdoc cref="IAmFilterableByNullableAccountModel.Account"/>
        public AccountModel? Account { get; set; }

        /// <inheritdoc/>
        IAccountModel? IAmFilterableByNullableAccountModel.Account { get => Account; set => Account = (AccountModel?)value; }

        /// <inheritdoc/>
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        public string? AccountName { get; set; }
        #endregion

        #region IAmFilterableByBrandModel Properties
        /// <inheritdoc/>
        public int? BrandID { get; set; }

        /// <inheritdoc cref="IAmFilterableByNullableBrandModel.Brand"/>
        public BrandModel? Brand { get; set; }

        /// <inheritdoc/>
        IBrandModel? IAmFilterableByNullableBrandModel.Brand { get => Brand; set => Brand = (BrandModel?)value; }

        /// <inheritdoc/>
        public string? BrandKey { get; set; }

        /// <inheritdoc/>
        public string? BrandName { get; set; }
        #endregion

        #region IAmFilterableByFranchiseModel Properties
        /// <inheritdoc/>
        public int? FranchiseID { get; set; }

        /// <inheritdoc cref="IAmFilterableByNullableFranchiseModel.Franchise"/>
        public FranchiseModel? Franchise { get; set; }

        /// <inheritdoc/>
        IFranchiseModel? IAmFilterableByNullableFranchiseModel.Franchise { get => Franchise; set => Franchise = (FranchiseModel?)value; }

        /// <inheritdoc/>
        public string? FranchiseKey { get; set; }

        /// <inheritdoc/>
        public string? FranchiseName { get; set; }
        #endregion

        #region IAmFilterableByStoreModel Properties
        /// <inheritdoc/>
        public int? StoreID { get; set; }

        /// <inheritdoc cref="IAmFilterableByNullableStoreModel.Store"/>
        public StoreModel? Store { get; set; }

        /// <inheritdoc/>
        IStoreModel? IAmFilterableByNullableStoreModel.Store { get => Store; set => Store = (StoreModel?)value; }

        /// <inheritdoc/>
        public string? StoreKey { get; set; }

        /// <inheritdoc/>
        public string? StoreName { get; set; }

        /// <inheritdoc/>
        public string? StoreSeoUrl { get; set; }
        #endregion

        /// <inheritdoc/>
        public Guid? SessionID { get; set; }

        /// <inheritdoc cref="ISalesCollectionBaseModel.Totals"/>
        public CartTotals Totals { get; set; } = new();

        /// <inheritdoc/>
        ICartTotals ISalesCollectionBaseModel.Totals { get => Totals; set => Totals = (CartTotals)value; }

        /// <inheritdoc/>
        public DateTime? OriginalDate { get; set; }

        /// <inheritdoc/>
        public DateTime? DueDate { get; set; }

        /// <inheritdoc/>
        public bool? ShippingSameAsBilling { get; set; }

        /// <inheritdoc/>
        public decimal? BalanceDue { get; set; }

        /// <inheritdoc/>
        public decimal ItemQuantity { get; set; }

        #region Related Objects
        /// <inheritdoc cref="ISalesCollectionBaseModel.ShippingDetail"/>
        public SalesOrderShippingModel? ShippingDetail { get; set; }

        /// <inheritdoc/>
        ISalesOrderShippingModel? ISalesCollectionBaseModel.ShippingDetail { get => ShippingDetail; set => ShippingDetail = (SalesOrderShippingModel?)value; }

        /// <inheritdoc/>
        public int? ShipmentID { get; set; }

        /// <inheritdoc/>
        public string? ShipmentKey { get; set; }

        /// <summary>Gets or sets the name of the shipment.</summary>
        /// <value>The name of the shipment.</value>
        public string? ShipmentName { get; set; }

        /// <inheritdoc cref="ISalesCollectionBaseModel.Shipment"/>
        public ShipmentModel? Shipment { get; set; }

        /// <inheritdoc/>
        IShipmentModel? ISalesCollectionBaseModel.Shipment { get => Shipment; set => Shipment = (ShipmentModel?)value; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc cref="IAmFilterableByNullableUserModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IAmFilterableByNullableUserModel.User { get => User; set => User = (UserModel?)value; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc/>
        public string? UserUserName { get; set; }

        /// <inheritdoc/>
        public string? UserContactEmail { get; set; }

        /// <inheritdoc/>
        public string? UserContactFirstName { get; set; }

        /// <inheritdoc/>
        public string? UserContactLastName { get; set; }

        /// <inheritdoc/>
        public int? BillingContactID { get; set; }

        /// <inheritdoc/>
        public string? BillingContactKey { get; set; }

        /// <inheritdoc cref="ISalesCollectionBaseModel.BillingContact"/>
        public ContactModel? BillingContact { get; set; }

        /// <inheritdoc/>
        IContactModel? ISalesCollectionBaseModel.BillingContact { get => BillingContact; set => BillingContact = (ContactModel?)value; }

        /// <inheritdoc/>
        public int? ShippingContactID { get; set; }

        /// <inheritdoc/>
        public string? ShippingContactKey { get; set; }

        /// <inheritdoc cref="ISalesCollectionBaseModel.ShippingContact"/>
        public ContactModel? ShippingContact { get; set; }

        /// <inheritdoc/>
        IContactModel? ISalesCollectionBaseModel.ShippingContact { get => ShippingContact; set => ShippingContact = (ContactModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ISalesCollectionBaseModel.RateQuotes"/>
        [ApiMember(Name = nameof(RateQuotes), DataType = "List<RateQuoteModel>", ParameterType = "body", IsRequired = false)]
        public List<RateQuoteModel>? RateQuotes { get; set; }

        /// <inheritdoc/>
        List<IRateQuoteModel>? ISalesCollectionBaseModel.RateQuotes { get => RateQuotes?.Cast<IRateQuoteModel>().ToList(); set => RateQuotes = value?.Cast<RateQuoteModel>().ToList(); }
        #endregion
    }
}
