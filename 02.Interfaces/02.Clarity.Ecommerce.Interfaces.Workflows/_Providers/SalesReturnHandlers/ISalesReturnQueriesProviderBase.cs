// <copyright file="ISalesReturnQueriesProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesReturnQueriesProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.SalesReturnHandlers.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for sales return queries provider.</summary>
    public interface ISalesReturnQueriesProviderBase : IProviderBase
    {
        /// <summary>Validates the sales order ready for return.</summary>
        /// <param name="salesOrderID">      Identifier for the sales order.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="isBackendOverride"> True if is backend override (to ignore status and created date requirements).</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ValidateSalesOrderReadyForReturnAsync(
            int salesOrderID,
            string? contextProfileName,
            bool isBackendOverride = false);

        /// <summary>Validates the sales order ready for return.</summary>
        /// <param name="salesOrderStatusKey">  The sales order status key.</param>
        /// <param name="salesOrderCreatedDate">The sales order created date.</param>
        /// <param name="isBackendOverride">    True if is backend override (to ignore status and created date
        ///                                     requirements).</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ValidateSalesOrderReadyForReturnAsync(
            string salesOrderStatusKey,
            DateTime salesOrderCreatedDate,
            bool isBackendOverride,
            string? contextProfileName);

        /// <summary>Validates the sales return.</summary>
        /// <param name="salesReturnModel">  The sales return model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="isBackendOverride"> True if is backend override (to ignore status and created date requirements).</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ValidateSalesReturnAsync(
            ISalesReturnModel salesReturnModel,
            string? contextProfileName,
            bool isBackendOverride = false);

        /// <summary>>Gets the sales return only if the supplied AccountID exists on the record.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="accountIDs">        The account IDs.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The Sales Return requested that is confirmed by the supplied account id.</returns>
        Task<ISalesReturnModel> GetRecordSecurelyAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName);
    }
}
