// <copyright file="ShoppingCartTransferredToSupervisorEmail.cs" company="clarity-ventures.com">
// Copyright (c) clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Ecommerce.Providers.Emails.Shopping
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    [PublicAPI, GeneratesAppSettings]
    public class ShoppingCartTransferredToSupervisorEmail : EmailSettingsBase
    {
        /// <inheritdoc/>
        [AppSettingsKey(".Enabled"),
         DefaultValue(true)]
        public override bool Enabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, GetType()) || asValue;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".From"),
         DefaultValue("noreply@claritymis.com")]
        public override string? From
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("Shopping.CartTransferred.ToSupervisor.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullReturnPath"),
         DefaultValue("Dashboard#!/db/Shopping-Lists/Detail")]
        public override string? FullReturnPath
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("{{ShopperFullName}} has sent you their shopping list on {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc />
        protected override string SettingsRoot => "Clarity.Notifications.Shopping.CartTransferred.ToSupervisor";

        /// <inheritdoc />
        public override async Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            if (!Enabled)
            {
                return 0.WrapInPassingCEFAR($"Notification {nameof(ShoppingCartTransferredToSupervisorEmail)} is disabled.");
            }
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("supervisorUser") == true && parameters["supervisorUser"] != null,
                "This email requires a parameter in the dictionary of { [\"supervisorUser\"] = <IUserModel> }");
            var supervisorUser = parameters!["supervisorUser"] as IUserModel;
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("shopperUser") && parameters["shopperUser"] != null,
                "This email requires a parameter in the dictionary of { [\"shopperUser\"] = <IUserModel> }");
            var shopperUser = parameters["shopperUser"] as IUserModel;
            Contract.Requires<ArgumentException>(
                parameters.ContainsKey("cart") && parameters["cart"] != null,
                "This email requires a parameter in the dictionary of { [\"cart\"] = <ICartModel> }");
            var cart = parameters["cart"] as ICartModel;
            var brandID = parameters.ContainsKey("brandID")
                ? (int?)parameters["brandID"]
                : null;
            var brandSiteUrl = await GetBrandDomain((int)brandID!, contextProfileName).ConfigureAwait(false);
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            var emailToUse = supervisorUser!.Email ?? to ?? To;
            replacementDictionary["{{Email}}"] = emailToUse;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{SupervisorFirstName}}"] = supervisorUser.Contact?.FirstName ?? string.Empty;
            replacementDictionary["{{SupervisorLastName}}"] = supervisorUser.Contact?.LastName ?? string.Empty;
            replacementDictionary["{{SupervisorFullName}}"] =
                replacementDictionary["{{SupervisorFirstName}}"] + " " + replacementDictionary["{{SupervisorLastName}}"];
            replacementDictionary["{{ShopperFirstName}}"] = shopperUser!.Contact?.FirstName ?? "A";
            replacementDictionary["{{ShopperLastName}}"] = shopperUser.Contact?.LastName ?? "user";
            replacementDictionary["{{ShopperFullName}}"] =
                replacementDictionary["{{ShopperFirstName}}"] + " " + replacementDictionary["{{ShopperLastName}}"];
            replacementDictionary["{{CartItemCount}}"] = cart!.SalesItems?.Count.ToString() ?? "N/A";
            replacementDictionary["{{CartTypeID}}"] = cart!.TypeID.ToString();
            replacementDictionary["{{BrandSiteUrl}}"] = Contract.CheckValidKey(brandSiteUrl)
                ? brandSiteUrl
                : CEFConfigDictionary.SiteRouteHostUrlSSL ?? CEFConfigDictionary.SiteRouteHostUrl;
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return await FormatAndQueueEmailAsync(emailToUse, replacementDictionary, contextProfileName).ConfigureAwait(false);
        }

        private async Task<string?> GetBrandDomain(int brandID, string? contextProfileName)
        {
            var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.BrandSiteDomains
                .FilterByActive(true)
                .Where(x => x.MasterID == brandID && x.Slave != null)
                .Select(x => x.Slave!.Url)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}
