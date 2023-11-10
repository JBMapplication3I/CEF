// <copyright file="IPaymentWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IWalletWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Interfaces.Providers.Payments;

    /// <summary>Interface for wallet workflow.</summary>
    public partial interface IPaymentWorkflow
    {
        /// <summary>Retrieves a payment provider authentication token.</summary>
        /// <param name="paymentsProviderName">The payments provider to retrieve the token from.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A payment provider authentication token.</returns>
        Task<CEFActionResponse<string>> GetPaymentsProviderAuthenticationTokenAsync(
            string paymentsProviderName,
            string? contextProfileName);

        /// <summary>Gets the Payment Transactions.</summary>
        /// <param name="startDate">The startDate.</param>
        /// <param name="endDate">The endDate.</param>
        /// <param name="transactionID">The transactionID.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Payment Transaction Response.</returns>
        Task<CEFActionResponse<ITransactionResponse?>> GetPaymentTransactionsReportAsync(
            DateTime? startDate,
            DateTime? endDate,
            string? transactionID,
            string? contextProfileName);
    }
}
