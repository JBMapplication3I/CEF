// <copyright file="Account.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for account.</summary>
    public interface IAccount
        : INameableBase,
            IHaveNotesBase,
            IHaveATypeBase<AccountType>,
            IHaveAStatusBase<AccountStatus>,
            IHaveImagesBase<Account, AccountImage, AccountImageType>,
            IHaveStoredFilesBase<Account, AccountFile>,
            IAmFilterableByBrand<BrandAccount>,
            IAmFilterableByFranchise<FranchiseAccount>,
            IAmFilterableByProduct<AccountProduct>,
            IAmFilterableByStore<StoreAccount>,
            IAmFilterableByUser<User>,
            IAmFilterableByVendor<VendorAccount>
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

        /// <summary>Gets or sets a value indicating whether this IAccount is on hold.</summary>
        /// <value>True if this IAccount is on hold, false if not.</value>
        bool IsOnHold { get; set; }

        /// <summary>Gets or sets a value indicating whether this IAccount is taxable.</summary>
        /// <value>True if this IAccount is taxable, false if not.</value>
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

        /// <summary>Gets or sets SAGE customer number.</summary>
        /// <value>The SAGE customer number.</value>
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

        /// <summary>Gets or sets the Rapid Reorder account token.</summary>
        /// <value>The account token.</value>
        string? Token { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the credit currency.</summary>
        /// <value>The identifier of the credit currency.</value>
        int? CreditCurrencyID { get; set; }

        /// <summary>Gets or sets the credit currency.</summary>
        /// <value>The credit currency.</value>
        Currency? CreditCurrency { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the account contacts.</summary>
        /// <value>The account contacts.</value>
        ICollection<AccountContact>? AccountContacts { get; set; }

        /// <summary>Gets or sets the account price points.</summary>
        /// <value>The account price points.</value>
        ICollection<AccountPricePoint>? AccountPricePoints { get; set; }

        /// <summary>Gets or sets the account currencies.</summary>
        /// <value>The account currencies.</value>
        ICollection<AccountCurrency>? AccountCurrencies { get; set; }

        /// <summary>Gets or sets the account user roles.</summary>
        /// <value>The account user roles.</value>
        ICollection<AccountUserRole>? AccountUserRoles { get; set; }

        /// <summary>Gets or sets the purchase orders.</summary>
        /// <value>The purchase orders.</value>
        ICollection<PurchaseOrder>? PurchaseOrders { get; set; }

        /// <summary>Gets or sets the subscriptions.</summary>
        /// <value>The subscriptions.</value>
        ICollection<Subscription>? Subscriptions { get; set; }

        /// <summary>Gets or sets the account associations.</summary>
        /// <value>The account associations.</value>
        ICollection<AccountAssociation>? AccountAssociations { get; set; }

        /// <summary>Gets or sets the accounts associated with.</summary>
        /// <value>The accounts associated with.</value>
        ICollection<AccountAssociation>? AccountsAssociatedWith { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Accounts", "Account")]
    public class Account : NameableBase, IAccount
    {
        private ICollection<AccountImage>? images;
        private ICollection<AccountFile>? storedFiles;
        private ICollection<StoreAccount>? stores;
        private ICollection<BrandAccount>? brands;
        private ICollection<FranchiseAccount>? franchises;
        private ICollection<Note>? notes;
        private ICollection<User>? users;
        private ICollection<VendorAccount>? vendors;
        private ICollection<AccountProduct>? products;
        private ICollection<Subscription>? subscriptions;
        private ICollection<PurchaseOrder>? purchaseOrders;
        private ICollection<AccountContact>? accountContacts;
        private ICollection<AccountUserRole>? accountUserRoles;
        private ICollection<AccountCurrency>? accountCurrencies;
        private ICollection<AccountPricePoint>? accountPricePoints;
        private ICollection<AccountAssociation>? accountAssociations;
        private ICollection<AccountAssociation>? accountsAssociatedWith;

        public Account()
        {
            // IHaveImagesBase
            images = new HashSet<AccountImage>();
            // IHaveStoredFiles
            storedFiles = new HashSet<AccountFile>();
            // IAmFilterableByStore
            stores = new HashSet<StoreAccount>();
            // IAmFilterableByBrand
            brands = new HashSet<BrandAccount>();
            // IAmFilterableByFranchise
            franchises = new HashSet<FranchiseAccount>();
            // IAmFilterableByProduct
            products = new HashSet<AccountProduct>();
            // IAmFilterableByVendor
            vendors = new HashSet<VendorAccount>();
            // IAmFilterableByUser
            users = new HashSet<User>();
            // IHaveNotesBase
            notes = new HashSet<Note>();
            // Account Properties
            accountContacts = new HashSet<AccountContact>();
            accountUserRoles = new HashSet<AccountUserRole>();
            accountCurrencies = new HashSet<AccountCurrency>();
            accountPricePoints = new HashSet<AccountPricePoint>();
            accountAssociations = new HashSet<AccountAssociation>();
            accountsAssociatedWith = new HashSet<AccountAssociation>();
            // Don't Map these out
            subscriptions = new HashSet<Subscription>();
            purchaseOrders = new HashSet<PurchaseOrder>();
        }

        #region HaveImagesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountImage>? Images { get => images; set => images = value; }
        #endregion

        #region HaveStoredFilesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountFile>? StoredFiles { get => storedFiles; set => storedFiles = value; }
        #endregion

        #region IAmFilterableByBrand Properties
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<BrandAccount>? Brands { get => brands; set => brands = value; }
        #endregion

        #region IAmFilterableByFranchise Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<FranchiseAccount>? Franchises { get => franchises; set => franchises = value; }
        #endregion

        #region IAmFilterableByStore Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver, DontMapInEver]
        public virtual ICollection<StoreAccount>? Stores { get => stores; set => stores = value; }
        #endregion

        #region IAmFilterableByProduct Properties
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountProduct>? Products { get => products; set => products = value; }
        #endregion

        #region IAmFilterableByVendor Properties
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<VendorAccount>? Vendors { get => vendors; set => vendors = value; }
        #endregion

        #region IAmFilterableByUser Properties
        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<User>? Users { get => users; set => users = value; }
        #endregion

        #region HaveNotesBase Properties
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<Note>? Notes { get => notes; set => notes = value; }
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual AccountType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual AccountStatus? Status { get; set; }
        #endregion

        #region Account Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? BusinessType { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? Credit { get; set; } = null;

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? DEANumber { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? DunsNumber { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? EIN { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsOnHold { get; set; }

        /// <inheritdoc/>
        [DefaultValue(true)]
        public bool IsTaxable { get; set; } = true;

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? MedicalLicenseHolderName { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? MedicalLicenseNumber { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? MedicalLicenseState { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? PreferredInvoiceMethod { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? SageID { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(50), DefaultValue(null)]
        public string? SalesmanCode { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? TaxEntityUseCode { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? TaxExemptionNo { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? Token { get; set; } = $"{Guid.NewGuid()}";
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(CreditCurrency)), DefaultValue(null)]
        public int? CreditCurrencyID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithLite, DefaultValue(null)]
        public virtual Currency? CreditCurrency { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore, DontMapOutEver]
        public virtual ICollection<AccountContact>? AccountContacts { get => accountContacts; set => accountContacts = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountCurrency>? AccountCurrencies { get => accountCurrencies; set => accountCurrencies = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountPricePoint>? AccountPricePoints { get => accountPricePoints; set => accountPricePoints = value; }

        /// <inheritdoc/>
        [AllowMapInWithAssociateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountAssociation>? AccountAssociations { get => accountAssociations; set => accountAssociations = value; }

        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountAssociation>? AccountsAssociatedWith { get => accountsAssociatedWith; set => accountsAssociatedWith = value; }

        #region Don't map these out
        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<AccountUserRole>? AccountUserRoles { get => accountUserRoles; set => accountUserRoles = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<Subscription>? Subscriptions { get => subscriptions; set => subscriptions = value; }

        /// <inheritdoc/>
        [DontMapInEver, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<PurchaseOrder>? PurchaseOrders { get => purchaseOrders; set => purchaseOrders = value; }
        #endregion
        #endregion
    }
}
