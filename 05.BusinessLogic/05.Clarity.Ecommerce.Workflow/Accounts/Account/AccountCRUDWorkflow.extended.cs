// <copyright file="AccountCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the account workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class AccountWorkflow
    {
        /// <inheritdoc/>
        public async Task<int?> CheckExistsByKeyAsync(string customKey, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Accounts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByCustomKey(customKey)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int?> GetIDByUserIDAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Accounts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterAccountsByUserID(Contract.RequiresValidID(userID))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public async Task<int?> GetIDByUserNameAsync(string username, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Accounts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterAccountsByUserUserName(Contract.RequiresValidKey(username))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public async Task<IAccountModel?> GetByUserIDAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var id = await context.Accounts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterAccountsByUserID(Contract.RequiresValidID(userID))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(true);
            return Contract.CheckValidID(id)
                ? await GetAsync(id!.Value, contextProfileName).ConfigureAwait(false)
                : null;
        }

        /// <inheritdoc/>
        public async Task<IAccountModel?> GetByUserNameAsync(string username, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var id = await context.Accounts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterAccountsByUserUserName(Contract.RequiresValidKey(username))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(true);
            return id.HasValue ? await GetAsync(id.Value, contextProfileName).ConfigureAwait(false) : null;
        }

        /// <inheritdoc/>
        public async Task<IAccountModel?> GetByUserNameOrEmailAsync(
            string userNameOrEmail,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var id = await context.Accounts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterAccountsByUserUserNameOrEmail(Contract.RequiresValidKey(userNameOrEmail))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            return id.HasValue ? await GetAsync(id.Value, contextProfileName).ConfigureAwait(false) : null;
        }

        /// <inheritdoc/>
        public async Task<IAccountModel?> GetForPricingByUserIDAsync(
            int userID,
            int affAccountID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.Accounts
                .AsNoTracking()
                .FilterByActive(true);
            query = Contract.CheckValidID(affAccountID)
               ? query.FilterByID(affAccountID)
               : query.FilterAccountsByUserID(userID);
            return (await query
                    .Select(x => new
                    {
                        x.ID,
                        x.CustomKey,
                        x.TypeID,
                        AccountPricePoints = x.AccountPricePoints!
                            .Where(y => y.Active && y.Slave!.Active)
                            .Select(y => new
                            {
                                y.ID,
                                y.Active,
                                SlaveKey = y.Slave!.CustomKey,
                            }),
                    })
                    .Take(1)
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new AccountModel
                {
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    TypeID = x.TypeID,
                    AccountPricePoints = x.AccountPricePoints
                        .Select(y => new AccountPricePointModel
                        {
                            ID = y.ID,
                            SlaveKey = y.SlaveKey,
                            Active = y.Active,
                        })
                        .ToList(),
                })
                .SingleOrDefault();
        }

        /// <inheritdoc/>
        public async Task<IAccountModel?> GetForPricingByUserNameAsync(
            string userName,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.Accounts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterAccountsByUserUserName(userName)
                    .Select(x => new
                    {
                        x.ID,
                        x.CustomKey,
                        x.TypeID,
                        AccountPricePoints = x.AccountPricePoints!
                            .Where(y => y.Active && y.Slave!.Active)
                            .Select(y => new
                            {
                                y.ID,
                                y.Active,
                                SlaveKey = y.Slave!.CustomKey,
                            }),
                    })
                    .Take(1)
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new AccountModel
                {
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    TypeID = x.TypeID,
                    AccountPricePoints = x.AccountPricePoints
                        .Select(y => new AccountPricePointModel
                        {
                            ID = y.ID,
                            SlaveKey = y.SlaveKey,
                            Active = y.Active,
                        })
                        .ToList(),
                })
                .SingleOrDefault();
        }

        /// <inheritdoc/>
        public async Task<IAccountModel?> GetForTaxesAsync(int? accountID, string? contextProfileName)
        {
            if (Contract.CheckInvalidID(accountID))
            {
                return null;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.Accounts
                    .AsNoTracking()
                    .FilterByID(accountID)
                    .Select(x => new
                    {
                        x.ID,
                        x.IsTaxable,
                        x.TaxEntityUseCode,
                        x.TaxExemptionNo,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new AccountModel
                {
                    ID = x.ID,
                    IsTaxable = x.IsTaxable,
                    TaxEntityUseCode = x.TaxEntityUseCode,
                    TaxExemptionNo = x.TaxExemptionNo,
                })
                .Single();
        }

        /// <inheritdoc/>
        public async Task<IAccountModel?> GetForCartValidatorAsync(int accountID, string? contextProfileName)
        {
            if (Contract.CheckInvalidID(accountID))
            {
                return null;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.Accounts
                    .AsNoTracking()
                    .FilterByID(accountID)
                    .Select(x => new
                    {
                        x.ID,
                        x.IsOnHold,
                        TypeName = x.Type!.Name,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new AccountModel
                {
                    ID = x.ID,
                    IsOnHold = x.IsOnHold,
                    TypeName = x.TypeName,
                })
                .Single();
        }

        /// <inheritdoc/>
        public async Task<string?> GetTypeNameForAccountAsync(int accountID, string? contextProfileName)
        {
            Contract.RequiresValidID(accountID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Accounts
                .AsNoTracking()
                .FilterByID(accountID)
                .Select(x => x.Type!.Name)
                .SingleAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IAccountModel?> FindExistingAccountAsync(
            string? accountName,
            string? city,
            int? regionID,
            string? contextProfileName)
        {
            // Something has to be valid
            Contract.RequiresValidIDOrAnyValidKey(regionID, city, accountName);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var id = await context.Accounts
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByName(accountName)
                .FilterAccountsByRegionID(regionID)
                .FilterAccountsByCity(city)
                .Select(x => (int?)x.ID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return id.HasValue ? await GetAsync(id.Value, contextProfileName).ConfigureAwait(false) : null;
        }

        /// <inheritdoc/>
        public async Task<int?> GetAccountIDByAttributeValueAsync(
            string attrName,
            string value,
            string? contextProfileName)
        {
            var model = new AccountSearchModel
            {
                JsonAttributes = new() { [attrName] = new[] { value } },
            };
            return (await SearchAsync(model, true, contextProfileName).ConfigureAwait(false))
                .results
                .FirstOrDefault()
                ?.ID;
        }

        /// <inheritdoc/>
        public async Task<(List<IUserModel> results, int totalPages, int totalCount)> GetUsersForCurrentAccountAsync(
            IUserSearchModel search,
            string? contextProfileName)
        {
            var idList = await Workflows.Users.SearchAsIDListAsync(search, contextProfileName).ConfigureAwait(false);
            if (idList.Count == 0)
            {
                return (new(), 0, 0);
            }
            var totalCount = idList.Count;
            var totalPages = 1;
            if (search.Paging?.Size != null
                && search.Paging.StartIndex != null
                && search.Paging.Size > 0
                && search.Paging.StartIndex >= 0)
            {
                idList = idList
                    .Skip(search.Paging.Size.Value * (search.Paging.StartIndex.Value - 1))
                    .Take(search.Paging.Size.Value)
                    .ToList();
                totalPages = (int)Math.Ceiling(totalCount / (double)search.Paging.Size.Value);
            }
            var search2 = new UserSearchModel
            {
                Active = true,
                IDs = idList.ToArray(),
                Sorts = search.Sorts,
            };
            var (users, _, _) = await Workflows.Users.SearchAsync(
                    search2,
                    true,
                    contextProfileName: null)
                .ConfigureAwait(false);
            return (users, totalPages, totalCount);
        }

        /// <inheritdoc/>
        public async Task<bool> CheckCanEmulateAccountForCurrentUserAsync(int userID, int accountID, string? contextProfileName)
        {
            return true;
        }

        public override async Task<IAccountModel?> GetAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var result = await GetAsync(id, context).ConfigureAwait(false);
            if (Contract.CheckNotNull(result))
            {
                result!.Users = null;
            }
            return result;
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Account>> FilterQueryByModelCustomAsync(
            IQueryable<Account> query,
            IAccountSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterAccountsByAccessibleFromAccountID(search.AccessibleFromAccountID)
                .FilterAccountsByUserUserName(search.UserUsername);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IAccount entity,
            IAccountModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateAccountFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Related Objects
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.Type?.ID ?? model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.Type?.CustomKey,
                    model.Type?.Name))
            {
                model.TypeKey = "Customer";
            }
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.Status?.ID ?? model.StatusID,
                    model.StatusKey,
                    model.StatusName,
                    model.Status?.CustomKey,
                    model.Status?.Name))
            {
                model.StatusKey = "NORMAL";
            }
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            // Associated Objects
            if (Contract.CheckEmpty(model.AccountContacts) && Contract.CheckValidID(model.ID))
            {
                model.AccountContacts = await Workflows.AddressBooks.GetAddressBookAsync(
                        model.ID,
                        context.ContextProfileName)
                    .ConfigureAwait(false);
            }
            else if (Contract.CheckNotEmpty(model.AccountContacts))
            {
                var accountContacts = model.AccountContacts!.ToList();
                foreach (var ac in accountContacts.Where(x => x?.Contact?.Address != null))
                {
                    // Clean up the address
                    ac.Contact!.Address = await Workflows.Addresses.ResolveAddressAsync(
                            ac.Contact.Address!,
                            context.ContextProfileName)
                        .ConfigureAwait(false);
                }
                model.AccountContacts = accountContacts;
            }
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Account? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            if (context.AccountImages != null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.AccountImages.Count(x => x.MasterID == entity.ID);)
                {
                    context.AccountImages.Remove(context.AccountImages.First(x => x.MasterID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.AccountAssociations != null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.AccountAssociations.Count(x => x.MasterID == entity.ID || x.SlaveID == entity.ID);)
                {
                    context.AccountAssociations.Remove(context.AccountAssociations.First(x => x.MasterID == entity.ID || x.SlaveID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.Users != null)
            {
                // Must wrap in null check for unit tests
                foreach (var user in context.Users.Where(x => x.AccountID == entity.ID))
                {
                    user.AccountID = null;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            if (context.SalesOrders != null)
            {
                // Must wrap in null check for unit tests
                foreach (var order in context.SalesOrders.Where(x => x.AccountID == entity.ID))
                {
                    order.AccountID = null;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            if (context.SalesQuotes != null)
            {
                // Must wrap in null check for unit tests
                foreach (var quote in context.SalesQuotes.Where(x => x.AccountID == entity.ID))
                {
                    quote.AccountID = null;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            if (context.SalesInvoices != null)
            {
                // Must wrap in null check for unit tests
                foreach (var invoice in context.SalesInvoices.Where(x => x.AccountID == entity.ID))
                {
                    invoice.AccountID = null;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            if (context.SampleRequests != null)
            {
                // Must wrap in null check for unit tests
                foreach (var request in context.SampleRequests.Where(x => x.AccountID == entity.ID))
                {
                    request.AccountID = null;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            // ReSharper disable once InvertIf
            if (context.Carts != null)
            {
                // Must wrap in null check for unit tests
                foreach (var cart in context.Carts.Where(x => x.AccountID == entity.ID))
                {
                    cart.AccountID = null;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            // Delete the account itself
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }
    }
}
