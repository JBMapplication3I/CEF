// <copyright file="StandardActionsProviderConfig.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard actions provider configuration class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Standard
{
    using Interfaces.Providers;

    /// <summary>A standard sales invoice actions provider configuration.</summary>
    internal static class StandardSalesInvoiceActionsProviderConfig
    {
        /// <summary>Query if this Config is valid.</summary>
        /// <param name="isDefaultAndActivated">True if this Provider is default and activated.</param>
        /// <returns>true if valid, false if not.</returns>
        internal static bool IsValid(bool isDefaultAndActivated)
            => ProviderConfig.CheckIsEnabledBySettings<StandardSalesInvoiceActionsProvider>() || isDefaultAndActivated;
    }
}
