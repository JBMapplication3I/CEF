// <copyright file="IWalletModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IWalletModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IWalletModel
    {
        #region Wallet Properties
        /// <summary>Gets or sets the credit card number.</summary>
        /// <value>The credit card number.</value>
        string? CreditCardNumber { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        string? AccountNumber { get; set; }

        /// <summary>Gets or sets the routing number.</summary>
        /// <value>The routing number.</value>
        string? RoutingNumber { get; set; }

        /// <summary>Gets or sets the name of the bank.</summary>
        /// <value>The name of the bank.</value>
        string? BankName { get; set; }

        /// <summary>Gets or sets the name of the card holder.</summary>
        /// <value>The name of the card holder.</value>
        string? CardHolderName { get; set; }

        /// <summary>Gets or sets the expiration month.</summary>
        /// <value>The expiration month.</value>
        int? ExpirationMonth { get; set; }

        /// <summary>Gets or sets the expiration year.</summary>
        /// <value>The expiration year.</value>
        int? ExpirationYear { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        string? Token { get; set; }

        /// <summary>Gets or sets the type of the card.</summary>
        /// <value>The type of the card.</value>
        string? CardType { get; set; }

        /// <summary>Gets or sets a value indicating whether this IWalletModel is the default.</summary>
        /// <value>True if this IWalletModel is default, false if not.</value>
        bool IsDefault { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserID { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }

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

        /// <summary>Gets or sets the identifier of the account contact.</summary>
        /// <value>The identifier of the account contact.</value>
        int? AccountContactID { get; set; }

        /// <summary>Gets or sets the account contact key.</summary>
        /// <value>The account contact key.</value>
        string? AccountContactKey { get; set; }

        /// <summary>Gets or sets the name of the account contact.</summary>
        /// <value>The name of the account contact.</value>
        string? AccountContactName { get; set; }

        /// <summary>Gets or sets the account contact.</summary>
        /// <value>The account contact.</value>
        IAccountContactModel? AccountContact { get; set; }
        #endregion

        /// <summary>Validates this IWalletModel.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        bool Validate();
    }
}
