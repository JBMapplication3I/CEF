// <copyright file="Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the wallet class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IWallet : INameableBase
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

        /// <summary>Gets or sets the name of the card holder.</summary>
        /// <value>The name of the card holder.</value>
        string? CardHolderName { get; set; }

        /// <summary>Gets or sets a value indicating whether this IWallet is the default for the user.</summary>
        /// <value>True if this IWallet is default, false if not.</value>
        bool IsDefault { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }

        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        Currency? Currency { get; set; }

        /// <summary>Gets or sets the identifier of the account contact.</summary>
        /// <value>The identifier of the account contact.</value>
        int? AccountContactID { get; set; }

        /// <summary>Gets or sets the account contact.</summary>
        /// <value>The account contact.</value>
        AccountContact? AccountContact { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "Wallet")]
    public class Wallet : NameableBase, IWallet
    {
        #region Wallet Properties
        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? CreditCardNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false)]
        public string? AccountNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false)]
        public string? RoutingNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false)]
        public string? BankName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ExpirationMonth { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ExpirationYear { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? Token { get; set; }

        /// <inheritdoc/>
        [StringLength(100), StringIsUnicode(false), DefaultValue(null)]
        public string? CardType { get; set; }

        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), DefaultValue(null)]
        public string? CardHolderName { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsDefault { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(0)]
        public int UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Currency)), DefaultValue(null)]
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Currency? Currency { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(AccountContact)), DefaultValue(null)]
        public int? AccountContactID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual AccountContact? AccountContact { get; set; }
        #endregion
    }
}
