// <copyright file="AuthenticationService.Registration.Normal.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Providers.Emails;
    using ServiceStack;
    using Utilities;

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ValidateUserNameIsGood", "POST",
            Summary = "Pass in user credentials to log into the site.")]
    public class ValidateUserNameIsGood : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string UserName { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ValidateEmailIsUnique", "POST",
            Summary = "Validate Email Is Unique")]
    public class ValidateEmailIsUnique : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(Email), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Email { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ValidatePassword", "POST",
            Summary = "Validates that the password provided for a username is the correct one.")]
    public class ValidatePassword : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(UserName), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string UserName { get; set; } = null!;

        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Password { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ValidatePasswordIsGood", "POST",
            Summary = "Pass in user credentials to log into the site.")]
    public class ValidatePasswordIsGood : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(Password), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string Password { get; set; } = null!;
    }

    [PublicAPI, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/ValidateCustomKeyIsUnique", "POST",
            Summary = "Validate Custom Key is Unique")]
    public class ValidateCustomKeyIsUnique : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(CustomKey), DataType = "string", ParameterType = "body", IsRequired = true)]
        public string CustomKey { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront, UsedInBrandAdmin, UsedInStoreAdmin, UsedInVendorAdmin, UsedInAdmin,
        Route("/Authentication/RegisterNewUser", "POST",
            Summary = "Registers a new user via the standard process")]
    public class RegisterNewUser : UserModel, IReturn<CEFActionResponse>
    {
        public List<AccountContactModel>? AddressBook { get; set; }

        public bool InService { get; set; }

        public string? RegistrationType { get; set; }
    }

    public partial class CEFSharedService : ClarityEcommerceServiceBase
    {
        public async Task<object?> Post(ValidateUserNameIsGood request)
        {
            return await Workflows.Authentication.ValidateUserNameIsGoodAsync(
                    request.UserName,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ValidateEmailIsUnique request)
        {
            return await Workflows.Authentication.ValidateEmailIsUniqueAsync(
                    request.Email,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ValidatePassword request)
        {
            return await Workflows.Authentication.ValidatePasswordAsync(
                    request.UserName,
                    request.Password,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ValidatePasswordIsGood request)
        {
            return await Workflows.Authentication.ValidatePasswordIsGoodAsync(
                    request.Password)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ValidateCustomKeyIsUnique request)
        {
            return await Workflows.Authentication.ValidateCustomKeyIsUniqueAsync(
                    request.CustomKey,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(RegisterNewUser request)
        {
            var createResponse = await Workflows.Users.CreateAsync(request, ServiceContextProfileName).ConfigureAwait(false);
            if (!createResponse.ActionSucceeded)
            {
                return createResponse;
            }
            var accountID = await Workflows.Accounts.GetIDByUserIDAsync(
                    createResponse.Result,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            var account = await Workflows.Accounts.GetByUserIDAsync(
                    createResponse.Result,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            if (Contract.CheckValidID(accountID)
                && Contract.CheckNotEmpty(request.AddressBook))
            {
                foreach (var contact in request.AddressBook!)
                {
                    contact.MasterID = accountID!.Value;
                    await Workflows.AccountContacts.CreateAsync(
                            contact,
                            ServiceContextProfileName)
                        .ConfigureAwait(false);
                }
            }
            if (CEFConfigDictionary.CreateReferralCodeDuringRegistration)
            {
                await Workflows.ReferralCodes.GenerateDefaultCodeForUserAsync(
                        userID: createResponse.Result,
                        typeKey: "GENERAL",
                        statusKey: "NORMAL",
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
            var roleAssignResult = CEFAR.PassingCEFAR();
            if (request.InService)
            {
                roleAssignResult = await Workflows.Authentication.AssignRoleToUserAsync(
                        new RoleForUserModel { UserId = createResponse.Result, Name = CEFConfigDictionary.DefaultUserRole },
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
            var username = await Workflows.Users.GetUsernameForIDAsync(
                    createResponse.Result,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            if (CEFConfigDictionary.RequireEmailVerificationForNewUsers)
            {
                var verificationToken = await Workflows.Authentication.GenerateEmailVerificationTokenAsync(
                        createResponse.Result,
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
                try
                {
                    await new AuthenticationEmailVerificationWithToken().QueueAsync(
                            ServiceContextProfileName,
                            to: request.Email,
                            parameters: new()
                            {
                                ["firstName"] = username,
                                ["token"] = verificationToken,
                            })
                        .ConfigureAwait(false);
                }
                catch (Exception ex2)
                {
                    await Logger.LogErrorAsync(
                            name: $"{nameof(RegisterNewUser)}.{nameof(AuthenticationEmailVerificationWithToken)}",
                            message: ex2.Message,
                            ex: ex2,
                            ServiceContextProfileName)
                        .ConfigureAwait(false);
                }
            }
            else
            {
                try
                {
                    await new AuthenticationUserAccountCompletedRegistrationToCustomerEmail().QueueAsync(
                            ServiceContextProfileName,
                            to: request.Email,
                            parameters: new() { ["username"] = username })
                        .ConfigureAwait(false);
                }
                catch (Exception ex1)
                {
                    await Logger.LogErrorAsync(
                            name: $"{nameof(RegisterNewUser)}.{nameof(AuthenticationUserAccountCompletedRegistrationToCustomerEmail)}",
                            message: ex1.Message,
                            ex: ex1,
                            ServiceContextProfileName)
                        .ConfigureAwait(false);
                }
            }
            try
            {
                var brand = await ParseRequestUrlReferrerToBrandAsync().ConfigureAwait(false);
                var brandName = brand?.Name;
                await new AuthenticationUserAccountCompletedRegistrationToBackOfficeEmail().QueueAsync(
                        ServiceContextProfileName,
                        to: request.Email,
                        parameters: new()
                        {
                            ["username"] = username,
                            ["brandName"] = brandName,
                            ["registrationType"] = request.RegistrationType,
                            ["account"] = account,
                            ["primaryContact"] = request.Contact,
                            ["addressBook"] = request.AddressBook,
                        },
                        customReplacements: new()
                        {
                            ["{{AccountName}}"] = account?.Name,
                            ["{{AccountID}}"] = account?.ID,
                            ["{{UserName}}"] = username,
                            ["{{UserID}}"] = createResponse.Result,
                        })
                    .ConfigureAwait(false);
            }
            catch (Exception ex2)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(RegisterNewUser)}.{nameof(AuthenticationUserAccountCompletedRegistrationToBackOfficeEmail)}",
                        message: ex2.Message,
                        ex: ex2,
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
            return roleAssignResult;
        }
    }
}
