// <copyright file="SalesQuoteActionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the actions provider base class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.SalesQuoteHandlers.Actions;
    using Models;

    /// <summary>The sales quote actions provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISalesQuoteActionsProviderBase"/>
    public abstract class SalesQuoteActionsProviderBase : ProviderBase, ISalesQuoteActionsProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SalesQuoteActionsHandler;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> AwardLineItemAsync(
            int originalItemID,
            int responseItemID,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsApprovedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsInProcessAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsProcessedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsRejectedAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SetRecordAsVoidAsync(int id, string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<bool> SetRecordsToExpiredAsync(string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SubmitRFQForGenericProductsAsync(
            ISalesQuoteModel quote,
            bool doShare,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> SubmitRFQForSingleProductAsync(
            ISalesQuoteModel quote,
            bool doRecommend,
            bool doShare,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<(int orderID, int? invoiceID)>> ConvertQuoteToOrderAsync(int id, string? contextProfileName);
    }
}
