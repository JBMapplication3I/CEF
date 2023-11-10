// <copyright file="IProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers
{
    /// <summary>Interface for provider base.</summary>
    public interface IProviderBase
    {
        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>Gets the type of the provider.</summary>
        /// <value>The type of the provider.</value>
        Enums.ProviderType ProviderType { get; }

        /// <summary>Gets a value indicating whether this IProviderBase has valid configuration.</summary>
        /// <value>true if this IProviderBase has valid configuration, false if not.</value>
        bool HasValidConfiguration { get; }

        /// <summary>Gets a value indicating whether this IProviderBase has a default provider.</summary>
        /// <value>true if this IProviderBase has a default provider, false if not.</value>
        bool HasDefaultProvider { get; }

        /// <summary>Gets a value indicating whether this IProviderBase is the default provider.</summary>
        /// <value>true if this IProviderBase is the default provider, false if not.</value>
        bool IsDefaultProvider { get; }

        /// <summary>Gets or sets a value indicating whether this IProviderBase is the default provider activated.</summary>
        /// <value>true if this IProviderBase is the default provider activated, false if not.</value>
        bool IsDefaultProviderActivated { get; set; }
    }
}
