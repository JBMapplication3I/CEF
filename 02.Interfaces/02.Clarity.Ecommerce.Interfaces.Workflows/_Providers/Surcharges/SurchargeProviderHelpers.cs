// <copyright file="SurchargeProviderHelpers.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the surcharge provider helpers class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Providers.Surcharges
{
    /// <summary>Common helpers for surcharge providers.</summary>
    public static class SurchargeProviderHelpers
    {
        /// <summary>(Immutable)
        /// Used as `model.JsonAttributes.Contains(SurchargeProviderHelpers.MissingCompletionPrefix)` to filter for
        /// those models which are missing surcharge completion.</summary>
        public static readonly string MissingCompletionPrefix = "\"MissingCompletion:";

        /// <summary>Format a provider key such that it can be attached to an invoice/order to mark that order as missing
        /// a completion request retry.</summary>
        /// <param name="providerKey">The provider key of the surcharge whose completion request failed.</param>
        /// <returns>A string suitable for use as the key in a serializable attribute on invoices/orders.</returns>
        public static string FormatMissingCompletion(string providerKey) => $"MissingCompletion:{providerKey}";
    }
}
