// <copyright file="AccountPricePointCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account price point workflow class</summary>
#pragma warning disable 1574, 1584
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class AccountPricePointWorkflow
    {
        /// <inheritdoc/>
        public async Task<IAccountPricePointModel?> GetAsync(
            (string accountKey, string pricePointKey) keys,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetAsync(keys, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public Task<IAccountPricePointModel?> GetAsync(
            (string accountKey, string pricePointKey) keys,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                context.AccountPricePoints
                    .FilterByActive(true)
                    .FilterIAmARelationshipTableByMasterKey<AccountPricePoint, Account, PricePoint>(Contract.RequiresValidKey(keys.accountKey))
                    .FilterIAmARelationshipTableBySlaveKey<AccountPricePoint, Account, PricePoint>(Contract.RequiresValidKey(keys.pricePointKey))
                    .SelectSingleFullAccountPricePointAndMapToAccountPricePointModel(context.ContextProfileName));
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeactivateAsync(
            (string accountKey, string pricePointKey) keys,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DeactivateAsync(keys, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeactivateAsync(
            (string accountKey, string pricePointKey) keys,
            IClarityEcommerceEntities context)
        {
            return await DeactivateAsync(
                    await context.AccountPricePoints
                        .FilterIAmARelationshipTableByMasterKey<AccountPricePoint, Account, PricePoint>(Contract.RequiresValidKey(keys.accountKey))
                        .FilterIAmARelationshipTableBySlaveKey<AccountPricePoint, Account, PricePoint>(Contract.RequiresValidKey(keys.pricePointKey))
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ReactivateAsync(
            (string accountKey, string pricePointKey) keys,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ReactivateAsync(keys, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ReactivateAsync(
            (string accountKey, string pricePointKey) keys,
            IClarityEcommerceEntities context)
        {
            return await ReactivateAsync(
                    await context.AccountPricePoints
                        .FilterIAmARelationshipTableByMasterKey<AccountPricePoint, Account, PricePoint>(Contract.RequiresValidKey(keys.accountKey))
                        .FilterIAmARelationshipTableBySlaveKey<AccountPricePoint, Account, PricePoint>(Contract.RequiresValidKey(keys.pricePointKey))
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeleteAsync(
            (string accountKey, string pricePointKey) keys,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DeleteAsync(keys, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeleteAsync(
            (string accountKey, string pricePointKey) keys,
            IClarityEcommerceEntities context)
        {
            return await DeleteAsync(
                    await context.AccountPricePoints
                        .FilterIAmARelationshipTableByMasterKey<AccountPricePoint, Account, PricePoint>(Contract.RequiresValidKey(keys.accountKey))
                        .FilterIAmARelationshipTableBySlaveKey<AccountPricePoint, Account, PricePoint>(Contract.RequiresValidKey(keys.pricePointKey))
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }
    }
}
