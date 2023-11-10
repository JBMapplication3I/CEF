// <copyright file="AccountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the account.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IAccountModel"/>
    public partial class AccountModel
    {
        #region Account Properties
        /// <inheritdoc/>
        [ApiMember(Name = nameof(BusinessType), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The business type")]
        public string? BusinessType { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Credit), DataType = "decimal?", ParameterType = "body", IsRequired = false,
            Description = "The amount of Credit this Account has available, optional")]
        public decimal? Credit { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(DEANumber), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The DEA Number")]
        public string? DEANumber { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(DunsNumber), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The DUNS number")]
        public string? DunsNumber { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(EIN), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The EIN number")]
        public string? EIN { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsOnHold), DataType = "bool", ParameterType = "body", IsRequired = false,
            Description = "Account's On Hold status. Accounts on hold cannot complete purchases until this is turned off")]
        public bool IsOnHold { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsTaxable), DataType = "bool?", ParameterType = "body", IsRequired = false,
            Description = "Whether the account can be charged Taxes for taxable products, optional, defaults to true when not set")]
        public bool IsTaxable { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MedicalLicenseHolderName), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The medical holder name")]
        public string? MedicalLicenseHolderName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MedicalLicenseNumber), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The medical license number")]
        public string? MedicalLicenseNumber { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(MedicalLicenseState), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The medical license state")]
        public string? MedicalLicenseState { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SalesmanCode), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The preferred invoice method")]
        public string? PreferredInvoiceMethod { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SageID), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The SAGE customer number")]
        public string? SageID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SalesmanCode), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The salesman code")]
        public string? SalesmanCode { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TaxEntityUseCode), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Tax code for complete or partial exemptions")]
        public string? TaxEntityUseCode { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TaxExemptionNo), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The exemption certificate value")]
        public string? TaxExemptionNo { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Token), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "The token for the account")]
        public string? Token { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CreditCurrencyID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Currency for the Account Credit, optional")]
        public int? CreditCurrencyID { get; set; }

        /// <inheritdoc/>
        public string? CreditCurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? CreditCurrencyName { get; set; }

        /// <inheritdoc cref="IAccountModel.CreditCurrency"/>
        [ApiMember(Name = nameof(CreditCurrency), DataType = "CurrencyModel", ParameterType = "body", IsRequired = false,
            Description = "The Currency for the Account Credit, optional")]
        public CurrencyModel? CreditCurrency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IAccountModel.CreditCurrency { get => CreditCurrency; set => CreditCurrency = (CurrencyModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IAccountModel.AccountContacts"/>
        [ApiMember(Name = nameof(AccountContacts), DataType = "List<AccountContactModel>", ParameterType = "body", IsRequired = false,
            Description = "Contact Book for the Account")]
        public List<AccountContactModel>? AccountContacts { get; set; }

        /// <inheritdoc/>
        List<IAccountContactModel>? IAccountModel.AccountContacts { get => AccountContacts?.ToList<IAccountContactModel>(); set => AccountContacts = value?.Cast<AccountContactModel>().ToList(); }

        /// <inheritdoc cref="IAccountModel.AccountPricePoints"/>
        [ApiMember(Name = nameof(AccountPricePoints), DataType = "List<AccountPricePointModel>", ParameterType = "body", IsRequired = false,
            Description = "Account's Price Points, required for the Multi-Tier Pricing settings, otherwise ignored")]
        public List<AccountPricePointModel>? AccountPricePoints { get; set; }

        /// <inheritdoc/>
        List<IAccountPricePointModel>? IAccountModel.AccountPricePoints { get => AccountPricePoints?.ToList<IAccountPricePointModel>(); set => AccountPricePoints = value?.Cast<AccountPricePointModel>().ToList(); }

        /// <inheritdoc cref="IAccountModel.AccountCurrencies"/>
        [ApiMember(Name = nameof(AccountCurrencies), DataType = "List<AccountCurrencyModel>", ParameterType = "body", IsRequired = false,
            Description = "Account's Currencies, used to provide custom names to the selected currencies, such as renaming 'Ticket' to 'Point'")]
        public List<AccountCurrencyModel>? AccountCurrencies { get; set; }

        /// <inheritdoc/>
        List<IAccountCurrencyModel>? IAccountModel.AccountCurrencies { get => AccountCurrencies?.ToList<IAccountCurrencyModel>(); set => AccountCurrencies = value?.Cast<AccountCurrencyModel>().ToList(); }

        /// <inheritdoc cref="IAccountModel.AccountAssociations"/>
        [ApiMember(Name = nameof(AccountAssociations), DataType = "List<AccountAssociationModel>", ParameterType = "body", IsRequired = false,
            Description = "Accounts that this account is associated to as the primary")]
        public List<AccountAssociationModel>? AccountAssociations { get; set; }

        /// <inheritdoc/>
        List<IAccountAssociationModel>? IAccountModel.AccountAssociations { get => AccountAssociations?.ToList<IAccountAssociationModel>(); set => AccountAssociations = value?.Cast<AccountAssociationModel>().ToList(); }

        /// <inheritdoc cref="IAccountModel.AccountsAssociatedWith"/>
        [ApiMember(Name = nameof(AccountsAssociatedWith), DataType = "List<AccountAssociationModel>", ParameterType = "body", IsRequired = false,
            Description = "Accounts that this account is associated to as the secondary")]
        public List<AccountAssociationModel>? AccountsAssociatedWith { get; set; }

        /// <inheritdoc/>
        List<IAccountAssociationModel>? IAccountModel.AccountsAssociatedWith { get => AccountsAssociatedWith?.ToList<IAccountAssociationModel>(); set => AccountsAssociatedWith = value?.Cast<AccountAssociationModel>().ToList(); }
        #endregion
    }
}
