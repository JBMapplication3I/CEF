// <copyright file="ISalesQuoteActionsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesQuoteActionsProviderBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Providers.SalesQuoteHandlers.Actions
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for sales quote actions provider.</summary>
    public interface ISalesQuoteActionsProviderBase : IProviderBase
    {
        /// <summary>Sets quotes to expired.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> SetRecordsToExpiredAsync(string? contextProfileName);

        /// <summary>Submit RFQ for single product.</summary>
        /// <param name="quote">             The quote.</param>
        /// <param name="doRecommend">       True to do recommend.</param>
        /// <param name="doShare">           True to do share.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SubmitRFQForSingleProductAsync(
            ISalesQuoteModel quote,
            bool doRecommend,
            bool doShare,
            string? contextProfileName);

        /// <summary>Submit RFQ for generic products.</summary>
        /// <param name="quote">             The quote.</param>
        /// <param name="doShare">           True to do share.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SubmitRFQForGenericProductsAsync(
            ISalesQuoteModel quote,
            bool doShare,
            string? contextProfileName);

        /// <summary>In process status.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsInProcessAsync(int id, string? contextProfileName);

        /// <summary>Processed status.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsProcessedAsync(int id, string? contextProfileName);

        /// <summary>Approved status.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsApprovedAsync(int id, string? contextProfileName);

        /// <summary>Rejected status.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsRejectedAsync(int id, string? contextProfileName);

        /// <summary>Void status.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRecordAsVoidAsync(int id, string? contextProfileName);

        /// <summary>Award line item.</summary>
        /// <param name="originalItemID">    Identifier for the original item.</param>
        /// <param name="responseItemID">    Identifier for the response item.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> AwardLineItemAsync(int originalItemID, int responseItemID, string? contextProfileName);

        /// <summary>Convert quote to order and mark the quote as Approved.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new order (and optionally invoice) identifier(s) wrapped in a
        /// <seealso cref="CEFActionResponse" />.</returns>
        Task<CEFActionResponse<(int orderID, int? invoiceID)>> ConvertQuoteToOrderAsync(int id, string? contextProfileName);
    }
}
