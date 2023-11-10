// <copyright file="IAccountModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for account model.</summary>
    public partial interface IAccountModel
    {
        #region Account Properties
        /// <summary>Gets or sets the type of the Business.</summary>
        /// <value>The Business type.</value>
        public string? BusinessType { get; set; }

        /// <summary>Gets or sets the credit.</summary>
        /// <value>The credit.</value>
        decimal? Credit { get; set; }

        /// <summary>Gets or sets the DEA Number.</summary>
        /// <value>A DEA number (DEA Registration Number) is an identifier assigned to a health care provider
        /// (such as a physician, physician assistant, nurse practitioner, optometrist, podiatrist, dentist, or veterinarian)
        /// by the United States Drug Enforcement Administration allowing them to write prescriptions for controlled substances.</value>
        public string? DEANumber { get; set; }

        /// <summary>Gets or sets the DUNS number.</summary>
        /// <value>The Dun and Bradstreet D‑U‑N‑S Number is a unique nine-digit identifier for businesses.</value>
        public string? DunsNumber { get; set; }

        /// <summary>Gets or sets the EIN number.</summary>
        /// <value>An Employer Identification Number (EIN) is also known as a Federal Tax Identification Number,
        /// and is used to identify a business entity.</value>
        public string? EIN { get; set; }

        /// <summary>Gets or sets a value indicating whether this IAccountModel is on hold.</summary>
        /// <value>True if this IAccountModel is on hold, false if not.</value>
        bool IsOnHold { get; set; }

        /// <summary>Gets or sets a value indicating whether this IAccountModel is taxable.</summary>
        /// <value>True if this IAccountModel is taxable, false if not.</value>
        bool IsTaxable { get; set; }

        /// <summary>Gets or sets the Medical License Holder Name.</summary>
        /// <value>The name of the licensed medical professional.</value>
        public string? MedicalLicenseHolderName { get; set; }

        /// <summary>Gets or sets the Medical License Number.</summary>
        /// <value>A license number that authorizes a medical professional to practice the profession in a particular state.</value>
        public string? MedicalLicenseNumber { get; set; }

        /// <summary>Gets or sets the Medical License State.</summary>
        /// <value>The state a medical professional is authorized to practice.</value>
        public string? MedicalLicenseState { get; set; }

        /// <summary>Gets or sets the Preferred invoices method.</summary>
        /// <value>The preferred method of receiving invoices (mail or email).</value>
        public string? PreferredInvoiceMethod { get; set; }

        /// <summary>Gets or sets the identifier of the sage.</summary>
        /// <value>The identifier of the sage.</value>
        string? SageID { get; set; }

        /// <summary>Gets or sets the code of the Salesman.</summary>
        /// <value>The Salesman code.</value>
        public string? SalesmanCode { get; set; }

        /// <summary>Gets or sets the tax entity use code.</summary>
        /// <value>The tax entity use code.</value>
        string? TaxEntityUseCode { get; set; }

        /// <summary>Gets or sets the tax exemption no.</summary>
        /// <value>The tax exemption no.</value>
        string? TaxExemptionNo { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        string? Token { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the credit currency.</summary>
        /// <value>The identifier of the credit currency.</value>
        int? CreditCurrencyID { get; set; }

        /// <summary>Gets or sets the credit currency key.</summary>
        /// <value>The credit currency key.</value>
        string? CreditCurrencyKey { get; set; }

        /// <summary>Gets or sets the name of the credit currency.</summary>
        /// <value>The name of the credit currency.</value>
        string? CreditCurrencyName { get; set; }

        /// <summary>Gets or sets the credit currency.</summary>
        /// <value>The credit currency.</value>
        ICurrencyModel? CreditCurrency { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the account contacts.</summary>
        /// <value>The account contacts.</value>
        List<IAccountContactModel>? AccountContacts { get; set; }

        /// <summary>Gets or sets the account currencies.</summary>
        /// <value>The account currencies.</value>
        List<IAccountCurrencyModel>? AccountCurrencies { get; set; }

        /// <summary>Gets or sets the account price points.</summary>
        /// <value>The account price points.</value>
        List<IAccountPricePointModel>? AccountPricePoints { get; set; }

        /// <summary>Gets or sets the account associations.</summary>
        /// <value>The account associations.</value>
        List<IAccountAssociationModel>? AccountAssociations { get; set; }

        /// <summary>Gets or sets the accounts associated with.</summary>
        /// <value>The accounts associated with.</value>
        List<IAccountAssociationModel>? AccountsAssociatedWith { get; set; }
        #endregion
    }
}
