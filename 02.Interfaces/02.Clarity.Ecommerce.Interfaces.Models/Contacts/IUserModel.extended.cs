// <copyright file="IUserModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUserModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for user model.</summary>
    public partial interface IUserModel
    {
        #region ASP.NET Identity Properties
        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserName { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        string? Email { get; set; }

        /// <summary>Gets or sets a value indicating whether the email confirmed.</summary>
        /// <value>True if email confirmed, false if not.</value>
        bool EmailConfirmed { get; set; }

        /// <summary>Gets or sets the password hash.</summary>
        /// <value>The password hash.</value>
        string? PasswordHash { get; set; }

        /// <summary>Gets or sets the override password.</summary>
        /// <value>The override password.</value>
        string? OverridePassword { get; set; }

        /// <summary>Gets or sets the security stamp.</summary>
        /// <value>The security stamp.</value>
        string? SecurityStamp { get; set; }

        /// <summary>Gets or sets the phone number.</summary>
        /// <value>The phone number.</value>
        string? PhoneNumber { get; set; }

        /// <summary>Gets or sets a value indicating whether the phone number confirmed.</summary>
        /// <value>True if phone number confirmed, false if not.</value>
        bool PhoneNumberConfirmed { get; set; }

        /// <summary>Gets or sets a value indicating whether the two factor is enabled.</summary>
        /// <value>True if two factor enabled, false if not.</value>
        bool TwoFactorEnabled { get; set; }

        /// <summary>Gets or sets the Date/Time of the lockout end date UTC.</summary>
        /// <value>The lockout end date UTC.</value>
        DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>Gets or sets a value indicating whether the lockout is enabled.</summary>
        /// <value>True if lockout enabled, false if not.</value>
        bool LockoutEnabled { get; set; }

        /// <summary>Gets or sets the number of access failed.</summary>
        /// <value>The number of access failed.</value>
        int AccessFailedCount { get; set; }
        #endregion

        #region Extended ASP.NET Identity Properties
        /// <summary>Gets or sets a value indicating whether this IUserModel is approved.</summary>
        /// <value>True if this IUserModel is approved, false if not.</value>
        bool IsApproved { get; set; }

        /// <summary>Gets or sets a value indicating whether the require password change on next login.</summary>
        /// <value>True if require password change on next login, false if not.</value>
        bool RequirePasswordChangeOnNextLogin { get; set; }
        #endregion

        #region User Properties
        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the percent discount.</summary>
        /// <value>The percent discount.</value>
        int? PercentDiscount { get; set; }

        /// <summary>Gets or sets a value indicating whether this IUserModel is deleted.</summary>
        /// <value>True if this IUserModel is deleted, false if not.</value>
        bool IsDeleted { get; set; }

        /// <summary>Gets or sets a value indicating whether this IUserModel is super admin.</summary>
        /// <value>True if this IUserModel is super admin, false if not.</value>
        bool IsSuperAdmin { get; set; }

        /// <summary>Gets or sets a value indicating whether this IUserModel is email subscriber.</summary>
        /// <value>True if this IUserModel is email subscriber, false if not.</value>
        bool IsEmailSubscriber { get; set; }

        /// <summary>Gets or sets a value indicating whether this IUserModel is catalog subscriber.</summary>
        /// <value>True if this IUserModel is catalog subscriber, false if not.</value>
        bool IsCatalogSubscriber { get; set; }

        /// <summary>Gets or sets the date of birth.</summary>
        /// <value>The date of birth.</value>
        DateTime? DateOfBirth { get; set; }

        /// <summary>Gets or sets the gender.</summary>
        /// <value>The gender.</value>
        string? Gender { get; set; }

        /// <summary>Gets or sets a value indicating whether is SMS allowed.</summary>
        /// <value>True if this is SMS allowed, false if not.</value>
        bool IsSMSAllowed { get; set; }

        /// <summary>Gets or sets a value indicating whether this IUserModel use automatic pay.</summary>
        /// <value>True if use automatic pay, false if not.</value>
        bool UseAutoPay { get; set; }

        /// <summary>Gets or sets the BusinessType.</summary>
        /// <value>The BusinessType.</value>
        string? BusinessType { get; set; }

        /// <summary>Gets or sets the DEANumber.</summary>
        /// <value>The DEANumber.</value>
        string? DEANumber { get; set; }

        /// <summary>Gets or sets the DunsNumber.</summary>
        /// <value>The DunsNumber.</value>
        string? DunsNumber { get; set; }

        /// <summary>Gets or sets the EIN.</summary>
        /// <value>The EIN.</value>
        string? EIN { get; set; }

        /// <summary>Gets or sets the MedicalLicenseHolderName.</summary>
        /// <value>The MedicalLicenseHolderName.</value>
        string? MedicalLicenseHolderName { get; set; }

        /// <summary>Gets or sets the MedicalLicenseNumber.</summary>
        /// <value>The MedicalLicenseNumber.</value>
        string? MedicalLicenseNumber { get; set; }

        /// <summary>Gets or sets the MedicalLicenseState.</summary>
        /// <value>The MedicalLicenseState.</value>
        string? MedicalLicenseState { get; set; }

        /// <summary>Gets or sets the PreferredInvoiceMethod.</summary>
        /// <value>The PreferredInvoiceMethod.</value>
        string? PreferredInvoiceMethod { get; set; }

        /// <summary>Gets or sets a value indicating whether this user is TaxExempt.</summary>
        /// <value>True if tax exempt, false if not.</value>
        bool? TaxExempt { get; set; }

        /// <summary>Gets or sets a value indicating the Tax Exempt Number.</summary>
        /// <value>The TaxExemptNumber.</value>
        string? TaxExemptNumber { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the sales rep contacts user.</summary>
        /// <value>The identifier of the sales rep contacts user.</value>
        int? SalesRepContactsUserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        string? AccountKey { get; set; }

        /// <summary>Gets or sets the name of the account.</summary>
        /// <value>The name of the account.</value>
        string? AccountName { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        IAccountModel? Account { get; set; }

        /// <summary>Gets or sets the billing address.</summary>
        /// <value>The billing address.</value>
        IAddressModel? BillingAddress { get; set; }

        /// <summary>Gets or sets the identifier of the preferred store.</summary>
        /// <value>The identifier of the preferred store.</value>
        int? PreferredStoreID { get; set; }

        /// <summary>Gets or sets the preferred store key.</summary>
        /// <value>The preferred store key.</value>
        string? PreferredStoreKey { get; set; }

        /// <summary>Gets or sets the name of the preferred store.</summary>
        /// <value>The name of the preferred store.</value>
        string? PreferredStoreName { get; set; }

        /// <summary>Gets or sets the preferred store.</summary>
        /// <value>The preferred store.</value>
        IStoreModel? PreferredStore { get; set; }

        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the currency.</summary>
        /// <value>The name of the currency.</value>
        string? CurrencyName { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        ICurrencyModel? Currency { get; set; }

        /// <summary>Gets or sets the identifier of the language.</summary>
        /// <value>The identifier of the language.</value>
        int? LanguageID { get; set; }

        /// <summary>Gets or sets the language key.</summary>
        /// <value>The language key.</value>
        string? LanguageKey { get; set; }

        /// <summary>Gets or sets the language.</summary>
        /// <value>The language.</value>
        ILanguageModel? Language { get; set; }

        /// <summary>Gets or sets the identifier of the user online status.</summary>
        /// <value>The identifier of the user online status.</value>
        int? UserOnlineStatusID { get; set; }

        /// <summary>Gets or sets the user online status key.</summary>
        /// <value>The user online status key.</value>
        string? UserOnlineStatusKey { get; set; }

        /// <summary>Gets or sets the name of the user online status.</summary>
        /// <value>The name of the user online status.</value>
        string? UserOnlineStatusName { get; set; }

        /// <summary>Gets or sets the user online status.</summary>
        /// <value>The user online status.</value>
        IStatusModel? UserOnlineStatus { get; set; }
        #endregion
    }
}
