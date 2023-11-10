// <copyright file="CurrencyCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the currency workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Models;
    using Utilities;

    public partial class CurrencyWorkflow
    {
        /// <inheritdoc/>
        public async Task<decimal> ConvertAsync(string keyA, string keyB, decimal value, string? contextProfileName)
        {
            var currencyA = await CheckExistsAsync(keyA, contextProfileName).ConfigureAwait(false);
            if (currencyA == null)
            {
                throw new ArgumentException($"Could not find a currency for {keyA}");
            }
            var currencyB = await CheckExistsAsync(keyB, contextProfileName).ConfigureAwait(false);
            if (currencyB == null)
            {
                throw new ArgumentException($"Could not find a currency for {keyB}");
            }
            var provider = RegistryLoaderWrapper.GetCurrencyConversionProvider(contextProfileName);
            if (provider == null)
            {
                // If no currency conversion provider is available, then assume we don't do
                // conversions and just return the original value
                return value;
            }
            return (decimal)await provider.ConvertAsync(
                    keyA,
                    keyB,
                    (double)value,
                    DateExtensions.GenDateTime,
                    contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Currency? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            if (context.CurrencyImages != null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.CurrencyImages.Count(x => x.MasterID == entity.ID);)
                {
                    context.CurrencyImages.Remove(context.CurrencyImages.First(x => x.MasterID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }
    }
}
