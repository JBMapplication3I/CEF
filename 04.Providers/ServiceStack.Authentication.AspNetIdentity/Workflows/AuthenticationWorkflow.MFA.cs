// <copyright file="AuthenticationWorkflow.MFA.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;

    /// <summary>An authentication workflow.</summary>
    public partial class AuthenticationWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse<MFARequirementsModel>> CheckForMFAForUsernameAsync(
            string username,
            string? contextProfileName)
        {
            if (!bool.Parse(ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.Enabled"] ?? "false"))
            {
                return CEFAR.FailingCEFAR<MFARequirementsModel>();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            try
            {
                var mfaRequirement = new MFARequirementsModel();
                ////if (!await userManager.GetTwoFactorEnabledAsync(username).ConfigureAwait(false))
                ////{
                ////    return mfaRequirement.WrapInPassingCEFAR();
                ////}
                if (bool.Parse(ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.BySMS.Enabled"] ?? "false"))
                {
                    mfaRequirement.Phone = true;
                    var userPhone = await context.Users
                        .FilterByActive(true)
                        .FilterUsersByUserName(username, true, false)
                        .Select(x => x.PhoneNumber)
                        .SingleAsync()
                        .ConfigureAwait(false);
                    mfaRequirement.PhoneLastFour = userPhone![^4..];
                }
                if (bool.Parse(ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.ByEmail.Enabled"] ?? "false"))
                {
                    mfaRequirement.Email = true;
                    var userEmail = await context.Users
                        .FilterByActive(true)
                        .FilterUsersByUserName(username, true, false)
                        .Select(x => x.Email)
                        .SingleAsync()
                        .ConfigureAwait(false);
                    mfaRequirement.EmailFirstAndLastFour = userEmail![..4]
                        + "***"
                        + userEmail[^4..];
                }
                return mfaRequirement.WrapInPassingCEFAR()!;
            }
            catch (Exception)
            {
                return CEFAR.FailingCEFAR<MFARequirementsModel>();
            }
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> RequestMFAForUsernameAsync(
            string username,
            bool usePhone,
            string? contextProfileName)
        {
            if (!bool.Parse(ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.Enabled"] ?? "false"))
            {
                return CEFAR.FailingCEFAR();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            try
            {
                return (await userManager.NotifyTwoFactorTokenAsync(username, usePhone).ConfigureAwait(false))
                    .BoolToCEFAR();
            }
            catch (Exception)
            {
                return CEFAR.FailingCEFAR();
            }
        }
    }
}
