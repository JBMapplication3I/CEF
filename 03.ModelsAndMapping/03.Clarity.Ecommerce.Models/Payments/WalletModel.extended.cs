// <copyright file="WalletModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the wallet model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using Utilities;

    /// <summary>A data Model for the wallet.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IWalletModel"/>
    public partial class WalletModel
    {
        /// <inheritdoc/>
        public string? CreditCardNumber { get; set; }

        /// <inheritdoc/>
        public string? AccountNumber { get; set; }

        /// <inheritdoc/>
        public string? RoutingNumber { get; set; }

        /// <inheritdoc/>
        public string? BankName { get; set; }

        /// <inheritdoc/>
        public string? CardHolderName { get; set; }

        /// <inheritdoc/>
        public int? ExpirationMonth { get; set; }

        /// <inheritdoc/>
        public int? ExpirationYear { get; set; }

        /// <inheritdoc/>
        public string? Token { get; set; }

        /// <inheritdoc/>
        public string? CardType { get; set; }

        /// <inheritdoc/>
        public bool IsDefault { get; set; }

        #region Related Objects
        /// <inheritdoc/>
        public int UserID { get; set; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc cref="IWalletModel.User" />
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? IWalletModel.User { get => User; set => User = (UserModel?)value; }

        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? CurrencyName { get; set; }

        /// <inheritdoc cref="IWalletModel.Currency"/>
        public CurrencyModel? Currency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IWalletModel.Currency { get => Currency; set => Currency = (CurrencyModel?)value; }

        /// <inheritdoc/>
        public int? AccountContactID { get; set; }

        /// <inheritdoc/>
        public string? AccountContactKey { get; set; }

        /// <inheritdoc/>
        public string? AccountContactName { get; set; }

        /// <inheritdoc cref="IWalletModel.AccountContact"/>
        public AccountContactModel? AccountContact { get; set; }

        /// <inheritdoc/>
        IAccountContactModel? IWalletModel.AccountContact { get => AccountContact; set => AccountContact = (AccountContactModel?)value; }
        #endregion

        /// <inheritdoc/>
        public bool Validate()
        {
            return Contract.CheckValidID(UserID)
                // Valid by Credit Card
                && (Contract.CheckAllValidKeys(
                        CreditCardNumber, CardHolderName, ExpirationMonth?.ToString(), ExpirationYear?.ToString())
                    // Valid by eCheck
                    || Contract.CheckAllValidKeys(AccountNumber, RoutingNumber, BankName, CardHolderName, CardType));
        }
    }
}
