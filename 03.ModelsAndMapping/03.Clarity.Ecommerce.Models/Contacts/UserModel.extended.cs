// <copyright file="UserModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the user.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IUserModel"/>
    public partial class UserModel
    {
        #region ASP.NET Identity Properties
        /// <inheritdoc/>
        public string? UserName { get; set; }

        /// <inheritdoc/>
        public string? Email { get; set; }

        /// <inheritdoc/>
        public bool EmailConfirmed { get; set; }

        /// <inheritdoc/>
        public string? PasswordHash { get; set; }

        /// <inheritdoc/>
        public string? OverridePassword { get; set; }

        /// <inheritdoc/>
        public string? SecurityStamp { get; set; }

        /// <inheritdoc/>
        public string? PhoneNumber { get; set; }

        /// <inheritdoc/>
        public bool PhoneNumberConfirmed { get; set; }

        /// <inheritdoc/>
        public bool TwoFactorEnabled { get; set; }

        /// <inheritdoc/>
        public DateTime? LockoutEndDateUtc { get; set; }

        /// <inheritdoc/>
        public bool LockoutEnabled { get; set; }

        /// <inheritdoc/>
        public int AccessFailedCount { get; set; }
        #endregion

        #region Extended ASP.NET Identity Properties
        /// <inheritdoc/>
        public bool IsApproved { get; set; }

        /// <inheritdoc/>
        public bool RequirePasswordChangeOnNextLogin { get; set; }
        #endregion

        #region User Properties
        /// <inheritdoc/>
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        public int? PercentDiscount { get; set; }

        /// <inheritdoc/>
        public bool IsDeleted { get; set; }

        /// <inheritdoc/>
        public bool IsSuperAdmin { get; set; }

        /// <inheritdoc/>
        public bool IsEmailSubscriber { get; set; }

        /// <inheritdoc/>
        public bool IsCatalogSubscriber { get; set; }

        /// <inheritdoc/>
        public DateTime? DateOfBirth { get; set; }

        /// <inheritdoc/>
        public string? Gender { get; set; }

        /// <inheritdoc/>
        public bool IsSMSAllowed { get; set; }

        /// <inheritdoc/>
        public bool UseAutoPay { get; set; }

        /// <inheritdoc/>
        public string? BusinessType { get; set; }

        /// <inheritdoc/>
        public string? DEANumber { get; set; }

        /// <inheritdoc/>
        public string? DunsNumber { get; set; }

        /// <inheritdoc/>
        public string? EIN { get; set; }

        /// <inheritdoc/>
        public string? MedicalLicenseHolderName { get; set; }

        /// <inheritdoc/>
        public string? MedicalLicenseNumber { get; set; }

        /// <inheritdoc/>
        public string? MedicalLicenseState { get; set; }

        /// <inheritdoc/>
        public string? PreferredInvoiceMethod { get; set; }

        /// <inheritdoc/>
        public bool? TaxExempt { get; set; }

        /// <inheritdoc/>
        public string? TaxExemptNumber { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        public string? AccountName { get; set; }

        /// <inheritdoc cref="IUserModel.Account"/>
        public AccountModel? Account { get; set; }

        /// <inheritdoc/>
        IAccountModel? IUserModel.Account { get => Account; set => Account = (AccountModel?)value; }

        /// <inheritdoc/>
        public int? PreferredStoreID { get; set; }

        /// <inheritdoc/>
        public string? PreferredStoreKey { get; set; }

        /// <inheritdoc/>
        public string? PreferredStoreName { get; set; }

        /// <inheritdoc cref="IUserModel.PreferredStore"/>
        public StoreModel? PreferredStore { get; set; }

        /// <inheritdoc/>
        IStoreModel? IUserModel.PreferredStore { get => PreferredStore; set => PreferredStore = (StoreModel?)value; }

        /// <inheritdoc cref="IUserModel.BillingAddress"/>
        public AddressModel? BillingAddress { get; set; }

        /// <inheritdoc/>
        IAddressModel? IUserModel.BillingAddress { get => BillingAddress; set => BillingAddress = (AddressModel?)value; }

        /// <inheritdoc/>
        public int? SalesRepContactsUserID { get; set; }

        /// <inheritdoc cref="IUserModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IUserModel.User { get => User; set => User = (UserModel?)value; }

        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? CurrencyName { get; set; }

        /// <inheritdoc cref="IUserModel.Currency"/>
        public CurrencyModel? Currency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IUserModel.Currency { get => Currency; set => Currency = (CurrencyModel?)value; }

        /// <inheritdoc/>
        public int? LanguageID { get; set; }

        /// <inheritdoc/>
        public string? LanguageKey { get; set; }

        /// <inheritdoc cref="IUserModel.Language"/>
        public LanguageModel? Language { get; set; }

        /// <inheritdoc/>
        ILanguageModel? IUserModel.Language { get => Language; set => Language = (LanguageModel?)value; }

        /// <inheritdoc/>
        public int? UserOnlineStatusID { get; set; }

        /// <inheritdoc/>
        public string? UserOnlineStatusKey { get; set; }

        /// <inheritdoc/>
        public string? UserOnlineStatusName { get; set; }

        /// <inheritdoc cref="IUserModel.UserOnlineStatus"/>
        public StatusModel? UserOnlineStatus { get; set; }

        /// <inheritdoc/>
        IStatusModel? IUserModel.UserOnlineStatus { get => UserOnlineStatus; set => UserOnlineStatus = (StatusModel?)value; }
        #endregion
    }
}
