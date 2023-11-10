// <copyright file="ITransactionProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITransactionProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Interface for transaction provider base.</summary>
    public interface ITransactionProviderBase
    {
        /// <summary>Exports Payment Transactions.</summary>
        /// <param name="startDate">        The startDate.</param>
        /// <param name="endDate">          The endDate.</param>
        /// <param name="transactionID">    The transactionID.</param>
        /// <returns>The Transaction Response.</returns>
        Task<ITransactionResponse> ExportTransactionsAsync(
            DateTime? startDate,
            DateTime? endDate,
            string? transactionID);
    }
}
