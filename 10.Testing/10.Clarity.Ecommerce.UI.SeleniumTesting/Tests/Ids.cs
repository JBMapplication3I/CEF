// <copyright file="Ids.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Ids.cs class/interface</summary>
namespace Clarity.Ecommerce.UI.SeleniumTesting
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    /// <summary>An identifiers.</summary>
    public static class Ids
    {
        /// <summary>Gets the selects and values.</summary>
        /// <value>The selects and values.</value>
        public static Dictionary<string, string> SelectsAndValues { get; } = new Dictionary<string, string>()
        {
            ["CatalogViewOptions.Step1CssSelector"] = "#btnCategoriesMenuLinkMega > span",
            ["CatalogViewOptions.Step2CssSelector"] = ".p-3",
            ["CatalogViewOptions.Step3XPathSelector"] = "//a[@id=\'btnMenuItemBrowseAllCategories\']/span",
            ["CatalogViewOptions.Step4CssSelector"] = "#btnCategoriesMenuLinkMega > span",
            ["CatalogViewOptions.Step5CssSelector"] = "#btnCategoriesMenuLinkMega > span",
            ["CatalogViewOptions.CatalogBreadcrumbNameResult"] = "Catalog",
        };

        /// <summary>Gets.</summary>
        /// <param name="key">   The key.</param>
        /// <param name="member">The member.</param>
        /// <returns>A string.</returns>
        public static string Get(string key, [CallerMemberName] string? member = null)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            var fullKey = member + "." + key;
            if (!SelectsAndValues.ContainsKey(fullKey))
            {
                throw new ArgumentException($"FullKey '{fullKey}' not in the dictionary");
            }
            return SelectsAndValues[fullKey];
        }

        /// <summary>Loads the settings.</summary>
        /// <returns>A Task.</returns>
        public static Task LoadAsync()
        {
            // TODO: Load values from settings
            return Task.CompletedTask;
        }
    }
}
