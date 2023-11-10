// <copyright file="PayTracePaymentsProvider.Transactions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace payments provider transactions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers.Payments;
    using Models;
    using Utilities;

    public partial class PayTracePaymentsProvider : ITransactionProviderBase
    {
        /// <inheritdoc/>
        public async Task<ITransactionResponse> ExportTransactionsAsync(
            DateTime? startDate,
            DateTime? endDate,
            string? transactionID)
        {
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new TransactionResponse { Success = false, StatusMessage = oAuthResult?.Error?.Error };
            }
            var request = new TransactionRequest
            {
                TransactionID = transactionID,
                StartDate = startDate,
                EndDate = endDate,
                IntegratorID = PayTracePaymentsProviderConfig.IntegratorId,
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlExportTransactions,
                    Contract.RequiresValidKey(oAuthResult.AccessToken),
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<TransactionResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            return response;
        }
    }
}
