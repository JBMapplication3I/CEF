// <copyright file="SplitTemplateEmailSettings.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the split template email settings class</summary>
#if SPLIT_TEMPLATES
namespace Clarity.Ecommerce.Workflow
{
    using System.Configuration;
    using System.Linq;
    using Interfaces.Workflow;
    using JSConfigs;


    /// <summary>A split template email settings.</summary>
    public class SplitTemplateEmailSettings
    {
        /// <summary>Initializes static members of the <see cref="SplitTemplateEmailSettings"/> class.</summary>
        static SplitTemplateEmailSettings()
        {
            CEFConfigDictionary.Load();
            RootUrl = CEFConfigDictionary.SiteRouteHostUrl;
            RootUrlSSL = CEFConfigDictionary.SiteRouteHostUrlSSL;
            ProductDetailUrlFragment = CEFConfigDictionary.ProductDetailRouteRelativePath;
            CatalogUrlFragment = CEFConfigDictionary.CatalogRouteRelativePath;
        }

        /// <summary>Initializes a new instance of the <see cref="SplitTemplateEmailSettings"/> class.</summary>
        /// <param name="settingGroup">         Group the setting belongs to.</param>
        /// <param name="settingWorkflow">      The setting workflow.</param>
        /// <param name="emailTemplateWorkflow">The email template workflow.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        public SplitTemplateEmailSettings(
            string settingGroup,
            ISettingWorkflow settingWorkflow,
            IEmailTemplateWorkflow emailTemplateWorkflow,
            string? contextProfileName)
        {
            // ReSharper disable AsyncConverter.AsyncWait
            var coreEmailSettings = settingWorkflow.GetSettingsByGroupNameAsync("Emails.Defaults", contextProfileName).Result.ToDictionary(x => x.CustomKey, x => x.Value);
            var emailSettings = settingWorkflow.GetSettingsByGroupNameAsync(settingGroup, contextProfileName).Result.ToDictionary(x => x.CustomKey, x => x.Value);
            RequiresHttps = bool.Parse(emailSettings.ContainsKey(settingGroup + ".RequiresHttps") ? emailSettings[settingGroup + ".RequiresHttps"] : coreEmailSettings.ContainsKey("Emails.Defaults.RequiresHttps") ? coreEmailSettings["Emails.Defaults.RequiresHttps"] : "false");
            ReturnPath = emailSettings.ContainsKey(settingGroup + ".ReturnPath") ? emailSettings[settingGroup + ".ReturnPath"] : coreEmailSettings.ContainsKey("Emails.Defaults.ReturnPath") ? coreEmailSettings["Emails.Defaults.ReturnPath"] : "/";
            From = emailSettings.ContainsKey(settingGroup + ".From") ? emailSettings[settingGroup + ".From"] : coreEmailSettings.ContainsKey("Emails.Defaults.From") ? coreEmailSettings["Emails.Defaults.From"] : string.Empty;
            To = emailSettings.ContainsKey(settingGroup + ".To") ? emailSettings[settingGroup + ".To"] : coreEmailSettings.ContainsKey("Emails.Defaults.To") ? coreEmailSettings["Emails.Defaults.To"] : string.Empty;
            CC = emailSettings.ContainsKey(settingGroup + ".CC") ? emailSettings[settingGroup + ".CC"] : coreEmailSettings.ContainsKey("Emails.Defaults.CC") ? coreEmailSettings["Emails.Defaults.CC"] : string.Empty;
            BCC = emailSettings.ContainsKey(settingGroup + ".BCC") ? emailSettings[settingGroup + ".BCC"] : coreEmailSettings.ContainsKey("Emails.Defaults.BCC") ? coreEmailSettings["Emails.Defaults.BCC"] : string.Empty;
            Subject = emailSettings.ContainsKey(settingGroup + ".Subject") ? emailSettings[settingGroup + ".Subject"] : coreEmailSettings.ContainsKey("Emails.Defaults.Subject") ? coreEmailSettings["Emails.Defaults.Subject"] : "A notification from {{CompanyName}}";
            CompanyName = emailSettings.ContainsKey(settingGroup + ".CompanyName") ? emailSettings[settingGroup + ".CompanyName"] : coreEmailSettings.ContainsKey("Emails.Defaults.CompanyName") ? coreEmailSettings["Emails.Defaults.CompanyName"] : "Clarity Ventures, Inc.";
            Token = emailSettings.ContainsKey(settingGroup + ".Token") ? emailSettings[settingGroup + ".Token"] : coreEmailSettings.ContainsKey("Emails.Defaults.Token") ? coreEmailSettings["Emails.Defaults.Token"] : string.Empty;
            var includesTemplateID = emailTemplateWorkflow.CheckExistsAsync(settingGroup + ".IncludesContent", contextProfileName).Result
                ?? emailTemplateWorkflow.CheckExistsAsync("Emails.Defaults.IncludesContent", contextProfileName).Result
                ?? throw new ConfigurationErrorsException("Could not locate Includes content for this email type or the defaults. Please use Settings Administration to provide these values");
            var headerTemplateID = emailTemplateWorkflow.CheckExistsAsync(settingGroup + ".HeaderContent", contextProfileName).Result
                ?? emailTemplateWorkflow.CheckExistsAsync("Emails.Defaults.HeaderContent", contextProfileName).Result
                ?? throw new ConfigurationErrorsException("Could not locate Header content for this email type or the defaults. Please use Settings Administration to provide these values");
            var footerTemplateID = emailTemplateWorkflow.CheckExistsAsync(settingGroup + ".FooterContent", contextProfileName).Result
                ?? emailTemplateWorkflow.CheckExistsAsync("Emails.Defaults.FooterContent", contextProfileName).Result
                ?? throw new ConfigurationErrorsException("Could not locate Footer content for this email type or the defaults. Please use Settings Administration to provide these values");
            var mainTemplateID = emailTemplateWorkflow.CheckExistsAsync(settingGroup + ".MainContent", contextProfileName).Result
                ?? throw new ConfigurationErrorsException("Could not locate Main Body content for this email type. Please use Settings Administration to provide these values");
            IncludesContentTemplate = emailTemplateWorkflow.GetAsync(includesTemplateID, contextProfileName).Result.Body;
            HeaderContentTemplate = emailTemplateWorkflow.GetAsync(headerTemplateID, contextProfileName).Result.Body;
            FooterContentTemplate = emailTemplateWorkflow.GetAsync(footerTemplateID, contextProfileName).Result.Body;
            MainContentTemplate = emailTemplateWorkflow.GetAsync(mainTemplateID, contextProfileName).Result.Body;
            // ReSharper restore AsyncConverter.AsyncWait
        }

