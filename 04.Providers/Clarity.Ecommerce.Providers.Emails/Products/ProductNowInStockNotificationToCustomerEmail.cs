﻿// <copyright file="ProductNowInStockNotificationToCustomerEmail.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product now in stock notification to customer email class</summary>
namespace Clarity.Ecommerce.Providers.Emails
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A product now in stock notification email.</summary>
    /// <seealso cref="EmailSettingsBase"/>
    [PublicAPI, GeneratesAppSettings]
    public class ProductNowInStockNotificationToCustomerEmail : EmailSettingsBase
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
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue("ProductNowInStock.html")]
        public override string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullReturnPath"),
         DefaultValue("/{{ProductDetailUrlFragment}}/{{ProductSeoUrl}}")]
        public override string? FullReturnPath
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue("Your product is now in stock at {{CompanyName}}")]
        public override string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string asValue, GetType()) ? asValue : null;
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
        protected override string SettingsRoot => "Clarity.Notifications.ProductNowInStock";

        /// <inheritdoc/>
        public override Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null)
        {
            // Validate
            Contract.Requires<ArgumentException>(
                parameters?.ContainsKey("productSeoUrl") == true && parameters["productSeoUrl"] != null,
                "This email requires a parameter in the dictionary of { [\"productSeoUrl\"] = <string> }");
            Contract.Requires<ArgumentException>(
                parameters!.ContainsKey("productName") && parameters["productName"] != null,
                "This email requires a parameter in the dictionary of { [\"productName\"] = <string> }");
            var productSeoUrl = parameters["productSeoUrl"] as string;
            var productName = parameters["productName"] as string;
            // Generate
            var replacementDictionary = GetBaseReplacementsDictionary();
            replacementDictionary["{{Email}}"] = to;
            replacementDictionary["{{FullReturnPath}}"] = FullReturnPath;
            replacementDictionary["{{ProductDetailUrlFragment}}"] = CEFConfigDictionary.ProductDetailRouteRelativePath;
            replacementDictionary["{{ProductSeoUrl}}"] = productSeoUrl;
            replacementDictionary["{{ProductName}}"] = productName;
            // Do
            MergeReplacementsIfPresent(replacementDictionary, customReplacements);
            return FormatAndQueueEmailAsync(to, replacementDictionary, contextProfileName);
        }
    }
}
