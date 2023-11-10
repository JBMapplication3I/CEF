// <copyright file="AuthenticationWorkflow.Shared.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication workflow class</summary>
// ReSharper disable StyleCop.SA1202
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Workflow;
    using Utilities;

    /// <summary>An authentication workflow.</summary>
    /// <seealso cref="WorkflowBase{IRoleUserModel, IRoleUserSearchModel, IRoleUser, RoleUser}"/>
    /// <seealso cref="IAuthenticationWorkflow"/>
    public partial class AuthenticationWorkflow
        : WorkflowBase<IRoleUserModel, IRoleUserSearchModel, IRoleUser, RoleUser>,
          IAuthenticationWorkflow
    {
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        /// <inheritdoc/>
        [Obsolete("This is here to provide compliance with an interface but is not actually used.", false)]
        protected override Func<RoleUser?, string?, IRoleUserModel?> MapFromConcreteFull => null!;

        /// <inheritdoc/>
        [Obsolete("This is here to provide compliance with an interface but is not actually used.", false)]
        protected override Func<IQueryable<RoleUser>, string?, IEnumerable<IRoleUserModel>> SelectLiteAndMapToModel => null!;

        /// <inheritdoc/>
        [Obsolete("This is here to provide compliance with an interface but is not actually used.", false)]
        protected override Func<IQueryable<RoleUser>, string?, IEnumerable<IRoleUserModel>> SelectListAndMapToModel => null!;

        /// <inheritdoc/>
        [Obsolete("This is here to provide compliance with an interface but is not actually used.", false)]
        protected override Func<IQueryable<RoleUser>, string?, IRoleUserModel> SelectFirstFullAndMapToModel => null!;

        /// <inheritdoc/>
        [Obsolete("This is here to provide compliance with an interface but is not actually used.", false)]
        protected override Func<IRoleUser, IRoleUserModel, DateTime, DateTime?, IRoleUser> UpdateEntityFromModel => null!;
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        private static Regex RequireDigitRegex { get; } = new(@".*\d.*");

        private static Regex RequireLowercaseRegex { get; } = new(".*[a-z].*");

        private static Regex RequireUppercaseRegex { get; } = new(".*[A-Z].*");

        private static Regex RequireNonLetterOrDigitRegex { get; } = new(@".*[\/!@#$%^&*()_+{}\[\]=':;\<\>,\.\?|-].*");

        private static int RequiredLength { get; }
            = ProviderConfig.GetIntegerSetting("Clarity.API.Auth.Providers.Identity.RequiredLength", 6);

        private static bool RequireDigit { get; }
            = ProviderConfig.GetBooleanSetting("Clarity.API.Auth.Providers.Identity.RequiredDigit");

        private static bool RequireLowercase { get; }
            = ProviderConfig.GetBooleanSetting("Clarity.API.Auth.Providers.Identity.RequireLowercase");

        private static bool RequireUppercase { get; }
            = ProviderConfig.GetBooleanSetting("Clarity.API.Auth.Providers.Identity.RequireUppercase");

        private static bool RequireNonLetterOrDigit { get; }
            = ProviderConfig.GetBooleanSetting("Clarity.API.Auth.Providers.Identity.RequireNonLetterOrDigit");

        private static bool UserNameIsEmail { get; }
            = ProviderConfig.GetBooleanSetting("Clarity.API.Auth.Providers.Identity.UserNameIsEmail");

        /// <summary>A censor.</summary>
        private static class Censor
        {
            private static string[] CensoredWords
            {
                get
                {
                    using var context = RegistryLoaderWrapper.GetContext(ContextProfileName);
                    var censoredWords = context.ProfanityFilters
                        .AsNoTracking()
                        .FilterByActive(true)
                        .OrderBy(x => x.CustomKey)
                        .Select(x => x.CustomKey!.ToLower())
                        .ToArray();
                    return censoredWords;
                }
            }

            private static string? ContextProfileName { get; set; }

            /// <summary>Censor text.</summary>
            /// <param name="text">              The text.</param>
            /// <param name="contextProfileName">Name of the context profile.</param>
            /// <returns>A string.</returns>
            public static string CensorText(string text, string? contextProfileName)
            {
                ContextProfileName = contextProfileName;
                Contract.RequiresNotNull(text); // Allow whitespace, but not nulls
                return CensoredWords
                    .Select(ToRegexPattern)
                    .Aggregate(
                        text,
                        (current, regularExpression) =>
                            Regex.Replace(
                                current,
                                regularExpression,
                                StarCensoredMatch,
                                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant));
            }

            private static string StarCensoredMatch(Match m)
            {
                var word = m.Captures[0].Value;
                return new('*', word.Length);
            }

            private static string ToRegexPattern(string wildcardSearch)
            {
                // ReSharper disable once MissingSpace
                var regexPattern = /*Regex.Escape(*/wildcardSearch/*)*/;
                ////regexPattern = regexPattern.Replace(@"\*", ".*?");
                ////regexPattern = regexPattern.Replace(@"\?", ".");
                ////if (regexPattern.StartsWith(".*?"))
                ////{
                ////    regexPattern = regexPattern.Substring(3);
                ////    regexPattern = @"(^\b)*?" + regexPattern;
                ////}
                ////regexPattern = $@"\b{regexPattern}\b";
                return regexPattern;
            }
        }
    }
}
