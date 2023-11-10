// <copyright file="PaymentCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Models;
    using Utilities;

    public partial class PaymentWorkflow
    {
        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> CreateAsync(
            IPaymentModel model,
            string? contextProfileName)
        {
            Contract.RequiresInvalidID(Contract.RequiresNotNull(model).ID);
            if (!OverrideDuplicateCheck)
            {
                // This will throw if it finds another entity with this model's key
                await DuplicateCheckAsync(model, contextProfileName).ConfigureAwait(false);
            }
            var entity = await CreateEntityWithoutSavingAsync(model, model.CreatedDate, contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.Payments.Add(entity.Result!);
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                throw new System.IO.InvalidDataException(
                    $"Something about creating '{model.GetType().FullName}' and saving it failed");
            }
            // Pull the entity fresh from the database and return it
            return entity.Result!.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<string>> GetPaymentsProviderAuthenticationTokenAsync(
            string paymentsProviderName,
            string? contextProfileName)
        {
            foreach (var paymentsProvider in RegistryLoaderWrapper.GetPaymentsProviders(contextProfileName))
            {
                if (paymentsProviderName == paymentsProvider.GetType().Name)
                {
                    return await paymentsProvider.GetAuthenticationToken(contextProfileName).ConfigureAwait(false);
                }
            }
            return CEFAR.FailingCEFAR<string>("The payments provider was not found.");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<ITransactionResponse?>> GetPaymentTransactionsReportAsync(
            DateTime? startDate,
            DateTime? endDate,
            string? transactionID,
            string? contextProfileName)
        {
            var provider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            if (provider == null || !provider.HasValidConfiguration)
            {
                return CEFAR.FailingCEFAR<ITransactionResponse?>(
                    "No Payment Provider Configured");
            }
            if (provider is not ITransactionProviderBase transactionProvider)
            {
                return CEFAR.FailingCEFAR<ITransactionResponse?>(
                    "The Payment Provider Configured is not a Transaction Provider");
            }
            var transactionResponse = await transactionProvider.ExportTransactionsAsync(
                    startDate,
                    endDate,
                    transactionID)
                .ConfigureAwait(false);
            return transactionResponse.WrapInPassingCEFAR();
        }
    }
}
