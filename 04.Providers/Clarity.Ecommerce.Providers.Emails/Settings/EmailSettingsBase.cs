// <copyright file="EmailSettingsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the email settings base class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Emails
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Interfaces.Workflow;
    using JSConfigs;
    using Models;

    /// <summary>An email settings.</summary>
    public abstract class EmailSettingsBase : IEmailSettings
    {
        /*
         *  const string DefaultsRoot = "Clarity.Emails.Defaults";
         *  RootUrl = CEFConfigDictionary.SiteRouteHostUrl;
         *  TemplateRootPath = ConfigurationManager.AppSettings[DefaultsRoot + ".TemplatesRoot"];
         *  TemplatePath = TemplateRootPath + ConfigurationManager.AppSettings[settingRoot + ".BodyTemplatePath"];
         *  ReturnPath = ConfigurationManager.AppSettings[settingRoot + ".ReturnPath"]
         *      ?? ConfigurationManager.AppSettings[DefaultsRoot + ".ReturnPath"];
         *  From = ConfigurationManager.AppSettings[settingRoot + ".From"]
         *      ?? ConfigurationManager.AppSettings[DefaultsRoot + ".From"];
         *  To = ConfigurationManager.AppSettings[settingRoot + ".To"]
         *      ?? ConfigurationManager.AppSettings[DefaultsRoot + ".To"];
         *  CC = ConfigurationManager.AppSettings[settingRoot + ".CC"]
         *      ?? ConfigurationManager.AppSettings[DefaultsRoot + ".CC"];
         *  BCC = ConfigurationManager.AppSettings[settingRoot + ".BCC"]
         *      ?? ConfigurationManager.AppSettings[DefaultsRoot + ".BCC"];
         *  Subject = ConfigurationManager.AppSettings[settingRoot + ".Subject"]
         *      ?? ConfigurationManager.AppSettings[DefaultsRoot + ".Subject"];
         *  CompanyName = ConfigurationManager.AppSettings[settingRoot + ".CompanyName"]
         *      ?? ConfigurationManager.AppSettings[DefaultsRoot + ".CompanyName"]
         *      ?? ConfigurationManager.AppSettings["Clarity.CompanyName"];
         */

        /// <summary>Initializes a new instance of the <see cref="EmailSettingsBase"/> class.</summary>
        protected EmailSettingsBase()
        {
            Load();
        }

        /// <inheritdoc/>
        [AppSettingsKey(".From"),
         DefaultValue(null)]
        public virtual string? From
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".To"),
         DefaultValue(null)]
        public virtual string? To
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".CC"),
         DefaultValue(null)]
        public virtual string? CC
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".BCC"),
         DefaultValue(null)]
        public virtual string? BCC
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Subject"),
         DefaultValue(null)]
        public virtual string? Subject
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullTemplatePath"),
         DefaultValue(null)]
        public virtual string? FullTemplatePath
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".FullReturnPath"),
         DefaultValue(null)]
        public virtual string? FullReturnPath
        {
            get => CEFConfigDictionary.TryGet(out string? asValue, GetType()) ? asValue : null;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <inheritdoc/>
        [AppSettingsKey(".Enabled"),
         DefaultValue(true)]
        public virtual bool Enabled
        {
            get => !CEFConfigDictionary.TryGet(out bool asValue, GetType()) || asValue;
            protected set => CEFConfigDictionary.TrySet(value, GetType());
        }

        /// <summary>Gets the settings root.</summary>
        /// <value>The settings root.</value>
        protected abstract string SettingsRoot { get; }

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; }
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets the workflows.</summary>
        /// <value>The workflows.</value>
        protected IWorkflowsController Workflows { get; }
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<int>> QueueAsync(
            string? contextProfileName,
            string? to,
            Dictionary<string, object?>? parameters = null,
            Dictionary<string, object?>? customReplacements = null);

        /// <inheritdoc/>
        public void Load()
        {
            CEFConfigDictionary.Load(GetType(), this, SettingsRoot);
        }

        /// <summary>Gets base replacements dictionary.</summary>
        /// <returns>The base replacements dictionary.</returns>
        protected static Dictionary<string, string?> GetBaseReplacementsDictionary()
        {
            return new()
            {
                ["{{CompanyName}}"] = CEFConfigDictionary.CompanyName,
                ["{{CompanyEmail}}"] = CEFConfigDictionary.CompanyEmail,
                ["{{CompanyPhone}}"] = CEFConfigDictionary.CompanyPhone,
                //["{{RootUrl}}"] = CEFConfigDictionary.SiteRouteHostUrlSSL ?? CEFConfigDictionary.SiteRouteHostUrl,
                ["{{RootUrl}}"] = CEFConfigDictionary.EmailTemplateRouteHostUrl ?? CEFConfigDictionary.SiteRouteHostUrlSSL,
            };
        }

        /// <summary>Merge replacements if present.</summary>
        /// <param name="replacementDictionary">Dictionary of replacements.</param>
        /// <param name="customReplacements">   The custom replacements.</param>
        protected virtual void MergeReplacementsIfPresent(
            Dictionary<string, string?> replacementDictionary,
            Dictionary<string, object?>? customReplacements)
        {
            if (customReplacements == null)
            {
                return;
            }
            foreach (var kvp in customReplacements)
            {
                switch (kvp.Value)
                {
                    case null:
                    {
                        replacementDictionary[kvp.Key] = string.Empty;
                        continue;
                    }
                    case string asString:
                    {
                        replacementDictionary[kvp.Key] = asString;
                        continue;
                    }
                    default:
                    {
                        replacementDictionary[kvp.Key] = kvp.Value!.ToString()!;
                        break;
                    }
                }
            }
        }

        /// <summary>Format and queue email.</summary>
        /// <param name="to">                   The 'to' email(s).</param>
        /// <param name="replacementDictionary">Dictionary of replacements.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The formatted and queue email.</returns>
        protected virtual async Task<CEFActionResponse<int>> FormatAndQueueEmailAsync(
            string? to,
            Dictionary<string, string?> replacementDictionary,
            string? contextProfileName)
        {
            var emailWorkflow = RegistryLoaderWrapper.GetInstance<IEmailQueueWorkflow>(contextProfileName);
            var result = await emailWorkflow.FormatAndQueueEmailAsync(
                    email: to,
                    replacementDictionary: replacementDictionary,
                    emailSettings: this,
                    attachmentPath: null,
                    attachmentType: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return await emailWorkflow.GenerateResultAsync(result).ConfigureAwait(false);
        }
    }
}
