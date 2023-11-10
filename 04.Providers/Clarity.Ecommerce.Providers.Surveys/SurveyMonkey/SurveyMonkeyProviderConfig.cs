// <copyright file="SurveyMonkeyProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the survey monkey provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Surveys.SurveyMonkey
{
    using Interfaces.Providers;
    using Utilities;

    /// <summary>A survey monkey provider configuration.</summary>
    internal static class SurveyMonkeyProviderConfig
    {
        /// <summary>Gets the survey token.</summary>
        /// <value>The survey token.</value>
        internal static string SurveyToken { get; }
            = ProviderConfig.GetStringSetting("Clarity.SurveyMonkey.Token");

        /// <summary>Gets the identifier of from survey.</summary>
        /// <value>The identifier of from survey.</value>
        internal static string FromSurveyID { get; }
            = ProviderConfig.GetStringSetting("Clarity.SurveyMonkey.SurveyID");

        /// <summary>Gets the API key.</summary>
        /// <value>The API key.</value>
        internal static string ApiKey { get; }
            = ProviderConfig.GetStringSetting("Clarity.SurveyMonkey.APIKey");

        /// <summary>Gets the template root.</summary>
        /// <value>The template root.</value>
        internal static string TemplateRoot { get; }
            = ProviderConfig.GetStringSetting("Clarity.Emails.Defaults.TemplatesRoot");

        /// <summary>Gets the full pathname of the description template file.</summary>
        /// <value>The full pathname of the description template file.</value>
        internal static string DescriptionTemplatePath { get; }
            = ProviderConfig.GetStringSetting("Clarity.Survey.DescriptionTemplatePath");

        /// <summary>Gets the full pathname of the body template file.</summary>
        /// <value>The full pathname of the body template file.</value>
        internal static string BodyTemplatePath { get; }
            = ProviderConfig.GetStringSetting("Clarity.Survey.BodyTemplatePath");

        /// <summary>Gets URL of the site root.</summary>
        /// <value>The site root URL.</value>
        internal static string SiteRootUrl { get; }
            = ProviderConfig.GetStringSetting("Clarity.Routes.Site.RootUrl");

        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => (ProviderConfig.CheckIsEnabledBySettings<SurveyMonkeyProvider>() || isDefaultAndActivated)
            && Contract.CheckValidKey(SurveyToken);
    }
}
