// <copyright file="UserCRUDWorkflow.extended.Special.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class UserWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpsertSelectedLanguageAsync(string username, string locale, string? contextProfileName)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return CEFAR.FailingCEFAR("Username is required");
            }
            if (string.IsNullOrWhiteSpace(locale))
            {
                return CEFAR.FailingCEFAR("Locale is required");
            }
            using var context = RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance<IClarityEcommerceEntities>();
            var entity = await context.Users
                .FilterByActive(true)
                .FilterUsersByUserNameOrCustomKey(username, true)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (entity == null)
            {
                return CEFAR.FailingCEFAR($"Unable to locate user: {username}");
            }
            var languageID = await context.Languages
                .AsNoTracking()
                .FilterByCustomKey(locale, true)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (languageID == null)
            {
                return CEFAR.FailingCEFAR($"Unable to locate locale: {locale}");
            }
            if (entity.LanguageID == languageID.Value)
            {
                return CEFAR.PassingCEFAR();
            }
            entity.LanguageID = languageID;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR($"Unable to save locale '{locale}' to user '{username}'");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpsertSelectedCurrencyAsync(
            string username,
            string currency,
            string? contextProfileName)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return CEFAR.FailingCEFAR("Username is required");
            }
            if (string.IsNullOrWhiteSpace(currency))
            {
                return CEFAR.FailingCEFAR("Currency is required");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var currencyID = await context.Currencies
                .AsNoTracking()
                .FilterByCustomKey(currency, true)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (currencyID == null)
            {
                return CEFAR.FailingCEFAR($"Unable to locate currency: {currency}");
            }
            var userCurrencyID = await context.Users
                .FilterByActive(true)
                .FilterUsersByUserNameOrCustomKey(username, true)
                .Select(x => x.CurrencyID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (userCurrencyID == currencyID.Value)
            {
                return CEFAR.PassingCEFAR();
            }
            var user = await context.Users
                .FilterByActive(true)
                .FilterUsersByUserNameOrCustomKey(username, true)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (user == null)
            {
                return CEFAR.FailingCEFAR($"Unable to locate user: {username}");
            }
            user.CurrencyID = currencyID;
            var saveResult = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!saveResult)
            {
                return CEFAR.FailingCEFAR($"Unable to save currency '{currency}' to user '{username}'");
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<(int userID, string inviteCode)> InviteWithCodeAsync(
            string email,
            IUserModel currentUser,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // Check if user already exists
            var emailAlreadyUsed = await context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Active && (u.UserName == email || u.Contact != null && u.Contact.Email1 == email))
                .ConfigureAwait(false);
            if (emailAlreadyUsed)
            {
                throw new InvalidOperationException($"Email address {email} is already used by an active user");
            }
            // Generate code
            var registrationCode = Guid.NewGuid();
            // Create user with specific type
            var typeID = await context.UserTypes
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByCustomKey("Customer", true)
                .Select(x => x.ID)
                .SingleAsync()
                .ConfigureAwait(false);
            var statusID = await context.UserStatuses
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByCustomKey("Email Sent", true)
                .Select(x => x.ID)
                .SingleAsync()
                .ConfigureAwait(false);
            var user = new UserModel
            {
                Active = true,
                CustomKey = email,
                UserName = email,
                TypeID = typeID,
                StatusID = statusID,
                Contact = new() { Active = true, Email1 = email },
                SerializableAttributes = new()
                {
                    ["INVITATION_CODE"] = new() { Key = "INVITATION_CODE", Value = registrationCode.ToString() },
                },
                // User cannot be created without a password -- use reg code as default while waiting for user to finish registration
                OverridePassword = registrationCode.ToString(),
            };
            // Add invitee email to the invitee list of the current user.
            // Will be use later on to figure out who the current user invited
            await AddToInviteeListAsync(currentUser.ID, email, contextProfileName).ConfigureAwait(false);
            return ((await CreateAsync(user, contextProfileName).ConfigureAwait(false)).Result, registrationCode.ToString());
        }

        /// <inheritdoc/>
        public async Task<IUserModel> GetByEmailAndInvitationCodeAsync(string email, string invitationCode, string? contextProfileName)
        {
            var user = await GetAsync(email, contextProfileName).ConfigureAwait(false);
            var userCode = user!.SerializableAttributes!["INVITATION_CODE"]?.Value;
            if (user == null || invitationCode != userCode)
            {
                throw new InvalidOperationException("Invitation email/code not found");
            }
            return user;
        }

        /// <inheritdoc/>
        public async Task<IStatusModel?> GetOnlineStatusAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (await context.Users.AsNoTracking().FilterByID(Contract.RequiresValidID(userID)).Select(x => x.UserOnlineStatusID).SingleOrDefaultAsync() == null
                && !(await SetOnlineStatusAsync(userID, "Offline", contextProfileName).ConfigureAwait(false)).ActionSucceeded)
            {
                throw new InvalidOperationException(
                    $"Unable to set a default of offline for user online status to user id {userID}");
            }
            // TODO@JTG: Time out the online status, requires a column to store the change timestamp (change read here)
            return context.Users
                .AsNoTracking()
                .FilterByID(userID)
                .Select(x => x.UserOnlineStatus!)
                .SelectSingleLiteUserOnlineStatusAndMapToStatusModel(contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> SetOnlineStatusAsync(int userID, string onlineStatus, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Users.FilterByID(userID).SingleOrDefaultAsync();
            if (entity is null)
            {
                return CEFAR.FailingCEFAR();
            }
            await RelateOptionalUserOnlineStatusAsync(
                    entity,
                    new UserModel { Active = true, UserOnlineStatusKey = onlineStatus },
                    DateExtensions.GenDateTime,
                    contextProfileName)
                .ConfigureAwait(false);
            // TODO@JTG: Time out the online status, requires a column to store the change timestamp (change set here)
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR("Failed to save the data");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ChangeTypeAsync(int userID, string typeKey, string? contextProfileName)
        {
            Contract.CheckValidID(userID);
            Contract.CheckValidKey(typeKey);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.Users.FilterByID(userID).SingleOrDefaultAsync().ConfigureAwait(false);
            await RelateRequiredTypeAsync(
                    entity,
                    new UserModel { Active = true, TypeKey = typeKey },
                    DateExtensions.GenDateTime,
                    contextProfileName)
                .ConfigureAwait(false);
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR("Failed to save the data");
        }

        /// <inheritdoc/>
        public async Task<List<UserAccountAssignment>> GetUsersAccountAssignmentsAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Select(x => new
                    {
                        UserID = x.ID,
                        UserKey = x.CustomKey,
                        UserUserName = x.UserName,
                        UserEmail = x.Email,
                        x.AccountID,
                        AccountKey = x.Account != null ? x.Account.CustomKey : null,
                        AccountName = x.Account != null ? x.Account.Name : null,
                    })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new UserAccountAssignment
                {
                    UserID = x.UserID,
                    UserKey = x.UserKey,
                    UserUserName = x.UserUserName,
                    UserEmail = x.UserEmail,
                    AccountID = x.AccountID,
                    AccountKey = x.AccountKey,
                    AccountName = x.AccountName,
                })
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateUsersAccountAssignmentsAsync(
            List<UserAccountAssignment> toUpdate,
            string? contextProfileName)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var timestamp = DateExtensions.GenDateTime;
                var changed = false;
                foreach (var record in toUpdate)
                {
                    if (!Contract.CheckValidID(record.UserID) && Contract.CheckValidKey(record.UserKey))
                    {
                        var id = await CheckExistsAsync(record.UserKey!, contextProfileName).ConfigureAwait(false);
                        if (Contract.CheckValidID(id))
                        {
                            record.UserID = id!.Value;
                        }
                    }
                    if (!Contract.CheckValidID(record.UserID) && Contract.CheckValidKey(record.UserEmail))
                    {
                        var id = await CheckExistsByEmailAsync(record.UserEmail!, contextProfileName).ConfigureAwait(false);
                        if (Contract.CheckValidID(id))
                        {
                            record.UserID = id!.Value;
                        }
                    }
                    if (!Contract.CheckValidID(record.UserID) && Contract.CheckValidKey(record.UserUserName))
                    {
                        var id = await CheckExistsAsync(record.UserUserName!, contextProfileName).ConfigureAwait(false);
                        if (Contract.CheckValidID(id))
                        {
                            record.UserID = id!.Value;
                        }
                    }
                    if (!Contract.CheckValidID(record.UserID))
                    {
                        // Couldn't find it a record for this yet
                        continue;
                    }
                    var user = context.Users.Single(x => x.ID == record.UserID);
                    user.AccountID = record.AccountID;
                    user.UpdatedDate = timestamp;
                    changed = true;
                }
                if (changed)
                {
                    return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                        .BoolToCEFAR("ERROR! Something about updating users and account assignments failed.");
                }
            }
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Adds to the invitee list.</summary>
        /// <param name="currentUserID">     Identifier for the current user.</param>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AddToInviteeListAsync(int currentUserID, string email, string? contextProfileName)
        {
            // Retrieve the currentUser
            var currentUser = await GetAsync(currentUserID, contextProfileName).ConfigureAwait(false);
            currentUser!.SerializableAttributes ??= new();
            currentUser.SerializableAttributes.TryGetValue("Invitee-List", out var inviteeListAttr);
            if (inviteeListAttr?.Value == null)
            {
                inviteeListAttr = new() { Key = "Invitee-List", Value = email };
                currentUser.SerializableAttributes["Invitee-List"] = inviteeListAttr;
                await UpdateAsync(currentUser, contextProfileName).ConfigureAwait(false);
                return;
            }
            var list = inviteeListAttr.Value;
            var split = list.Split(';').ToList();
            split.Add(email);
            inviteeListAttr.Value = string.Join(";", split);
            await UpdateAsync(currentUser, contextProfileName).ConfigureAwait(false);
        }
    }
}
