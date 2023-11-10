// <copyright file="PayFabricGetTransactionResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric get transaction response class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using JetBrains.Annotations;

    /// <summary>A pay fabric get transaction response.</summary>
    [PublicAPI]
    public class PayFabricGetTransactionResponse
    {
        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        public string? Amount { get; set; }

        /// <summary>Gets or sets the type of the authorization.</summary>
        /// <value>The type of the authorization.</value>
        public string? AuthorizationType { get; set; }

        /// <summary>Gets or sets the batch number.</summary>
        /// <value>The batch number.</value>
        public string? BatchNumber { get; set; }

        /// <summary>Gets or sets the Cc entry indicator.</summary>
        /// <value>The Cc entry indicator.</value>
        public string? CCEntryIndicator { get; set; }

        /// <summary>Gets or sets the card.</summary>
        /// <value>The card.</value>
        public PayFabricCard? Card { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        public string? Currency { get; set; }

        /// <summary>Gets or sets the customer.</summary>
        /// <value>The customer.</value>
        public string? Customer { get; set; }

        /// <summary>Gets or sets the document.</summary>
        /// <value>The document.</value>
        public PayFabricDocument? Document { get; set; }

        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        public string? Key { get; set; }

        /// <summary>Gets or sets a unique identifier of the mso engine.</summary>
        /// <value>Unique identifier of the mso engine.</value>
        // ReSharper disable once InconsistentNaming
        public string? MSO_EngineGUID { get; set; }

        /// <summary>Gets or sets the modified on.</summary>
        /// <value>The modified on.</value>
        public string? ModifiedOn { get; set; }

        /// <summary>Gets or sets the pay date.</summary>
        /// <value>The pay date.</value>
        public string? PayDate { get; set; }

        /// <summary>Gets or sets the reference key.</summary>
        /// <value>The reference key.</value>
        public object? ReferenceKey { get; set; }

        /// <summary>Gets or sets the reference trxs.</summary>
        /// <value>The reference trxs.</value>
        public object[]? ReferenceTrxs { get; set; }

        /// <summary>Gets or sets the request authentication code.</summary>
        /// <value>The request authentication code.</value>
        public string? ReqAuthCode { get; set; }

        /// <summary>Gets or sets the identifier of the setup.</summary>
        /// <value>The identifier of the setup.</value>
        public string? SetupId { get; set; }

        /// <summary>Gets or sets the shipto.</summary>
        /// <value>The shipto.</value>
        public PayFabricAddress? Shipto { get; set; }

        /// <summary>Gets or sets the tender.</summary>
        /// <value>The tender.</value>
        public string? Tender { get; set; }

        /// <summary>Gets or sets the trx initiation.</summary>
        /// <value>The trx initiation.</value>
        public string? TrxInitiation { get; set; }

        /// <summary>Gets or sets the trx response.</summary>
        /// <value>The trx response.</value>
        public PayFabricTransactionResponse? TrxResponse { get; set; }

        /// <summary>Gets or sets the trx schedule.</summary>
        /// <value>The trx schedule.</value>
        public string? TrxSchedule { get; set; }

        /// <summary>Gets or sets the trx user define 1.</summary>
        /// <value>The trx user define 1.</value>
        public string? TrxUserDefine1 { get; set; }

        /// <summary>Gets or sets the trx user define 2.</summary>
        /// <value>The trx user define 2.</value>
        public string? TrxUserDefine2 { get; set; }

        /// <summary>Gets or sets the trx user define 3.</summary>
        /// <value>The trx user define 3.</value>
        public string? TrxUserDefine3 { get; set; }

        /// <summary>Gets or sets the trx user define 4.</summary>
        /// <value>The trx user define 4.</value>
        public string? TrxUserDefine4 { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        public string? Type { get; set; }
    }
}
