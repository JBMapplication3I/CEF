using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ServiceStack.Html
{
    public static class UnobtrusiveValidationAttributesGenerator
    {
        public static void GetValidationAttributes(IEnumerable<ModelClientValidationRule> clientRules, IDictionary<string, object> results)
        {
            if (clientRules == null)
            {
                throw new ArgumentNullException(nameof(clientRules));
            }
            if (results == null)
            {
                throw new ArgumentNullException(nameof(results));
            }

            var renderedRules = false;

            foreach (var rule in clientRules)
            {
                renderedRules = true;
                var ruleName = "data-val-" + rule.ValidationType;

                ValidateUnobtrusiveValidationRule(rule, results, ruleName);

                results.Add(ruleName, rule.ErrorMessage ?? string.Empty);
                ruleName += "-";

                foreach (var kvp in rule.ValidationParameters)
                {
                    results.Add(ruleName + kvp.Key, kvp.Value ?? string.Empty);
                }
            }

            if (renderedRules)
            {
                results.Add("data-val", "true");
            }
        }

        private static void ValidateUnobtrusiveValidationRule(ModelClientValidationRule rule, IDictionary<string, object> resultsDictionary, string dictionaryKey)
        {
            if (string.IsNullOrEmpty(rule.ValidationType))
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        MvcResources.UnobtrusiveJavascript_ValidationTypeCannotBeEmpty,
                        rule.GetType().FullName));
            }

            if (resultsDictionary.ContainsKey(dictionaryKey))
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        MvcResources.UnobtrusiveJavascript_ValidationTypeMustBeUnique,
                        rule.ValidationType));
            }

            if (rule.ValidationType.Any(c => !char.IsLower(c)))
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, MvcResources.UnobtrusiveJavascript_ValidationTypeMustBeLegal,
                                  rule.ValidationType,
                                  rule.GetType().FullName));
            }

            foreach (var key in rule.ValidationParameters.Keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new InvalidOperationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            MvcResources.UnobtrusiveJavascript_ValidationParameterCannotBeEmpty,
                            rule.GetType().FullName));
                }

                if (!char.IsLower(key.First()) || key.Any(c => !char.IsLower(c) && !char.IsDigit(c)))
                {
                    throw new InvalidOperationException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            MvcResources.UnobtrusiveJavascript_ValidationParameterMustBeLegal,
                            key,
                            rule.GetType().FullName));
                }
            }
        }
    }
}
