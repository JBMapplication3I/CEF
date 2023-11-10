// <copyright file="UserCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using LinqKit;
    using Mapper;
    using Models;
    using MoreLinq;
    using Providers.Emails;
    using Utilities;

    public partial class UserWorkflow
    {
        static UserWorkflow()
        {
            // Ensure Hooks as needed
            try
            {
                if (CEFConfigDictionary.EmailNotificationsUserProfileUpdatedToBackOfficeByEmailOnSave)
                {
                    OnRecordUpdatedAsyncHook = (model, context)
                        => new AuthenticationUserAccountProfileUpdatedNotificationToBackOfficeEmail().QueueAsync(
                            contextProfileName: context.ContextProfileName,
                            to: model.Email,
                            parameters: new() { ["username"] = model.UserName });
                }
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <inheritdoc/>
        public override async Task<IUserModel?> GetAsync(int id, IClarityEcommerceEntities context)
        {
            return CleanModel(await base.GetAsync(id, context).ConfigureAwait(false));
        }

        /// <inheritdoc/>
        public override Task<IUserModel?> GetAsync(string username, IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                CleanModel(
                    context.Users
                        .FilterByActive(true)
                        .FilterUsersByUserNameOrCustomKeyOrEmail(Contract.RequiresValidKey(username), true)
                        .SelectSingleFullUserAndMapToUserModel(context.ContextProfileName)));
        }

        /// <inheritdoc/>
        public Task<IUserModel?> GetByKeyAsync(string customKey, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                CleanModel(
                    context.Users
                        .FilterByActive(true)
                        .FilterByCustomKey(Contract.RequiresValidKey(customKey), true)
                        .SelectSingleFullUserAndMapToUserModel(contextProfileName)));
        }

        /// <inheritdoc/>
        public Task<IUserModel?> GetLiteByKeyAsync(string customKey, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                CleanModel(
                    context.Users
                        .FilterByActive(true)
                        .FilterByCustomKey(Contract.RequiresValidKey(customKey), true)
                        .SelectSingleLiteUserAndMapToUserModel(contextProfileName)));
        }

        /// <inheritdoc/>
        public Task<IUserModel?> GetByEmailAsync(string email, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                CleanModel(
                    context.Users
                        .FilterByActive(true)
                        .FilterUsersByEmail(Contract.RequiresValidKey(email), true)
                        .SelectSingleFullUserAndMapToUserModel(contextProfileName)));
        }

        /// <inheritdoc/>
        public Task<IUserModel?> GetByUserNameAsync(string userName, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByUserName(userName, true)
                    .SelectSingleFullUserAndMapToUserModel(contextProfileName));
        }

        public async Task<List<IUserModel?>?> GetSupervisorsOnAccountForUserAsync(int userID, int currentAccountID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var acctSupervisors = (await context.Accounts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterAccountsByUserID(userID)
                    .SelectMany(u => u.Users)
                    .Where(r => r.Roles.Any(ur => ur.Role!.Name == "Supervisor"))
                    .Distinct()
                    .Select(x => new
                    {
                        x.ID,
                        Contact = x.Contact == null
                            ? null
                            : new
                            {
                                x.Contact.FirstName,
                                x.Contact.LastName,
                            },
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(s => new UserModel
                {
                    ContactFirstName = s.Contact!.FirstName,
                    ContactLastName = s.Contact!.LastName,
                    ID = s.ID,
                })
                .Select(CleanModel)
                .ToList();
            var connectedSupervisors = (await context.Users
                    .Where(x => x.JsonAttributes!.Contains("\"Key\":\"associatedAccounts\""))
                    .Select(y => new
                    {
                        y.ID,
                        y.Contact!.FirstName,
                        y.Contact.LastName,
                        y.JsonAttributes,
                    })
                    .ToListAsync()
                .ConfigureAwait(false))
                .Select(x => new UserModel
                {
                    ID = x.ID,
                    ContactFirstName = x.FirstName,
                    ContactLastName = x.LastName,
                    SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary(),
                })
                .Select(CleanModel)
                .Where(x => x!.SerializableAttributes
                    .SingleOrDefault(y => y.Key == "associatedAccounts")
                    .Value
                    .ToString()
                    .Contains(currentAccountID.ToString()))
                .ToList();
            return connectedSupervisors.Concat(acctSupervisors).DistinctBy(x => x!.ID).ToList();
        }

        /// <inheritdoc/>
        public async Task<string> GetUsernameForIDAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Users
                .AsNoTracking()
                .FilterByID(id)
                .Select(x => x.UserName!)
                .SingleAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IUserForPricingModel?> GetForPricingAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var timestamp = DateExtensions.GenDateTime;
            return (await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(id)
                    .Take(1)
                    .Select(x => new
                    {
                        x.ID,
                        x.CustomKey,
                        x.UserName,
                        x.CurrencyID,
                        x.PreferredStoreID,
                        CountryID = x.Contact != null && x.Contact.Address != null
                            ? x.Contact.Address.CountryID
                            : null,
                        Roles = x.Roles
                            .Where(y => !y.EndDate.HasValue || y.EndDate > timestamp)
                            .Select(y => y.Role!.Name),
                        x.JsonAttributes,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x =>
                {
                    var retVal = RegistryLoaderWrapper.GetInstance<IUserForPricingModel>();
                    retVal.ID = x.ID;
                    retVal.CustomKey = x.CustomKey;
                    retVal.UserName = x.UserName;
                    retVal.CurrencyID = x.CurrencyID;
                    retVal.PreferredStoreID = x.PreferredStoreID;
                    retVal.CountryID = x.CountryID;
                    retVal.Roles = x.Roles.ToList();
                    retVal.SerializableAttributes = x.JsonAttributes.DeserializeAttributesDictionary();
                    return retVal;
                })
                .SingleOrDefault();
        }

        /// <inheritdoc/>
        public override Task<int?> CheckExistsAsync(string customKey, IClarityEcommerceEntities context)
        {
            return context.Users
                .AsNoTracking()
                .FilterByActive(true)
                .FilterUsersByUserNameOrCustomKeyOrEmail(Contract.RequiresValidKey(customKey), true)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<int?> CheckExistsByEmailAsync(string email, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Users
                .AsNoTracking()
                .FilterByActive(true)
                .FilterUsersByEmail(Contract.RequiresValidKey(email), true)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int?> CheckExistsByUsernameAsync(string username, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.Users
                .AsNoTracking()
                .FilterByActive(true)
                .FilterUsersByUserName(Contract.RequiresValidKey(username), true)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow, CognitiveComplexity
        public override async Task<CEFActionResponse<int>> CreateAsync(
            IUserModel model,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresInvalidID(model.ID);
            await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            // ReSharper disable once PossibleInvalidOperationException
            if (model.Contact != null)
            {
                if (model.Contact.Address != null)
                {
                    if (Contract.CheckAllInvalidKeys(
                        model.Contact.Address.Street1,
                        model.Contact.Address.Street2,
                        model.Contact.Address.Street3,
                        model.Contact.Address.City,
                        model.Contact.Address.PostalCode))
                    {
                        // Not a valid address, don't try to use it
                        model.Contact.Address = null;
                    }
                    else
                    {
                        model.Contact.Address = await Workflows.Addresses.ResolveAddressAsync(
                                model.Contact.Address,
                                context.ContextProfileName)
                            .ConfigureAwait(false);
                    }
                }
                model.Contact.FullName ??= model.Contact.FirstName + " " + model.Contact.LastName;
            }
            var timestamp = DateExtensions.GenDateTime;
            User entity;
            if (model.Contact != null)
            {
                entity = (User)model.CreateUserEntity(timestamp, context.ContextProfileName);
                entity.Contact = (Contact)model.Contact.CreateContactEntity(timestamp, context.ContextProfileName);
                if (Contract.CheckValidKey(model.Contact.Address?.Street1))
                {
                    entity.Contact.Address = (Address?)model.Contact.Address?.CreateAddressEntity(timestamp, context.ContextProfileName);
                }
                entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, null, context.ContextProfileName);
                entity.DisplayName = model.DisplayName ?? model.Contact.FirstName + " " + model.Contact.LastName;
            }
            else
            {
                var userModel = RegistryLoaderWrapper.GetInstance<IUserModel>(context.ContextProfileName);
                userModel.CustomKey = model.CustomKey;
                userModel.SerializableAttributes = model.SerializableAttributes;
                var contact = userModel.Contact = RegistryLoaderWrapper.GetInstance<IContactModel>(context.ContextProfileName);
                contact.Active = userModel.Contact.Active = true;
                contact.CustomKey = userModel.Contact.CustomKey = model.CustomKey;
                contact.Email1 = userModel.Contact.Email1 = userModel.Email = model.Email;
                // Do the create, but without saving it, this ensure several objects are actually valid so it can go into the db
                userModel = await CreateForIdentityAsync(userModel, context).ConfigureAwait(false);
                // Save it using our stuff
                entity = (User)userModel!.CreateUserEntity(userModel!.CreatedDate, context.ContextProfileName);
                entity.Email = model.Email;
                entity.Contact = (Contact)(userModel.Contact ?? contact).CreateContactEntity(timestamp, context.ContextProfileName);
                if (Contract.CheckValidKey(userModel.Contact?.Address?.Street1))
                {
                    entity.Contact.Address = (Address?)userModel.Contact!.Address!.CreateAddressEntity(timestamp, context.ContextProfileName);
                }
                entity.Contact.AssignPostPropertiesToContactAndAddress(userModel.Contact, timestamp, null, context.ContextProfileName);
            }
            if (entity.Contact != null && !Contract.CheckValidID(entity.Contact.TypeID))
            {
                entity.Contact.TypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: model.Contact?.TypeID,
                        byKey: model.Contact?.TypeKey ?? "User",
                        byName: model.Contact?.TypeName ?? "User",
                        byDisplayName: model.Contact?.TypeDisplayName ?? "User",
                        model: model.Contact?.Type,
                        context: context)
                    .ConfigureAwait(false);
            }
            entity.Active = CEFConfigDictionary.NewUsersAreDefaultActive;
            if (CEFConfigDictionary.CustomUserApproval)
            {
                entity.IsApproved =
                model.SerializableAttributes.TryGetValue("fromFusion", out var fromFusion)
                && Contract.CheckNotNull(fromFusion?.Value)
                && bool.Parse(fromFusion!.Value) is true;
            }
            else
            {
                entity.IsApproved = CEFConfigDictionary.NewUsersAreDefaultApproved;
            }
            entity.LockoutEnabled = CEFConfigDictionary.NewUsersAreDefaultLockedOut;
            entity.EmailConfirmed = !CEFConfigDictionary.RequireEmailVerificationForNewUsers;
            entity.UserName = CEFConfigDictionary.AuthProviderUsernameIsEmail ? model.Email : model.UserName;
            entity.TwoFactorEnabled = CEFConfigDictionary.TwoFactorEnabled;
            entity.RequirePasswordChangeOnNextLogin = model.RequirePasswordChangeOnNextLogin; // Usually when created by an integration
            // TODO@BE: Look at moving this before CreateForIdentity() to prevent double work
            entity = await AssignUserTypeAsync(
                    entity: entity,
                    model: model,
                    defaultKey: "Customer",
                    defaultName: "Customer",
                    defaultDisplayName: "Customer",
                    contextProfileName: context.ContextProfileName)
                .ConfigureAwait(false);
            entity = await AssignUserStatusAsync(
                    entity: entity,
                    model: model,
                    defaultKey: "Registered",
                    defaultName: "Registered",
                    defaultDisplayName: "Registered",
                    contextProfileName: context.ContextProfileName)
                .ConfigureAwait(false);
            // ReSharper disable once PossibleInvalidOperationException
            var password = string.IsNullOrWhiteSpace(model.OverridePassword)
                ? Guid.NewGuid().ToString()
                : model.OverridePassword;
            using var userManager = GetUserManager(context);
            int id;
            if (userManager != null!)
            {
                var result1 = await userManager.CreateAsync(entity, password).ConfigureAwait(false);
                if (!result1.Succeeded)
                {
                    throw new InvalidDataException(
                        result1.Errors?.DefaultIfEmpty(null).Aggregate((c, n) => c + "\r\n" + n)
                        ?? "Identity Exception creating user");
                }
                id = entity.ID;
                // Add the basic user role
                if (CEFConfigDictionary.NewUsersGainDefaultRole
                    && Contract.CheckValidKey(CEFConfigDictionary.DefaultUserRole))
                {
                    var result2 = await userManager.AddRoleToUserAsync(
                            userId: entity.ID,
                            role: CEFConfigDictionary.DefaultUserRole!,
                            startDate: null,
                            endDate: null)
                        .ConfigureAwait(false);
                    if (!result2.Succeeded)
                    {
                        throw new InvalidDataException(
                            result2.Errors?.DefaultIfEmpty(null).Aggregate((c, n) => c + "\r\n" + n)
                            ?? "Identity Exception assigning role");
                    }
                }
                // Assign supervisor if role is present in attriubtes.
                if (entity.SerializableAttributes.TryGetValue("isSupervisor", out var isSupervisor)
                    && isSupervisor?.Value == "IBE_BUSINESS_USER_ROLE")
                {
                    var result2 = await userManager.AddRoleToUserAsync(
                        userId: entity.ID,
                        role: "Supervisor",
                        startDate: null,
                        endDate: null)
                        .ConfigureAwait(false);
                    if (!result2.Succeeded)
                    {
                        throw new InvalidDataException(
                            result2.Errors?.DefaultIfEmpty(null).Aggregate((c, n) => c + "\r\n" + n)
                            ?? "Identity Exception assigning role");
                    }
                }
            }
            else
            {
                // This will only happen in unit tests
                context.Users.Add(entity);
                var saveWorked = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                if (!saveWorked)
                {
                    throw new InvalidDataException($"Something about creating '{model.GetType().FullName}' and saving it failed");
                }
                id = entity.ID;
            }
            IAccountModel? accountToUse;
            if (Contract.CheckValidID(model.AccountID)
                && Contract.CheckValidID(
                    (accountToUse = await Workflows.Accounts.GetAsync(model.AccountID!.Value, context).ConfigureAwait(false))?.ID))
            {
                entity.AccountID = accountToUse!.ID;
            }
            else
            {
                accountToUse = await TryNewAccountToUseAsync(
                        model: model,
                        contextProfileName: context.ContextProfileName,
                        timestamp: timestamp,
                        entity: entity)
                    .ConfigureAwait(false);
            }
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            /*
            if (Contract.CheckEmpty(accountToUse!.AccountContacts))
            {
                AssignNewUserAddressToAccountContacts(
                    account: accountToUse,
                    contact: entity.Contact!,
                    timestamp: timestamp,
                    contextProfileName: context.ContextProfileName);
            }
            */
            var updatedUser = context.Users.Single(x => x.ID == id);
            updatedUser.AccountID = accountToUse!.ID;
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (entity.Active)
            {
                model.ID = entity.ID;
                if (CEFConfigDictionary.ShouldQueuePasswordResetEmailOnSetUserAsActive)
                {
                    await QueuePasswordResetEmailAsync(model, context, context.ContextProfileName).ConfigureAwait(false);
                }
                if (CEFConfigDictionary.ShouldQueueEmailOnNewUserCreatedBackOffice)
                {
                    await new AuthenticationUserAccountCompletedRegistrationToBackOfficeEmail().QueueAsync(
                            contextProfileName: context.ContextProfileName,
                            to: model.Email,
                            parameters: new() { ["username"] = model.UserName, })
                        .ConfigureAwait(false);
                }
            }
            return id.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> UpdateAsync(
            IUserModel model,
            IClarityEcommerceEntities context)
        {
            _ = Contract.RequiresValidIDOrKey(Contract.RequiresNotNull(model).ID, model.CustomKey);
            var entity = Contract.CheckValidID(model.ID)
                ? await context.Users.FilterByID(model.ID).SingleOrDefaultAsync().ConfigureAwait(false)
                : null;
            if (entity == null && Contract.CheckValidKey(model.CustomKey))
            {
                entity = await context.Users
                    .FilterByActive(true)
                    .FilterByCustomKey(model.CustomKey, true)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            _ = Contract.RequiresNotNull<ArgumentException, User>(
                entity,
                "Must supply an ID or CustomKey that matches an existing record");
            if (entity!.CustomKey != model.CustomKey && !OverrideDuplicateCheck)
            {
                // This will throw if it finds another entity with this model's key
                await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            }
            if (!entity.Active && model.Active)
            {
                model.ID = entity.ID;
                if (CEFConfigDictionary.ShouldQueuePasswordResetEmailOnSetUserAsActive)
                {
                    await QueuePasswordResetEmailAsync(model, context, context.ContextProfileName).ConfigureAwait(false);
                }
            }
            var timestamp = DateExtensions.GenDateTime;
            // Base Properties
            entity.Active = model.Active;
            entity.CustomKey = model.CustomKey;
            entity.UpdatedDate = timestamp;
            entity.Hash = model.Hash;
            await AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            var saveWorked = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            if (!saveWorked)
            {
                throw new InvalidDataException("Something about updating this object and saving it failed");
            }
            var updated = await GetAsync(entity.ID, context).ConfigureAwait(false);
            if (OnRecordUpdatedAsyncHook != null && updated is not null)
            {
                await OnRecordUpdatedAsyncHook(updated, context).ConfigureAwait(false);
            }
            return entity.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<IUserModel?> CreateForIdentityAsync(IUserModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CreateForIdentityAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IUserModel?> CreateForIdentityAsync(IUserModel model, IClarityEcommerceEntities context)
        {
            await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            var timestamp = DateExtensions.GenDateTime;
            var entity = new User
            {
                // Base Properties
                Active = true,
                CreatedDate = timestamp,
                CustomKey = model.CustomKey,
            };
            await AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            // Do ASP.NET Identity Requirements
            // We won't actually save it to the database here, just via the AuthenticationWorkflow
            return entity.CreateUserModelFromEntityFull(context.ContextProfileName);
        }

        /// <inheritdoc/>
        public async Task<IUser> CreateForIdentityEntityAsync(IUserModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CreateForIdentityEntityAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IUser> CreateForIdentityEntityAsync(IUserModel model, IClarityEcommerceEntities context)
        {
            await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            var timestamp = DateExtensions.GenDateTime;
            var entity = new User
            {
                // Base Properties
                Active = true,
                CreatedDate = timestamp,
                CustomKey = model.CustomKey,
            };
            await AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            // Do ASP.NET Identity Requirements
            // We won't actually save it to the database here, just via the AuthenticationWorkflow
            return entity;
        }

        /// <inheritdoc/>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<List<int>> SearchAsIDListAsync(IUserSearchModel search, string? contextProfileName)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var id = search.AccessibleFromAccountID;
            // This account
            var test1 = context.Users
                .AsNoTracking()
                .Where(x => x.AccountID == id!.Value)
                .FilterByBaseSearchModel(search)
                .FilterByHaveATypeSearchModel<User, UserType>(search)
                .FilterByHaveAStatusSearchModel<User, UserStatus>(search)
                .FilterUsersByUserNameOrCustomKey(search.UserName, false)
                .FilterUsersByContactEmail(search.Email)
                .FilterUsersByContactName(search.Name)
                .FilterContactsByFirstOrLastName(search.ContactFirstName)
                .FilterUsersByUserNameOrContactName(search.UserNameOrContactName, false)
                .FilterUsersByUserNameOrCustomKeyOrEmailOrContactName(search.UserNameOrCustomKeyOrEmailOrContactName, false)
                .FilterUsersByIDOrUserNameOrCustomKeyOrEmailOrContactName(search.IDOrUserNameOrCustomKeyOrEmailOrContactName, false)
                .Select(x => x.ID);
            var unioned = new List<int>(test1);
            if (search.AccessibleLevels < 1)
            {
                return ReturnDistinct(unioned);
            }
            ////var _ = await test1.ToListAsync().ConfigureAwait(false);
            // One Level up
            var test2 = context.Users
                .AsNoTracking()
                .Where(x => x.Account != null
                    && x.Account.Active
                    && x.Account.AccountsAssociatedWith!
                        .Any(y => y.Active && y.MasterID == id!.Value))
                .FilterByBaseSearchModel(search)
                .FilterByHaveATypeSearchModel<User, UserType>(search)
                .FilterByHaveAStatusSearchModel<User, UserStatus>(search)
                .FilterUsersByUserNameOrCustomKey(search.UserName, false)
                .FilterUsersByContactEmail(search.Email)
                .FilterUsersByContactName(search.Name)
                .FilterContactsByFirstOrLastName(search.ContactFirstName)
                .FilterUsersByUserNameOrContactName(search.UserNameOrContactName, false)
                .FilterUsersByUserNameOrCustomKeyOrEmailOrContactName(search.UserNameOrCustomKeyOrEmailOrContactName, false)
                .FilterUsersByIDOrUserNameOrCustomKeyOrEmailOrContactName(search.IDOrUserNameOrCustomKeyOrEmailOrContactName, false)
                .Select(x => x.ID);
            unioned.AddRange(test2);
            if (search.AccessibleLevels < 2)
            {
                return ReturnDistinct(unioned);
            }
            ////var __ = await test2.ToListAsync().ConfigureAwait(false);
            // Two Levels Up
            var test3 = context.Users
                .AsNoTracking()
                .Where(x => x.Account != null
                    && x.Account.Active
                    && x.Account.AccountsAssociatedWith!
                        .Any(y => y.Active
                            && y.Master!.Active
                            && y.Master.AccountsAssociatedWith!
                                .Any(z => z.Active && z.MasterID == id!.Value)))
                .FilterByBaseSearchModel(search)
                .FilterByHaveATypeSearchModel<User, UserType>(search)
                .FilterByHaveAStatusSearchModel<User, UserStatus>(search)
                .FilterUsersByUserNameOrCustomKey(search.UserName, false)
                .FilterUsersByContactEmail(search.Email)
                .FilterUsersByContactName(search.Name)
                .FilterContactsByFirstOrLastName(search.ContactFirstName)
                .FilterUsersByUserNameOrContactName(search.UserNameOrContactName, false)
                .FilterUsersByUserNameOrCustomKeyOrEmailOrContactName(search.UserNameOrCustomKeyOrEmailOrContactName, false)
                .FilterUsersByIDOrUserNameOrCustomKeyOrEmailOrContactName(search.IDOrUserNameOrCustomKeyOrEmailOrContactName, false)
                .Select(x => x.ID);
            unioned.AddRange(test3);
            if (search.AccessibleLevels < 3)
            {
                return ReturnDistinct(unioned);
            }
            ////var results3 = await test3.ToListAsync().ConfigureAwait(false);
            // Three Levels Up
            var test4 = context.Users
                .AsNoTracking()
                .Where(x => x.Account != null
                    && x.Account.Active
                    && x.Account.AccountsAssociatedWith!
                        .Any(y => y.Active
                            && y.Master!.Active
                            && y.Master.AccountsAssociatedWith!
                                .Any(z => z.Active
                                        && z.Master!.Active
                                        && z.Master.AccountsAssociatedWith!
                                            .Any(a => a.Active && a.MasterID == id!.Value))))
                .FilterByBaseSearchModel(search)
                .FilterByHaveATypeSearchModel<User, UserType>(search)
                .FilterByHaveAStatusSearchModel<User, UserStatus>(search)
                .FilterUsersByUserNameOrCustomKey(search.UserName, false)
                .FilterUsersByContactEmail(search.Email)
                .FilterUsersByContactName(search.Name)
                .FilterContactsByFirstOrLastName(search.ContactFirstName)
                .FilterUsersByUserNameOrContactName(search.UserNameOrContactName, false)
                .FilterUsersByUserNameOrCustomKeyOrEmailOrContactName(search.UserNameOrCustomKeyOrEmailOrContactName, false)
                .FilterUsersByIDOrUserNameOrCustomKeyOrEmailOrContactName(search.IDOrUserNameOrCustomKeyOrEmailOrContactName, false)
                .Select(x => x.ID);
            unioned.AddRange(test4);
            return ReturnDistinct(unioned);
            List<int> ReturnDistinct(List<int> result)
            {
                result = result.Distinct().OrderBy(x => x).ToList();
                return result;
            }
            ////var results4 = await test4.ToListAsync().ConfigureAwait(false);
            // ReSharper disable PossibleMultipleEnumeration
            ////if (request.Paging == null) { return unioned; }
            ////if (request.Paging.Size == null
            ////    || request.Paging.StartIndex == null
            ////    || request.Paging.Size <= 0
            ////    || request.Paging.StartIndex < 0)
            ////{
            ////}
            ////return unioned
            ////    .Skip(request.Paging.Size.Value * (request.Paging.StartIndex.Value - 1))
            ////    .Take(request.Paging.Size.Value)
            ////    .ToList();
            ////// ReSharper restore PossibleMultipleEnumeration
            ////// Real Request
            ////var query = FilterQueryByModelExtension(context.Users.AsNoTracking(), request, contextProfileName)
            ////    .Select(x => x.ID);
            ////return query.ToList();
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IUser entity,
            IUserModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateUserFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // ASP.NET Identity Actions
            if (Contract.CheckValidKey(model.OverridePassword))
            {
                entity.PasswordHash = Workflows.Authentication.CreatePasswordHash(
                        password: model.OverridePassword!,
                        contextProfileName: context.ContextProfileName)
                    .Result;
            }
            // Related Objects
            // ReSharper disable PossibleInvalidOperationException
            if (!Contract.CheckValidIDOrKey(model.StatusID, model.StatusKey))
            {
                model.StatusID = await Workflows.UserStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "NORMAL",
                        byName: "Normal",
                        byDisplayName: "Normal",
                        model: null,
                        context: context)
                    .ConfigureAwait(false);
            }
            if (model.TypeID <= 0 && string.IsNullOrWhiteSpace(model.TypeKey))
            {
                model.TypeID = await Workflows.UserTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Customer",
                        byName: "Customer",
                        byDisplayName: "Customer",
                        model: null,
                        context: context)
                    .ConfigureAwait(false);
            }
            if (model.Contact != null)
            {
                if (model.Contact.TypeID <= 0)
                {
                    model.Contact.TypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: model.Contact.TypeKey ?? "User",
                            byName: model.Contact.TypeName ?? "User",
                            byDisplayName: model.Contact.TypeDisplayName ?? "User",
                            model: model.Contact.Type,
                            context: context)
                        .ConfigureAwait(false);
                }
                await model.Contact.AssignPrePropertiesToContactAndAddressAsync(
                        Workflows.Addresses,
                        context.ContextProfileName)
                    .ConfigureAwait(false);
            }
            // ReSharper restore PossibleInvalidOperationException
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            // Associated Objects
            if (Contract.CheckNotEmpty(model.Notes) && Contract.CheckValidID(entity.ID))
            {
                foreach (var note in model.Notes!.Where(x => x is { Active: true }))
                {
                    note.UserID = entity.ID;
                }
            }
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<User>> FilterQueryByModelCustomAsync(
            IQueryable<User> query,
            IUserSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterUsersByUserNameOrCustomKey(search.UserName, false)
                .FilterUsersByUserNameOrContactName(search.UserNameOrContactName, false)
                .FilterUsersByUserNameOrCustomKeyOrEmailOrContactName(search.UserNameOrCustomKeyOrEmailOrContactName, false)
                .FilterUsersByIDOrUserNameOrCustomKeyOrEmailOrContactName(search.IDOrUserNameOrCustomKeyOrEmailOrContactName, false)
                .FilterUsersByUserNameOrCustomKeyOrEmail(search.UserNameOrCustomKeyOrEmail, false)
                .FilterUsersByUserNameOrEmail(search.UserNameOrEmail, false)
                .FilterUsersByAccessibleFromAccountID(search.AccessibleFromAccountID)
                .FilterUsersByAccountName(search.AccountName, false)
                .FilterUsersByAccountKey(search.AccountKey, false)
                .FilterUsersByEmailOrContactEmail(search.Email)
                .FilterUsersByContactName(search.Name)
                .FilterContactsByFirstOrLastName(search.ContactFirstName)
                .FilterContactsByFirstName(search.FirstName)
                .FilterContactsByLastName(search.LastName);
        }

        /// <summary>Executes the limited relate workflows operation.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task RunLimitedRelateWorkflowsAsync(
            IUser entity,
            IUserModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            await RelateRequiredTypeAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredStatusAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredContactAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            if (model.Account != null)
            {
                await Workflows.Accounts.UpsertAsync(model.Account, contextProfileName).ConfigureAwait(false);
            }
            await RelateOptionalAccountAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalCurrencyAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalLanguageAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateOptionalUserOnlineStatusAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow, CognitiveComplexity
        protected override async Task<CEFActionResponse> DeleteAsync(
            User? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            if (context.UserImages != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.UserImages.Count(x => x.MasterID == entity.ID);)
                {
                    context.UserImages.Remove(context.UserImages.First(x => x.MasterID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.UserLogins != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.UserLogins.Count(x => x.UserId == entity.ID);)
                {
                    context.UserLogins.Remove(context.UserLogins.First(x => x.UserId == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.RoleUsers != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.RoleUsers.Count(x => x.UserId == entity.ID);)
                {
                    context.RoleUsers.Remove(context.RoleUsers.First(x => x.UserId == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.SalesOrders != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.SalesOrders.Count(x => x.UserID == entity.ID);)
                {
                    context.SalesOrders.Remove(context.SalesOrders.First(x => x.UserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.SalesInvoices != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.SalesInvoices.Count(x => x.UserID == entity.ID);)
                {
                    context.SalesInvoices.Remove(context.SalesInvoices.First(x => x.UserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.Carts != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Carts.Count(x => x.UserID == entity.ID);)
                {
                    await Workflows.Carts.DeleteAsync(
                            await context.Carts.Where(x => x.UserID == entity.ID).Select(x => x.ID).FirstAsync().ConfigureAwait(false),
                            context.ContextProfileName)
                        .ConfigureAwait(false);
                }
            }
            if (context.PageViews != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.PageViews.Count(x => x.UserID == entity.ID);)
                {
                    context.PageViews.Remove(context.PageViews.First(x => x.UserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.Visits != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Visits.Count(x => x.UserID == entity.ID);)
                {
                    context.Visits.Remove(context.Visits.First(x => x.UserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.Visitors != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Visitors.Count(x => x.UserID == entity.ID);)
                {
                    context.Visitors.Remove(context.Visitors.First(x => x.UserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.Events != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Events.Count(x => x.UserID == entity.ID);)
                {
                    context.Events.Remove(context.Events.First(x => x.UserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.Messages != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Messages.Count(x => x.SentByUserID == entity.ID);)
                {
                    context.Messages.Remove(context.Messages.First(x => x.SentByUserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.MessageRecipients != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.MessageRecipients.Count(x => x.SlaveID == entity.ID);)
                {
                    context.MessageRecipients.Remove(context.MessageRecipients.First(x => x.SlaveID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            if (context.Reviews != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Reviews.Count(x => x.ApprovedByUserID == entity.ID);)
                {
                    context.Reviews.Remove(context.Reviews.First(x => x.ApprovedByUserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
                for (var i = 0; i < context.Reviews.Count(x => x.SubmittedByUserID == entity.ID);)
                {
                    context.Reviews.Remove(context.Reviews.First(x => x.SubmittedByUserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
                for (var i = 0; i < context.Reviews.Count(x => x.UserID == entity.ID);)
                {
                    context.Reviews.Remove(context.Reviews.First(x => x.UserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            // ReSharper disable once InvertIf
            if (context.Notes != null!)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Notes.Count(x => x.UserID == entity.ID);)
                {
                    context.Notes.Remove(context.Notes.First(x => x.UserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
                for (var i = 0; i < context.Notes.Count(x => x.CreatedByUserID == entity.ID);)
                {
                    context.Notes.Remove(context.Notes.First(x => x.CreatedByUserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
                for (var i = 0; i < context.Notes.Count(x => x.UpdatedByUserID == entity.ID);)
                {
                    context.Notes.Remove(context.Notes.First(x => x.UpdatedByUserID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            // Delete the user itself
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Try new account to use.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="entity">            The entity.</param>
        /// <returns>An IAccountModel.</returns>
        protected virtual async Task<IAccountModel?> TryNewAccountToUseAsync(
            IUserModel model,
            string? contextProfileName,
            DateTime timestamp,
            IUser entity)
        {
            var newAccount = RegistryLoaderWrapper.GetInstance<IAccountModel?>();
            newAccount!.Active = true;
            newAccount.CreatedDate = timestamp;
            newAccount.CustomKey = model.Account?.CustomKey ?? model.AccountKey ?? model.CustomKey;
            if (entity.Contact != null)
            {
                var contactName = $"{entity.Contact.FirstName} {entity.Contact.LastName}".Trim();
                newAccount.Name = !string.IsNullOrEmpty(contactName) ? contactName : entity.UserName;
            }
            else
            {
                newAccount.Name = entity.UserName;
            }
            newAccount.StatusKey = "NORMAL";
            newAccount.TypeKey = "CUSTOMER";
            newAccount.SerializableAttributes = model.SerializableAttributes;
            newAccount.IsTaxable = model.TaxExempt.HasValue ? model.TaxExempt.Value : false;
            if (model.BusinessType != null)
            {
                newAccount.BusinessType = model.BusinessType;
            }
            if (model.DEANumber != null)
            {
                newAccount.DEANumber = model.DEANumber;
            }
            if (model.DunsNumber != null)
            {
                newAccount.DunsNumber = model.DunsNumber;
            }
            if (model.EIN != null)
            {
                newAccount.EIN = model.EIN;
            }
            if (model.MedicalLicenseHolderName != null)
            {
                newAccount.MedicalLicenseHolderName = model.MedicalLicenseHolderName;
            }
            if (model.MedicalLicenseNumber != null)
            {
                newAccount.MedicalLicenseNumber = model.MedicalLicenseNumber;
            }
            if (model.MedicalLicenseState != null)
            {
                newAccount.MedicalLicenseState = model.MedicalLicenseState;
            }
            if (model.PreferredInvoiceMethod != null)
            {
                newAccount.PreferredInvoiceMethod = model.PreferredInvoiceMethod;
            }
            if (model.TaxExemptNumber != null)
            {
                newAccount.TaxExemptionNo = model.TaxExemptNumber;
            }
            /*
            var accountContacts = model.Account?.AccountContacts
                ?? new List<IAccountContactModel>();
            if (model.Contact != null
                && (model.Account?.AccountContacts == null
                    || model.Account.AccountContacts.Count == 0))
            {
                accountContacts.Add(new AccountContactModel
                {
                    Active = true,
                    CreatedDate = timestamp,
                    Name = newAccount.Name,
                    Slave = (ContactModel)model.Contact,
                    IsBilling = true,
                });
            }
            newAccount.AccountContacts = accountContacts;
            */
            if (!CEFConfigDictionary.NewUsersAllowLookupExistingAccountOnRegistration)
            {
                var createResponse = await Workflows.Accounts.CreateAsync(newAccount, contextProfileName).ConfigureAwait(false);
                return await Workflows.Accounts.GetAsync(createResponse.Result, contextProfileName).ConfigureAwait(false);
            }
            return await Workflows.Accounts.FindExistingAccountAsync(
                            newAccount.Name,
                            model.Contact?.Address?.City,
                            model.Contact?.Address?.RegionID,
                            contextProfileName)
                        .ConfigureAwait(false)
                    ?? (await Workflows.Accounts.ResolveWithAutoGenerateAsync(
                                model.AccountID,
                                model.AccountKey,
                                model.AccountName,
                                model.Account ?? newAccount,
                                contextProfileName)
                            .ConfigureAwait(false))
                        .Result;
        }

        /// <summary>Gets user manager.</summary>
        /// <param name="context">The context.</param>
        /// <returns>The user manager.</returns>
        private static CEFUserManager GetUserManager(IDbContext context)
        {
            try
            {
                var userStore = new CEFUserStore(context);
                return new(userStore);
            }
            catch
            {
                // This should only happen during unit tests
                return null!;
            }
        }

        /// <summary>Assign new user address to account contacts.</summary>
        /// <param name="account">           The account.</param>
        /// <param name="contact">           The contact.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static void AssignNewUserAddressToAccountContacts(
            IBaseModel account,
            IContact contact,
            DateTime timestamp,
            string? contextProfileName)
        {
            if (!CEFConfigDictionary.AddressBookEnabled
                || !Contract.CheckValidID(account.ID)
                || !Contract.CheckValidID(contact.ID)
                || contact.Address == null)
            {
                return;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.AccountContacts.Add(new()
            {
                Active = true,
                CreatedDate = timestamp,
                Name = contact.CustomKey ?? contact.Address?.CustomKey,
                CustomKey = contact.CustomKey ?? contact.Address?.CustomKey,
                SlaveID = contact.ID,
                MasterID = account.ID,
            });
        }

        /// <summary>Clean model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>An IUserModel.</returns>
        private static IUserModel? CleanModel(IUserModel? model)
        {
            if (model == null)
            {
                return null;
            }
            // Clear anything to do with the password from the model before returning
            model.OverridePassword = model.PasswordHash = null;
            model.IsSuperAdmin = false; // This should never be set, it's actually deprecated in favor of identity roles
            // Return a cleaned model
            return model;
        }

        /// <summary>Queue password reset email.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task QueuePasswordResetEmailAsync(
            IUserModel model,
            IDbContext context,
            string? contextProfileName)
        {
            _ = Contract.RequiresValidID(model.ID);
            using var userManager = GetUserManager(context);
            await new AuthenticationForgotPasswordToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: model.Email,
                    parameters: new()
                    {
                        ["username"] = model.UserName,
                        ["firstName"] = model.ContactFirstName,
                        ["lastName"] = model.ContactLastName,
                        ["resetToken"] = await userManager.GeneratePasswordResetTokenAsync(model.ID).ConfigureAwait(false),
                    })
                .ConfigureAwait(false);
        }

        /// <summary>Assign user type.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="defaultKey">        The default key.</param>
        /// <param name="defaultName">       The default name.</param>
        /// <param name="defaultDisplayName">The default display name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A User.</returns>
        private async Task<User> AssignUserTypeAsync(
            User entity,
            IUserModel model,
            string defaultKey,
            string defaultName,
            string defaultDisplayName,
            string? contextProfileName)
        {
            if (entity.TypeID > 0)
            {
                return entity;
            }
            var typeKey = (model.TypeKey ?? model.Type?.CustomKey) ?? defaultKey;
            var typeName = (model.TypeName ?? model.Type?.Name) ?? defaultName;
            var displayName = model.Type?.DisplayName ?? defaultDisplayName;
            // ReSharper disable once PossibleInvalidOperationException
            entity.TypeID = await Workflows.UserTypes.ResolveWithAutoGenerateToIDAsync(
                    null,
                    typeKey,
                    typeName,
                    displayName,
                    model.Type,
                    contextProfileName)
                .ConfigureAwait(false);
            return entity;
        }

        /// <summary>Assign user status.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="defaultKey">        The default key.</param>
        /// <param name="defaultName">       The default name.</param>
        /// <param name="defaultDisplayName">The default display name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A User.</returns>
        private async Task<User> AssignUserStatusAsync(
            User entity,
            IUserModel model,
            string defaultKey,
            string defaultName,
            string defaultDisplayName,
            string? contextProfileName)
        {
            if (entity.StatusID > 0)
            {
                return entity;
            }
            var statusKey = (model.StatusKey ?? model.Status?.CustomKey) ?? defaultKey;
            var statusName = (model.StatusName ?? model.Status?.Name) ?? defaultName;
            var displayName = model.Status?.DisplayName ?? defaultDisplayName;
            // ReSharper disable once PossibleInvalidOperationException
            entity.StatusID = await Workflows.UserStatuses.ResolveWithAutoGenerateToIDAsync(
                    null,
                    statusKey,
                    statusName,
                    displayName,
                    model.Status,
                    contextProfileName)
                .ConfigureAwait(false);
            return entity;
        }
    }
}
