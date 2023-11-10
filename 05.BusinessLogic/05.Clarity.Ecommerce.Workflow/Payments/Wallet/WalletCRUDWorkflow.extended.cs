// <copyright file="WalletCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the wallet workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using JSConfigs;
    using Mapper;
    using Models;
    using Newtonsoft.Json;
    using Utilities;

    public partial class WalletWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse<List<IWalletModel>>> GetWalletForUserAsync(
            int userID,
            IWalletProviderBase provider,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var wallets = context.Wallets
                .AsNoTracking()
                .FilterByActive(true)
                .Where(w => w.UserID == userID && w.User!.Active)
                .SelectLiteWalletAndMapToWalletModel(contextProfileName)
                .ToList();
            try
            {
                if (provider != null && CEFConfigDictionary.UseProviderGetWallet)
                {
                    var user = await context.Users
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(userID)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false);
                    var attrName = CEFConfigDictionary.WalletAccountNumberSerializableAttributeName;
                    var walletAccountNumber = user != null
                        && Contract.CheckValidKey(attrName)
                        && user.SerializableAttributes.ContainsKey(attrName!)
                        ? user.SerializableAttributes[attrName!].Value
                        : string.Empty;
                    var response = await provider.GetCustomerProfilesAsync(
                            walletAccountNumber,
                            contextProfileName)
                        .ConfigureAwait(false);
                    wallets.AddRange(response.Select(x => new WalletModel
                    {
                        Active = true,
                        AccountNumber = x.Customer,
                        Token = x.Token,
                        CardType = x.CardType,
                        CreditCardNumber = x.Account,
                    }));
                }
            }
            catch
            {
                // Do Nothing
            }
            return wallets.WrapInPassingCEFARIfNotNullOrEmpty<List<IWalletModel>, IWalletModel>();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IWalletModel>> GetWalletEntryForUserAsync(
            int userID,
            int entryID,
            IWalletProviderBase provider,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entry = context.Wallets
                .AsNoTracking()
                .FilterByActive(true)
                .Where(w => w.UserID == userID && w.User!.Active)
                .FilterByID(entryID)
                .SelectLiteWalletAndMapToWalletModel(contextProfileName)
                .FirstOrDefault();
            /*
            try
            {
                if (provider != null && CEFConfigDictionary.UseProviderGetWallet)
                {
                    var user = context.Users
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(userID)
                        .FirstOrDefault();
                    var walletAccountNumber = user != null
                        && Contract.CheckValidKey(CEFConfigDictionary.WalletAccountNumberSerializableAttributeName)
                        && user.SerializableAttributes.ContainsKey(
                            CEFConfigDictionary.WalletAccountNumberSerializableAttributeName)
                        ? user.SerializableAttributes[CEFConfigDictionary.WalletAccountNumberSerializableAttributeName].Value
                        : string.Empty;
                    var response = await provider.GetCustomerProfilesAsync(
                            walletAccountNumber,
                            contextProfileName)
                        .ConfigureAwait(false);
                    entry = response
                        .Select(x => new WalletModel
                        {
                            Active = true,
                            AccountNumber = x.Customer,
                            Token = x.Token,
                            CardType = x.CardType,
                            CreditCardNumber = x.Account,
                        })
                        .FirstOrDefault(x => x.Token == entry.Token);
                }
            }
            catch
            {
                // Do Nothing
            }
            */
            return entry.WrapInPassingCEFARIfNotNull("Could not find wallet entry");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IWalletModel>> GetDecryptedWalletAsync(
            int userID,
            int walletID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(userID);
            Contract.RequiresValidID(walletID);
            // Retrieve the cards in the wallet of the user
            var wallet = await GetAsync(walletID, contextProfileName).ConfigureAwait(false);
            if (wallet == null)
            {
                return CEFAR.FailingCEFAR<IWalletModel>("Unable to locate the referenced wallet");
            }
            if (wallet.UserID != userID)
            {
                return CEFAR.FailingCEFAR<IWalletModel>(
                    "The referenced wallet entry does not belong to the referenced user");
            }
            return wallet.WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IWalletModel>> CreateWalletEntryAsync(
            IWalletModel model,
            IWalletProviderBase provider,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(model);
            Contract.RequiresInvalidID(model.ID);
            // ReSharper disable HeuristicUnreachableCode
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (!Contract.CheckValidID(model.UserID))
            {
                return CEFAR.FailingCEFAR<IWalletModel>("ERROR! UserID is required");
            }
            if (Contract.CheckValidKey(model.CreditCardNumber))
            {
                if (model.CreditCardNumber!.Length < 15)
                {
                    return CEFAR.FailingCEFAR<IWalletModel>(
                        "ERROR! A valid Credit Card Number is required for Credit Cards");
                }
                if (!Contract.CheckValidKey(model.CardHolderName))
                {
                    return CEFAR.FailingCEFAR<IWalletModel>(
                        "ERROR! A valid Card Holder Name is required for Credit Cards");
                }
                if (!model.ExpirationMonth.HasValue)
                {
                    return CEFAR.FailingCEFAR<IWalletModel>(
                        "ERROR! A valid Expiration Month is required for Credit Cards");
                }
                if (!model.ExpirationYear.HasValue)
                {
                    return CEFAR.FailingCEFAR<IWalletModel>(
                        "ERROR! A valid Expiration Year is required for Credit Cards");
                }
            }
            else if (Contract.CheckValidKey(model.RoutingNumber))
            {
                if (!Contract.CheckValidKey(model.AccountNumber))
                {
                    return CEFAR.FailingCEFAR<IWalletModel>("ERROR! A valid Account Number is required for eChecks");
                }
                if (!Contract.CheckValidKey(model.BankName))
                {
                    return CEFAR.FailingCEFAR<IWalletModel>("ERROR! A valid Bank Name is required for eChecks");
                }
                if (!Contract.CheckValidKey(model.CardType))
                {
                    return CEFAR.FailingCEFAR<IWalletModel>("ERROR! A valid Account Type is required for eChecks");
                }
                if (!Contract.CheckValidKey(model.CardHolderName))
                {
                    return CEFAR.FailingCEFAR<IWalletModel>(
                        "ERROR! A valid Account Holder Name is required for eChecks");
                }
            }
            else
            {
                return CEFAR.FailingCEFAR<IWalletModel>("ERROR! Either a Credit Card Number or Routing Number is required");
            }
            var user = await Workflows.Users.GetAsync(model.UserID, context).ConfigureAwait(false);
            if (user == null)
            {
                return CEFAR.FailingCEFAR<IWalletModel>("Unable to locate the referenced user");
            }
            var timestamp = DateExtensions.GenDateTime;
            var newEntity = (Wallet)model.CreateWalletEntity(timestamp, contextProfileName);
            await AssignAdditionalPropertiesAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            // Only ever store the last 4 digits unencrypted
            newEntity.CreditCardNumber = newEntity.CreditCardNumber?[^4..];
            // Send the full card number to the payment provider for conversion to a token
            var payment = new PaymentModel
            {
                ReferenceName = model.Name,
                CardHolderName = model.CardHolderName,
                CardNumber = model.CreditCardNumber,
                ExpirationMonth = model.ExpirationMonth,
                ExpirationYear = model.ExpirationYear,
                AccountNumber = model.AccountNumber,
                RoutingNumber = model.RoutingNumber,
                BankName = model.BankName,
                CardType = model.CardType,
                Token = model.Token,
                Amount = 0m,
            };
            var response = await provider.CreateCustomerProfileAsync(payment, user.Contact!, context.ContextProfileName).ConfigureAwait(false);
            if (!response.Approved)
            {
                return CEFAR.FailingCEFAR<IWalletModel>("The payment provider was unable to create the profile");
            }
            newEntity.Token = response.Token;
            CaptureBIN(model, newEntity);
            context.Wallets.Add(newEntity);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            return (await GetAsync(newEntity.ID, contextProfileName).ConfigureAwait(false))
                .WrapInPassingCEFARIfNotNull();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IWalletModel>> UpdateWalletEntryAsync(
            IWalletModel model,
            IWalletProviderBase provider,
            string? contextProfileName)
        {
            Contract.RequiresValidID(model?.ID);
            if (!Contract.CheckValidID(model!.UserID))
            {
                return CEFAR.FailingCEFAR<IWalletModel>("ERROR! UserID is required");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = context.Wallets.SingleOrDefault(w => w.ID == model.ID);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR<IWalletModel>("Unable to locate referenced wallet");
            }
            if (entity.UserID != model.UserID)
            {
                return CEFAR.FailingCEFAR<IWalletModel>(
                    "The referenced wallet entry does not belong to the referenced user");
            }
            var user = await Workflows.Users.GetAsync(model.UserID, contextProfileName).ConfigureAwait(false);
            if (user == null)
            {
                return CEFAR.FailingCEFAR<IWalletModel>("Unable to locate the referenced user");
            }
            var timestamp = DateExtensions.GenDateTime;
            entity.UpdatedDate = timestamp;
            entity.Name = model.Name;
            entity.Description = model.Description;
            var payment = new PaymentModel
            {
                ReferenceName = model.Name,
                CardHolderName = model.CardHolderName,
                CardNumber = model.CreditCardNumber,
                AccountNumber = model.AccountNumber,
                RoutingNumber = model.RoutingNumber,
                BankName = model.BankName,
                CardType = model.CardType,
                ExpirationMonth = model.ExpirationMonth,
                ExpirationYear = model.ExpirationYear,
                Token = model.Token,
            };
            try
            {
                var response = await provider.UpdateCustomerProfileAsync(payment, user.Contact!, contextProfileName).ConfigureAwait(false);
                if (response.Approved)
                {
                    entity.Token = response.Token;
                    CaptureBIN(model, entity);
                }
                else
                {
                    return CEFAR.FailingCEFAR<IWalletModel>(
                        "The payment provider was unable to update the profile");
                }
            }
            catch (NotSupportedException)
            {
                return CEFAR.FailingCEFAR<IWalletModel>(
                    "The payment provider does not support updating profiles, you must remove the older entry and add a new one");
            }
            await AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return (await GetAsync(entity.ID, contextProfileName).ConfigureAwait(false)).WrapInPassingCEFARIfNotNull();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetWalletEntryAsDefaultAsync(
            int userID,
            int entryID,
            IWalletProviderBase provider,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Wallets
                .FilterByActive(true)
                .Where(w => w.UserID == userID && w.User!.Active)
                .FilterByID(entryID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR("Cannot locate entry");
            }
            if (entity.IsDefault)
            {
                // Already set
                return CEFAR.PassingCEFAR();
            }
            var otherEntities = await context.Wallets
                .FilterByActive(true)
                .Where(w => w.UserID == userID && w.User!.Active && w.IsDefault)
                .FilterByExcludedID(entryID)
                .ToListAsync()
                .ConfigureAwait(false);
            var timestamp = DateExtensions.GenDateTime;
            entity.UpdatedDate = timestamp;
            entity.IsDefault = true;
            foreach (var otherEntity in otherEntities)
            {
                otherEntity.UpdatedDate = timestamp;
                otherEntity.IsDefault = false;
            }
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR("Error: Something about saving the data failed");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeactivateWalletEntryAsync(
            int id, IWalletProviderBase provider, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = context.Wallets.SingleOrDefault(w => w.ID == id);
            if (entity?.Active != true)
            {
                return CEFAR.PassingCEFAR();
            }
            var payment = new PaymentModel
            {
                ReferenceName = entity.Name,
                CardNumber = entity.CreditCardNumber,
                ExpirationMonth = entity.ExpirationMonth,
                ExpirationYear = entity.ExpirationYear,
                Token = entity.Token,
            };
            try
            {
                var response = await provider.DeleteCustomerProfileAsync(payment, contextProfileName).ConfigureAwait(false);
                if (!response.Approved)
                {
                    return CEFAR.FailingCEFAR("The payment provider was unable to delete the profile.");
                }
            }
            catch (NotSupportedException)
            {
                return CEFAR.FailingCEFAR(
                    "The payment provider does not support deleting profiles, please contact support for assistance");
            }
            entity.UpdatedDate = DateExtensions.GenDateTime;
            entity.Active = false;
            entity.Token = string.Empty;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <summary>Capture the BIN (bank identification number) from a credit card if permissible.</summary>
        /// <param name="model">The wallet of the card.</param>
        /// <param name="entity">Also the wallet of the card.</param>
        private static void CaptureBIN(IWalletModel model, Wallet entity)
        {
            if (CEFConfigDictionary.CaptureBINEnabled
                && model.CreditCardNumber is { } ccn
                && Contract.CheckValidKey(ccn)
                && ccn.Length > 8)
            {
                var attributes = entity.SerializableAttributes ?? new();
                attributes["BIN"] = new()
                {
                    Key = "BIN",
                    Value = ccn.Length switch
                    {
                        // See https://pcissc.secure.force.com/faq/articles/Frequently_Asked_Question/What-are-acceptable-formats-for-truncation-of-primary-account-numbers
                        16 => ccn[..8],
                        15 => ccn[..6], // Amex
                        < 15 => ccn[..6], // Discover
                        _ => string.Empty,
                    },
                };
                if (attributes["BIN"].Value is "")
                {
                    // No need to clean up attributes as it was only a temporary object created when
                    // entity.SerializableAttributes.get is invoked
                    return;
                }
                entity.JsonAttributes = attributes.SerializeAttributesDictionary();
            }
            // Don't allow a BIN to linger on a wallet entry that turned CC -> else.
            else if (entity.SerializableAttributes.ContainsKey("BIN"))
            {
                var attributes = entity.SerializableAttributes;
                _ = attributes.TryRemove("BIN", out _);
                entity.JsonAttributes = attributes.SerializeAttributesDictionary();
            }
        }
    }
}
