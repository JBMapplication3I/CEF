// <copyright file="AuthenticationWorkflow.Registration.Membership.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Models;
    using Providers.Emails;
    using Utilities;

    /// <summary>An authentication workflow.</summary>
    public partial class AuthenticationWorkflow
    {
        /// <inheritdoc/>
        public Task<CEFActionResponse> ValidateInvitationAsync(string email, string token)
        {
            return Task.FromResult(
                !Contract.CheckValidKey(email)
                    ? CEFAR.FailingCEFAR("Must provide an Email.")
                    : !Contract.CheckValidKey(token)
                        ? CEFAR.FailingCEFAR("Must provide the Validation Token.")
                        : (token == CEFConfigDictionary.InvitationToken)
                            .BoolToCEFAR("Invalid Token"));
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ValidateEmailAsync(string email, string token, string? contextProfileName)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return CEFAR.FailingCEFAR("Must provide an Email.");
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                return CEFAR.FailingCEFAR("Must provide the Validation Token.");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false);
            if (user == null)
            {
                return CEFAR.FailingCEFAR("No user found associated with this email.");
            }
            var decodedBytes = Convert.FromBase64String(token);
            var decodedToken = decodedBytes.GetUTF8DecodedString();
            var result = await userManager.ConfirmEmailAsync(user.ID, decodedToken).ConfigureAwait(false);
            return result.Succeeded.BoolToCEFAR(result.Errors?.ToArray());
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CreateLiteAccountAndSendValidationEmailAsync(
            ICreateLiteAccountAndSendValidationEmail model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            IUserModel userModel;
            var existing = await userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == model.UserName
                    || x.UserName == model.Email
                    || x.Email == model.Email
                    || x.Contact!.Email1 == model.Email)
                .ConfigureAwait(false);
            int id;
            if (existing == null)
            {
                userModel = RegistryLoaderWrapper.GetInstance<IUserModel>(contextProfileName);
                userModel.Active = true;
                userModel.Contact = RegistryLoaderWrapper.GetInstance<IContactModel>(contextProfileName);
                userModel.Contact.Active = true;
                userModel.SerializableAttributes = new()
                {
                    ["Membership Level"] = new()
                    {
                        Key = "Membership Level",
                        Value = model.Membership,
                    },
                };
            }
            else
            {
                id = existing.ID;
                userModel = (await Workflows.Users.GetAsync(id, contextProfileName).ConfigureAwait(false))!;
            }
            var username = UserNameIsEmail ? model.Email : model.UserName;
            userModel.UserName = userModel.Contact!.CustomKey = userModel.CustomKey = username ?? model.Email;
            userModel.Email = model.Email;
            userModel.DisplayName = model.FirstName + " " + model.LastName;
            userModel.Contact.FirstName = model.FirstName;
            userModel.Contact.LastName = model.LastName;
            userModel.Contact.FullName = model.FirstName + " " + model.LastName;
            userModel.Contact.Email1 = model.Email;
            userModel.TypeName = model.SellerType;
            if (existing == null)
            {
                // Do the create, but without saving it, this ensure several objects are actually valid so it can go into the db
                var entity = await Workflows.Users.CreateForIdentityEntityAsync(userModel, contextProfileName).ConfigureAwait(false);
                entity.Email = userModel.Email;
                var defaultPassword = Guid.NewGuid().ToString().Replace("-", string.Empty) + new Random().Next(1, 1000000);
                defaultPassword = defaultPassword.Replace(defaultPassword[..6], defaultPassword[..6].ToUpper());
                using (var userManager2 = GetUserManager(context, contextProfileName))
                {
                    var result1 = await userManager2.CreateAsync((User)entity, defaultPassword).ConfigureAwait(false);
                    if (!result1.Succeeded)
                    {
                        await Logger.LogErrorAsync("Authentication", result1.Errors?.Aggregate((c, n) => c + "\r\n" + n), contextProfileName).ConfigureAwait(false);
                        return CEFAR.FailingCEFAR(result1.Errors?.ToArray());
                    }
                    id = entity.ID;
                    // Add the basic user role
                    var result2 = await userManager2.AddRoleToUserAsync(entity.ID, "CEF User", null, null).ConfigureAwait(false);
                    if (!result2.Succeeded)
                    {
                        await Logger.LogErrorAsync("Authentication", result2.Errors?.Aggregate((c, n) => c + "\r\n" + n), contextProfileName).ConfigureAwait(false);
                        return CEFAR.FailingCEFAR(result2.Errors?.ToArray());
                    }
                    // Add the Store Administrator user role
                    // ReSharper disable once StringLiteralTypo
                    if (!string.Equals(model.Membership, "simpleregistration", StringComparison.OrdinalIgnoreCase))
                    {
                        var result3 = await userManager2.AddRoleToUserAsync(entity.ID, "CEF Store Administrator", null, null).ConfigureAwait(false);
                        if (!result3.Succeeded)
                        {
                            await Logger.LogErrorAsync("Authentication", result3.Errors?.Aggregate((c, n) => c + "\r\n" + n), contextProfileName).ConfigureAwait(false);
                            return CEFAR.FailingCEFAR(result3.Errors?.ToArray());
                        }
                    }
                }
                // Create and assign an account
                var account = RegistryLoaderWrapper.GetInstance<IAccountModel>(contextProfileName);
                account.Active = true;
                account.CreatedDate = DateExtensions.GenDateTime;
                account.CustomKey = userModel.CustomKey;
                account.Name = userModel.DisplayName;
                account.IsTaxable = true;
                account.StatusKey = "General";
                account.TypeKey = "Seller";
                var accountCreateResponse = await Workflows.Accounts.CreateAsync(account, contextProfileName).ConfigureAwait(false);
                account = await Workflows.Accounts.GetAsync(accountCreateResponse.Result, contextProfileName).ConfigureAwait(false);
                entity.AccountID = account!.ID;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            else
            {
                await Workflows.Users.UpdateAsync(userModel, contextProfileName).ConfigureAwait(false);
                id = existing.ID;
            }
            // Send them an email to reset their password (validates the account)
            var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(id).ConfigureAwait(false);
            // Return the status of the email send attempt
            var result = await new AuthenticationAccountValidationWithResetToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: model.Email,
                    parameters: new()
                    {
                        ["resetToken"] = Uri.EscapeDataString(passwordResetToken),
                        ["sellerType"] = model.SellerType,
                        ["membershipLevel"] = model.Membership,
                        ["membershipType"] = model.MembershipType,
                    })
                .ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CompleteRegistrationAsync(
            ICompleteRegistration request,
            string? contextProfileName)
        {
            // A Lite User has already been created, now we are just filling
            // in more info
            // The previous entry was set up with custom key and username of the email
            // address and we have already applied the password and logged in with it
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            // Get the User
            var user = (await userManager.FindByEmailAsync(request.Email).ConfigureAwait(false))
                .CreateUserModelFromEntityFull(contextProfileName);
            if (user == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot complete registration for a user that does not exist");
            }
            // Apply the new information
            user.Email = request.Email;
            user.BillingAddress = request.Address;
            // We took care of these Names already and the form blanks them for last section
            user.DisplayName = request.FirstName + " " + request.LastName;
            user.PhoneNumber = request.Phone;
            user.EmailConfirmed = true;
            var username = UserNameIsEmail ? request.Email : request.UserName;
            user.Contact ??= new ContactModel();
            user.UserName = user.CustomKey = user.Contact.CustomKey = username ?? request.Email;
            if (Contract.CheckValidKey(request.RoleName))
            {
                user.SerializableAttributes = new()
                {
                    ["Membership Level"] = new()
                    {
                        Key = "Membership Level",
                        Value = request.RoleName!,
                    },
                };
            }
            user.TypeName = request.TypeName;
            // Update Contact Info
            var contact = await Workflows.Contacts.GetAsync(user.ContactID, contextProfileName).ConfigureAwait(false);
            contact!.FirstName = request.FirstName;
            contact.LastName = request.LastName;
            contact.FullName = request.FirstName + " " + request.LastName;
            contact.Email1 = request.Email;
            contact.Phone1 = request.Phone;
            await Workflows.Contacts.UpdateAsync(contact, contextProfileName).ConfigureAwait(false);
            if (contact.AddressID != null)
            {
                var address = await Workflows.Addresses.GetAsync(contact.AddressID.Value, contextProfileName).ConfigureAwait(false);
                address!.Street1 = request.Address!.Street1;
                address.Street2 = request.Address.Street2;
                address.City = request.Address.City;
                address.Country = request.Address.Country;
                address.Region = request.Address.Region;
                await Workflows.Addresses.UpdateAsync(address, contextProfileName).ConfigureAwait(false);
            }
            // Update Account Info
            if (user.AccountID != null)
            {
                var account = await Workflows.Accounts.GetAsync(user.AccountID.Value, contextProfileName).ConfigureAwait(false);
                if (account != null)
                {
                    account.Name = request.CompanyName;
                    account.CustomKey = user.CustomKey;
                    account.TypeName = user.TypeName;
                    await Workflows.Accounts.UpdateAsync(account, contextProfileName).ConfigureAwait(false);
                }
            }
            // Update the User
            var updateResponse = await Workflows.Users.UpdateAsync(user, contextProfileName).ConfigureAwait(false);
            user = await Workflows.Users.GetAsync(updateResponse.Result, contextProfileName).ConfigureAwait(false);
            // Try to send the emails, but don't break process if they fail
            var result = CEFAR.PassingCEFAR();
            // Assign the role for the store and the specific membership type
            await userManager.AssignRoleIfNotAssignedAsync(Contract.RequiresValidID(user!.ID), "CEF User", null, null).ConfigureAwait(false);
            if (Contract.CheckValidKey(request.RoleName))
            {
                await userManager.AssignRoleIfNotAssignedAsync(user.ID, request.RoleName!, null, null).ConfigureAwait(false);
            }
            if (string.Equals(request.ProfileType, "buyer", StringComparison.OrdinalIgnoreCase))
            {
                // Send Buyer Created Email
                result.ActionSucceeded = true; // TODO@BE: Send Buyer Created Email
                return result;
            }
            // Moving the store creation to the SalesOrderProcessing
            // Now create the Store
            var store = RegistryLoaderWrapper.GetInstance<IStoreModel>(contextProfileName);
            store.Active = true;
            store.SeoUrl = store.CustomKey = request.CompanyName.ToSeoUrl();
            store.Name = request.CompanyName;
            store.MissionStatement = request.ProfileType; // TODO: Update with Key Field or TypeID relationship
            ////var storeContactWorkflow = new ContactWorkflow();
            ////if (store.ContactID != null)
            ////{
            ////    var storeContact = contactWorkflow.Get(store.ContactID.Value);
            store.Contact = RegistryLoaderWrapper.GetInstance<IContactModel>(contextProfileName);
            store.Contact.Active = true;
            store.Contact.CustomKey = request.CompanyName;
            store.Contact.FirstName = request.FirstName;
            store.Contact.LastName = request.LastName;
            store.Contact.FullName = request.FirstName + " " + request.LastName;
            store.Contact.Email1 = request.Email;
            store.Contact.TypeID = 5; // Store Contact Type TODO@BE: No Magic Numbers!
            store.Contact.Phone1 = request.Phone;
            store.Contact.Address = request.Address;
            await store.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, contextProfileName).ConfigureAwait(false);
            ////}
            ////else
            ////{
            ////}
            store.ExternalUrl = request.Website;
            if (!string.IsNullOrWhiteSpace(request.TypeName))
            {
                store.TypeID = await Workflows.StoreTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: null,
                        byName: request.TypeName,
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            // Assign the User to the Store
            store.Users = new();
            var storeUser = RegistryLoaderWrapper.GetInstance<IStoreUserModel>(contextProfileName);
            storeUser.Active = true;
            // ReSharper disable once PossibleInvalidOperationException
            storeUser.SlaveID = user.ID;
            storeUser.SlaveKey = user.CustomKey;
            storeUser.StoreKey = store.CustomKey?.ToSeoUrl();
            var storeCreateResponse = await Workflows.Stores.CreateAsync(store, contextProfileName).ConfigureAwait(false);
            store = await Workflows.Stores.GetAsync(storeCreateResponse.Result, contextProfileName).ConfigureAwait(false);
            // ReSharper disable once PossibleInvalidOperationException
            storeUser.StoreID = store!.ID;
            var su = store.Users;
            su!.Add(storeUser);
            store.Users = su;
            // Loop on request.StoreContacts to associate Store since previously not available
            foreach (var s in request.StoreContacts!)
            {
                s.Store = store;
            }
            store.StoreContacts = request.StoreContacts;
            var storeUpdateResponse = await Workflows.Stores.UpdateAsync(store, contextProfileName).ConfigureAwait(false);
            store = await Workflows.Stores.GetAsync(storeUpdateResponse.Result, contextProfileName).ConfigureAwait(false);
            await userManager.AssignRoleIfNotAssignedAsync(user.ID, "CEF Store Administrator", null, null).ConfigureAwait(false);
            try
            {
                await new AuthenticationSellerAccountCompletedRegistrationToSellerEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: user.Email)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: "Authentication",
                        message: "There was an issue sending the seller account creation notification: " + ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                result.Messages.Add("There was an issue sending the seller account creation notification");
            }
            try
            {
                await new AuthenticationSellerAccountCompletedRegistrationToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: "Authentication",
                        message: "There was an issue sending the back office seller account creation notification: " + ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                result.Messages.Add(
                    "There was an issue sending the back office seller account creation notification");
            }
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ApproveUserRegistrationAsync(
            string token,
            int userID,
            string? contextProfileName)
        {
            if (string.IsNullOrWhiteSpace(token) || !Contract.CheckValidID(userID))
            {
                return CEFAR.FailingCEFAR("Must provide a Token and a UserID");
            }
            if (CEFConfigDictionary.UserApprovalToken != token)
            {
                return CEFAR.FailingCEFAR("Invalid Token Settings");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var user = await context.Users.FirstOrDefaultAsync(x => x.ID == userID).ConfigureAwait(false);
            if (user == null)
            {
                return CEFAR.FailingCEFAR("Unable to locate User by UserID");
            }
            var timestamp = DateExtensions.GenDateTime;
            user.StatusID = await Workflows.UserStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Registered",
                    byName: "Registered",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            user.UpdatedDate = timestamp;
            var salesOrderStatusID = await Workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Submitted",
                    byName: "Submitted",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var searchModel = RegistryLoaderWrapper.GetInstance<ISalesOrderSearchModel>(contextProfileName);
            searchModel.Active = true;
            searchModel.UserID = userID;
            searchModel.StatusName = "New";
            foreach (var order in context.SalesOrders
                    .FilterSalesCollectionsBySearchModel<SalesOrder,
                        SalesOrderStatus,
                        SalesOrderType,
                        SalesOrderItem,
                        AppliedSalesOrderDiscount,
                        SalesOrderState,
                        SalesOrderFile,
                        SalesOrderContact,
                        AppliedSalesOrderItemDiscount,
                        SalesOrderItemTarget,
                        SalesOrderEvent,
                        SalesOrderEventType>(searchModel))
            {
                order.StatusID = salesOrderStatusID;
                order.UpdatedDate = timestamp;
            }
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("Must provide a Token and a UserID");
        }
    }
}