        /// <summary>Gets URL of the root.</summary>
        /// <value>The root URL.</value>
        public static string RootUrl { get; }

        /// <summary>Gets the root URL ssl.</summary>
        /// <value>The root URL ssl.</value>
        public static string RootUrlSSL { get; }

        /// <summary>Gets the product detail URL fragment.</summary>
        /// <value>The product detail URL fragment.</value>
        public static string ProductDetailUrlFragment { get; }

        /// <summary>Gets the catalog URL fragment.</summary>
        /// <value>The catalog URL fragment.</value>
        public static string CatalogUrlFragment { get; }

        /// <summary>Gets a value indicating whether the requires HTTPS.</summary>
        /// <value>True if requires https, false if not.</value>
        public bool RequiresHttps { get; }

        /// <summary>Gets the full pathname of the return file.</summary>
        /// <value>The full pathname of the return file.</value>
        public string ReturnPath { get; }

        /// <summary>Gets the 'from' email address.</summary>
        /// <value>The 'from' email address.</value>
        public string From { get; }

        /// <summary>Gets the 'to' email addresses.</summary>
        /// <value>The 'to' email addresses.</value>
        public string To { get; }

        /// <summary>Gets the 'cc' email addresses.</summary>
        /// <value>The 'cc' email addresses.</value>
        public string CC { get; }

        /// <summary>Gets the 'bcc' email addresses.</summary>
        /// <value>The 'bcc' email addresses.</value>
        public string BCC { get; }

        /// <summary>Gets the subject.</summary>
        /// <value>The subject.</value>
        public string Subject { get; }

        /// <summary>Gets the name of the company.</summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; }

        /// <summary>Gets the token.</summary>
        /// <value>The token.</value>
        public string Token { get; }

        /// <summary>Gets the includes content template.</summary>
        /// <value>The includes content template.</value>
        public string IncludesContentTemplate { get; }

        /// <summary>Gets the header content template.</summary>
        /// <value>The header content template.</value>
        public string HeaderContentTemplate { get; }

        /// <summary>Gets the main content template.</summary>
        /// <value>The main content template.</value>
        public string MainContentTemplate { get; }

        /// <summary>Gets the footer content template.</summary>
        /// <value>The footer content template.</value>
        public string FooterContentTemplate { get; }
    }
}
#endif
